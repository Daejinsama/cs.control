using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace com.outlook_styner07.cs.control.Charting
{
    public class DjsmCustomStyleSeries : Series
    {
        public void ApplyStyle(SeriesStyleObject style)
        {
            Color = Color.FromArgb(style.LineColor);
            BorderWidth = style.LineWidth;
            BorderDashStyle = style.LineStyle;

            MarkerStyle = style.MarkerStyle;
            MarkerStep = style.MarkerStep;
            MarkerSize = style.MarkerSize;
            MarkerColor = Color.FromArgb(style.MarkerColor);
            MarkerBorderColor = Color.FromArgb(style.MarkerBorderColor);
            MarkerBorderWidth = style.MarkerBorderWidth;

            StyleUpdated?.Invoke(this, null);

            LatestBorderWidth = style.LineWidth;
        }

        public SeriesStyleObject ExtractStyle()
        {
            return new SeriesStyleObject
            {
                LineColor = Color.ToArgb(),
                LineStyle = BorderDashStyle,
                LineWidth = BorderWidth,
                MarkerBorderColor = MarkerBorderColor.ToArgb(),
                MarkerBorderWidth = MarkerBorderWidth,
                MarkerColor = MarkerColor.ToArgb(),
                MarkerSize = MarkerSize,
                MarkerStep = MarkerStep,
                MarkerStyle = MarkerStyle
            };
        }

        public event EventHandler StyleUpdated;

        public new Color Color
        {
            get { return Color.FromArgb(_color); }
            set
            {
                _color = value.ToArgb();
                base.Color = value;
            }
        }
        private int _color = Color.Empty.ToArgb();

        public int LatestBorderWidth { get; set; } = 1;
    }
}
