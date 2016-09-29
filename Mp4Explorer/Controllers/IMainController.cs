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
    public interface IMainController
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        void OnBoxSelected(Mp4Box box);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        void ShowFile(Mp4File file);

        /// <summary>
        /// 
        /// </summary>
        void RemoveViews();

        #endregion
    }
}
