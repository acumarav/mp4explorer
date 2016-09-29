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
    [BoxViewType(typeof(Mp4VmhdBox))]
    public partial class VmhdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4VmhdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public VmhdView()
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
                box = (Mp4VmhdBox)value;

                BoxViewUtil.AddHeader(grid, "Video Media Header Box");

                BoxViewUtil.AddField(grid, "Graphics mode:", box.GraphicsMode);
                BoxViewUtil.AddField(grid, "Red:", box.OpColor[0]);
                BoxViewUtil.AddField(grid, "Green:", box.OpColor[1]);
                BoxViewUtil.AddField(grid, "Blue:", box.OpColor[2]);
            }
        }

        #endregion
    }
}
