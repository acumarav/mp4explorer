//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Xml.Serialization;
using System.Collections.Generic;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("switch")]
    public class SmilSwitch
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SmilSwitch()
        {
            this.Video = new SmilVideo[] { };
            this.Audio = new SmilAudio[] { };
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("video")]
        public SmilVideo[] Video { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("audio")]
        public SmilAudio[] Audio { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="video"></param>
        public void AddVideo(SmilVideo video)
        {
            List<SmilVideo> l = new List<SmilVideo>(this.Video);
            l.Add(video);
            this.Video = l.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="audio"></param>
        public void AddAudio(SmilAudio audio)
        {
            List<SmilAudio> l = new List<SmilAudio>(this.Audio);
            l.Add(audio);
            this.Audio = l.ToArray();
        }

        #endregion
    }
}
