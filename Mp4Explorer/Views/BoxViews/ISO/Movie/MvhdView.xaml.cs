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
    [BoxViewType(typeof(Mp4MvhdBox))]
    public partial class MvhdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4MvhdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MvhdView()
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
                box = (Mp4MvhdBox)value;

                BoxViewUtil.AddHeader(grid, "Movie Header Box");
                BoxViewUtil.AddField(grid, "Creation time(UTC):", Mp4Util.FormatTime(box.CreationTime));
                BoxViewUtil.AddField(grid, "Modification time(UTC):", Mp4Util.FormatTime(box.ModificationTime));
                BoxViewUtil.AddField(grid, "Time scale:", box.TimeScale.ToString());
                BoxViewUtil.AddField(grid, "Duration:", box.Duration.ToString());
                BoxViewUtil.AddField(grid, "Duration(ms):", Mp4Util.ConvertTime(box.Duration, box.TimeScale, 1000));
                BoxViewUtil.AddField(grid, "Rate:", Mp4Util.FormatDouble(box.Rate));
                BoxViewUtil.AddField(grid, "Volume:", Mp4Util.FormatFloat(box.Volume));
                BoxViewUtil.AddField(grid, "Next track id:", box.NextTrackId.ToString());
            }
        }

        #endregion
    }
}
