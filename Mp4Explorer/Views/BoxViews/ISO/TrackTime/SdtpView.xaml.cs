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
    [BoxViewType(typeof(Mp4SdtpBox))]
    public partial class SdtpView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4SdtpBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public SdtpView()
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
                box = (Mp4SdtpBox)value;

                BoxViewUtil.AddHeader(grid, "Independent and Disposable Samples Box");

                //TODO: Change
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
        private ListView BuildListView(Mp4SdtpBox sdtp)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.DisplayMemberBinding = new Binding("SampleDependsOn");
            c1.Header = "Sample depends on";
            grid.Columns.Add(c1);

            GridViewColumn c2 = new GridViewColumn();
            c2.DisplayMemberBinding = new Binding("SampleIsDependOn");
            c2.Header = "Sample is depend on";
            grid.Columns.Add(c2);

            GridViewColumn c3 = new GridViewColumn();
            c3.DisplayMemberBinding = new Binding("SampleHasRedundancy");
            c3.Header = "Sample has redundancy";
            grid.Columns.Add(c3);

            ObservableCollection<Mp4SdtpEntry> coll = new ObservableCollection<Mp4SdtpEntry>(sdtp.Entries);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
