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
    [BoxViewType(typeof(Mp4HmhdBox))]
    public partial class HmhdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4HmhdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public HmhdView()
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
                box = (Mp4HmhdBox)value;

                BoxViewUtil.AddHeader(grid, "Hint Media Header Box");

                BoxViewUtil.AddField(grid, "Maximum PDU size:", box.MaxPduSize);
                BoxViewUtil.AddField(grid, "Average PDU size:", box.AvgPduSize);
                BoxViewUtil.AddField(grid, "Maximum bitrate:", box.MaxBitrate);
                BoxViewUtil.AddField(grid, "Average bitrate:", box.AvgBitrate);
            }
        }

        #endregion
    }
}
