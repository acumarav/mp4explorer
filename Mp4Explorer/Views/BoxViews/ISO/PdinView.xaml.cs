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
    [BoxViewType(typeof(Mp4PdinBox))]
    public partial class PdinView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4PdinBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PdinView()
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
                box = (Mp4PdinBox)value;

                BoxViewUtil.AddHeader(grid, "Progressive Download Information Box");

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
        /// <param name="trefType"></param>
        /// <returns></returns>
        private ListView BuildListView(Mp4PdinBox pdin)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.DisplayMemberBinding = new Binding("InitialDelay");
            c1.Header = "Initial delay";
            grid.Columns.Add(c1);

            GridViewColumn c2 = new GridViewColumn();
            c2.DisplayMemberBinding = new Binding("Rate");
            c2.Header = "Rate";
            grid.Columns.Add(c2);

            ObservableCollection<Mp4PdinEntry> coll = new ObservableCollection<Mp4PdinEntry>(pdin.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
