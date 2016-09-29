//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Collections.Generic;
using System.Reflection;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class MainService : IMainService
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Type, Type> viewsCache;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public MainService()
        {
            viewsCache = new Dictionary<Type, Type>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        public IBoxView GetView(Mp4Box box)
        {
            Type type = null;

            if (!viewsCache.ContainsKey(box.GetType()))
            {
                Assembly assembly = Assembly.GetAssembly(this.GetType());

                List<Type> types = new List<Type>(assembly.GetTypes());

                type = types.Find(t =>
                    {
                        object[] customAttributes = t.GetCustomAttributes(typeof(BoxViewTypeAttribute), false);

                        return typeof(IBoxView).IsAssignableFrom(t) && customAttributes.Length == 1 &&
                            ((BoxViewTypeAttribute)customAttributes[0]).BoxType == box.GetType();
                    });

                if (type != null)
                {
                    viewsCache.Add(box.GetType(), type);
                }
                else
                {
                    type = null;
                }
            }
            else
            {
                type = viewsCache[box.GetType()];
            }

            if (type != null)
            {
                IBoxView view = (IBoxView)type.GetConstructor(Type.EmptyTypes).Invoke(null);

                view.Box = box;

                return view;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
