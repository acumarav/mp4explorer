//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class BoxViewTypeAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boxType"></param>
        public BoxViewTypeAttribute(Type boxType)
        {
            this.BoxType = boxType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Type BoxType { get; set; }

        #endregion
    }
}
