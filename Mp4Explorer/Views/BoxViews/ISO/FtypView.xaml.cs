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
    [BoxViewType(typeof(Mp4FtypBox))]
    public partial class FtypView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4FtypBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public FtypView()
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
                box = (Mp4FtypBox)value;

                BoxViewUtil.AddHeader(grid, "File Type Box");
                BoxViewUtil.AddField(grid, "Major brand:", Mp4Util.FormatFourChars(box.MajorBrand));
                BoxViewUtil.AddField(grid, "Minor version:", box.MinorVersion.ToString());
                BoxViewUtil.AddField(grid, "Compatible brands:", Mp4Util.FormatFourChars(box.CompatibleBrands));
            }
        }

        #endregion
    }
}
