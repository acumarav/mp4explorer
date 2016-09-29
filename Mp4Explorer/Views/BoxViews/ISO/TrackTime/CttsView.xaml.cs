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
    [BoxViewType(typeof(Mp4CttsBox))]
    public partial class CttsView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4CttsBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public CttsView()
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
                box = (Mp4CttsBox)value;

                BoxViewUtil.AddHeader(grid, "Composition Time to Sample Box");

                //TODO: Change
                //BoxViewUtil.AddField(grid, "Entry count:", box.EntryCount);
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
        /// <param name="elst"></param>
        /// <returns></returns>
        private ListView BuildListView(Mp4CttsBox ctts)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.DisplayMemberBinding = new Binding("SampleCount");
            c1.Header = "Sample count";
            grid.Columns.Add(c1);

            GridViewColumn c2 = new GridViewColumn();
            c2.DisplayMemberBinding = new Binding("SampleOffset");
            c2.Header = "Sample offset";
            grid.Columns.Add(c2);

            ObservableCollection<Mp4CttsEntry> coll = new ObservableCollection<Mp4CttsEntry>(ctts.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
