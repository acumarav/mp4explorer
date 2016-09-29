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
    [BoxViewType(typeof(Mp4MfraBox))]
    public partial class MfraView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4MfraBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MfraView()
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
                box = (Mp4MfraBox)value;

                BoxViewUtil.AddHeader(grid, "Movie Fragment Random Access Box");
                BoxViewUtil.AddField(grid, "Size:", box.Size);
            }
        }

        #endregion
    }
}
