//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Xml.Serialization;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("body")]
    public class SmilBody
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SmilBody()
        {
            this.Switch = new SmilSwitch();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("switch")]
        public SmilSwitch Switch { get; set; }

        #endregion
    }
}
