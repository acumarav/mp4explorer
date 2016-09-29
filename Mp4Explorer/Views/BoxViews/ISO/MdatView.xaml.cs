//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows;
using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    [BoxViewType(typeof(Mp4MdatBox))]
    public partial class MdatView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4MdatBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MdatView()
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
                box = (Mp4MdatBox)value;

                byte[] data = new byte[Math.Min(32 * 1024, box.Size - box.HeaderSize)];

                box.Stream.Position = box.Position;
                box.Stream.Read(data, data.Length);

                ScrollViewer sc = new ScrollViewer();
                sc.Content = new HexView() { Data = data };
                sc.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

                BoxViewUtil.AddHeader(grid, "Media Data Box");
                BoxViewUtil.AddSubHeader(grid, "Raw data (up to 32K bytes)");
                BoxViewUtil.AddControl(grid, sc).Height = new GridLength(400);
            }
        }

        #endregion
    }
}
