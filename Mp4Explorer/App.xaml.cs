//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class App : Application
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        #endregion

        private Bootstrapper bootstrapper;

        #region Protected methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            var args = Environment.GetCommandLineArgs();
            if (args.Length == 2 && File.Exists(args[1]))
            {
                var presenter = bootstrapper.Container.Resolve<IMainTreePresenter>();
                Dispatcher.Invoke(new Action( delegate 
                {
                    //Thread.Sleep(5000);
               // presenter.OpenFile(new FileStream(args[1], FileMode.Open));
                }));
                //presenter.OpenFile(new FileStream(args[0], FileMode.Open));
            }
        }


        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            ErrorWindow errorWindow = new ErrorWindow(e.Exception);
            errorWindow.ShowDialog();
        }

        #endregion
    }
}
