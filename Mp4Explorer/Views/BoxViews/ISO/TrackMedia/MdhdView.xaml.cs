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
    [BoxViewType(typeof(Mp4MdhdBox))]
    public partial class MdhdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4MdhdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MdhdView()
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
                box = (Mp4MdhdBox)value;

                BoxViewUtil.AddHeader(grid, "Media Header Box");
                BoxViewUtil.AddField(grid, "Creation time(UTC):", Mp4Util.FormatTime(box.CreationTime));
                BoxViewUtil.AddField(grid, "Modification time(UTC):", Mp4Util.FormatTime(box.ModificationTime));
                BoxViewUtil.AddField(grid, "Time scale:", box.TimeScale.ToString());
                BoxViewUtil.AddField(grid, "Duration:", box.Duration.ToString());
                BoxViewUtil.AddField(grid, "Language:", box.Language.ToString());
            }
        }

        #endregion
    }
}
