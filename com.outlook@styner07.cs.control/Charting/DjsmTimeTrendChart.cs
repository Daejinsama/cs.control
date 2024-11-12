using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace com.outlook_styner07.cs.control.Charting
{
    public class DjsmTimeTrendChart : DjsmChart
    {
        //public const string USER_DEFINED_MY_STRIPLINE = "myStripLIne";
        //public const string USER_DEFINED_MY_LEGEND = "myLegend";
        public const string USER_DEFINED_MY_REFERENCE = "myReference";

        private const string PROPERTY_NAME_ACTIVE = "ACTIVE";
        private const string ACTIVE_SYMBOL = "v";
        private const string DEACTIVE_SYMBOL = "-";

        private const string X_AXIS_LABEL = "Time(sec)";
        private const string Y_AXIS_LABEL = "Intensity";

        private const int X_AXIS_MINIMUM = 0;
        private const int X_AXIS_MAXIMUM = 60;

        private const int Y_AXIS_MINIMUM = 0;
        private const int Y_AXIS_MAXIMUM = 65535;

        private const int MAXIMUM_VISIBLE_TIME_RANGE = 600;//100; unit: sec
        private const int MAXIMUM_POINT_COUNT = 30000;//30000;

        public DjsmTimeTrendChart()
        {
            SetXAxisLabel(X_AXIS_LABEL);
            SetYAxisLabel(Y_AXIS_LABEL);
            //SetXAxisRange(X_AXIS_MINIMUM, MAXIMUM_POINT_COUNT);
            SetYAxisRange(Y_AXIS_MINIMUM, Y_AXIS_MAXIMUM);

            SetZoomEnabled(true);

            InitializeLegend();

            BackgroundColorChanged += (object sender, BackgroundColorChangeEventArgs e) =>
            {
                AreaMain.BackColor = e.Color;
            };

            ContextMenuEnabled = true;
        }

        private LegendCellColumn colCheck, colColor, colName, colValue;
        private Legend legendEquation;

        private void InitializeLegend()
        {
            Font headerFont = new Font("Arial", 9f, FontStyle.Bold);
            legendEquation = AddLegend("Equation", 0);
            /// Main Legend
            colCheck = new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.Text,
                HeaderFont = headerFont,
                Name = "Active",
                Text = string.Format("#CUSTOMPROPERTY({0})", PROPERTY_NAME_ACTIVE),
            };
            legendEquation.CellColumns.Add(colCheck);

            colColor = new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.SeriesSymbol,
                HeaderFont = headerFont,
                Name = "Color"
            };
            legendEquation.CellColumns.Add(colColor);

            colName = new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.Text,
                HeaderFont = headerFont,
                HeaderText = "Name",
                Name = "Name",
                Text = "#SERIESNAME",
                Alignment = ContentAlignment.MiddleRight
            };
            legendEquation.CellColumns.Add(colName);

            colValue = new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.Text,
                HeaderFont = headerFont,
                HeaderText = "Value",
                Name = "Value",
                Text = "#LAST{N3}"
            };
            legendEquation.CellColumns.Add(colValue);


            legendEquation.LegendStyle = LegendStyle.Table;
            legendEquation.HeaderSeparator = LegendSeparatorStyle.Line;
            legendEquation.HeaderSeparatorColor = Color.Gray;
        }

        public void HideValueInLegend()
        {
            if (legendEquation != null && legendEquation.CellColumns.Contains(colValue))
            {
                legendEquation.CellColumns.Remove(colValue);
            }
        }

        public void HideCheckInLegend()
        {
            if (legendEquation != null && legendEquation.CellColumns.Contains(colCheck))
            {
                legendEquation.CellColumns.Remove(colCheck);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            HitTestResult hitResult = HitTest(e.X, e.Y);
            if (hitResult != null && hitResult.Object != null)
            {
                if (hitResult.Object is LegendItem)
                {
                    var item = hitResult.Object as LegendItem;
                    DjsmCustomStyleSeries relatedSeries = Series[item.Name] as DjsmCustomStyleSeries;

                    if (e.Button == MouseButtons.Left)
                    {
                        relatedSeries.BorderWidth = relatedSeries.BorderWidth == 0 ? relatedSeries.LatestBorderWidth : 0;
                        relatedSeries.SetCustomProperty(PROPERTY_NAME_ACTIVE, relatedSeries.BorderWidth == 0 ? DEACTIVE_SYMBOL : ACTIVE_SYMBOL);
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        SeriesCustomizingDialog dlgCustom = new SeriesCustomizingDialog(relatedSeries);
                        dlgCustom.Location = new Point(MousePosition.X - dlgCustom.Width, MousePosition.Y);

                        if (dlgCustom.ShowDialog() == DialogResult.OK)
                        {
                            relatedSeries.ApplyStyle(dlgCustom.GetSeriesStyle());
                        }
                    }
                }
                else
                {
                    //base.OnMouseClick(e);
                }
            }
        }

        public DjsmCustomStyleSeries AddFastLineSeries(string name)
        {
            if (Series.FindByName(name) != null)
            {
                return Series.FindByName(name) as DjsmCustomStyleSeries;
            }

            DjsmCustomStyleSeries s1 = new DjsmCustomStyleSeries
            {
                ChartType = SeriesChartType.FastLine,// fastline is cannot hide;
                YValueType = ChartValueType.Double,
                BorderWidth = 1,
                Name = name,
                //ToolTip = name,
            };

            s1.SetCustomProperty(PROPERTY_NAME_ACTIVE, ACTIVE_SYMBOL);
            Series.Add(s1);
            return s1;
        }

        public DjsmCustomStyleSeries AddSeries(string name)
        {
            if (Series.FindByName(name) != null)
            {
                return Series.FindByName(name) as DjsmCustomStyleSeries;
            }

            DjsmCustomStyleSeries s1 = new DjsmCustomStyleSeries
            {
                ChartType = SeriesChartType.Line,// fastline is cannot hide;
                YValueType = ChartValueType.Double,
                BorderWidth = 1,
                Name = name,
                //ToolTip = name,
            };
            s1.SetCustomProperty(PROPERTY_NAME_ACTIVE, ACTIVE_SYMBOL);
            Series.Add(s1);
            return s1;
        }

        public void AddPointWithShift(double x, double y, Series series)
        {
            /// 제한값 이상 포인트 저장시 올드값 부터 삭제
            int count = series.Points.Count;
            if (count > MAXIMUM_POINT_COUNT)
            {
                AddPoint(series, x, y);
                RemovePointAtFirst(series);
            }
            else
            {
                AddPoint(series, x, y);
            }

            if (x > MAXIMUM_VISIBLE_TIME_RANGE
                && x - MAXIMUM_VISIBLE_TIME_RANGE > XAxis.Minimum)
            {
                SetXAxisRange(x - MAXIMUM_VISIBLE_TIME_RANGE, x);
            }
        }

        public void ResetXAxis()
        {
            XAxis.Minimum = 0;
        }
    }
}
