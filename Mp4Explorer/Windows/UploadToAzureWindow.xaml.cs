//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Mp4Explorer.SmoothStreaming;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UploadToAzureWindow : Window
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private string fovFilename;

        /// <summary>
        /// 
        /// </summary>
        private ManifestInfo manifestInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public UploadToAzureWindow()
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
            buttonUpload.IsEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowseFov_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Fixed Offset Files (*.fov)|*.fov|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == true)
                {
                    fovFilename = openFileDialog.FileName;

                    string directoryName = new FileInfo(fovFilename).Directory.FullName;

                    string manifestFilename = Path.Combine(directoryName, Path.GetFileNameWithoutExtension(fovFilename) + ".ismc");

                    manifestInfo = new ManifestParser().Parse(manifestFilename);

                    textBoxFovPath.Text = fovFilename;
                    textBoxFovPath.ToolTip = fovFilename;

                    buttonUpload.IsEnabled = true;
                }
                else
                {
                    buttonUpload.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                buttonUpload.IsEnabled = false;

                ErrorWindow errorWin = new ErrorWindow("Error loading video data.", ex);
                errorWin.Owner = this;
                errorWin.ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonbuttonUpload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BackgroundWorker backgroundWorker = new BackgroundWorker();

                Exception exceptionError = null;

                string account = textBoxStorateAccount.Text.Trim();
                string key = passwordBoxSharedKey.Password.Trim();
                string containerName = textBoxContainerName.Text.Trim();

                backgroundWorker.DoWork += delegate(object s, DoWorkEventArgs args)
                {
                    try
                    {
                        StorageCredentialsAccountAndKey credentials = new StorageCredentialsAccountAndKey(account, key);                        

                        CloudBlobClient client = new CloudBlobClient("http://" + account + ".blob.core.windows.net", credentials);

                        CloudBlobContainer container;
                        CloudBlob blob;

                        container = new CloudBlobContainer(containerName, client);
                        BlobContainerPermissions containerPermissions = new BlobContainerPermissions();
                        containerPermissions.PublicAccess = BlobContainerPublicAccessType.Container;                        
                        container.CreateIfNotExist();
                        container.SetPermissions(containerPermissions);

                        string directoryName = new FileInfo(fovFilename).Directory.FullName;

                        blob = new CloudBlob(containerName + "/" + manifestInfo.Name + ".ism/Manifest", client);
                        blob.DeleteIfExists();
                        blob.Properties.ContentType = "text/xml";
                        blob.UploadFile(manifestInfo.Filename);

                        foreach (string line in File.ReadAllLines(fovFilename))
                        {
                            string[] arr = line.Split(',');

                            string mediatype = arr[0].Split('=')[1];
                            string bitrate = arr[1].Split('=')[1];
                            string starttime = arr[2].Split('=')[1];
                            string file = arr[3].Split('=')[1];
                            string offset = arr[4].Split('=')[1];
                            string size = arr[5].Split('=')[1];

                            string path = string.Format("{0}/{1}.ism/QualityLevels({2})/Fragments({3}={4})", containerName, manifestInfo.Name, bitrate, mediatype, starttime);

                            FileStream stream = File.OpenRead(Path.Combine(directoryName, file));

                            byte[] buffer = new byte[int.Parse(size)];
                            stream.Position = int.Parse(offset);
                            stream.Read(buffer, 0, buffer.Length);

                            stream.Close();

                            blob = new CloudBlob(path, client);
                            blob.DeleteIfExists();
                            blob.Properties.ContentType = "video/mp4";
                            blob.UploadByteArray(buffer);
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
                    MessageBox.Show("Video uploaded to Azure Storage account.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    throw exceptionError;
                }
            }
            catch (Exception ex)
            {
                ErrorWindow errorWin = new ErrorWindow("Error uploading to azure.", ex);
                errorWin.Owner = this;
                errorWin.ShowDialog();
            }
        }

        #endregion
    }
}
