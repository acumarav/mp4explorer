//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageTreeViewItem : TreeViewItem
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private TextBlock textBlock;

        /// <summary>
        /// 
        /// </summary>
        private Image image;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ImageTreeViewItem()
        {
            StackPanel stackPanel = new StackPanel();

            stackPanel.Orientation = Orientation.Horizontal;

            Header = stackPanel;

            image = new Image();

            image.VerticalAlignment = VerticalAlignment.Center;

            image.Margin = new Thickness(0, 0, 2, 0);

            stackPanel.Children.Add(image);

            textBlock = new TextBlock();

            textBlock.Margin = new Thickness(0, 0, 4, 0);

            textBlock.VerticalAlignment = VerticalAlignment.Center;

            stackPanel.Children.Add(textBlock);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual string Text
        {
            get { return textBlock.Text; }
            set { textBlock.Text = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ImageSource
        {
            get
            {
                try
                {
                    return image.Source.ToString();
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(value, UriKind.Relative);
                    bmp.EndInit();

                    image.Stretch = Stretch.Fill;
                    image.Source = bmp;
                }
            }
        }

        #endregion
    }
}
