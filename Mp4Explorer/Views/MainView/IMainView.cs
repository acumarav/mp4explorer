//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMainView
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        Mp4File File { get; set; }

        #endregion
    }
}
