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
    public class ManifestInfo
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filename"></param>
        public ManifestInfo(string name, string filename)
        {
            this.Name = name;
            this.Filename = filename;
            this.Streams = new List<StreamInfo>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<StreamInfo> Streams { get; private set; }

        #endregion
    }
}
