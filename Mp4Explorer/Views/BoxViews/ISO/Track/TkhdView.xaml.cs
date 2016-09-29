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
    [BoxViewType(typeof(Mp4TkhdBox))]
    public partial class TkhdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4TkhdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public TkhdView()
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
                box = (Mp4TkhdBox)value;

                BoxViewUtil.AddHeader(grid, "Track Header Box");
                BoxViewUtil.AddField(grid, "Flags:", FormatFlags(box.Flags.ToString()));
                BoxViewUtil.AddField(grid, "Creation time(UTC):", Mp4Util.FormatTime(box.CreationTime));
                BoxViewUtil.AddField(grid, "Modification time(UTC):", Mp4Util.FormatTime(box.ModificationTime));
                BoxViewUtil.AddField(grid, "Track id:", box.TrackId.ToString());
                BoxViewUtil.AddField(grid, "Duration:", box.Duration.ToString());
                BoxViewUtil.AddField(grid, "Layer:", box.Layer.ToString());
                BoxViewUtil.AddField(grid, "Alternate group:", box.AlternateGroup.ToString());
                BoxViewUtil.AddField(grid, "Volume:", Mp4Util.FormatFloat(box.Volume));
                BoxViewUtil.AddField(grid, "Width:", Mp4Util.FormatDouble(box.Width));
                BoxViewUtil.AddField(grid, "Height:", Mp4Util.FormatDouble(box.Height));
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flagsValue"></param>
        /// <returns></returns>
        private string FormatFlags(string flagsValue)
        {
            if ((box.Flags & Mp4Track.TRACK_FLAG_ENABLED) != 0)
            {
                flagsValue += " ENABLED";
            }
            if ((box.Flags & Mp4Track.TRACK_FLAG_IN_MOVIE) != 0)
            {
                flagsValue += " IN-MOVIE";
            }
            if ((box.Flags & Mp4Track.TRACK_FLAG_IN_PREVIEW) != 0)
            {
                flagsValue += " IN-PREVIEW";
            }

            return flagsValue;
        }

        #endregion
    }
}
