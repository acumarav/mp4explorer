//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    [BoxViewType(typeof(Mp4Co64Box))]
    public partial class Co64View : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4Co64Box box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Co64View()
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
                box = (Mp4Co64Box)value;

                BoxViewUtil.AddHeader(grid, "64-bit Chunk Offset Box.");
                //TODO: Change
                //BoxViewUtil.AddField(grid, "Entry count:", box.EntriesCount);
                BoxViewUtil.AddField(grid, "Entry count:", box.Entries.Count);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stco"></param>
        /// <returns></returns>
        private ListView BuildListView(Mp4Co64Box co64)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.Header = "Sample size";
            grid.Columns.Add(c1);

            ObservableCollection<ulong> coll = new ObservableCollection<ulong>(co64.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
