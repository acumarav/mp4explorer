//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

namespace Mp4Explorer.SmoothStreaming
{
    /// <summary>
    /// 
    /// </summary>
    public class QualityLevelInfo
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitrate"></param>
        public QualityLevelInfo(ulong bitrate)
        {
            this.Bitrate = bitrate;
            this.TrackId = -1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public ulong Bitrate { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int TrackId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; set; }

        #endregion
    }
}
