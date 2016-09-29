//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Mp4Explorer.SmoothStreaming
{
    /// <summary>
    /// 
    /// </summary>
    public class ManifestParser
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ManifestParser()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public ManifestInfo Parse(string filename)
        {
            ManifestInfo manifestInfo = new ManifestInfo(Path.GetFileNameWithoutExtension(filename), filename);

            XmlReader manifest = XmlReader.Create(filename);

            while (manifest.Read())
            {
                if (manifest.IsStartElement("StreamIndex"))
                {
                    StreamInfo streamInfo = ParseStreamInfo(manifest);

                    if (streamInfo != null)
                    {
                        manifestInfo.Streams.Add(streamInfo);
                    }
                }
            }

            return manifestInfo;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manifest"></param>
        /// <returns></returns>
        private StreamInfo ParseStreamInfo(XmlReader manifest)
        {
            string attribute = manifest.GetAttribute("Type");

            MediaType mediaType = attribute.Equals("video", StringComparison.InvariantCultureIgnoreCase) ? MediaType.Video : (attribute.Equals("audio", StringComparison.InvariantCultureIgnoreCase) ? MediaType.Audio : MediaType.Unknown);

            if (mediaType == MediaType.Unknown)
            {
                throw new Exception("Stream media type in manifest may be 'audio' or 'video' only");
            }

            int numberOfChunks = Convert.ToInt32(manifest.GetAttribute("Chunks"), CultureInfo.InvariantCulture);

            StreamInfo streamInfo = new StreamInfo(mediaType);

            GetQualityLevels(manifest, streamInfo);
            
            return streamInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manifest"></param>
        /// <param name="streamInfo"></param>
        private void GetQualityLevels(XmlReader manifest, StreamInfo streamInfo)
        {
            while (manifest.Read())
            {
                if (manifest.IsStartElement("QualityLevel"))
                {
                    ulong bitrate = Convert.ToUInt64(manifest.GetAttribute("Bitrate"), CultureInfo.InvariantCulture);

                    streamInfo.QualityLevels.Add(new QualityLevelInfo(bitrate));
                }
                else if (manifest.IsStartElement("c"))
                {
                    int chunkId = Convert.ToInt32(manifest.GetAttribute("n"), CultureInfo.InvariantCulture);
                    ulong duration = Convert.ToUInt64(manifest.GetAttribute("d"), CultureInfo.InvariantCulture);

                    streamInfo.Chunks.Add(new MediaChunk(chunkId, duration));
                }
                else if (manifest.Name.Equals("StreamIndex"))
                {
                    break;
                }
            }
        }

        #endregion
    }
}
