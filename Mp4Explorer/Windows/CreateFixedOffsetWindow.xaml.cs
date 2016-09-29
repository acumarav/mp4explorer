//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using CMStream.Mp4;
using Microsoft.Win32;
using Mp4Explorer.SmoothStreaming;
using Mp4Explorer.SmoothStreaming.Smil;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CreateFixedOffsetWindow : Window
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private ManifestInfo manifestInfo;

        /// <summary>
        /// 
        /// </summary>
        private SmilDocument smilDocument;

        #endregion\

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public CreateFixedOffsetWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            labelStatus.Content = string.Empty;
            buttonCalculate.IsEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowseManifest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Manifest Files (*.ismc)|*.ismc|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == true)
                {
                    BackgroundWorker backgroundWorker = new BackgroundWorker();

                    Exception exceptionError = null;

                    backgroundWorker.DoWork += delegate(object s, DoWorkEventArgs args)
                    {
                        try
                        {
                            string fileName = openFileDialog.FileName;
                            
                            string directoryName = new FileInfo(fileName).Directory.FullName;

                            string smilFilename = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(fileName) + ".ism");

                            manifestInfo = new ManifestParser().Parse(fileName);

                            smilDocument = SmilDocument.Load(smilFilename);

                            foreach (QualityLevelInfo qualityLevel in manifestInfo.Streams.Find(stream => stream.MediaType == MediaType.Video).QualityLevels)
                            {
                                SmilVideo smilVideo = smilDocument.Body.Switch.Video.First(stream => stream.SystemBitrate == qualityLevel.Bitrate.ToString());

                                qualityLevel.TrackId = Convert.ToInt32(smilVideo.Param.First(p => p.Name == "trackID").Value, CultureInfo.InvariantCulture);
                                qualityLevel.Filename = Path.Combine(directoryName, smilVideo.Src);

                                if (!File.Exists(qualityLevel.Filename))
                                    throw new FileNotFoundException("Video file not found", qualityLevel.Filename);
                            }

                            foreach (QualityLevelInfo qualityLevel in manifestInfo.Streams.Find(stream => stream.MediaType == MediaType.Audio).QualityLevels)
                            {
                                SmilAudio smilAudio = smilDocument.Body.Switch.Audio.First(stream => stream.SystemBitrate == qualityLevel.Bitrate.ToString());

                                qualityLevel.TrackId = Convert.ToInt32(smilAudio.Param.First(p => p.Name == "trackID").Value, CultureInfo.InvariantCulture);
                                qualityLevel.Filename = Path.Combine(directoryName, smilAudio.Src);

                                if (!File.Exists(qualityLevel.Filename))
                                    throw new FileNotFoundException("Video file not found", qualityLevel.Filename);
                            }

                            Dispatcher.Invoke(new Action(delegate
                                {
                                    textBoxManifestPath.Text = fileName;
                                    textBoxManifestPath.ToolTip = fileName;

                                    GridView grid = new GridView();

                                    GridViewColumn c1 = new GridViewColumn();
                                    c1.DisplayMemberBinding = new Binding("Type");
                                    c1.Header = "Type";
                                    grid.Columns.Add(c1);

                                    GridViewColumn c2 = new GridViewColumn();
                                    c2.DisplayMemberBinding = new Binding("File");
                                    c2.Header = "File";
                                    grid.Columns.Add(c2);

                                    GridViewColumn c3 = new GridViewColumn();
                                    c3.DisplayMemberBinding = new Binding("Path");
                                    c3.Header = "Path";
                                    grid.Columns.Add(c3);

                                    ObservableCollection<object> coll = new ObservableCollection<object>();

                                    coll.Add(new { Type = "SMIL", File = Path.GetFileName(smilFilename), Path = smilFilename });
                                    coll.Add(new { Type = "Manifest", File = Path.GetFileName(fileName), Path = fileName });

                                    foreach (QualityLevelInfo qualityLevel in manifestInfo.Streams.Find(stream => stream.MediaType == MediaType.Video).QualityLevels)
                                    {
                                        coll.Add(new { Type = "Quality level", File = Path.GetFileNameWithoutExtension(qualityLevel.Filename), Path = qualityLevel.Filename });
                                    }

                                    listViewFiles.ItemsSource = coll;
                                    listViewFiles.View = grid;
                                }));
                        }
                        catch (Exception ex)
                        {
                            exceptionError = ex;
                        }
                    };

                    ProgressWindow progressWindow = new ProgressWindow();

                    progressWindow.Owner = this;
                    progressWindow.Worker = backgroundWorker;

                    progressWindow.ShowDialog();

                    if (exceptionError == null)
                    {
                        labelStatus.Content = "OK";
                        labelStatus.Background = Brushes.LightGreen;
                        buttonCalculate.IsEnabled = true;
                    }
                    else
                    {
                        labelStatus.Content = "Error";
                        labelStatus.Background = Brushes.Red;
                        buttonCalculate.IsEnabled = false;

                        throw exceptionError;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorWindow errorWin = new ErrorWindow("Error reading the video set of files.", ex);
                errorWin.Owner = this;
                errorWin.ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonbuttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog safeFileDialog = new SaveFileDialog();

                safeFileDialog.Filter = "Fixed offset video file (*.fov)|*.fov";
                safeFileDialog.FilterIndex = 1;
                safeFileDialog.FileName = Path.Combine(Path.GetDirectoryName(manifestInfo.Filename), manifestInfo.Name + ".fov");

                if (safeFileDialog.ShowDialog() == true)
                {
                    BackgroundWorker backgroundWorker = new BackgroundWorker();

                    Exception exceptionError = null;

                    backgroundWorker.DoWork += delegate(object s, DoWorkEventArgs args)
                    {
                        try
                        {
                            string fileName = safeFileDialog.FileName;

                            TextWriter output = new StreamWriter(fileName, false, Encoding.UTF8);

                            try
                            {
                                foreach (StreamInfo streamInfo in manifestInfo.Streams)
                                {
                                    foreach (QualityLevelInfo qualityLevel in streamInfo.QualityLevels)
                                    {
                                        Mp4Stream stream = new Mp4FileStream(qualityLevel.Filename, FileAccess.Read);

                                        try
                                        {
                                            Mp4MfraBox mfra = GetMfra(stream);

                                            Mp4TfraBox tfra = (Mp4TfraBox)mfra.Children.First(b => b.Type == Mp4BoxType.TFRA && ((Mp4TfraBox)b).TrackId == qualityLevel.TrackId);

                                            foreach (MediaChunk chunk in streamInfo.Chunks)
                                            {

                                                Mp4TfraEntry entry = tfra.Entries[chunk.ChunkId];

                                                stream.Position = (long)entry.MoofOffset;

                                                uint size = 0;

                                                // moof
                                                size = stream.ReadUInt32();
                                                stream.Position += (size - 4);

                                                // mdat
                                                size += stream.ReadUInt32();

                                                string line = string.Format("mediatype={0},bitrate={1},starttime={2},file={3},offset={4},size={5}", streamInfo.MediaType.ToString().ToLower(), qualityLevel.Bitrate, entry.Time, Path.GetFileName(qualityLevel.Filename), entry.MoofOffset, size);

                                                output.WriteLine(line);
                                            }
                                        }
                                        finally
                                        {
                                            stream.Close();
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                output.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            exceptionError = ex;
                        }
                    };

                    ProgressWindow progressWindow = new ProgressWindow();

                    progressWindow.Owner = this;
                    progressWindow.Worker = backgroundWorker;

                    progressWindow.ShowDialog();

                    if (exceptionError == null)
                    {
                        MessageBox.Show("Fixed offset file created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        throw exceptionError;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorWindow errorWindow = new ErrorWindow("Error reading the video set of files.", ex);
                errorWindow.Owner = this;
                errorWindow.ShowDialog();
            }
        }

        /// <summary>
        /// Read Mfra box.
        /// </summary>
        /// <param name="stream">Mp4 stream.</param>
        /// <returns>Mfra box.</returns>
        private Mp4MfraBox GetMfra(Mp4Stream stream)
        {
            stream.Position = stream.Length - 4;

            uint mfraSize = stream.ReadUInt32();

            stream.Position = stream.Length - mfraSize;

            Mp4File file = new Mp4File(stream);

            return (Mp4MfraBox)file.Boxes[0];
        }

        #endregion
    }
}
