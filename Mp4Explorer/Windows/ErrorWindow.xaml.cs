//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ErrorWindow : Window
    {
        #region Contructors
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public ErrorWindow(Exception e)
        {
            InitializeComponent();
            if (e != null)
            {
                ErrorTextBox.Text = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public ErrorWindow(string message, Exception e)
        {
            InitializeComponent();
            if (e != null)
            {
                ErrorTextBox.Text = message + Environment.NewLine + e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        public ErrorWindow(Uri uri)
        {
            InitializeComponent();
            if (uri != null)
            {
                ErrorTextBox.Text = "Page not found: \"" + uri.ToString() + "\"";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ErrorWindow(string message)
            : this(message, "")
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="details"></param>
        public ErrorWindow(string message, string details)
        {
            InitializeComponent();
            ErrorTextBox.Text = message + Environment.NewLine + Environment.NewLine + details;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        #endregion
    }
}
