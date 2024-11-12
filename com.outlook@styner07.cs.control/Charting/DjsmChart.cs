using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace com.outlook_styner07.cs.control.Charting
{
    public class DjsmChart : Chart
    {
        public const string EXT_CHART_IMAGE = "jpg";

        public static readonly Font DEFAULT_TITLE_FONT = new Font("Arial", 9f, FontStyle.Bold);
        public static readonly Font DEFAULT_LABEL_FONT = new Font("Arial", 7f, FontStyle.Bold);
        public static readonly Font LARGE_LABEL_FONT = new Font("Arial", 11f, FontStyle.Bold);

        public static readonly Font BOUNDARY_TEXT_FONT = new Font("Arial", 8f, FontStyle.Bold);

        public event EventHandler<XYCursorEventArgs> XYCursorPositionChanged;

        public class XYCursorEventArgs : EventArgs
        {
            public int XValueIndex { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
        }

        public static event EventHandler<BackgroundColorChangeEventArgs> BackgroundColorChanged;

        public class BackgroundColorChangeEventArgs : EventArgs
        {
            public Color Color { get; set; }
        }

        private bool _contextMenuEnabeld = false;
        public bool ContextMenuEnabled
        {
            get
            {
                return _contextMenuEnabeld;
            }
            set
            {
                _contextMenuEnabeld = value;
                if (value && ctxMnuChart == null)
                {
                    InitializeContextMenu();
                }
            }
        }

        private PointF moveInitPosition = new PointF();
        private PointF moveCurrentPosition = new PointF();


        protected double fixedXAxisMinimum;
        protected double fixedXAxisMaximum;

        protected double fixedXAxis2Minimum;
        protected double fixedXAxis2Maximum;

        protected double fixedYAxisMinimum;
        protected double fixedYAxisMaximum;

        protected double fixedYAxis2Minimum;
        protected double fixedYAxis2Maximum;

        private int cursorXValueIndex;

        private bool panProcessed = false;

        private Series cursorTargetSeries = null;
        private double[] cursorTargetSeriesXValues = null;

        private bool isAutoScaled = false;

        private bool zoomAllXAxis = false;
        private bool zoomAllYAxis = false;

        public bool IsZoomEnabled { get; private set; } = false;

        public string CursorXValueDateTimeFormat { get; set; } = null;

        public int XAxisScrollResolution { get; set; }
        public int YAxisScrollResolution { get; set; }

        public ChartArea AreaMain
        {
            get
            {
                if (ChartAreas.Count == 0)
                {
                    ChartAreas.Add(new ChartArea());
                }

                return ChartAreas[0];
            }
        }
        public Axis XAxis { get; set; }
        public Axis XAxis2 { get; set; }
        public Axis YAxis { get; set; }
        public Axis YAxis2 { get; set; }

        public ContextMenuStrip ctxMnuChart;

        public DjsmChart()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Opaque, true);

            AntiAliasing = AntiAliasingStyles.None;
            TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            IsSoftShadows = false;

            Dock = DockStyle.Fill;

            XAxisScrollResolution = 80;
            YAxisScrollResolution = 80;

            InitializeArea();
            InitializeXAxis();
            InitializeXAxis2();
            InitializeYAxis();
            InitializeYAxis2();

            AxisViewChanged += (sender, e) =>
            {
                if (zoomAllXAxis && e.Axis == XAxis)
                {
                    if (e.Axis.ScaleView.IsZoomed)
                    {
                        double scaleRatio = (XAxis2.Maximum - XAxis2.Minimum) / (XAxis.Maximum - XAxis.Minimum);

                        double pos = XAxis2.Minimum + XAxis.ScaleView.ViewMinimum * scaleRatio;
                        double size = XAxis2.Minimum + XAxis.ScaleView.ViewMaximum * scaleRatio - pos;

                        XAxis2.ScaleView.Zoom(pos, size, XAxis2.IntervalType, true);
                    }
                }

                if (zoomAllYAxis && e.Axis == YAxis)
                {
                    if (e.Axis.ScaleView.IsZoomed)
                    {
                        double scaleRatio = (YAxis2.Maximum - YAxis2.Minimum) / (YAxis.Maximum - YAxis.Minimum);

                        double pos = YAxis2.Minimum + YAxis.ScaleView.ViewMinimum * scaleRatio;
                        double size = YAxis2.Minimum + YAxis.ScaleView.ViewMaximum * scaleRatio;
                        
                        YAxis2.ScaleView.Zoom(pos, size, YAxis2.IntervalType, true);
                    }
                }
            };
        }

        private void InitializeArea()
        {
            AreaMain.Position.X = 0;
            AreaMain.Position.Y = 0;
            AreaMain.Position.Width = 99;
            AreaMain.Position.Height = 100;
            AreaMain.InnerPlotPosition.Auto = true;
            AreaMain.IsSameFontSizeForAllAxes = true;
            AreaMain.BackColor = Properties.Settings.Default.Background;
            AreaMain.AlignmentStyle = AreaAlignmentStyles.PlotPosition | AreaAlignmentStyles.AxesView;

            AreaMain.CursorX.IntervalType = DateTimeIntervalType.Milliseconds;
            AreaMain.CursorX.IntervalOffsetType = DateTimeIntervalType.Milliseconds;
            AreaMain.CursorX.IsUserEnabled = false;
            /// 줌 동작 및 스케일 개선에 필수
            AreaMain.CursorX.Interval = 0.01;

            AreaMain.CursorY.IntervalType = DateTimeIntervalType.Number;
            AreaMain.CursorY.IntervalOffsetType = DateTimeIntervalType.Number;
            AreaMain.CursorY.IsUserEnabled = false;
            AreaMain.CursorY.Interval = 0.01;
        }

        private void InitializeXAxis()
        {
            XAxis = AreaMain.AxisX;
            XAxis.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;
            XAxis.Title = "wavelength";
            XAxis.Enabled = AxisEnabled.True;
            XAxis.IntervalAutoMode = IntervalAutoMode.VariableCount;  /// error in 64bit debugging
            XAxis.MajorGrid.Enabled = false;
            XAxis.MajorTickMark.Enabled = false;
            XAxis.LabelStyle.Font = Properties.Settings.Default.LargeFont ? LARGE_LABEL_FONT : DEFAULT_LABEL_FONT;
            XAxis.LabelStyle.IsEndLabelVisible = true;
            XAxis.IsMarginVisible = false;
            //XAxis.IsLabelAutoFit = true;
            XAxis.ScrollBar.Enabled = false;

            /// zoom 동작시 x axis label 사라지는 현상 보완
            //XAxis.ScaleView.MinSize = 0.1;
        }

        private void InitializeXAxis2()
        {
            XAxis2 = AreaMain.AxisX2;
            XAxis2.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;
            XAxis2.Title = "wavelength";
            XAxis2.Enabled = AxisEnabled.False;
            XAxis2.IntervalAutoMode = IntervalAutoMode.VariableCount;  /// error in 64bit debugging
            XAxis2.MajorGrid.Enabled = false;
            XAxis2.MajorTickMark.Enabled = false;
            XAxis2.LabelStyle.Font = Properties.Settings.Default.LargeFont ? LARGE_LABEL_FONT : DEFAULT_LABEL_FONT;
            XAxis2.LabelStyle.IsEndLabelVisible = true;
            XAxis2.IsMarginVisible = false;
            //XAxis2.IsLabelAutoFit = true;
            XAxis2.ScrollBar.Enabled = false;
        }

        private void InitializeYAxis()
        {
            YAxis = AreaMain.AxisY;
            YAxis.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;
            YAxis.Title = "Intensity";
            YAxis.Enabled = AxisEnabled.True;
            YAxis.MajorGrid.Enabled = false;
            YAxis.IntervalAutoMode = IntervalAutoMode.VariableCount;
            YAxis.MajorTickMark.Enabled = false;
            YAxis.LabelStyle.Font = Properties.Settings.Default.LargeFont ? LARGE_LABEL_FONT : DEFAULT_LABEL_FONT;
            YAxis.IsMarginVisible = false;
            //YAxis.IsLabelAutoFit = true;
            YAxis.ScrollBar.Enabled = false;
            YAxis.IsStartedFromZero = false;
            YAxis.LabelStyle.Format = "0";
        }

        private void InitializeYAxis2()
        {
            YAxis2 = AreaMain.AxisY2;
            YAxis2.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;
            YAxis2.Title = "Intensity";
            YAxis2.Enabled = AxisEnabled.False;
            YAxis2.MajorGrid.Enabled = false;
            YAxis2.IntervalAutoMode = IntervalAutoMode.VariableCount;
            YAxis2.MajorTickMark.Enabled = false;
            YAxis2.LabelStyle.Font = Properties.Settings.Default.LargeFont ? LARGE_LABEL_FONT : DEFAULT_LABEL_FONT;
            YAxis2.IsMarginVisible = false;
            //YAxis2.IsLabelAutoFit = true;
            YAxis2.ScrollBar.Enabled = false;
        }

        public bool ContextMenuEnabledBackground { get; set; } = true;
        public bool ContextMenuEnabledSave { get; set; } = true;
        public bool ContextMenuEnabledClipboard { get; set; } = true;
        public bool ContextMenuEnabledLegend { get; set; } = true;
        public bool ContextMenuEnabledZoom { get; set; } = true;
        public bool ContextMenuEnabledAutoScale { get; set; } = true;

        private void InitializeContextMenu()
        {
            const string MENU_BACKGROUND = "ctxMnuChart_BackgroundColor";

            const string MENU_SAVE_PATH = "ctxMnuChart_SavePath";
            const string MENU_SAVE = "ctxMnuChart_Save";

            const string MENU_COPY_TO_CLIPBOARD = "ctxMnuChart_CopyToClipboard";

            const string MENU_ZOOM_ENABLED = "ctxMnuChart_ZoomEnabled";
            const string MENU_RESET_ZOOM = "ctxMnuChart_ResetZoom";
            const string MENU_AUTO_SCALE = "ctxMnuChart_AutoScale";

            ctxMnuChart = new ContextMenuStrip { AutoSize = true, Font = new Font("Arial", 9f, FontStyle.Regular), BackColor = SystemColors.Control, ShowItemToolTips = true };

            ctxMnuChart.Opening += (object sender, CancelEventArgs e) =>
            {
                ctxMnuChart.Items.Clear();

                if (ContextMenuEnabledBackground)
                {
                    ctxMnuChart.Items.Add(new ToolStripMenuItem { Name = MENU_BACKGROUND, Text = "Background Color", AutoSize = true });

                    ctxMnuChart.Items.Add(new ToolStripSeparator());
                }

                if (ContextMenuEnabledSave)
                {
                    ctxMnuChart.Items.Add(new ToolStripMenuItem { Name = MENU_SAVE_PATH, Text = "Save Path", AutoSize = true, ToolTipText = Properties.Settings.Default.SavePath, AutoToolTip = true });
                    ctxMnuChart.Items.Add(new ToolStripMenuItem { Name = MENU_SAVE, Text = "Save", AutoSize = true });

                    ctxMnuChart.Items.Add(new ToolStripSeparator());
                }

                if (ContextMenuEnabledClipboard)
                {
                    ctxMnuChart.Items.Add(new ToolStripMenuItem { Name = MENU_COPY_TO_CLIPBOARD, Text = "Copy to Clipboard", AutoSize = true });
                    ctxMnuChart.Items.Add(new ToolStripSeparator());
                }

                if (ContextMenuEnabledLegend && Legends.Count > 0)
                {
                    string[] MENU_LEGENDS = new string[Legends.Count];

                    for (int len = Legends.Count, i = 0; i < len; i++)
                    {
                        MENU_LEGENDS[i] = string.Format("ctxMnuChart_Legend_{0}", Legends[i].Name);
                        if (!ctxMnuChart.Items.ContainsKey(MENU_LEGENDS[i]))
                        {
                            ctxMnuChart.Items.Add(new ToolStripMenuItem
                            {
                                Name = MENU_LEGENDS[i],
                                Text = string.Format("Legned \"{0}\"", Legends[i].Name),
                                AutoSize = true,
                                CheckOnClick = true,
                                Checked = Legends[i].Enabled,
                                Tag = Legends[i].Name
                            });
                        }
                    }
                    ctxMnuChart.Items.Add(new ToolStripSeparator());
                }

                if (ContextMenuEnabledZoom)
                {
                    ctxMnuChart.Items.Add(new ToolStripMenuItem
                    {
                        Name = MENU_ZOOM_ENABLED,
                        Text = "Enable Zoom",
                        AutoSize = true,
                        CheckOnClick = true,
                        Checked = IsZoomEnabled
                    });
                    ctxMnuChart.Items.Add(new ToolStripMenuItem { Name = MENU_RESET_ZOOM, Text = "Reset Zoom", AutoSize = true });
                }

                if (ContextMenuEnabledAutoScale)
                {
                    ctxMnuChart.Items.Add(new ToolStripMenuItem
                    {
                        Name = MENU_AUTO_SCALE,
                        Text = "Auto Scale",
                        AutoSize = true,
                        CheckOnClick = true,
                        Checked = isAutoScaled
                    });
                }
            };

            ctxMnuChart.ItemClicked += (object sender, ToolStripItemClickedEventArgs e) =>
            {
                ctxMnuChart.Hide();

                switch (e.ClickedItem.Name)
                {
                    case MENU_BACKGROUND:
                        ColorDialog dlgColor = new ColorDialog();
                        if (dlgColor.ShowDialog() == DialogResult.OK)
                        {
                            Properties.Settings.Default.Background = dlgColor.Color;
                            Properties.Settings.Default.Save();

                            BackgroundColorChanged?.Invoke(null, new BackgroundColorChangeEventArgs
                            {
                                Color = dlgColor.Color
                            });
                        }
                        break;

                    case MENU_SAVE_PATH:
                        FolderBrowserDialog dlgFolder = new FolderBrowserDialog { SelectedPath = Properties.Settings.Default.SavePath };
                        if (dlgFolder.ShowDialog() == DialogResult.OK)
                        {
                            if (Directory.Exists(dlgFolder.SelectedPath))
                            {
                                Properties.Settings.Default.SavePath = dlgFolder.SelectedPath;
                                Properties.Settings.Default.Save();
                            }
                        }
                        break;

                    case MENU_SAVE:
                        string path = string.Format("{0}{1}screenshot_{2}.{3}", Properties.Settings.Default.SavePath, Path.DirectorySeparatorChar, DateTime.Now.ToString("yyyyMMdd_HHmmss"), EXT_CHART_IMAGE);
                        SaveImage(path, ChartImageFormat.Jpeg);
                        DjsmToastMessage.Show("Chart image saved.", 3000, DjsmToastMessage.Theme.DARK);
                        break;

                    case MENU_COPY_TO_CLIPBOARD:
                        CaptureToClipboard();
                        break;

                    case MENU_ZOOM_ENABLED:
                        SetZoomEnabled(!(e.ClickedItem as ToolStripMenuItem).Checked);
                        break;

                    case MENU_RESET_ZOOM:
                        ResetZoom();
                        break;

                    case MENU_AUTO_SCALE:
                        SetAutoScale(!(e.ClickedItem as ToolStripMenuItem).Checked);
                        break;

                    default:
                        /// Legend menu
                        if (e.ClickedItem.Tag != null)
                        {
                            SetLegendVisible(!(e.ClickedItem as ToolStripMenuItem).Checked, e.ClickedItem.Tag.ToString());
                        }
                        break;
                }
            };

            ContextMenuStrip = ctxMnuChart;
        }

        public static void SetLabelsLargeFont(bool enabled)
        {
            Properties.Settings.Default.LargeFont = enabled;
        }

        public void SetAutoScale(bool enabled)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetAutoScale(enabled);
                });
            }
            else
            {
                if (enabled)
                {
                    if (Series.Count > 0)
                    {
                        int refSeriesIndex = 0;
                        if (Series[0].Name.Equals(DUMMY_SERIES_NAME))
                        {
                            refSeriesIndex = 1;
                        }

                        if (Series.Count <= refSeriesIndex)
                        {
                            return;
                        }

                        if (Series[refSeriesIndex].Points.Count > 3)
                        {
                            double yMin = double.MaxValue;
                            double yMax = double.MinValue;

                            for (int sl = Series.Count, s = 0; s < sl; s++)
                            {
                                if (Series[s].Name != DUMMY_SERIES_NAME)
                                {
                                    for (int il = Series[s].Points.Count, i = 0; i < il; i++)
                                    {
                                        double value = Series[s].Points[i].YValues[0];
                                        if (value < yMin)
                                        {
                                            yMin = value;
                                        }
                                        if (value > yMax)
                                        {
                                            yMax = value;
                                        }
                                    }
                                }
                            }

                            if (yMin != 0 && yMax != 0)
                            {
                                if (yMin < 0)
                                {
                                    YAxis.Minimum = yMin + yMin * 0.1d;
                                }
                                else
                                {
                                    YAxis.Minimum = yMin - yMin * 0.1d;
                                }

                                if (yMax < 0)
                                {
                                    YAxis.Maximum = yMax - yMax * 0.1d;
                                }
                                else
                                {
                                    YAxis.Maximum = yMax + yMax * 0.1d;
                                }
                            }
                        }
                    }
                }
                else
                {
                    YAxis.Minimum = fixedYAxisMinimum;
                    YAxis.Maximum = fixedYAxisMaximum;

                    if (YAxis2.Enabled == AxisEnabled.True)
                    {
                        YAxis2.Minimum = fixedYAxis2Minimum;
                        YAxis2.Maximum = fixedYAxis2Maximum;
                    }
                }

                isAutoScaled = enabled;
            }
        }

        //public void SetAutoScale(bool enabled)
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke((MethodInvoker)delegate
        //        {
        //            SetAutoScale(enabled);
        //        });
        //    }
        //    else
        //    {
        //        YAxis.Minimum = enabled ? double.NaN : fixedYAxisMinimum;
        //        YAxis.Maximum = enabled ? double.NaN : fixedYAxisMaximum;

        //        if (YAxis2.Enabled == AxisEnabled.True)
        //        {
        //            YAxis2.Minimum = enabled ? double.NaN : fixedYAxis2Minimum;
        //            YAxis2.Maximum = enabled ? double.NaN : fixedYAxis2Maximum;
        //        }

        //        isAutoScaled = enabled;
        //    }
        //}

        public void ClearPoints(Series series)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    ClearPoints(series);
                });
            }
            else
            {
                if (series?.Points?.Count > 0)
                {
                    series.Points.SuspendUpdates();
                    while (series.Points.Count > 0)
                    {
                        series.Points.RemoveAt(series.Points.Count - 1);
                    }
                    series.Points.ResumeUpdates();
                }
            }
        }

        public void ClearSeries()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    ClearSeries();
                });
            }
            else
            {
                SuspendLayout();
                for (int i = Series.Count - 1; i >= 0; i--)
                {
                    if (Series[i].Name != DjsmChart.DUMMY_SERIES_NAME)
                    {
                        Series.RemoveAt(i);
                    }
                }
                ResumeLayout();
            }
        }

        #region /// Events
        //private ToolTip toolTip = new ToolTip();

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (IsZoomEnabled)
            {
                if (e.Button == MouseButtons.Right)
                {
                    moveCurrentPosition.X = e.X;
                    moveCurrentPosition.Y = e.Y;

                    // 스킵과 분기로 스크롤이 조금 더 부드러워짐.
                    if (Math.Abs(moveInitPosition.X - moveCurrentPosition.X) > 3)
                    {
                        if (zoomAllXAxis)
                        {
                            XAxis.ScaleView.Scroll(moveInitPosition.X < moveCurrentPosition.X ? ScrollType.SmallDecrement : ScrollType.SmallIncrement);
                            XAxis2.ScaleView.Scroll(moveInitPosition.X < moveCurrentPosition.X ? ScrollType.SmallDecrement : ScrollType.SmallIncrement);
                        }
                        else
                        {
                            if (AreaMain.CursorX.AxisType == AxisType.Primary)
                            {
                                XAxis.ScaleView.Scroll(moveInitPosition.X < moveCurrentPosition.X ? ScrollType.SmallDecrement : ScrollType.SmallIncrement);
                            }
                            else
                            {
                                XAxis2.ScaleView.Scroll(moveInitPosition.X < moveCurrentPosition.X ? ScrollType.SmallDecrement : ScrollType.SmallIncrement);
                            }
                        }

                        panProcessed = true;
                    }
                    else if (Math.Abs(moveInitPosition.Y - moveCurrentPosition.Y) > 3)
                    {
                        if (zoomAllYAxis)
                        {
                            YAxis.ScaleView.Scroll(moveInitPosition.Y > moveCurrentPosition.Y ? ScrollType.SmallDecrement : ScrollType.SmallIncrement);
                            YAxis2.ScaleView.Scroll(moveInitPosition.Y > moveCurrentPosition.Y ? ScrollType.SmallDecrement : ScrollType.SmallIncrement);
                        }
                        else
                        {
                            if (AreaMain.CursorY.AxisType == AxisType.Primary)
                            {
                                YAxis.ScaleView.Scroll(moveInitPosition.Y > moveCurrentPosition.Y ? ScrollType.SmallDecrement : ScrollType.SmallIncrement);
                            }
                            else
                            {
                                YAxis2.ScaleView.Scroll(moveInitPosition.Y > moveCurrentPosition.Y ? ScrollType.SmallDecrement : ScrollType.SmallIncrement);
                            }
                        }

                        panProcessed = true;
                    }

                    moveInitPosition = moveCurrentPosition;
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    AreaMain.CursorX.SetCursorPixelPosition(new PointF(e.X, e.Y), false);
                    SetCursorPositionByValue(AreaMain.CursorX.Position);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IsZoomEnabled)
                {
                    if (moveInitPosition.X > e.X && moveInitPosition.Y > e.Y)
                    {
                        UndoZoom();
                        // ↓ 선택 영역 클리어.
                        AreaMain.CursorX.SetSelectionPosition(0, 0);
                        AreaMain.CursorY.SetSelectionPosition(0, 0);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (panProcessed)
                {
                    ContextMenuStrip?.Hide();
                }
            }
            panProcessed = false;

            //base 이벤트가 선행될 경우 셀렉션 이벤트로 인해 줌 이벤트가 상쇄되어 버림.
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            moveInitPosition.X = e.X;
            moveInitPosition.Y = e.Y;

            if (e.Button == MouseButtons.Right)
            {
                // 스케일이 변해도 줌 스크롤 속도의 항상성을 위해 매번 계산

                if (zoomAllXAxis)
                {
                    XAxis.ScaleView.SmallScrollSize = XAxis.ScaleView.Size / XAxisScrollResolution;
                    XAxis2.ScaleView.SmallScrollSize = XAxis2.ScaleView.Size / XAxisScrollResolution;
                }
                else
                {
                    if (AreaMain.CursorX.AxisType == AxisType.Primary)
                    {
                        XAxis.ScaleView.SmallScrollSize = XAxis.ScaleView.Size / XAxisScrollResolution;
                    }
                    else
                    {
                        XAxis2.ScaleView.SmallScrollSize = XAxis2.ScaleView.Size / XAxisScrollResolution;
                    }
                }

                if (zoomAllYAxis)
                {
                    YAxis.ScaleView.SmallScrollSize = YAxis.ScaleView.Size / YAxisScrollResolution;
                    YAxis2.ScaleView.SmallScrollSize = YAxis2.ScaleView.Size / YAxisScrollResolution;
                }
                else
                {
                    if (AreaMain.CursorY.AxisType == AxisType.Primary)
                    {
                        YAxis.ScaleView.SmallScrollSize = YAxis.ScaleView.Size / YAxisScrollResolution;
                    }
                    else
                    {
                        YAxis2.ScaleView.SmallScrollSize = YAxis2.ScaleView.Size / YAxisScrollResolution;
                    }
                }
            }
            else
            {
                if (!IsZoomEnabled)
                {
                    AreaMain.CursorX.SetCursorPixelPosition(new PointF(e.X, e.Y), false);
                    SetCursorPositionByValue(AreaMain.CursorX.Position);
                }
            }
        }

        private bool IsNullSeries(string seriesName)
        {
            return (string.IsNullOrEmpty(seriesName) || Series.FindByName(seriesName) == null);
        }

        public void SetCursorPositionByIndex(int index)
        {
            if (cursorTargetSeries == null)
            {
                return;
            }

            if (cursorTargetSeries.Points.Count > index && index >= 0)
            {
                SetCursorPosition(cursorTargetSeries.Points[index], index);
            }
        }

        public void SetCursorPositionByValue(double xValue)
        {
            if (cursorTargetSeries == null || double.IsNaN(xValue))
            {
                return;
            }

            try
            {
                double cmp = double.MaxValue;
                int index = 0;
                for (int len = cursorTargetSeries.Points.Count - 1, i = 0; i < len; i++)
                {
                    double diff = Math.Abs(cursorTargetSeries.Points[i].XValue - xValue);
                    if (cmp > diff)
                    {
                        cmp = diff;
                        index = i;
                    }
                }

                SetCursorPosition(cursorTargetSeries.Points[index]);
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void SetCursorPosition(DataPoint dp, int index = -1)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetCursorPosition(dp, index);
                });
            }
            else
            {
                double xValue = dp.XValue;
                double yValue = dp.YValues[0];

                AreaMain.CursorX.Position = xValue;
                AreaMain.CursorY.Position = yValue;

                int posX = (int)XAxis.ValueToPixelPosition(xValue);
                int posY = (int)YAxis.ValueToPixelPosition(Math.Max(YAxis.ScaleView.ViewMinimum, Math.Min(YAxis.ScaleView.ViewMaximum, yValue)));

                cursorXValueIndex = index == -1 ? cursorTargetSeries.Points.IndexOf(dp) : index;

                XYCursorPositionChanged?.Invoke(this, new XYCursorEventArgs { XValueIndex = cursorXValueIndex, X = xValue, Y = yValue });
            }
        }

        protected override void OnPostPaint(ChartPaintEventArgs e)
        {
            try
            {
                if (e.ChartElement is ChartArea)
                {
                    Graphics g = e.ChartGraphics.Graphics;
                    {
                        if (VerticalTextAnnotations.Count > 0)
                        {
                            for (int len = VerticalTextAnnotations.Count, i = 0; i < len; i++)
                            {
                                //Debug.WriteLine($"vertical text annotation index: {i}");
                                string text = VerticalTextAnnotations[i].Text;
                                Font f = new Font("Arial", 9, FontStyle.Bold);
                                SizeF txtSize = g.MeasureString(text, f);

                                double xPos = XAxis.ValueToPixelPosition(VerticalTextAnnotations[i].Position.XValue);
                                double yPos = YAxis.ValueToPixelPosition(VerticalTextAnnotations[i].Position.YValues[0]);
                                //double yHeight = YAxis.ValueToPixelPosition(YAxis.ScaleView.ViewMaximum);

                                //RectangleF rect = new RectangleF((float)(yPos / yHeight), (float)xPos - txtSize.Height, txtSize.Width, txtSize.Height);
                                RectangleF rect = new RectangleF((float)(yPos - txtSize.Width), (float)xPos - txtSize.Height, txtSize.Width, txtSize.Height);

                                g.TranslateTransform(0, (float)yPos);
                                g.RotateTransform(270);

                                g.DrawString(text, f, new SolidBrush(VerticalTextAnnotations[i].TextColor), rect);
                                g.ResetTransform();
                            }
                        }

                        if (horizontalLines.Count > 0)
                        {
                            float xMinValue = (float)XAxis.ValueToPixelPosition(XAxis.ScaleView.ViewMinimum);
                            float xMaxValue = (float)XAxis.ValueToPixelPosition(XAxis.ScaleView.ViewMaximum);

                            for (int len = horizontalLines.Count, i = 0; i < len; i++)
                            {
                                using (Pen p = new Pen(horizontalLines[i].Color) { Width = 0.5f, })
                                {
                                    if (horizontalLines[i].Value >= YAxis.ScaleView.ViewMinimum && horizontalLines[i].Value <= YAxis.ScaleView.ViewMaximum)
                                    {
                                        float yValue = (float)YAxis.ValueToPixelPosition(horizontalLines[i].Value);
                                        g.DrawLine(p, new PointF(xMinValue, yValue), new PointF(xMaxValue, yValue));
                                    }
                                }
                            }
                        }

                        if (verticalLines.Count > 0)
                        {
                            float yMinValue = (float)YAxis.ValueToPixelPosition(YAxis.ScaleView.ViewMinimum);
                            float yMaxValue = (float)YAxis.ValueToPixelPosition(YAxis.ScaleView.ViewMaximum);

                            for (int len = verticalLines.Count, i = 0; i < len; i++)
                            {
                                using (Pen p = new Pen(new SolidBrush(verticalLines[i].Color)) { Width = 0.5f, })
                                {
                                    if (verticalLines[i].Value >= XAxis.ScaleView.ViewMinimum && verticalLines[i].Value <= XAxis.ScaleView.ViewMaximum)
                                    {
                                        float xValue = (float)XAxis.ValueToPixelPosition(verticalLines[i].Value);
                                        g.DrawLine(p, new PointF(xValue, yMinValue), new PointF(xValue, yMaxValue));
                                    }
                                }
                            }
                        }

                        if (boxes.Count > 0)
                        {
                            AxisScaleView xAxisScaleView = XAxis.ScaleView;
                            AxisScaleView yAxisscaleView = YAxis.ScaleView;

                            using (Pen p = new Pen(Brushes.Orange) { Width = 1f })
                            {
                                for (int len = boxes.Count, i = 0; i < len; i++)
                                {
                                    if (boxes[i].Left >= xAxisScaleView.ViewMaximum
                                        || boxes[i].Top <= yAxisscaleView.ViewMinimum
                                        || boxes[i].Right <= xAxisScaleView.ViewMinimum
                                        || boxes[i].Bottom >= yAxisscaleView.ViewMaximum)
                                    {
                                        continue;
                                    }

                                    float left = (float)XAxis.ValueToPixelPosition(Math.Max(boxes[i].Left, xAxisScaleView.ViewMinimum));
                                    float top = (float)YAxis.ValueToPixelPosition(Math.Min(boxes[i].Top, yAxisscaleView.ViewMaximum));
                                    float right = (float)XAxis.ValueToPixelPosition(Math.Min(boxes[i].Right, xAxisScaleView.ViewMaximum));
                                    float bottom = (float)YAxis.ValueToPixelPosition(Math.Max(boxes[i].Bottom, yAxisscaleView.ViewMinimum));

                                    g.DrawLine(p, new PointF(left, top), new PointF(right, top));
                                    g.DrawLine(p, new PointF(right, top), new PointF(right, bottom));
                                    g.DrawLine(p, new PointF(right, bottom), new PointF(left, bottom));
                                    g.DrawLine(p, new PointF(left, bottom), new PointF(left, top));
                                }
                            }
                            g.ResetClip();
                        }

                        if (boundaryTexts.Count > 0)
                        {
                            for (int il = boundaryTexts.Count, i = 0; i < il; i++)
                            {
                                SizeF size = g.MeasureString(boundaryTexts[i].Text, boundaryTexts[i].Font);

                                g.DrawString(boundaryTexts[i].Text, boundaryTexts[i].Font, new SolidBrush(boundaryTexts[i].FontColor),
                                    new PointF((int)XAxis.ValueToPixelPosition(XAxis.ScaleView.ViewMinimum),
                                    (int)(YAxis.ValueToPixelPosition(boundaryTexts[i].Y) - size.Height)));
                            }
                        }
                    }
                }
            }
            catch (OverflowException ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            base.OnPostPaint(e);
        }
        #endregion

        protected Legend AddLegend(string name, int index)
        {
            Legend legend = new Legend(name)
            {
                IsTextAutoFit = false,
                Enabled = true,
                Alignment = StringAlignment.Near,
                Docking = Docking.Right,
                BackColor = Color.Transparent,
            };

            if (Legends.Count > index)
            {
                Legends.Insert(index, legend);
            }
            else
            {
                Legends.Add(legend);
            }

            return legend;
        }

        #region /// Horizontal lines
        private List<LineObject> horizontalLines = new List<LineObject>();

        public void AddHorizontalLine(double yValue, Color? color)
        {
            horizontalLines.Add(new LineObject { Value = yValue, Color = color ?? Color.Red });
        }

        public void ClearHorizontalLine()
        {
            horizontalLines.Clear();
        }

        public HorizontalLineAnnotation AddHorizontalLineAnnotation(Color color)
        {
            if (InvokeRequired)
            {
                return (HorizontalLineAnnotation)Invoke((Func<HorizontalLineAnnotation>)delegate
                {
                    return AddHorizontalLineAnnotation(color);
                });
            }
            else
            {
                HorizontalLineAnnotation annot = new HorizontalLineAnnotation
                {
                    ClipToChartArea = AreaMain.Name,
                    LineDashStyle = ChartDashStyle.Dot,
                    LineColor = color,
                    IsInfinitive = true,
                    LineWidth = 1,
                    StartCap = LineAnchorCapStyle.None,
                    EndCap = LineAnchorCapStyle.None,
                    AxisY = YAxis,
                    Visible = true
                };
                Annotations.Add(annot);
                return annot;
            }
        }

        public void SetHorizontalLineAnnotationValue(HorizontalLineAnnotation annot, float value)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetHorizontalLineAnnotationValue(annot, value);
                });
            }
            else
            {
                annot.Y = value;
            }
        }

        public void ClearHorizontalLineAnnotation()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    ClearHorizontalLineAnnotation();
                });
            }
            else
            {
                Annotations.SuspendUpdates();
                for (int i = Annotations.Count - 1; i >= 0; i--)
                {
                    if (Annotations[i] is HorizontalLineAnnotation)
                    {
                        Annotations.RemoveAt(i);
                    }
                }
                Annotations.ResumeUpdates();
            }
        }
        #endregion

        #region /// Vertical line
        private List<LineObject> verticalLines = new List<LineObject>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>current line's index</returns>
        public int AddVerticalLine(double value, Color color)
        {
            if (InvokeRequired)
            {
                return (int)Invoke(new Func<int>(() => { return AddVerticalLine(value, color); }));
            }
            else
            {
                verticalLines.Add(new LineObject { Value = value, Color = color });
                Invalidate();
                return verticalLines.Count - 1;
            }
        }

        public void AddVerticalLines(List<double> values, Color color)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    AddVerticalLines(values, color);
                });
            }
            else
            {
                List<LineObject> lines = new List<LineObject>();

                for (int il = values.Count, i = 0; i < il; i++)
                {
                    lines.Add(new LineObject { Value = values[i], Color = color });
                }
                verticalLines.AddRange(lines);
                Invalidate();
            }
        }

        public void MoveVerticalLine(int index, double value, Color? color)
        {
            verticalLines[index] = new LineObject { Value = value, Color = color ?? Color.Red };
            Invalidate();
        }

        public void RemoveVerticalLine(int index)
        {
            verticalLines.RemoveAt(index);
            Invalidate();
        }

        public void ClearVerticalLine()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    ClearVerticalLine();
                });
            }
            else
            {
                verticalLines.Clear();
                Invalidate();
            }
        }

        public double GetVerticalLineValue(int index)
        {
            return verticalLines[index].Value;
        }

        #endregion

        #region /// Vertical annotation
        public List<VerticalTextAnnotation> VerticalTextAnnotations { get; set; } = new List<VerticalTextAnnotation>();

        public void AddVerticalTextAnnotation(VerticalTextAnnotation annot)
        {
            VerticalTextAnnotations.Add(annot);
        }

        public void ClearVerticalTextAnnotation()
        {
            VerticalTextAnnotations.Clear();
        }

        public void RemoveVerticalTextAnnotation(VerticalTextAnnotation annot)
        {
            if (annot != null && VerticalTextAnnotations.Contains(annot))
            {
                VerticalTextAnnotations.Remove(annot);
            }
        }

        public double VerticalTextAnnotationYPosition()
        {
            switch (VerticalTextAnnotations.Count % 3)
            {
                case 1:
                    return 0;
                case 2:
                    return YAxis.ScaleView.ViewMinimum;
                default:
                    return YAxis.ScaleView.ViewMaximum / 2;
            }
        }

        public class VerticalTextAnnotation
        {
            public DataPoint Position { get; }
            public string Text { get; }

            public Color TextColor { get; }

            public VerticalTextAnnotation(DataPoint position, string text)
            {
                Position = position;
                Text = text;
                TextColor = Color.White;
            }

            public VerticalTextAnnotation(DataPoint position, string text, Color textColor)
            {
                Position = position;
                Text = text;
                TextColor = textColor;
            }
        }
        #endregion

        #region /// Calculate coefficient
        public enum CurveType { LINEAR_0, LINEAR, QUADRATIC, CUBIC }
        public double[] GetCoefficient(CurveType curveType, Series series)
        {
            DataPointCollection pts = series.Points;
            int i;
            int pointCount = pts.Count;
            double delta;
            double[] sumX = new double[7];
            double[] sumY = new double[4];
            double[] temp = new double[10];
            double[] coefficient = new double[] { 0, 1, 0, 0 };
            switch (curveType)
            {
                case CurveType.LINEAR:
                    for (i = 0; i < pointCount; i++)
                    {

                        sumX[0] = sumX[0] + 1;
                        sumX[1] = sumX[1] + pts[i].XValue;
                        sumX[2] = sumX[2] + Math.Pow(pts[i].XValue, 2);


                        sumY[0] = sumY[0] + pts[i].YValues[0];
                        sumY[1] = sumY[1] + pts[i].YValues[0] * pts[i].XValue;
                    }
                    delta = sumX[0] * sumX[2] - Math.Pow(sumX[1], 2);

                    if (delta != 0)
                    {
                        temp[0] = sumX[2] / delta;
                        temp[1] = (-1) * sumX[1] / delta;
                        temp[2] = sumX[0] / delta;

                        coefficient[0] = temp[0] * sumY[0] + temp[1] * sumY[1];
                        coefficient[1] = temp[1] * sumY[0] + temp[2] * sumY[1];
                    }
                    break;
                case CurveType.QUADRATIC:
                    for (i = 0; i < pointCount; i++)
                    {

                        sumX[0] = sumX[0] + 1;
                        sumX[1] = sumX[1] + pts[i].XValue;
                        sumX[2] = sumX[2] + Math.Pow(pts[i].XValue, 2);
                        sumX[3] = sumX[3] + Math.Pow(pts[i].XValue, 3);
                        sumX[4] = sumX[4] + Math.Pow(pts[i].XValue, 4);

                        sumY[0] = sumY[0] + pts[i].YValues[0];
                        sumY[1] = sumY[1] + pts[i].YValues[0] * pts[i].XValue;
                        sumY[2] = sumY[2] + pts[i].YValues[0] * Math.Pow(pts[i].XValue, 2);
                    }
                    delta = sumX[0] * sumX[2] * sumX[4] + 2
                            * sumX[1] * sumX[2] * sumX[3] - Math.Pow(sumX[2], 3) - sumX[4]
                            * Math.Pow(sumX[1], 2) - sumX[0]
                            * Math.Pow(sumX[3], 2);

                    if (delta != 0)
                    {
                        temp[0] = (sumX[2] * sumX[4] - Math.Pow(sumX[3], 2)) / delta;
                        temp[1] = ((-1) * sumX[4] * sumX[1] + sumX[2] * sumX[3]) / delta;
                        temp[2] = ((-1) * Math.Pow(sumX[2], 2) + sumX[1] * sumX[3]) / delta;
                        temp[3] = (sumX[0] * sumX[4] - Math.Pow(sumX[2], 2)) / delta;
                        temp[4] = (sumX[1] * sumX[2] - sumX[0] * sumX[3]) / delta;
                        temp[5] = (sumX[0] * sumX[2] - Math.Pow(sumX[1], 2)) / delta;

                        coefficient[0] = temp[0] * sumY[0] + temp[1] * sumY[1] + temp[2] * sumY[2];
                        coefficient[1] = temp[1] * sumY[0] + temp[3] * sumY[1] + temp[4] * sumY[2];
                        coefficient[2] = temp[2] * sumY[0] + temp[4] * sumY[1] + temp[5] * sumY[2];
                    }
                    break;
                case CurveType.CUBIC:
                    for (i = 0; i < pointCount; i++)
                    {

                        sumX[0] = sumX[0] + 1;
                        sumX[1] = sumX[1] + pts[i].XValue;
                        sumX[2] = sumX[2] + Math.Pow(pts[i].XValue, 2);
                        sumX[3] = sumX[3] + Math.Pow(pts[i].XValue, 3);
                        sumX[4] = sumX[4] + Math.Pow(pts[i].XValue, 4);
                        sumX[5] = sumX[5] + Math.Pow(pts[i].XValue, 5);
                        sumX[6] = sumX[6] + Math.Pow(pts[i].XValue, 6);

                        sumY[0] = sumY[0] + pts[i].YValues[0];
                        sumY[1] = sumY[1] + pts[i].YValues[0] * pts[i].XValue;
                        sumY[2] = sumY[2] + pts[i].YValues[0] * Math.Pow(pts[i].XValue, 2);
                        sumY[3] = sumY[3] + pts[i].YValues[0] * Math.Pow(pts[i].XValue, 3);
                    }
                    delta = sumX[0] * sumX[2] * sumX[4] * sumX[6]
                            - sumX[4] * sumX[6] * Math.Pow(sumX[1], 2)
                            - sumX[2] * sumX[6] * Math.Pow(sumX[2], 2)
                            - sumX[2] * sumX[4] * Math.Pow(sumX[3], 2)
                            + 2 * sumX[6] * sumX[1] * sumX[2] * sumX[3]
                            - sumX[0] * sumX[6] * Math.Pow(sumX[3], 2)
                            + Math.Pow(sumX[3], 4)
                            + 2 * sumX[1] * sumX[3] * Math.Pow(sumX[4], 2)
                            - 2 * sumX[2] * sumX[4] * Math.Pow(sumX[3], 2)
                            - sumX[0] * Math.Pow(sumX[4], 3)
                            + Math.Pow(sumX[2], 2) * Math.Pow(sumX[4], 2)
                            + 2 * Math.Pow(sumX[2], 2) * sumX[3] * sumX[5]
                            - 2 * sumX[1] * Math.Pow(sumX[3], 2) * sumX[5]
                            - 2 * sumX[1] * sumX[2] * sumX[4] * sumX[5]
                            + 2 * sumX[0] * sumX[3] * sumX[4] * sumX[5]
                            - sumX[0] * sumX[2] * Math.Pow(sumX[5], 2)
                            + Math.Pow(sumX[1], 2) * Math.Pow(sumX[5], 2);

                    if (delta != 0)
                    {
                        temp[0] = (sumX[2] * sumX[4] * sumX[6] - sumX[6] * Math.Pow(sumX[3], 2)
                                - Math.Pow(sumX[4], 3) + 2 * sumX[3] * sumX[4] * sumX[5]
                                - sumX[2] * Math.Pow(sumX[5], 2)) / delta;
                        temp[1] = ((-1) * sumX[4] * sumX[6] * sumX[1] + sumX[6] * sumX[2] * sumX[3]
                                + sumX[3] * Math.Pow(sumX[4], 2) - Math.Pow(sumX[3], 2) * sumX[5]
                                - sumX[2] * sumX[4] * sumX[5] + sumX[1] * Math.Pow(sumX[5], 2)) / delta;
                        temp[2] = ((-1) * Math.Pow(sumX[2], 2) * sumX[6] + sumX[6] * sumX[1] * sumX[3]
                                - Math.Pow(sumX[3], 2) * sumX[4] + sumX[2] * Math.Pow(sumX[4], 2)
                                + sumX[2] * sumX[3] * sumX[5] - sumX[1] * sumX[4] * sumX[5]) / delta;
                        temp[3] = ((-1) * sumX[2] * sumX[3] * sumX[4] + Math.Pow(sumX[3], 3)
                                + sumX[4] * sumX[1] * sumX[4] - sumX[2] * sumX[3] * sumX[4]
                                + sumX[2] * sumX[2] * sumX[5] - sumX[1] * sumX[3] * sumX[5]) / delta;
                        temp[4] = (sumX[0] * sumX[4] * sumX[6] - sumX[2] * sumX[2] * sumX[6]
                                - sumX[4] * sumX[3] * sumX[3] + 2 * sumX[2] * sumX[3] * sumX[5]
                                - sumX[0] * sumX[5] * sumX[5]) / delta;
                        temp[5] = (sumX[6] * sumX[1] * sumX[2] - sumX[0] * sumX[6] * sumX[3]
                                + sumX[3] * sumX[3] * sumX[3] - sumX[2] * sumX[3] * sumX[4]
                                - sumX[1] * sumX[3] * sumX[5] + sumX[0] * sumX[4] * sumX[5]) / delta;
                        temp[6] = (sumX[4] * sumX[1] * sumX[3] - sumX[2] * sumX[3] * sumX[3]
                                - sumX[0] * sumX[4] * sumX[4] + sumX[2] * sumX[2] * sumX[4]
                                - sumX[1] * sumX[2] * sumX[5] + sumX[0] * sumX[3] * sumX[5]) / delta;
                        temp[7] = (sumX[0] * sumX[2] * sumX[6] - sumX[6] * sumX[1] * sumX[1]
                                - sumX[2] * sumX[3] * sumX[3] + 2 * sumX[1] * sumX[3] * sumX[4]
                                - sumX[0] * sumX[4] * sumX[4]) / delta;
                        temp[8] = (sumX[2] * sumX[2] * sumX[3] - sumX[1] * sumX[3] * sumX[3]
                                - sumX[1] * sumX[2] * sumX[4] + sumX[0] * sumX[3] * sumX[4]
                                - sumX[0] * sumX[2] * sumX[5] + sumX[1] * sumX[1] * sumX[5]) / delta;
                        temp[9] = (sumX[0] * sumX[2] * sumX[4] - sumX[4] * sumX[1] * sumX[1]
                                - sumX[2] * sumX[2] * sumX[2] + 2 * sumX[1] * sumX[2] * sumX[3]
                                - sumX[0] * sumX[3] * sumX[3]) / delta;
                        coefficient[0] = temp[0] * sumY[0] + temp[1] * sumY[1] + temp[2] * sumY[2] + temp[3] * sumY[3];
                        coefficient[1] = temp[1] * sumY[0] + temp[4] * sumY[1] + temp[5] * sumY[2] + temp[6] * sumY[3];
                        coefficient[2] = temp[2] * sumY[0] + temp[5] * sumY[1] + temp[7] * sumY[2] + temp[8] * sumY[3];
                        coefficient[3] = temp[3] * sumY[0] + temp[6] * sumY[1] + temp[8] * sumY[2] + temp[9] * sumY[3];
                    }
                    break;
                default:
                    for (i = 0; i < pointCount; i++)
                    {
                        sumX[0] = sumX[0] + Math.Pow(pts[i].XValue, 2);
                        sumY[0] = sumY[0] + pts[i].YValues[0] * pts[i].XValue;
                    }
                    if (sumX[0] != 0)
                    {
                        coefficient[1] = sumY[0] / sumX[0];
                    }
                    break;

            }
            return coefficient;
        }
        #endregion

        #region /// Calculate rsq
        public double GetRSquare(Series origin, Series trend)
        {
            double sst = 0, ssr = 0, sse = 0;
            double mean = DataManipulator.Statistics.Mean(origin.Name);

            for (int len = origin.Points.Count, i = 0; i < len; i++)
            {
                sst += Math.Pow(origin.Points[i].YValues[0] - mean, 2);
                ssr += Math.Pow(trend.Points[i].YValues[0] - mean, 2);
                sse += Math.Pow(origin.Points[i].YValues[0] - trend.Points[i].YValues[0], 2);
            }
            Debug.WriteLine("sst: {0}, sse + ssr = {1}", sst, ssr + sse);
            return 1 - (sse / sst); // or ssr/sst
        }
        #endregion

        public void SetThemeColor(Color backColor, Color foreColor)
        {
            AreaMain.BackColor = backColor;
            BackSecondaryColor = backColor;
            BackColor = backColor;
            ForeColor = foreColor;

            XAxis.LabelStyle.ForeColor = foreColor;
            XAxis.LineColor = foreColor;
            XAxis.TitleForeColor = foreColor;

            YAxis.LabelStyle.ForeColor = foreColor;
            YAxis.LineColor = foreColor;
            YAxis.TitleForeColor = foreColor;

            YAxis2.LabelStyle.ForeColor = foreColor;
            YAxis2.LineColor = foreColor;
            YAxis2.TitleForeColor = foreColor;
        }

        public void RemovePointAtFirst(Series series)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    RemovePointAtFirst(series);
                });
            }
            else
            {
                series.Points.RemoveAt(0);
                SetAutoScale(isAutoScaled);
            }
        }

        public virtual void AddPoint(Series series, DataPoint point)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    AddPoint(series, point);
                });
            }
            else
            {
                series.Points.Add(point);
                SetAutoScale(isAutoScaled);
            }
        }

        public virtual void AddPoint(Series series, object x, double y)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    AddPoint(series, x, y);
                });
            }
            else
            {
                series.Points.AddXY(x, y);
                SetAutoScale(isAutoScaled);
            }
        }

        public void AddPointRange(Series series, double[] x, double[] y)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    AddPointRange(series, x, y);
                });
            }
            else
            {
                SuspendLayout();

                int length = Math.Min(x.Length, y.Length);

                for (int i = 0; i < length; i++)
                {
                    series.Points.AddXY(x[i], y[i]);
                }
                SetAutoScale(isAutoScaled);
                ResumeLayout();
            }
        }

        public void DataBindXY(IEnumerable xValues, double[] yValues, Series series)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    DataBindXY(xValues, yValues, series);
                });
            }
            else
            {
                series.Points.DataBindXY(xValues, yValues);
                SetAutoScale(isAutoScaled);
            }
        }

        public void DataBindXY(IEnumerable xValues, float[] yValues, Series series)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    DataBindXY(xValues, yValues, series);
                });
            }
            else
            {
                series.Points.DataBindXY(xValues, yValues);
                SetAutoScale(isAutoScaled);
            }
        }

        public void DataBindXY(IEnumerable xValues, ushort[] yValues, Series series)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    DataBindXY(xValues, yValues, series);
                });
            }
            else
            {
                series.Points.DataBindXY(xValues, yValues);
                SetAutoScale(isAutoScaled);
            }
        }

        public void DataBindY(float[] yValues, Series series)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    DataBindY(yValues, series);
                });
            }
            else
            {
                series.Points.DataBindY(yValues);
                SetAutoScale(isAutoScaled);
            }
        }

        public void DataBindY(ushort[] yValues, Series series)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    DataBindY(yValues, series);
                });
            }
            else
            {
                series.Points.DataBindY(yValues);
                SetAutoScale(isAutoScaled);
            }
        }

        public void DataBindY(double[] yValues, Series series)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    DataBindY(yValues, series);
                });
            }
            else
            {
                series.Points.DataBindY(yValues);
                SetAutoScale(isAutoScaled);
            }
        }

        public Series AddLineSeries(string name)
        {
            Series series = new Series
            {
                ChartType = SeriesChartType.Line,
                Name = name
            };
            Series.Add(series);
            return series;
        }

        public const string DUMMY_SERIES_NAME = "dummy";

        public void AddDummySeriesForVisualization()
        {
            Series dummySeries = AddLineSeries(DUMMY_SERIES_NAME);
            dummySeries.Points.AddXY(XAxis.Minimum, YAxis.Minimum);
            dummySeries.IsVisibleInLegend = false;
        }

        private List<BoundaryTextObject> boundaryTexts = new List<BoundaryTextObject>();

        public BoundaryTextObject AddBoundaryText(float x, float y, string text, Font font, Color fontColor)
        {
            BoundaryTextObject obj = new BoundaryTextObject { X = x, Y = y, Text = text, Font = font, FontColor = fontColor };
            boundaryTexts.Add(obj);
            return obj;
        }

        public void ClearBoundaryText()
        {
            boundaryTexts.Clear();
        }

        #region /// Box
        private List<Box> boxes = new List<Box>();

        public void AddBox(double left, double top, double right, double bottom)
        {
            boxes.Add(new Box { Left = left, Top = top, Right = right, Bottom = bottom });
            //Debug.WriteLine(string.Format("name:{0}, boxcount:{1}, {2}, {3}, {4}, {5}", Name, boxes.Count, left, top, right, bottom));
        }

        public void ClearBoxes()
        {
            boxes.Clear();
        }

        private class Box
        {
            public double Left { get; set; }
            public double Top { get; set; }
            public double Right { get; set; }
            public double Bottom { get; set; }
        }
        #endregion

        public void AddAnnotation(double x, double y, string text, Color backColor)
        {
            CalloutAnnotation annot = new CalloutAnnotation
            {
                AllowSelecting = true,
                AllowMoving = true,
                CalloutStyle = CalloutStyle.Borderline,
                CalloutAnchorCap = LineAnchorCapStyle.None,
                LineDashStyle = ChartDashStyle.Solid,
                AxisX = XAxis,
                AxisY = YAxis,
                Text = text,
                //AnchorAlignment = ContentAlignment.BottomLeft,
                ForeColor = backColor.ToArgb() > Color.FromArgb(128, 128, 128).ToArgb() ? Color.Black : Color.White,
                BackColor = backColor,
                AnchorX = x,
                AnchorY = y,
                LineColor = AreaMain.BackColor.ToArgb() > Color.FromArgb(128, 128, 128).ToArgb() ? Color.Black : Color.White,
                LineWidth = 1,
                Alignment = ContentAlignment.MiddleCenter
            };
            annot.SmartLabelStyle.Enabled = true;
            //annot.SmartLabelStyle.IsOverlappedHidden = true;
            annot.SmartLabelStyle.IsMarkerOverlappingAllowed = true;
            annot.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
            Annotations.Add(annot);
        }

        public CalloutAnnotation AddAnnotation()
        {
            CalloutAnnotation annot = new CalloutAnnotation
            {
                AllowSelecting = true,
                AllowMoving = true,
                CalloutStyle = CalloutStyle.Borderline,
                CalloutAnchorCap = LineAnchorCapStyle.None,
                LineDashStyle = ChartDashStyle.Solid,
                AxisX = XAxis,
                AxisY = YAxis,
                //Text = text,
                ////AnchorAlignment = ContentAlignment.BottomLeft,
                //ForeColor = backColor.ToArgb() > Color.FromArgb(128, 128, 128).ToArgb() ? Color.Black : Color.White,
                //BackColor = backColor,
                //AnchorX = x,
                //AnchorY = y,
                LineColor = AreaMain.BackColor.ToArgb() > Color.FromArgb(128, 128, 128).ToArgb() ? Color.Black : Color.White,
                LineWidth = 1,
                Alignment = ContentAlignment.MiddleCenter
            };
            annot.SmartLabelStyle.Enabled = true;
            //annot.SmartLabelStyle.IsOverlappedHidden = true;
            annot.SmartLabelStyle.IsMarkerOverlappingAllowed = true;
            annot.SmartLabelStyle.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes;
            Annotations.Add(annot);
            return annot;
        }

        public void ClearAnnotation()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    ClearAnnotation();
                });
            }
            else
            {
                Annotations.Clear();
            }
        }

        public StripLine AddStripLine(double offset, double range, Color startColor, Color endColor, string text)
        {
            StripLine line = new StripLine
            {
                TextOrientation = TextOrientation.Rotated270,
                Text = text,

                IntervalOffset = offset,
                StripWidth = range,

                BackColor = startColor,
                BackSecondaryColor = endColor,
                BackGradientStyle = GradientStyle.LeftRight,
                Font = new Font("Arial", 9f, FontStyle.Bold),
                ForeColor = Color.White,
            };

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { XAxis.StripLines.Add(line); });
            }
            else
            {
                XAxis.StripLines.Add(line);
            }
            return line;
        }

        public StripLine AddBorderedStripLIne(double offset, double range, string text, Color borderColor)
        {
            StripLine line = new StripLine
            {
                TextOrientation = TextOrientation.Rotated270,
                Text = text,
                BackColor = Color.Transparent,
                StripWidth = range,
                BackSecondaryColor = Color.Transparent,
                IntervalOffset = offset,
                Font = new Font("Arial", 9f, FontStyle.Bold),
                ForeColor = Color.Black,
                BorderColor = borderColor,
                BorderDashStyle = ChartDashStyle.Dash,
                BorderWidth = 3
            };

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { XAxis.StripLines.Add(line); });
            }
            else
            {
                XAxis.StripLines.Add(line);
            }
            return line;
        }

        public StripLine AddTransparentStripLine(double offset, double range, string text, StringAlignment stringAlignment = StringAlignment.Far)
        {
            StripLine line = new StripLine
            {
                TextOrientation = TextOrientation.Rotated270,
                Text = text,
                BackColor = Color.Transparent,
                StripWidth = range,
                BackSecondaryColor = Color.Transparent,
                IntervalOffset = offset,
                Font = new Font("Arial", 9f, FontStyle.Bold),
                ForeColor = Color.Black,
                TextLineAlignment = stringAlignment,
            };

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { XAxis.StripLines.Add(line); });
            }
            else
            {
                XAxis.StripLines.Add(line);
            }
            return line;
        }

        public void ClearStripLine()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    ClearStripLine();
                });
            }
            else
            {
                XAxis?.StripLines.Clear();
                YAxis?.StripLines.Clear();
            }
        }

        private CalloutAnnotation annotTrendTooltip;

        public void InitializeXYCursor(string targetSeriesName)
        {
            annotTrendTooltip = AddAnnotation();
            annotTrendTooltip.ForeColor = Color.White.ToArgb() > Color.FromArgb(128, 128, 128).ToArgb() ? Color.Black : Color.White;
            annotTrendTooltip.BackColor = Color.White;
            annotTrendTooltip.AllowMoving = false;
            annotTrendTooltip.AllowSelecting = false;

            MouseClick += (sender, e) =>
            {
                if (!IsZoomEnabled)
                {
                    SetCursorTarget(targetSeriesName);
                }
            };

            XYCursorPositionChanged += (sender, e) =>
            {
                annotTrendTooltip.AnchorX = e.X;
                annotTrendTooltip.AnchorY = e.Y;
                annotTrendTooltip.Text = $"({e.X:0.0###}, {e.Y:0.0###})";
            };
        }

        //public void SetCursorEnabled(bool enabled)
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke((MethodInvoker)delegate
        //        {
        //            SetCursorEnabled(enabled);
        //        });
        //    }
        //    else
        //    {
        //        SetXAxisZoomEnabled(!enabled);
        //        SetYAxisZoomEnabled(!enabled);

        //        AreaMain.CursorX.IsUserEnabled = enabled;
        //        AreaMain.CursorX.IsUserSelectionEnabled = false;
        //        AreaMain.CursorX.AxisType = AxisType.Primary;

        //        AreaMain.CursorX.LineWidth = 2;

        //        AreaMain.CursorY.IsUserEnabled = false;
        //        AreaMain.CursorY.IsUserSelectionEnabled = false;
        //    }
        //}

        private void SetXAxisZoomEnabled(bool enabled, AxisType type = AxisType.Primary)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetXAxisZoomEnabled(enabled);
                });
            }
            else
            {
                XAxis.ScaleView.Zoomable = enabled;
                XAxis2.ScaleView.Zoomable = enabled;
                AreaMain.CursorX.IsUserSelectionEnabled = enabled;
                AreaMain.CursorX.AxisType = type;
            }
        }

        private void SetYAxisZoomEnabled(bool enabled, AxisType type = AxisType.Primary)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetYAxisZoomEnabled(enabled);
                });
            }
            else
            {
                YAxis.ScaleView.Zoomable = enabled;

                if (type == AxisType.Secondary)
                {
                    YAxis2.ScaleView.Zoomable = enabled;
                }

                AreaMain.CursorY.IsUserSelectionEnabled = enabled;
                AreaMain.CursorY.AxisType = type;
            }
        }

        public void SetZoomEnabled(bool enabled, AxisType type = AxisType.Primary)
        {
            //AreaMain.CursorX.IsUserEnabled = !enabled;
            //AreaMain.CursorX.IsUserSelectionEnabled = false;
            //AreaMain.CursorX.AxisType = type;

            //AreaMain.CursorX.LineWidth = 2;

            //AreaMain.CursorY.IsUserEnabled = false;
            //AreaMain.CursorY.IsUserSelectionEnabled = false;

            SetXAxisZoomEnabled(enabled, type);
            SetYAxisZoomEnabled(enabled, type);

            /// 줌 스케일 제한
            //AreaMain.CursorX.Interval = 2;
            IsZoomEnabled = enabled;
        }

        public void SetZoomEnabled(bool enabled, bool zoomAllXAxis, bool zoomAllYAxis)
        {
            this.zoomAllXAxis = zoomAllXAxis;
            this.zoomAllYAxis = zoomAllYAxis;

            XAxis.ScaleView.Zoomable = enabled;

            AreaMain.CursorX.IsUserSelectionEnabled = enabled;
            AreaMain.CursorX.AxisType = AxisType.Primary;

            if (zoomAllXAxis)
            {
                XAxis2.ScaleView.Zoomable = enabled;
            }

            YAxis.ScaleView.Zoomable = enabled;

            AreaMain.CursorY.IsUserSelectionEnabled = enabled;
            AreaMain.CursorY.AxisType = AxisType.Primary;

            if (zoomAllYAxis)
            {
                YAxis2.ScaleView.Zoomable = enabled;
            }

            IsZoomEnabled = enabled;
        }

        public void UndoZoom()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { UndoZoom(); });
            }
            else
            {
                if (zoomAllXAxis)
                {
                    XAxis.ScaleView.ZoomReset();
                    XAxis2.ScaleView.ZoomReset();
                }
                else
                {
                    switch (AreaMain.CursorX.AxisType)
                    {
                        case AxisType.Primary:
                            XAxis.ScaleView.ZoomReset();
                            break;
                        default:
                            XAxis2.ScaleView.ZoomReset();
                            break;
                    }
                }

                if (zoomAllYAxis)
                {
                    YAxis.ScaleView.ZoomReset();
                    YAxis2.ScaleView.ZoomReset();
                }
                else
                {
                    switch (AreaMain.CursorY.AxisType)
                    {
                        case AxisType.Primary:
                            YAxis.ScaleView.ZoomReset();
                            break;
                        default:
                            YAxis2.ScaleView.ZoomReset();
                            break;
                    }
                }
            }
        }

        public void ResetZoom()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { ResetZoom(); });
            }
            else
            {
                if (zoomAllXAxis)
                {
                    XAxis.ScaleView.ZoomReset(0);
                    XAxis2.ScaleView.ZoomReset(0);
                }
                else
                {
                    switch (AreaMain.CursorX.AxisType)
                    {
                        case AxisType.Primary:
                            XAxis.ScaleView.ZoomReset(0);
                            break;
                        default:
                            XAxis2.ScaleView.ZoomReset(0);
                            break;
                    }
                }

                if (zoomAllYAxis)
                {
                    YAxis.ScaleView.ZoomReset(0);
                    YAxis2.ScaleView.ZoomReset(0);
                }
                else
                {
                    switch (AreaMain.CursorY.AxisType)
                    {
                        case AxisType.Primary:
                            YAxis.ScaleView.ZoomReset(0);
                            break;
                        default:
                            YAxis2.ScaleView.ZoomReset(0);
                            break;
                    }
                }
            }
        }

        private void SetXCursorEnabled(bool enabled)
        {
            AreaMain.CursorX.IsUserSelectionEnabled = false;

            if (!enabled)
            {
                AreaMain.CursorX.SetCursorPosition(double.NaN);
            }
        }

        private void SetYCursorEnabled(bool enabled)
        {
            AreaMain.CursorY.IsUserSelectionEnabled = false;

            if (!enabled)
            {
                AreaMain.CursorY.SetCursorPosition(double.NaN);
            }
        }

        public Series GetCursorTarget()
        {
            return cursorTargetSeries;
        }

        private void SetCursorTarget()
        {
            if (AreaMain.CursorX.IsUserEnabled || AreaMain.CursorY.IsUserEnabled)
            {
                double xValue = AreaMain.CursorX.Position;
                double yValue = AreaMain.CursorY.Position;

                double diff = double.MaxValue;
                int dataPointIndex = 0;

                /// find x value position
                for (int len = Series.Count, i = 0; i < len; i++)
                {
                    for (int plen = Series[i].Points.Count, p = 0; p < plen; p++)
                    {
                        double currentDiff = Math.Abs(xValue - Series[i].Points[p].XValue);
                        if (currentDiff < diff)
                        {
                            diff = currentDiff;
                            dataPointIndex = p;

                        }
                    }
                }

                /// find series index
                diff = double.MaxValue;
                int seriesIndex = 0;
                for (int len = Series.Count, i = 0; i < len; i++)
                {
                    if (Series[i].Points.Count > dataPointIndex)
                    {
                        double currentDiff = Math.Abs(yValue - Series[i].Points[dataPointIndex].YValues[0]);
                        if (currentDiff < diff)
                        {
                            diff = currentDiff;
                            seriesIndex = i;
                        }
                    }
                }

                SetCursorTarget(Series[seriesIndex].Name);
            }
        }

        public void SetCursorTarget(string seriesName)
        {
            if (!IsNullSeries(seriesName))
            {
                cursorTargetSeries = Series[seriesName];

                int count = cursorTargetSeries.Points.Count;
                cursorTargetSeriesXValues = new double[count];

                for (int i = 0; i < count; i++)
                {
                    cursorTargetSeriesXValues[i] = cursorTargetSeries.Points[i].XValue;
                }

                SetCursorPositionByValue(AreaMain.CursorX.Position);
            }
            else
            {
                cursorTargetSeries = null;
                cursorTargetSeriesXValues = null;
            }
        }

        public void SetXAxisLabel(string label)
        {
            XAxis.Title = label;
        }

        public void SetXAxisRange(double min, double max)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetXAxisRange(min, max);
                });
            }
            else
            {
                XAxis.Minimum = min;
                XAxis.Maximum = max;

                fixedXAxisMinimum = min;
                fixedXAxisMaximum = max;
            }
        }

        public void SetXAxis2Label(string label)
        {
            XAxis2.Title = label;
        }

        public void SetXAxis2Range(double min, double max)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetXAxis2Range(min, max);
                });
            }
            else
            {
                XAxis2.Minimum = min;
                XAxis2.Maximum = max;

                fixedXAxis2Minimum = min;
                fixedXAxis2Maximum = max;
            }
        }

        public void SetYAxisLabel(string label)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetYAxisLabel(label);
                });
            }
            else
            {
                YAxis.Title = label;
            }
        }

        public void SetYAxisRange(double min, double max)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetYAxisRange(min, max);
                });
            }
            else
            {
                YAxis.Minimum = min;
                YAxis.Maximum = max;

                fixedYAxisMinimum = min;
                fixedYAxisMaximum = max;
            }
        }

        public void SetYAxis2Label(string label)
        {
            YAxis2.Title = label;
        }

        public void SetYAxis2Range(double min, double max)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    SetYAxis2Range(min, max);
                });
            }
            else
            {
                YAxis2.Minimum = min;
                YAxis2.Maximum = max;

                fixedYAxis2Minimum = min;
                fixedYAxis2Maximum = max;
            }
        }

        public void SetLegendVisible(bool visible, string name = null)
        {
            if (name == null)
            {
                for (int len = Legends.Count, i = 0; i < len; i++)
                {
                    Legends[i].Enabled = visible;
                }
            }
            else
            {
                Legend legend = Legends.FindByName(name);
                if (legend != null)
                {
                    legend.Enabled = visible;
                }
            }
        }

        public void SeriesToFront(Series targetSeries)
        {
            if (Series.Contains(targetSeries))
            {
                Series.Remove(targetSeries);
                Series.Insert(Series.Count - 1, targetSeries);
            }
        }

        /// <summary>
        /// make series points gap
        /// </summary>
        /// <param name="series"></param>
        public void CheckSeriesEmptyPoint(Series series)
        {
            if (series.Points.Count > 1)
            {
                double prevInterval = series.Points[1].XValue - series.Points[0].XValue;
                double prevValue = series.Points[0].XValue;

                for (int xlen = series.Points.Count - 1, x = 2; x < xlen; x++)
                {
                    double currentValue = series.Points[x].XValue;

                    double interval = currentValue - prevValue;

                    if (interval > prevInterval * 2)
                    {
                        series.Points[x].IsEmpty = true;
                    }
                    prevValue = currentValue;
                    prevInterval = interval;
                }
            }
        }

        public void CheckSeriesEmptyPoint()
        {
            for (int len = Series.Count, i = 0; i < len; i++)
            {
                CheckSeriesEmptyPoint(Series[i]);
            }
        }

        public void CaptureToClipboard()
        {
            MemoryStream ms = new MemoryStream();
            SaveImage(ms, ChartImageFormat.Bmp);
            Bitmap bmp = new Bitmap(ms);
            Clipboard.SetDataObject(bmp);
        }

        public List<DataPoint> GetPeakPoints(int seriesIndex, double slope)
        {
            if (Series.Count > seriesIndex)
            {
                return GetPeakPoints(Series[seriesIndex], slope);
            }
            return null;
        }

        public List<DataPoint> GetPeakPoints(string seriesName, double slope)
        {
            if (Series.FindByName(seriesName) != null)
            {
                return GetPeakPoints(Series[seriesName], slope);
            }
            return null;
        }

        public List<DataPoint> GetPeakPoints(Series series, double slope)
        {
            List<DataPoint> ret = new List<DataPoint>();
            List<DiffPoint> diffs = new List<DiffPoint>();

            List<int> indexes = new List<int>();

            for (int plen = series.Points.Count - 1, p = 0; p < plen; p++)
            {
                double y = Math.Atan2(series.Points[p + 1].XValue / series.Points[p].XValue,
                    series.Points[p + 1].YValues[0] / series.Points[p].YValues[0]);

                diffs.Add(new DiffPoint
                {
                    X = p,
                    Y = (y * 180) / Math.PI
                });
            }

            for (int len = diffs.Count - 1, i = 0; i < len; i++)
            {
                if (diffs[i].Y < 45 && diffs[i + 1].Y > 45)
                {
                    if (Math.Abs(diffs[i + 1].Y) > slope * 90)
                    {
                        indexes.Add(i + 1);
                    }
                }
            }

            for (int len = indexes.Count, i = 0; i < len; i++)
            {
                ret.Add(series.Points[indexes[i]]);
            }

            return ret;
        }

        public static void Export(DjsmChart chart)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Comma separated values (*.csv)|*.csv",
                DefaultExt = "csv",
                AddExtension = true
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string path = dlg.FileName;
                List<string> sb = new List<string>();
                /// column: filename
                /// row: elapsed time

                int rowIndex = -1;
                int columnIndex = 0;

                string[] tempLine;
                while (true)
                {
                    bool finished = true;

                    tempLine = new string[chart.Series.Count + 1];
                    if (rowIndex == -1)
                    {
                        tempLine[0] = "";
                        columnIndex = 1;
                        for (int il = chart.Series.Count, i = 0; i < il; i++)
                        {
                            if (chart.Series[i].Name.Equals(DUMMY_SERIES_NAME))
                            {
                                continue;
                            }
                            tempLine[columnIndex] = $"{chart.Series[i].Name}";
                            columnIndex++;
                        }
                        finished = false;
                    }
                    else
                    {
                        tempLine[0] = "";
                        columnIndex = 1;
                        for (int il = chart.Series.Count, i = 0; i < il; i++)
                        {
                            if (chart.Series[i].Name.Equals(DUMMY_SERIES_NAME))
                            {
                                continue;
                            }
                            if (chart.Series[i].Points.Count > rowIndex)
                            {
                                if (tempLine[0] == "")
                                {
                                    tempLine[0] = chart.Series[i].Points[rowIndex].XValue.ToString();
                                }

                                tempLine[columnIndex] = $"{chart.Series[i].Points[rowIndex].YValues[0]}";
                                finished = false;
                            }
                            else
                            {
                                tempLine[columnIndex] = "";
                            }
                            columnIndex++;
                        }
                    }

                    sb.Add(string.Join(",", tempLine));
                    rowIndex++;

                    if (finished)
                    {
                        break;
                    }
                }

                File.WriteAllLines(path, sb.ToArray());

                DjsmToastMessage.Show("Export completed.", 3000, DjsmToastMessage.Theme.DARK);
            }
        }
        public static int FindPointByXValue(double xValue, int low, int high, Series targetSeries)
        {
            if (targetSeries != null && targetSeries.Points.Count > 0)
            {
                if (low > high)
                {
                    double lowValue = Math.Abs(targetSeries.Points[low].XValue - xValue);
                    double highValue = Math.Abs(targetSeries.Points[high].XValue - xValue);
                    if (lowValue > highValue)
                    {
                        return high;
                    }
                    else
                    {
                        return low;
                    }
                }

                int index = Math.Min((low + high) / 2, targetSeries.Points.Count - 1);

                if (targetSeries.Points[index].XValue > xValue)
                {
                    return FindPointByXValue(xValue, low, index - 1, targetSeries);
                }
                else if (targetSeries.Points[index].XValue < xValue)
                {
                    return FindPointByXValue(xValue, index + 1, high, targetSeries);
                }
                else
                {
                    return index;
                }
            }
            return -1;
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        internal class DiffPoint
        {
            public int X;
            public double Y;
        }

        public class LineObject : object
        {
            public double Value { get; set; }
            public Color Color { get; set; }
        }

        public class ValueComparer : Comparer<DataPoint>
        {
            public enum SortBase { X, Y }
            private readonly SortOrder order;
            private readonly SortBase sortBase;

            public ValueComparer(SortOrder order, SortBase sortBase) : base()
            {
                this.order = order;
                this.sortBase = sortBase;
            }

            public override int Compare(DataPoint dp1, DataPoint dp2)
            {
                double v1 = sortBase == SortBase.X ? dp1.XValue : dp1.YValues[0];
                double v2 = sortBase == SortBase.X ? dp2.XValue : dp2.YValues[0];

                int result = v1 < v2 ? -1 : v1 > v2 ? 1 : 0;
                if (order == SortOrder.Descending)
                {
                    result *= -1;
                }
                return result;
            }
        }

        public class BoundaryTextObject
        {
            public float X;
            public float Y;
            public string Text;
            public Font Font;
            public Color FontColor;
        }
    }
}
