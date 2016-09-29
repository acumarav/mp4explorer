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
    [XmlRoot("param")]
    public class SmilParam
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SmilParam()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        public SmilParam(string name, string value, string valueType)
        {
            this.Name = name;
            this.Value = value;
            this.ValueType = valueType;
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
        [XmlAttribute("value")]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("valuetype")]
        public string ValueType { get; set; }

        #endregion
    }
}
