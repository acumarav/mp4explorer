//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Shell : Window
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Shell()
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
        private void menuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutBoxWindow aboutBoxWindow = new AboutBoxWindow();

            aboutBoxWindow.Owner = this;
            aboutBoxWindow.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemCreateFixedOffset_Click(object sender, RoutedEventArgs e)
        {
            CreateFixedOffsetWindow createFoxedOffsetWindow = new CreateFixedOffsetWindow();

            createFoxedOffsetWindow.Owner = this;
            createFoxedOffsetWindow.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemConvertVideoToMp4_Click(object sender, RoutedEventArgs e)
        {
            ConvertVideoToMp4Window convertVideoToMp4Window = new ConvertVideoToMp4Window();

            convertVideoToMp4Window.Owner = this;
            convertVideoToMp4Window.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemUploadToAzure_Click(object sender, RoutedEventArgs e)
        {
            UploadToAzureWindow uploadToAzureWindow = new UploadToAzureWindow();

            uploadToAzureWindow.Owner = this;
            uploadToAzureWindow.ShowDialog();
        }

        

        #endregion
    }
}
