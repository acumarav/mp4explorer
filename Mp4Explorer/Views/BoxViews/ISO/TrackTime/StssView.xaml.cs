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
    [BoxViewType(typeof(Mp4StssBox))]
    public partial class StssView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4StssBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public StssView()
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
                box = (Mp4StssBox)value;

                BoxViewUtil.AddHeader(grid, "Sync Sample Box");

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
        private ListView BuildListView(Mp4StssBox stss)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            //c1.DisplayMemberBinding = new Binding("?");
            c1.Header = "Sample number";
            grid.Columns.Add(c1);

            ObservableCollection<uint> coll = new ObservableCollection<uint>(stss.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
