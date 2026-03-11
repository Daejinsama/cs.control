using System.Windows.Forms.DataVisualization.Charting;

namespace com.outlook_styner07.cs.control.Charting
{
    public class DjsmTimeTrendChart : DjsmChart
    {
        #region Constructors
        public DjsmTimeTrendChart()
        {
            SetXAxisLabel(X_AXIS_LABEL);
            SetYAxisLabel(Y_AXIS_LABEL);
            SetYAxisRange(Y_AXIS_MINIMUM, Y_AXIS_MAXIMUM);

            SetZoomEnabled(true);

            InitializeLegend();

            BackgroundColorChanged += (sender, e) =>
            {
                AreaMain.BackColor = e.Color;
            };

            ContextMenuEnabled = true;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
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

        private LegendCellColumn _colCheck;
        private LegendCellColumn _colColor;
        private LegendCellColumn _colName;
        private LegendCellColumn _colValue;
        private Legend _legendEquation;
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void InitializeLegend()
        {
            Font headerFont = new Font("Arial", 9f, FontStyle.Bold);
            _legendEquation = AddLegend("Equation", 0);
            /// Main Legend
            _colCheck = new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.Text,
                HeaderFont = headerFont,
                Name = "Active",
                Text = string.Format("#CUSTOMPROPERTY({0})", PROPERTY_NAME_ACTIVE),
            };
            _legendEquation.CellColumns.Add(_colCheck);

            _colColor = new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.SeriesSymbol,
                HeaderFont = headerFont,
                Name = "Color"
            };
            _legendEquation.CellColumns.Add(_colColor);

            _colName = new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.Text,
                HeaderFont = headerFont,
                HeaderText = "Name",
                Name = "Name",
                Text = "#SERIESNAME",
                Alignment = ContentAlignment.MiddleRight
            };
            _legendEquation.CellColumns.Add(_colName);

            _colValue = new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.Text,
                HeaderFont = headerFont,
                HeaderText = "Value",
                Name = "Value",
                Text = "#LAST{N3}"
            };
            _legendEquation.CellColumns.Add(_colValue);


            _legendEquation.LegendStyle = LegendStyle.Table;
            _legendEquation.HeaderSeparator = LegendSeparatorStyle.Line;
            _legendEquation.HeaderSeparatorColor = Color.Gray;
        }

        public void HideValueInLegend()
        {
            if (_legendEquation != null && _legendEquation.CellColumns.Contains(_colValue))
            {
                _legendEquation.CellColumns.Remove(_colValue);
            }
        }

        public void HideCheckInLegend()
        {
            if (_legendEquation != null && _legendEquation.CellColumns.Contains(_colCheck))
            {
                _legendEquation.CellColumns.Remove(_colCheck);
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
                    DjsmCustomStyleSeries relatedSeries = (DjsmCustomStyleSeries)Series[item.Name];

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
                return (DjsmCustomStyleSeries)Series.FindByName(name);
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
                return (DjsmCustomStyleSeries)Series.FindByName(name);
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
        #endregion
    }
}
