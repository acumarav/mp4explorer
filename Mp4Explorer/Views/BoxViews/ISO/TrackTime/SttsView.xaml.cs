//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    [BoxViewType(typeof(Mp4SttsBox))]
    public partial class SttsView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4SttsBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SttsView()
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
                box = (Mp4SttsBox)value;

                BoxViewUtil.AddHeader(grid, "Decoding Time to Sample Box");

                BoxViewUtil.AddField(grid, "Entry count:", box.EntryCount);
                
                BoxViewUtil.AddSubHeader(grid, "Entries");

                BoxViewUtil.AddControl(grid, BuildListView(box)).Height = new GridLength(400, GridUnitType.Pixel);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elst"></param>
        /// <returns></returns>
        private ListView BuildListView(Mp4SttsBox stts)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.DisplayMemberBinding = new Binding("SampleCount");
            c1.Header = "Sample count";
            grid.Columns.Add(c1);

            GridViewColumn c2 = new GridViewColumn();
            c2.DisplayMemberBinding = new Binding("SampleDelta");
            c2.Header = "Sample delta";
            grid.Columns.Add(c2);

            ObservableCollection<Mp4SttsEntry> coll = new ObservableCollection<Mp4SttsEntry>(stts.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
