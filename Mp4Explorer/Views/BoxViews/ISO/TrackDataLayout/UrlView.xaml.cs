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
    [BoxViewType(typeof(Mp4UrlBox))]
    public partial class UrlView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4UrlBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public UrlView()
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
                box = (Mp4UrlBox)value;

                BoxViewUtil.AddHeader(grid, "Data Entry Url Box");

                if ((box.Flags & 1) != 0)
                    BoxViewUtil.AddField(grid, "Location:", "Same file");
                else
                    BoxViewUtil.AddField(grid, "Location:", box.Location);
            }
        }

        #endregion
    }
}
