//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

namespace Mp4Explorer.SmoothStreaming.Smil
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISmilStream
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string Src { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string SystemBitrate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        SmilParam[] Param { get; set; }

        #endregion
    }
}
