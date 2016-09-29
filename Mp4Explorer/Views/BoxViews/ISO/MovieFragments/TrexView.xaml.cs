//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    [BoxViewType(typeof(Mp4TrexBox))]
    public partial class TrexView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4TrexBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public TrexView()
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
                box = (Mp4TrexBox)value;

                BoxViewUtil.AddHeader(grid, "Track Extends Box");

                BoxViewUtil.AddField(grid, "Track id:", box.TrackId.ToString());
                BoxViewUtil.AddField(grid, "Default sample description index:", box.DefaultSampleDescriptionIndex.ToString());
                BoxViewUtil.AddField(grid, "Default sample duration:", box.DefaultSampleDuration.ToString());
                BoxViewUtil.AddField(grid, "Default sample size:", box.DefaultSampleSize.ToString());
                BoxViewUtil.AddField(grid, "Default sample flags:", box.DefaultSampleFlags.ToString());

                BoxViewUtil.AddSubHeader(grid, "Default sample flags data");

                Mp4SampleFlagsData flagsData = new Mp4SampleFlagsData(box.DefaultSampleFlags);
                BoxViewUtil.AddField(grid, "Sample depends on:", flagsData.SampleDependsOn.ToString());
                BoxViewUtil.AddField(grid, "Sample is depend on:", flagsData.SampleIsDependOn.ToString());
                BoxViewUtil.AddField(grid, "Sample has redudancy:", flagsData.SampleHasRedudancy.ToString());
                BoxViewUtil.AddField(grid, "Sample padding value:", flagsData.SamplePaddingValue.ToString());
                BoxViewUtil.AddField(grid, "Sample is difference sample:", flagsData.SampleIsDifferenceSample.ToString());
                BoxViewUtil.AddField(grid, "Sample degradation priority:", flagsData.SampleDegradationPriority.ToString());
            }
        }

        #endregion
    }
}
