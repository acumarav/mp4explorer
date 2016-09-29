//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.IO;
using System.Xml.Serialization;

namespace Mp4Explorer.SmoothStreaming.Smil
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("smil", Namespace = "http://www.w3.org/2001/SMIL20/Language", IsNullable = false)]
    public class SmilDocument
    {
        #region Fields

        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(SmilDocument));

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SmilDocument()
        {
            this.Head = new SmilHead();
            this.Body = new SmilBody();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("head")]
        public SmilHead Head { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("body")]
        public SmilBody Body { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            using (Stream stream = File.OpenWrite(filename))
            {
                serializer.Serialize(stream, this);
            }
        }

        #endregion

        #region Static public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static SmilDocument Load(string filename)
        {
            using (Stream stream = File.OpenRead(filename))
            {
                return (SmilDocument)serializer.Deserialize(stream);
            }
        }

        #endregion
    }
}
