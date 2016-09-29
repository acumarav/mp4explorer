//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Collections.Generic;
using CMStream.Mp4;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class MainController : IMainController
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private IUnityContainer container;

        /// <summary>
        /// 
        /// </summary>
        private IRegionManager regionManager;

        /// <summary>
        /// 
        /// </summary>
        private IMainService mainService;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="regionManager"></param>
        public MainController(IUnityContainer container, IRegionManager regionManager, IMainService mainService)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.mainService = mainService;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        public void OnBoxSelected(Mp4Box box)
        {
            RemoveViews();

            if (box != null)
            {
                IBoxView view = mainService.GetView(box);

                if (view != null)
                {
                    IRegion mainRegion = regionManager.Regions[RegionNames.MainRegion];

                    mainRegion.Add(view);
                    mainRegion.Activate(view);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public void ShowFile(Mp4File file)
        {
            RemoveViews();

            if (file != null)
            {
                IMainView view = this.container.Resolve<IMainView>();

                view.File = file;

                IRegion mainRegion = regionManager.Regions[RegionNames.MainRegion];

                mainRegion.Add(view);
                mainRegion.Activate(view);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveViews()
        {
            IRegion mainRegion = regionManager.Regions[RegionNames.MainRegion];

            List<object> views = new List<object>(mainRegion.Views);

            foreach (object view in views)
            {
                mainRegion.Remove(view);
            }
        }

        #endregion
    }
}
