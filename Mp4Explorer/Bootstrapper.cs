//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        #region Protected methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            Shell shell = Container.Resolve<Shell>();
            shell.Show();
            return shell;
        }

                /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog GetModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            catalog.AddModule(typeof(MainModule));

            return catalog;
        }

        #endregion
    }
}
