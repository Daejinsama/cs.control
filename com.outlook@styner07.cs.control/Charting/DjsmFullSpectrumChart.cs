using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace com.outlook_styner07.cs.control.Charting
{
    public class DjsmFullSpectrumChart : DjsmChart
    {
        public const string FULL_SPECTRUM_SERIES_NAME = "Full Spectrum";

        private const string X_AXIS_LABEL = "Wavelength";
        private const string Y_AXIS_LABEL = "Intensity";

        private const int X_AXIS_MINIMUM = 200;
        private const int X_AXIS_MAXIMUM = 850;

        private const int Y_AXIS_MINIMUM = 0;
        private const int Y_AXIS_MAXIMUM = 65535;

        public DjsmFullSpectrumChart()
        {
            SetXAxisLabel(X_AXIS_LABEL);
            SetYAxisLabel(Y_AXIS_LABEL);
            SetXAxisRange(X_AXIS_MINIMUM, X_AXIS_MAXIMUM);
            SetYAxisRange(Y_AXIS_MINIMUM, Y_AXIS_MAXIMUM);
            SetZoomEnabled(true);

            XAxis.LabelStyle.Format = "0.0";

            BackgroundColorChanged += (object sender, BackgroundColorChangeEventArgs e) =>
            {
                AreaMain.BackColor = e.Color;
            };

            ContextMenuEnabled = true;
            /// contextmenuenabled 에서 context menu 초기화
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            ToolStripItem ctxMnuChart_Style_Line = new ToolStripMenuItem("Line");
            ctxMnuChart_Style_Line.Click += delegate
            {
                for (int len = Series.Count, i = 0; i < len; i++)
                {
                    Series[i].ChartType = SeriesChartType.Line;
                }
            };

            ToolStripItem ctxMnuChart_Style_Area = new ToolStripMenuItem("Area");
            ctxMnuChart_Style_Area.Click += delegate
            {
                for (int len = Series.Count, i = 0; i < len; i++)
                {
                    Series[i].ChartType = SeriesChartType.Area;
                }
            };

            ToolStripItem ctxMnuChart_Style_Bar = new ToolStripMenuItem("Bar");
            ctxMnuChart_Style_Bar.Click += delegate
            {
                for (int len = Series.Count, i = 0; i < len; i++)
                {
                    Series[i].ChartType = SeriesChartType.Column;
                }
            };

            ToolStripMenuItem ctxMnuChart_Style = new ToolStripMenuItem("Graph Style");
            ctxMnuChart_Style.DropDownItems.AddRange(new ToolStripItem[] { ctxMnuChart_Style_Line, ctxMnuChart_Style_Area, ctxMnuChart_Style_Bar });
            ctxMnuChart.Items.Insert(0, ctxMnuChart_Style);
        }

        private Font legendheaderFont = new Font("Arial", 9f, FontStyle.Bold);
        private Legend legendFullSpectrum, legendRegion;

        public void AddRegionLegend()
        {
            if (legendRegion == null)
            {
                legendRegion = AddLegend("Region", 0);
                legendRegion.CellColumns.Add(new LegendCellColumn
                {
                    ColumnType = LegendCellColumnType.SeriesSymbol,
                    HeaderText = string.Empty,
                    HeaderFont = legendheaderFont,
                    Name = "Color",
                });

                legendRegion.CellColumns.Add(new LegendCellColumn
                {
                    ColumnType = LegendCellColumnType.Text,
                    HeaderText = "Name",
                    HeaderFont = legendheaderFont,
                    Name = "Name",
                });

                legendRegion.CellColumns.Add(new LegendCellColumn
                {
                    ColumnType = LegendCellColumnType.Text,
                    HeaderText = "Region",
                    HeaderFont = legendheaderFont,
                    Name = "Region",
                });

                legendRegion.LegendStyle = LegendStyle.Table;
                legendRegion.HeaderSeparator = LegendSeparatorStyle.Line;
                legendRegion.HeaderSeparatorColor = Color.Gray;
            }
            else
            {
                if (!Legends.Contains(legendRegion))
                {
                    Legends.Insert(0, legendRegion);
                }
            }
        }

        public void RemoveRegionLegend()
        {
            if (legendRegion != null && Legends.Contains(legendRegion))
            {
                Legends.Remove(legendRegion);
            }
        }

        public void AddFullSpectrumLegend()
        {

            if (legendFullSpectrum == null)
            {
                legendFullSpectrum = AddLegend("Full Spectrum", 1);

                legendFullSpectrum.CellColumns.Add(new LegendCellColumn
                {
                    ColumnType = LegendCellColumnType.SeriesSymbol,
                    HeaderText = string.Empty,
                    HeaderFont = legendheaderFont,
                    Name = "Color",

                });

                legendFullSpectrum.CellColumns.Add(new LegendCellColumn
                {
                    ColumnType = LegendCellColumnType.Text,
                    HeaderText = "Name",
                    HeaderFont = legendheaderFont,
                    Name = "Name"
                });

                legendFullSpectrum.CellColumns.Add(new LegendCellColumn
                {
                    ColumnType = LegendCellColumnType.Text,
                    HeaderText = "Min",
                    HeaderFont = legendheaderFont,
                    Name = "Min",
                    Text = "#MIN{N2}"
                });

                legendFullSpectrum.CellColumns.Add(new LegendCellColumn
                {
                    ColumnType = LegendCellColumnType.Text,
                    HeaderText = "Max",
                    HeaderFont = legendheaderFont,
                    Name = "Max",
                    Text = "#MAX{N2}"
                });

                legendFullSpectrum.CellColumns.Add(new LegendCellColumn
                {
                    ColumnType = LegendCellColumnType.Text,
                    HeaderText = "Avg",
                    HeaderFont = legendheaderFont,
                    Name = "Avg",
                    Text = "#AVG{N2}"
                });
            }
            else
            {
                if (!Legends.Contains(legendFullSpectrum))
                {
                    Legends.Add(legendFullSpectrum);
                }
            }
        }

        public LegendItem AddSvLegend(Color color, string name, string region)
        {
            LegendItem item = new LegendItem
            {
                Color = color,
            };
            item.Cells.Add(LegendCellType.SeriesSymbol, string.Empty, ContentAlignment.MiddleCenter);
            item.Cells.Add(LegendCellType.Text, name, ContentAlignment.MiddleCenter);
            item.Cells.Add(LegendCellType.Text, region, ContentAlignment.MiddleCenter);

            legendRegion.CustomItems.Add(item);
            return item;
        }

        public void ClearRegionLegend()
        {
            legendRegion.CustomItems.Clear();
        }

        public Series AddSeries(string name)
        {
            Series s1 = new Series
            {
                ChartType = SeriesChartType.FastLine,
                XValueType = ChartValueType.Double,
                YValueType = ChartValueType.Int32,
                BorderWidth = 1,
                Name = name,
                Legend = legendFullSpectrum.Name,
                //ToolTip = name,
            };

            Series.Add(s1);
            return s1;
        }

        public Series AddReferenceSpectrum(string name)
        {
            Series s1 = new Series
            {
                ChartType = SeriesChartType.FastLine,
                XValueType = ChartValueType.Double,
                YValueType = ChartValueType.Int32,
                BorderWidth = 1,
                Name = name,
                BorderDashStyle = ChartDashStyle.Dash,
                Legend = legendFullSpectrum.Name,
                //ToolTip = name,
            };

            Series.Add(s1);
            return s1;
        }

        /// <summary>
        /// SV레퍼런스 스펙트럼 제거
        /// </summary>
        public void ClearReferenceSpectrum()
        {
            for (int len = Series.Count, i = 0; i < len; i++)
            {
                if (!Series[i].Name.Equals(FULL_SPECTRUM_SERIES_NAME)
                    && !Series[i].Name.Equals("Dark Current"))
                {
                    Series.Remove(Series[i]);
                }
            }
        }

        private bool _referenceSpectrumVisible;
        public bool ReferenceSpectrumVisible
        {
            get { return _referenceSpectrumVisible; }
            set
            {
                _referenceSpectrumVisible = value;

                for (int len = Series.Count, i = 0; i < len; i++)
                {
                    if (!Series[i].Name.Equals(FULL_SPECTRUM_SERIES_NAME))
                    {
                        Series[i].Enabled = _referenceSpectrumVisible;
                    }
                }
            }
        }

        private bool _stripLineVisible;
        public bool StripLineVisible
        {
            get { return _stripLineVisible; }
            set
            {
                _stripLineVisible = value;
                for (int len = XAxis.StripLines.Count, i = 0; i < len; i++)
                {
                    StripLine s = XAxis.StripLines[i];

                    if (s.BackSecondaryColor != Color.Transparent && s.BackColor != Color.Transparent)
                    {
                        s.Tag = new Color[] { s.BackColor, s.BackSecondaryColor };
                    }

                    s.BackColor = _stripLineVisible ? (s.Tag as Color[])[0] : Color.Transparent;
                    s.BackSecondaryColor = _stripLineVisible ? (s.Tag as Color[])[1] : Color.Transparent;
                }
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
