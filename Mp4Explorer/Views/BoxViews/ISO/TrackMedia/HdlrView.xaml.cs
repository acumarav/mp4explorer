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
    [BoxViewType(typeof(Mp4HdlrBox))]
    public partial class HdlrView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4HdlrBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public HdlrView()
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
                box = (Mp4HdlrBox)value;

                BoxViewUtil.AddHeader(grid, "Handler Reference Box");
                BoxViewUtil.AddField(grid, "Handler type:", Mp4Util.FormatFourChars(box.HandlerType));
                BoxViewUtil.AddField(grid, "Name:", box.Name);
            }
        }

        #endregion
    }
}
