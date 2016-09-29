//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System.Windows.Controls;
using CMStream.Mp4;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class Mp4TreeNode : ImageTreeViewItem
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        public Mp4TreeNode(Mp4Box box)
        {
            this.Box = box;

            Text = Mp4Util.FormatFourChars(box.Type);
            ImageSource = GetImageSource(box.Type);

            if (box is Mp4ContainerBox)
            {
                foreach (Mp4Box child in ((Mp4ContainerBox)box).Children)
                {
                    Items.Add(new Mp4TreeNode(child));
                }
            }

            if (box is Mp4StsdBox)
            {
                foreach (Mp4Box child in ((Mp4StsdBox)box).Entries)
                {
                    Items.Add(new Mp4TreeNode(child));
                }
            }

            if (box is Mp4DrefBox)
            {
                foreach (Mp4Box child in ((Mp4DrefBox)box).Entries)
                {
                    Items.Add(new Mp4TreeNode(child));
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Mp4Box Box { get; set; }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetImageSource(uint type)
        {
            return "/Mp4Explorer;component/Images/" + Mp4Util.FormatFourChars(type) + "_16.ico";
        }

        #endregion
    }
}
