//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    [BoxViewType(typeof(Mp4TrakBox))]
    public partial class TrakView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4TrakBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public TrakView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Mp4Box Box
        {
            get
            {
                return box;
            }
            set
            {
                box = (Mp4TrakBox)value;

                BoxViewUtil.AddHeader(grid, "Track Box");

                BoxViewUtil.AddField(grid, "Size:", box.Size);
            }
        }

        #endregion
    }
}
