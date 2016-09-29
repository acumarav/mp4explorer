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
    [XmlRoot("meta")]
    public class SmilMeta
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SmilMeta()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="content"></param>
        public SmilMeta(string name, string content)
        {
            this.Name = name;
            this.Content = content;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("content")]
        public string Content { get; set; }

        #endregion
    }
}
