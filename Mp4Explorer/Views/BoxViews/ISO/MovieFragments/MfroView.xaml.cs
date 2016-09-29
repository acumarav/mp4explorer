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
    [BoxViewType(typeof(Mp4MfroBox))]
    public partial class MfroView : UserControl, IBoxView
    {
        /// <summary>
        /// 
        /// </summary>
        private Mp4MfroBox box;

        /// <summary>
        /// 
        /// </summary>
        public MfroView()
        {
            InitializeComponent();
        }

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
                box = (Mp4MfroBox)value;

                BoxViewUtil.AddHeader(grid, "Movie Fragment Random Access Offset Box");
                BoxViewUtil.AddField(grid, "Mfra size:", box.MfraSize);
            }
        }
    }
}
