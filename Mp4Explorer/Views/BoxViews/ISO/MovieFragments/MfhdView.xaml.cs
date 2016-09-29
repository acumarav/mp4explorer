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
    [BoxViewType(typeof(Mp4MfhdBox))]
    public partial class MfhdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4MfhdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MfhdView()
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
                box = (Mp4MfhdBox)value;

                BoxViewUtil.AddHeader(grid, "Movie Fragment Header Box");
                BoxViewUtil.AddField(grid, "Sequence number:", box.SequenceNumber);
            }
        }

        #endregion
    }
}
