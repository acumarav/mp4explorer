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
    [BoxViewType(typeof(Mp4MoofBox))]
    public partial class MoofView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4MoofBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MoofView()
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
                box = (Mp4MoofBox)value;

                BoxViewUtil.AddHeader(grid, "Movie Fragment Box");
                BoxViewUtil.AddField(grid, "Size:", box.Size);
            }
        }

        #endregion
    }
}
