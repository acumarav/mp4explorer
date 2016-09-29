//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;

namespace Mp4Explorer.SmoothStreaming
{
    /// <summary>
    /// 
    /// </summary>
    public class StreamInfo
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaType"></param>
        public StreamInfo(MediaType mediaType)
        {
            this.MediaType = mediaType;
            this.QualityLevels = new List<QualityLevelInfo>();
            this.Chunks = new List<MediaChunk>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public MediaType MediaType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<QualityLevelInfo> QualityLevels { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<MediaChunk> Chunks { get; private set; }

        #endregion
    }
}
