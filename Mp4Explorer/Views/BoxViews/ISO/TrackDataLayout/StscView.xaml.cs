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
    [BoxViewType(typeof(Mp4StscBox))]
    public partial class StscView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4StscBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public StscView()
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
                box = (Mp4StscBox)value;

                BoxViewUtil.AddHeader(grid, "Sample To Chunk Box");

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
        private ListView BuildListView(Mp4StscBox trun)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.DisplayMemberBinding = new Binding("FirstChunk");
            c1.Header = "First chunk";
            grid.Columns.Add(c1);

            GridViewColumn c2 = new GridViewColumn();
            c2.DisplayMemberBinding = new Binding("SamplesPerChunk");
            c2.Header = "Samples per chunk";
            grid.Columns.Add(c2);

            GridViewColumn c3 = new GridViewColumn();
            c3.DisplayMemberBinding = new Binding("SampleDescriptionIndex");
            c3.Header = "Sample description index";
            grid.Columns.Add(c3);

            ObservableCollection<Mp4StscEntry> coll = new ObservableCollection<Mp4StscEntry>(trun.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
