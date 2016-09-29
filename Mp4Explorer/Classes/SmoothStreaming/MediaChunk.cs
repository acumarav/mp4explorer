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
    public class MediaChunk
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chunkId"></param>
        /// <param name="duration"></param>
        public MediaChunk(int chunkId, ulong duration)
        {
            this.ChunkId = chunkId;
            this.Duration = duration;
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// 
        /// </summary>
        public int ChunkId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public ulong Duration { get; set; }

        #endregion
    }
}
