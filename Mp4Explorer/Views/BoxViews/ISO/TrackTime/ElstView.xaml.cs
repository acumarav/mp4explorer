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
    [BoxViewType(typeof(Mp4ElstBox))]
    public partial class ElstView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4ElstBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ElstView()
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
                box = (Mp4ElstBox)value;

                BoxViewUtil.AddHeader(grid, "Edit List Box");
                BoxViewUtil.AddField(grid, "Entry count:", box.EntryCount.ToString());
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
        private ListView BuildListView(Mp4ElstBox elst)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.DisplayMemberBinding = new Binding("SegmentDuration");
            c1.Header = "Segment duration";
            grid.Columns.Add(c1);
            
            GridViewColumn c2 = new GridViewColumn();
            c2.DisplayMemberBinding = new Binding("MediaTime");
            c2.Header = "Media time";
            grid.Columns.Add(c2);
            
            GridViewColumn c3 = new GridViewColumn();
            c3.DisplayMemberBinding = new Binding("MediaRate");
            c3.Header = "Media rate";
            grid.Columns.Add(c3);

            ObservableCollection<Mp4ElstEntry> coll = new ObservableCollection<Mp4ElstEntry>(elst.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
