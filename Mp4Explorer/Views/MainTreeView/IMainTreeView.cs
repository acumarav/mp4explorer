//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Practices.Composite.Events;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMainTreeView
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        TreeViewItem RootNode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Dispatcher Dispatcher { get; }

        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<DataEventArgs<TreeViewItem>> NodeSelected;

        #endregion
    }
}
