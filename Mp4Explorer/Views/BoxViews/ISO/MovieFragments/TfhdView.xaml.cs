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
    [BoxViewType(typeof(Mp4TfhdBox))]
    public partial class TfhdView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4TfhdBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public TfhdView()
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
                box = (Mp4TfhdBox)value;

                BoxViewUtil.AddHeader(grid, "Track Fragment Header Box");

                BoxViewUtil.AddField(grid, "Track id:", box.TrackId);

                if ((box.Flags & Mp4TfhdBox.FLAG_BASE_DATA_OFFSET_PRESENT) != 0)
                {
                    BoxViewUtil.AddField(grid, "Base data offset:", box.BaseDataOffset);
                }
                if ((box.Flags & Mp4TfhdBox.FLAG_SAMPLE_DESCRIPTION_INDEX_PRESENT) != 0)
                {
                    BoxViewUtil.AddField(grid, "Sample description index:", box.SampleDescriptionIndex);
                }
                if ((box.Flags & Mp4TfhdBox.FLAG_DEFAULT_SAMPLE_DURATION_PRESENT) != 0)
                {
                    BoxViewUtil.AddField(grid, "Default sample duration:", box.DefaultSampleDuration);
                }
                if ((box.Flags & Mp4TfhdBox.FLAG_DEFAULT_SAMPLE_SIZE_PRESENT) != 0)
                {
                    BoxViewUtil.AddField(grid, "Default sample size:", box.DefaultSampleSize);
                }
                if ((box.Flags & Mp4TfhdBox.FLAG_DEFAULT_SAMPLE_FLAGS_PRESENT) != 0)
                {
                    BoxViewUtil.AddSubHeader(grid, "Default sample flags data");

                    Mp4SampleFlagsData flagsData = new Mp4SampleFlagsData(box.DefaultSampleFlags);
                    BoxViewUtil.AddField(grid, "Sample depends on:", flagsData.SampleDependsOn);
                    BoxViewUtil.AddField(grid, "Sample is depend on:", flagsData.SampleIsDependOn);
                    BoxViewUtil.AddField(grid, "Sample has redudancy:", flagsData.SampleHasRedudancy);
                    BoxViewUtil.AddField(grid, "Sample padding value:", flagsData.SamplePaddingValue);
                    BoxViewUtil.AddField(grid, "Sample is difference sample:", flagsData.SampleIsDifferenceSample);
                    BoxViewUtil.AddField(grid, "Sample degradation priority:", flagsData.SampleDegradationPriority);
                }
            }
        }

        #endregion
    }
}
