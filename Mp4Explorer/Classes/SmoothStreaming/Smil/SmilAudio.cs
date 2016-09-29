//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("audio")]
    public class SmilAudio : ISmilStream
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SmilAudio()
        {
            this.Param = new SmilParam[] { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="systemBitrate"></param>
        public SmilAudio(string src, string systemBitrate)
        {
            this.Src = src;
            this.SystemBitrate = systemBitrate;
            this.Param = new SmilParam[] { };
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("src")]
        public string Src { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("systemBitrate")]
        public string SystemBitrate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("param")]
        public SmilParam[] Param { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public void AddParam(SmilParam param)
        {
            List<SmilParam> l = new List<SmilParam>(this.Param);
            l.Add(param);
            this.Param = l.ToArray();
        }

        #endregion
    }
}
