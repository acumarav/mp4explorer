//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Composite.Events;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainTreeView : UserControl, IMainTreeView
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private TreeViewItem rootNode;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MainTreeView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public TreeViewItem RootNode
        {
            get
            {
                return this.rootNode;
            }
            set
            {
                treeView.Items.Clear();

                this.rootNode = value;

                if (this.rootNode != null)
                {
                    treeView.Items.Add(this.rootNode);
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DataEventArgs<TreeViewItem>> NodeSelected = delegate { };

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_Selected(object sender, RoutedEventArgs e)
        {
            NodeSelected(this, new DataEventArgs<TreeViewItem>(e.Source as TreeViewItem));
        }

        #endregion
    }
}
