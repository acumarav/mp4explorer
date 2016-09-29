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
    [XmlRoot("head")]
    public class SmilHead
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SmilHead()
        {
            this.Meta = new SmilMeta[] { };
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("meta")]
        public SmilMeta[] Meta { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meta"></param>
        public void AddMeta(SmilMeta meta)
        {
            List<SmilMeta> l = new List<SmilMeta>(this.Meta);
            l.Add(meta);
            this.Meta = l.ToArray();
        }

        #endregion
    }
}
