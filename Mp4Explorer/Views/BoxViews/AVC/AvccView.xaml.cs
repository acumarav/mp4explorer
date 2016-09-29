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
    [BoxViewType(typeof(Mp4AvccBox))]
    public partial class AvccView : UserControl, IBoxView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4AvccBox box;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public AvccView()
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
                box = (Mp4AvccBox)value;

                BoxViewUtil.AddHeader(grid, "AVC Decoder Configuration Record");

                BoxViewUtil.AddField(grid, "Configuration version:", box.ConfigurationVersion);
                BoxViewUtil.AddField(grid, "AVC profile indication:", Mp4Util.GetProfileName(box.AVCProfileIndication));
                BoxViewUtil.AddField(grid, "AVC level indication:", box.AVCLevelIndication);
                BoxViewUtil.AddField(grid, "AVC compatible profiles:", box.AVCCompatibleProfiles.ToString("X"));
                BoxViewUtil.AddField(grid, "NALU length size:", box.NaluLengthSize);
                
                for (int i = 0; i < box.SequenceParameters.Count; i++)
                {
                    BoxViewUtil.AddField(grid, "Sequence parameter " + i + ":", box.SequenceParameters[i]);
                }

                for (int i = 0; i < box.PictureParameters.Count; i++)
                {
                    BoxViewUtil.AddField(grid, "Picture parameter " + i + ":", box.PictureParameters[i]);
                }
            }
        }

        #endregion
    }
}
