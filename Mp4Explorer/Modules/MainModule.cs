//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class MainModule : IModule
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// 
        /// </summary>
        private readonly IRegionManager regionManager;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="regionManager"></param>
        public MainModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            IMainTreePresenter presenter = this.container.Resolve<IMainTreePresenter>();

            IRegion region = this.regionManager.Regions[RegionNames.LeftRegion];
            region.Add(presenter.View);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<IMainController, MainController>();

            this.container.RegisterType<IMainService, MainService>(new ContainerControlledLifetimeManager());

            this.container.RegisterType<IMainTreeView, MainTreeView>();
            this.container.RegisterType<IMainTreePresenter, MainTreePresenter>();

            this.container.RegisterType<IMainView, MainView>();
        }

        #endregion
    }
}
