//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public static class BoxViewUtil
    {
        #region Public static methods

        #region AddField

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        public static void AddField(Grid grid, string caption, string value)
        {
            Label label;
            TextBox textBox;

            grid.RowDefinitions.Add(new RowDefinition());

            label = new Label();
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            label.Content = caption;

            textBox = new TextBox();
            textBox.IsReadOnly = true;
            textBox.Text = value;

            Grid.SetColumn(label, 0);
            Grid.SetRow(label, grid.RowDefinitions.Count - 1);
            grid.Children.Add(label);
            Grid.SetColumn(textBox, 1);
            Grid.SetRow(textBox, grid.RowDefinitions.Count - 1);
            grid.Children.Add(textBox);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        public static void AddField(Grid grid, string caption, uint value)
        {
            AddField(grid, caption, value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        public static void AddField(Grid grid, string caption, int value)
        {
            AddField(grid, caption, value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        public static void AddField(Grid grid, string caption, ulong value)
        {
            AddField(grid, caption, value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        public static void AddField(Grid grid, string caption, bool value)
        {
            AddField(grid, caption, value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        public static void AddField(Grid grid, string caption, byte[] value)
        {
            string str = Convert.ToBase64String(value);

            str += "(";

            for (int i = 0; i < value.Length; i++)
            {
                str += value[i].ToString("X2");
            }

            str += ")";

            AddField(grid, caption, str);
        }

        #endregion

        #region AddControl

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        public static RowDefinition AddControl(Grid grid, Control control)
        {
            RowDefinition row = new RowDefinition();

            grid.RowDefinitions.Add(row);

            Grid.SetColumn(control, 0);
            Grid.SetRow(control, grid.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(control, 2);
            grid.Children.Add(control);

            return row;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="control"></param>
        /// <param name="brush"></param>
        public static void AddControl(Grid grid, Control control, Brush brush)
        {
            grid.RowDefinitions.Add(new RowDefinition());

            Grid.SetColumn(control, 0);
            Grid.SetRow(control, grid.RowDefinitions.Count - 1);
            Grid.SetColumnSpan(control, 2);
            grid.Children.Add(control);

            control.Background = brush;
        }

        #endregion

        #region AddHeader

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        public static void AddHeader(Grid grid, string text)
        {
            Label label = new Label();
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.Content = text;

            AddControl(grid, label, Brushes.LightBlue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="text"></param>
        public static void AddSubHeader(Grid grid, string text)
        {
            Label label = new Label();
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.Content = text;

            AddControl(grid, label, Brushes.LightGray);
        }

        #endregion

        #endregion
    }
}
