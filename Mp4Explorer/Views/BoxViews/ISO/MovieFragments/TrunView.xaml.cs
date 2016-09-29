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
    [BoxViewType(typeof(Mp4TrunBox))]
    public partial class TrunView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4TrunBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public TrunView()
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
                box = (Mp4TrunBox)value;

                BoxViewUtil.AddHeader(grid, "Track Fragment Run Box");

                BoxViewUtil.AddField(grid, "Sample count:", box.Entries.Count);
                BoxViewUtil.AddField(grid, "Data offset:", box.DataOffset);

                BoxViewUtil.AddSubHeader(grid, "First sample flags data");

                Mp4SampleFlagsData flagsData = new Mp4SampleFlagsData(box.FirstSampleFlags);
                BoxViewUtil.AddField(grid, "Sample depends on:", flagsData.SampleDependsOn);
                BoxViewUtil.AddField(grid, "Sample is depend on:", flagsData.SampleIsDependOn);
                BoxViewUtil.AddField(grid, "Sample has redudancy:", flagsData.SampleHasRedudancy);
                BoxViewUtil.AddField(grid, "Sample padding value:", flagsData.SamplePaddingValue);
                BoxViewUtil.AddField(grid, "Sample is difference sample:", flagsData.SampleIsDifferenceSample);
                BoxViewUtil.AddField(grid, "Sample degradation priority:", flagsData.SampleDegradationPriority);

                BoxViewUtil.AddSubHeader(grid, "Samples");

                BoxViewUtil.AddControl(grid, BuildListView(box)).Height = new GridLength(200, GridUnitType.Pixel);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trun"></param>
        /// <returns></returns>
        private ListView BuildListView(Mp4TrunBox trun)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.DisplayMemberBinding = new Binding("SampleDuration");
            c1.Header = "Duration";
            grid.Columns.Add(c1);

            GridViewColumn c2 = new GridViewColumn();
            c2.DisplayMemberBinding = new Binding("SampleSize");
            c2.Header = "Size";
            grid.Columns.Add(c2);

            GridViewColumn c3 = new GridViewColumn();
            c3.DisplayMemberBinding = new Binding("SampleFlags");
            c3.Header = "Flags";
            grid.Columns.Add(c3);

            GridViewColumn c4 = new GridViewColumn();
            c4.DisplayMemberBinding = new Binding("SampleCompositionTimeOffset");
            c4.Header = "Composition time offset";
            grid.Columns.Add(c4);

            ObservableCollection<Mp4TrunEntry> coll = new ObservableCollection<Mp4TrunEntry>(trun.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
