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
    [BoxViewType(typeof(Mp4SmhdBox))]
    public partial class SmhdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4SmhdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SmhdView()
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
                box = (Mp4SmhdBox)value;

                BoxViewUtil.AddHeader(grid, "Sound Media Header Box");

                BoxViewUtil.AddField(grid, "Balance:", Mp4Util.FormatFloat((ushort)box.Balance));
            }
        }

        #endregion
    }
}
