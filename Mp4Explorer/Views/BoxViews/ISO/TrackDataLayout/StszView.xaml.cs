//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    [BoxViewType(typeof(Mp4StszBox))]
    public partial class StszView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4StszBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public StszView()
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
                box = (Mp4StszBox)value;

                BoxViewUtil.AddHeader(grid, "Sample Size Box.");
                BoxViewUtil.AddField(grid, "Sample size:", box.SampleSize);
                BoxViewUtil.AddField(grid, "Sample count:", box.SampleCount);
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(box)).Height = new GridLength(380, GridUnitType.Pixel);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stsz"></param>
        /// <returns></returns>
        private ListView BuildListView(Mp4StszBox stsz)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.Header = "#";
            c1.DisplayMemberBinding = new Binding("Key");
            c1.Width = 50;
            grid.Columns.Add(c1);

            GridViewColumn c3 = new GridViewColumn();
            //c1.DisplayMemberBinding = new Binding("?");
            c3.Header = "Sample size";
            c3.DisplayMemberBinding = new Binding("Value");
            grid.Columns.Add(c3);
            

           // ObservableCollection<uint> coll = new ObservableCollection<uint>(stsz.Entries);
            var pairs = stsz.Entries.Select((val, index) => new KeyValuePair<int, uint>(index, val)).ToList();
            ObservableCollection<KeyValuePair<int, uint>> coll = new ObservableCollection<KeyValuePair<int, uint>>(pairs);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
