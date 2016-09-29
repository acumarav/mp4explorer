//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainView : UserControl, IMainView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Mp4File file;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MainView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Mp4File File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;

                ShowFileInfo(file.FileType);

                Mp4Movie movie = file.Movie;

                if (movie != null)
                {
                    ShowMovieInfo(movie);

                    List<Mp4Track> tracks = movie.Tracks;
                    
                    ShowTracks(tracks, false, true);
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType"></param>
        public void ShowFileInfo(Mp4FtypBox fileType)
        {
            if (fileType == null)
                return;

            textBoxMajorBrand.Text = Mp4Util.FormatFourChars(fileType.MajorBrand);
            textBoxMinorVersion.Text = fileType.MinorVersion.ToString();
            textBoxCompatibleBrands.Text = Mp4Util.FormatFourChars(fileType.CompatibleBrands);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movie"></param>
        public void ShowMovieInfo(Mp4Movie movie)
        {
            textBoxDurationMs.Text = movie.DurationMs.ToString();
            textBoxTimeScale.Text = movie.TimeScale.ToString();
            textBoxTracks.Text = movie.Tracks.Count.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tracks"></param>
        /// <param name="show_samples"></param>
        /// <param name="verbose"></param>
        public void ShowTracks(List<Mp4Track> tracks, bool show_samples, bool verbose)
        {
            int index = 1;

            foreach (Mp4Track track in tracks)
            {
                ShowTrackInfo(index, track);

                index++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackIndex"></param>
        /// <param name="track"></param>
        public void ShowTrackInfo(int trackIndex, Mp4Track track)
        {
            Expander expander = new Expander();
            expander.Header = string.Format("Track {0}", trackIndex);
            expander.IsExpanded = true;
            expander.Background = Brushes.LightBlue;

            Grid grid = new Grid();
            grid.Background = Brushes.White;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Label label;
            TextBox textBox;

            #region Flag

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            label.Content = "Flags:";
            
            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.Text = track.Flags.ToString();
            if ((track.Flags & Mp4Track.TRACK_FLAG_ENABLED) != 0)
            {
                textBox.Text += " ENABLED";
            }
            if ((track.Flags & Mp4Track.TRACK_FLAG_IN_MOVIE) != 0)
            {
                textBox.Text += " IN-MOVIE";
            }
            if ((track.Flags & Mp4Track.TRACK_FLAG_IN_PREVIEW) != 0)
            {
                textBox.Text += " IN-PREVIEW";
            }

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);

            #endregion

            #region Id

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            label.Content = "Id:";

            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.Text = track.Id.ToString();

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);

            #endregion

            #region Type

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            label.Content = "Type:";

            textBox = new TextBox();
            textBox.IsReadOnly = true;
            switch (track.Type)
            {
                case Mp4TrackType.AUDIO:  textBox.Text = "Audio"; break;
                case Mp4TrackType.VIDEO: textBox.Text = "Video"; break;
                case Mp4TrackType.HINT: textBox.Text = "Hint"; break;
                case Mp4TrackType.SYSTEM: textBox.Text = "System"; break;
                case Mp4TrackType.TEXT: textBox.Text = "Text"; break;
                case Mp4TrackType.JPEG: textBox.Text = "Jpeg"; break;
                default:
                    {
                        string hdlr = Mp4Util.FormatFourChars(track.HandlerType);
                        textBox.Text = "Unknown [" + hdlr + "]";
                        break;
                    }
            }

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);

            #endregion

            #region Duration(ms)

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            label.Content = "Duration(ms):";

            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.Text = Mp4Util.ConvertTime(track.Duration, track.MediaTimeScale, 1000).ToString();

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);

            #endregion

            #region Time scale

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            label.Content = "Time scale:";

            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.Text = track.MediaTimeScale.ToString();

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);

            #endregion

            #region Sample count

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            label.Content = "Sample count:";

            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.Text = track.SampleCount.ToString();

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);

            #endregion

            Mp4Sample sample;
            
            int index = 0;
            uint desc_index = 0xFFFFFFFF;
            if (track.SampleCount > 0)
            {
                while ((sample = track.GetSample(index)) != null)
                {
                    if (sample.DescriptionIndex != desc_index)
                    {
                        desc_index = sample.DescriptionIndex;

                        // get the sample description
                        Mp4SampleEntry sample_desc = track.GetSampleDescription((int)desc_index);
                        if (sample_desc != null)
                        {
                            ShowSampleDescription(grid, (int)desc_index, sample_desc);
                        }
                        break;
                    }
                    index++;
                }
            }
            else
            {
                for (int i = 0; i < track.SampleDescriptionCount; i++)
                {
                    Mp4SampleEntry sampleDescription = track.GetSampleDescription(i);
                    if (sampleDescription != null)
                    {
                        ShowSampleDescription(grid, i, sampleDescription);
                    }
                }
            }

            expander.Content = grid;

            stackPanel.Children.Add(expander);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="sampleDescIndex"></param>
        /// <param name="description"></param>
        public void ShowSampleDescription(Grid grid, int sampleDescIndex, Mp4SampleEntry description)
        {
            Label label;
            TextBox textBox;

            #region Header

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.Background = Brushes.LightGray;
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            label.Content = "Sample Description " + sampleDescIndex;

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(label, 2);
            grid.Children.Add(label);

            #endregion

            Mp4SampleEntry desc = description;

            //if (desc is Mp4ProtectedSampleEntry) {
            //    Mp4ProtectedSampleDescription prot_desc = desc as Mp4ProtectedSampleDescription;
            //    if (prot_desc) ShowProtectedSampleDescription(*prot_desc, verbose);
            //    desc = prot_desc->GetOriginalSampleDescription();
            //}

            string coding = Mp4Util.FormatFourChars(desc.Type);

            #region Coding

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            label.Content = "Coding:";

            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.Text = coding.ToString();

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);

            #endregion

            // MPEG sample description
            if (desc is Mp4MpegVideoSampleEntry || desc is Mp4MpegAudioSampleEntry)
            {
                Mp4EsdsBox esds = desc.GetChild<Mp4EsdsBox>(Mp4BoxType.ESDS);

                if (esds != null && esds.EsDescriptor != null)
                {
                    Mp4DecoderConfigDescriptor dcd = esds.EsDescriptor.DecoderConfigDescriptor;

                    if (dcd != null)
                    {
                        #region Stream Type

                        grid.RowDefinitions.Add(new RowDefinition());

                        label = new Label();
                        label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                        label.Content = "Stream Type:";

                        textBox = new TextBox();
                        textBox.IsReadOnly = true;
                        textBox.Text = Mp4Util.GetStreamTypeString((Mp4StreamType)dcd.StreamType);

                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);

                        #endregion

                        #region Object Type

                        grid.RowDefinitions.Add(new RowDefinition());

                        label = new Label();
                        label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                        label.Content = "Object Type:";

                        textBox = new TextBox();
                        textBox.IsReadOnly = true;
                        textBox.Text = Mp4Util.GetObjectTypeString((Mp4ObjectTypeIndication)dcd.ObjectTypeIndication);

                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);

                        #endregion

                        #region Max Bitrate

                        grid.RowDefinitions.Add(new RowDefinition());

                        label = new Label();
                        label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                        label.Content = "Max Bitrate:";

                        textBox = new TextBox();
                        textBox.IsReadOnly = true;
                        textBox.Text = dcd.MaxBitrate.ToString();

                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);

                        #endregion

                        #region Avg Bitrate

                        grid.RowDefinitions.Add(new RowDefinition());

                        label = new Label();
                        label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                        label.Content = "Avg Bitrate:";

                        textBox = new TextBox();
                        textBox.IsReadOnly = true;
                        textBox.Text = dcd.AverageBitrate.ToString();

                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);

                        #endregion

                        #region Buffer Size

                        grid.RowDefinitions.Add(new RowDefinition());

                        label = new Label();
                        label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                        label.Content = "Buffer Size:";

                        textBox = new TextBox();
                        textBox.IsReadOnly = true;
                        textBox.Text = dcd.BufferSize.ToString();

                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);

                        #endregion
                    }

                    if (desc is Mp4MpegAudioSampleEntry)
                    {
                        #region MPEG-4 Audio Object Type

                        grid.RowDefinitions.Add(new RowDefinition());

                        label = new Label();
                        label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                        label.Content = "MPEG-4 Audio Object Type:";

                        textBox = new TextBox();
                        textBox.IsReadOnly = true;
                        textBox.Text = Mp4Util.GetMpeg4AudioObjectTypeString((Mp4Mpeg4AudioObjectType)((Mp4MpegAudioSampleEntry)desc).Mpeg4AudioObjectType);

                        Grid.SetColumn(label, 0);
                        Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(label);
                        Grid.SetColumn(textBox, 1);
                        Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                        grid.Children.Add(textBox);

                        #endregion
                    }
                }
            }

            // AVC Sample Description
            Mp4Avc1SampleEntry avc_desc = desc as Mp4Avc1SampleEntry;
            if (avc_desc != null)
            {
                string profile_name = Mp4Util.GetProfileName(avc_desc.Profile);

                if (profile_name == null)
                {
                    profile_name = avc_desc.Profile.ToString();
                }

                #region AVC Profile

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "AVC Profile:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = profile_name.ToString();

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion

                #region AVC Profile Compat

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "AVC Profile Compat:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = avc_desc.ProfileCompatibility.ToString("X");

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion

                #region AVC Level

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "AVC Level:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = avc_desc.Level.ToString();

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion

                #region AVC NALU Length Size

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "AVC NALU Length Size:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = avc_desc.NaluLengthSize.ToString();

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion
            }

            Mp4AudioSampleEntry audio_desc = desc as Mp4AudioSampleEntry;
            if (audio_desc != null)
            {
                // Audio sample description

                #region Sample Rate

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "Sample Rate:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = Mp4Util.FormatDouble(audio_desc.SampleRate);

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion

                #region Sample Size

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "Sample Size:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = audio_desc.SampleSize.ToString();

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion

                #region Channels

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "Channels:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = audio_desc.ChannelCount.ToString();

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion
            }

            Mp4VisualSampleEntry video_desc = desc as Mp4VisualSampleEntry;
            if (video_desc != null)
            {
                // Video sample description

                #region Width

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "Width:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = video_desc.Width.ToString();

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion

                #region Height

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "Height:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = video_desc.Height.ToString();

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion

                #region Depth

                grid.RowDefinitions.Add(new RowDefinition());

                label = new Label();
                label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                label.Content = "Depth:";

                textBox = new TextBox();
                textBox.IsReadOnly = true;
                textBox.Text = video_desc.Depth.ToString();

                Grid.SetColumn(label, 0);
                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                grid.Children.Add(label);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
                grid.Children.Add(textBox);

                #endregion
            }
        }

        #endregion
    }
}
