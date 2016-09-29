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
    [BoxViewType(typeof(Mp4IodsBox))]
    public partial class IodsView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4IodsBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public IodsView()
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
                box = (Mp4IodsBox)value;

                BoxViewUtil.AddHeader(grid, "Object Descriptor Box");
            }
        }

        #endregion
    }
}
