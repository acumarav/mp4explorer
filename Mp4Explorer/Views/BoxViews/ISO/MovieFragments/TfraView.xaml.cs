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
    [BoxViewType(typeof(Mp4TfraBox))]
    public partial class TfraView : UserControl, IBoxView
    {
        /// <summary>
        /// 
        /// </summary>
        private Mp4TfraBox box;

        /// <summary>
        /// 
        /// </summary>
        public TfraView()
        {
            InitializeComponent();
        }

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
                box = (Mp4TfraBox)value;

                BoxViewUtil.AddHeader(grid, "Track Fragment Random Access Box");
                BoxViewUtil.AddField(grid, "Track id:", box.TrackId);
                BoxViewUtil.AddField(grid, "Length size of traf num:", box.LengthSizeOfTrafNum);
                BoxViewUtil.AddField(grid, "Length size of trun num:", box.LengthSizeOfTrunNum);
                BoxViewUtil.AddField(grid, "Length size of sample num:", box.LengthSizeOfSampleNum);
                BoxViewUtil.AddField(grid, "Number of entries:", box.NumberOfEntry);                
                BoxViewUtil.AddSubHeader(grid, "Entries");
                BoxViewUtil.AddControl(grid, BuildListView(box)).Height = new GridLength(200, GridUnitType.Pixel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elst"></param>
        /// <returns></returns>
        private ListView BuildListView(Mp4TfraBox tfra)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.DisplayMemberBinding = new Binding("Time");
            c1.Header = "Time";
            grid.Columns.Add(c1);
            
            GridViewColumn c2 = new GridViewColumn();
            c2.DisplayMemberBinding = new Binding("MoofOffset");
            c2.Header = "Moof offset";
            grid.Columns.Add(c2);
            
            GridViewColumn c3 = new GridViewColumn();
            c3.DisplayMemberBinding = new Binding("TrafNumber");
            c3.Header = "Traf number";
            grid.Columns.Add(c3);

            GridViewColumn c4 = new GridViewColumn();
            c4.DisplayMemberBinding = new Binding("TrunNumber");
            c4.Header = "Trun number";
            grid.Columns.Add(c4);

            GridViewColumn c5 = new GridViewColumn();
            c5.DisplayMemberBinding = new Binding("SampleNumber");
            c5.Header = "Sample number";
            grid.Columns.Add(c5);

            ObservableCollection<Mp4TfraEntry> coll = new ObservableCollection<Mp4TfraEntry>(tfra.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }
    }
}
