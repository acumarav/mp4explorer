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
    [BoxViewType(typeof(Mp4TrefTypeBox))]
    public partial class TrefTypeView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4TrefTypeBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public TrefTypeView()
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
                box = (Mp4TrefTypeBox)value;

                BoxViewUtil.AddHeader(grid, "Track Reference Box");

                if (box.Type == Mp4BoxType.HINT)
                    BoxViewUtil.AddField(grid, "hint:", "The referenced track(s) contain the original media for this hint track.");
                else if (box.Type == Mp4BoxType.CDSC)
                    BoxViewUtil.AddField(grid, "cdsc:", "This track describes the referenced track.");
                //TODO: Add box type.
                //else if (box.Type == Mp4BoxType.HIND)
                  //  BoxViewUtil.AddField(grid, "hind:", "This track depends on the referenced hint track, it should only be used if the referenced hint track is used.");
                
                BoxViewUtil.AddSubHeader(grid, "Tracks");

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
        private ListView BuildListView(Mp4TrefTypeBox trefType)
        {
            ListView listView = new ListView();

            GridView grid = new GridView();

            GridViewColumn c1 = new GridViewColumn();
            c1.Header = "ID";
            grid.Columns.Add(c1);

            ObservableCollection<uint> coll = new ObservableCollection<uint>(trefType.TrackIds);

            listView.ItemsSource = coll;
            listView.View = grid;

            return listView;
        }

        #endregion
    }
}
