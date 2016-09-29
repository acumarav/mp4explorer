//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mp4Explorer
{
    /// <summary>
    /// 
    /// </summary>
    public class HexView : Control
    {
        #region Consts

        /// <summary>
        /// 
        /// </summary>
        private const string fontFamilyName = "GlobalMonospace.CompositeFont, Courier New, Courier, Lucida Console";

        #endregion

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private int byteCount = 0;

        /// <summary>
        /// 
        /// </summary>
        private const int lineLength = 74;

        /// <summary>
        /// 
        /// </summary>
        private Size charSize;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        static HexView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HexView), new FrameworkPropertyMetadata(typeof(HexView)));

            DataProperty = DependencyProperty.Register("Data", typeof(IEnumerable<byte>), typeof(HexView),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(OnDataChanged)));
            FontFamilyProperty = Control.FontFamilyProperty.AddOwner(typeof(HexView),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnFontFamilyChanged),
                    new CoerceValueCallback(OnFontFamilyCoerce)));
            FontSizeProperty = Control.FontSizeProperty.AddOwner(typeof(HexView));
            FontWeightProperty = Control.FontWeightProperty.AddOwner(typeof(HexView));
        }

        #endregion

        #region Dependency properties

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DataProperty;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<byte> Data
        {
            get { return (IEnumerable<byte>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HexView ths = d as HexView;

            IEnumerable<byte> data = ths.Data;
            if (data == null)
                ths.byteCount = 0;
            else
            {
                ICollection<byte> coll = data as ICollection<byte>;
                if (coll != null)
                    ths.byteCount = coll.Count;
                else
                {
                    IEnumerator<byte> en = data.GetEnumerator();
                    ths.byteCount = 0;
                    while (en.MoveNext())
                        ++ths.byteCount;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public new static readonly DependencyProperty FontFamilyProperty;

        /// <summary>
        /// 
        /// </summary>
        public new FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnFontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        private static object OnFontFamilyCoerce(DependencyObject d, Object baseValue)
        {
            HexView hv = d as HexView;
            FontFamily ff = baseValue as FontFamily;
            if (hv.IsFixedPitch(ff))
                return baseValue;
            else
                return new FontFamily(fontFamilyName);
        }

        /// <summary>
        /// 
        /// </summary>
        public new static readonly DependencyProperty FontSizeProperty;

        /// <summary>
        /// 
        /// </summary>
        public new double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public new static readonly DependencyProperty FontWeightProperty;

        /// <summary>
        /// 
        /// </summary>
        public new FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }

        #endregion Dependency properties

        #region Protected methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            if (!IsFixedPitch(FontFamily))
                FontFamily = new FontFamily(fontFamilyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizeAvailable"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            if (Data == null)
                return new Size(4, 4);

            int lineCount = byteCount / 16;
            if (byteCount % 16 > 0)
                lineCount++;

            Typeface tf = new Typeface(FontFamily, FontStyles.Normal, FontWeight, FontStretches.Normal);
            FormattedText txt = new FormattedText("M",
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                tf,
                FontSize,
                Foreground);

            charSize = new Size(txt.Width, txt.Height);

            double width = txt.Width * lineLength + Padding.Left + Padding.Right;
            double height = txt.Height * lineCount + Padding.Top + Padding.Bottom;

            return new Size(width + 4, height + 4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(DrawingContext dc)
        {
            object objBackgroundBrush = ReadLocalValue(BackgroundProperty);

            if (!object.ReferenceEquals(objBackgroundBrush, DependencyProperty.UnsetValue))
            {
                dc.DrawRectangle(objBackgroundBrush as Brush, null, new Rect(new Point(0, 0), RenderSize));
            }

            if (Data == null)
                return;

            int lineCount = byteCount / 16;
            if (byteCount % 16 > 0)
                lineCount++;

            IEnumerator<byte> en = Data.GetEnumerator();
            double y = Padding.Top;
            Typeface tf = new Typeface(FontFamily, FontStyles.Normal, FontWeight, FontStretches.Normal);
            for (int i = 0; i < lineCount; ++i)
            {
                FormattedText txt = new FormattedText(NextHexLine(i, en),
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    tf,
                    FontSize,
                    Foreground);

                dc.DrawText(txt, new Point(Padding.Left, (int)Math.Ceiling(y)));
                y += txt.Height;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineIndex"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        private string NextHexLine(int lineIndex, IEnumerator<byte> en)
        {
            StringBuilder sbHex = new StringBuilder(48);
            StringBuilder sbChar = new StringBuilder(16);

            int i = 0;

            while (i < 16 && en.MoveNext())
            {
                sbHex.Append(String.Format("{0:X2}", en.Current));
                sbHex.Append(i == 7 ? "-" : " ");
                
                char ch = Convert.ToChar(en.Current);
                sbChar.Append(Char.IsControl(ch) ? "." : ch.ToString());

                ++i;
            }

            if (i == 0)
                return string.Empty;

            int first = lineIndex * 16;

            StringBuilder sb = new StringBuilder(String.Format("{0:X4}-{1:X4} ", first, first + i - 1));

            for (; i < 16; ++i)
            {
                sbHex.Append("   ");
                sbChar.Append(" ");
            }

            sb.Append(sbHex);
            sb.Append(sbChar);

            Debug.Assert(sb.Length == lineLength);

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ff"></param>
        /// <returns></returns>
        private bool IsFixedPitch(FontFamily fontFamily)
        {
            Typeface tf = new Typeface(fontFamily, FontStyles.Normal, FontWeight, FontStretches.Normal);
            FormattedText W = new FormattedText("W", CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight, tf, FontSize, Foreground);
            FormattedText I = new FormattedText("I", CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight, tf, FontSize, Foreground);

            return ((I.Width == W.Width) && (I.Height == W.Height));
        }

        #endregion
    }
}
