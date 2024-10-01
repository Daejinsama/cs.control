using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace com.outlook_styner07.cs.control.Charting
{
    public partial class SeriesCustomizingDialog : Form
    {
        private DjsmChart chartPreview;
        private DjsmCustomStyleSeries series;

        public SeriesCustomizingDialog(DjsmCustomStyleSeries oldSeries)
        {
            series = new DjsmCustomStyleSeries { ChartType = oldSeries.ChartType };
            series.ApplyStyle(oldSeries.ExtractStyle());

            InitializeComponent();
            InitializeSeriesComponent();
            InitializePreviewChart();

            LoadOldSeriesStyle();
            RegisterControlEvent();
            titleToolStripLabel.Text = string.Format("{0}_{1}", titleToolStripLabel.Text, oldSeries.Name);
        }

        private void LoadOldSeriesStyle()
        {
            cmbLineDash.SelectedItem = series.BorderDashStyle;
            cmbLineWidth.SelectedItem = series.BorderWidth;
            btnLineColor.BackColor = series.Color;

            cmbMarkerShape.SelectedItem = series.MarkerStyle;
            cmbMarkerStep.SelectedItem = series.MarkerStep;
            cmbMarkerSize.SelectedItem = series.MarkerSize;
            btnMarkerColor.BackColor = series.MarkerColor;
            cmbMarkerBorderWidth.SelectedItem = series.MarkerBorderWidth;
            btnMarkerBorderColor.BackColor = series.MarkerBorderColor;
        }

        private object[] WIDTH = { 1, 2, 3, 4, 5 };
        private object[] STEP = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private object[] SIZE = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private void InitializeSeriesComponent()
        {
            cmbLineDash.Items.Clear();
            cmbLineDash.Items.AddRange(new object[] {
                ChartDashStyle.Dash,
                ChartDashStyle.DashDot,
                ChartDashStyle.DashDotDot,
                ChartDashStyle.Dot,
                ChartDashStyle.NotSet,
                ChartDashStyle.Solid});

            cmbLineWidth.Items.Clear();
            cmbLineWidth.Items.AddRange(WIDTH);
            cmbLineWidth.SelectedIndex = 0;

            cmbMarkerShape.Items.Clear();
            cmbMarkerShape.Items.AddRange(new object[] {
                MarkerStyle.Circle,
                MarkerStyle.Cross,
                MarkerStyle.Diamond,
                MarkerStyle.None,
                MarkerStyle.Square,
                MarkerStyle.Star10,
                MarkerStyle.Star4,
                MarkerStyle.Star5,
                MarkerStyle.Star6,
                MarkerStyle.Triangle
            });

            cmbMarkerStep.Items.Clear();
            cmbMarkerStep.Items.AddRange(STEP);

            cmbMarkerSize.Items.Clear();
            cmbMarkerSize.Items.AddRange(SIZE);

            cmbMarkerBorderWidth.Items.Clear();
            cmbMarkerBorderWidth.Items.AddRange(WIDTH);
        }

        private void RegisterControlEvent()
        {
            cmbLineDash.SelectedValueChanged += delegate
            {
                if (cmbLineDash.SelectedItem != null)
                {
                    series.BorderDashStyle = (ChartDashStyle)cmbLineDash.SelectedItem;
                }
            };

            cmbLineWidth.SelectedIndexChanged += delegate
            {
                series.BorderWidth = (int)WIDTH[cmbLineWidth.SelectedIndex];
            };

            btnLineColor.Click += (object sender, EventArgs e) =>
            {
                Color color = (sender as System.Windows.Forms.Button).BackColor;
                GetColorFromPicker(ref color);
                btnLineColor.BackColor = color;
                series.Color = color;
            };

            cmbMarkerShape.SelectedValueChanged += delegate
            {
                if (cmbMarkerShape.SelectedItem != null)
                {
                    series.MarkerStyle = (MarkerStyle)(cmbMarkerShape.SelectedItem);
                }
            };

            cmbMarkerStep.SelectedIndexChanged += delegate
            {
                series.MarkerStep = (int)STEP[cmbMarkerStep.SelectedIndex];
            };

            cmbMarkerSize.SelectedIndexChanged += delegate
            {
                series.MarkerSize = (int)SIZE[cmbMarkerSize.SelectedIndex];
            };

            btnMarkerColor.Click += (object sender, EventArgs e) =>
            {
                Color color = (sender as System.Windows.Forms.Button).BackColor;
                GetColorFromPicker(ref color);
                btnMarkerColor.BackColor = color;
                series.MarkerColor = color;
            };

            cmbMarkerBorderWidth.SelectedIndexChanged += delegate
            {
                series.MarkerBorderWidth = (int)WIDTH[cmbMarkerBorderWidth.SelectedIndex];
            };

            btnMarkerBorderColor.Click += (object sender, EventArgs e) =>
            {
                Color color = (sender as System.Windows.Forms.Button).BackColor;
                GetColorFromPicker(ref color);
                btnMarkerBorderColor.BackColor = color;
                series.MarkerBorderColor = color;
            };
        }

        private void GetColorFromPicker(ref Color color)
        {
            ColorDialog dlgColor = new ColorDialog();
            if (dlgColor.ShowDialog() == DialogResult.OK)
            {
                color = dlgColor.Color;
            }
        }

        private void InitializePreviewChart()
        {
            chartPreview = new DjsmChart
            {
                Dock = DockStyle.Fill,
                BackColor = this.BackColor,
            };

            chartPreview.AreaMain.Visible = false;

            Legend legend = chartPreview.Legends.Add("Style");
            legend.Enabled = true;
            legend.Position.X = 0;
            legend.Position.Y = 0;
            legend.Position.Width = 100;
            legend.Position.Height = 100;
            legend.CellColumns.Add(new LegendCellColumn
            {
                ColumnType = LegendCellColumnType.SeriesSymbol,
                MinimumWidth = 200,
                MaximumWidth = 250
            });

            gbxPreview.Controls.Add(chartPreview);
            chartPreview.Series.Add(series);
        }

        public SeriesStyleObject GetSeriesStyle()
        {
            return series.ExtractStyle();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        #region dialog close when click out side
        //private const int WM_NACTIVATE = 0x86;
        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == WM_NACTIVATE)
        //    {
        //        if (isShown)
        //        {
        //            if (!RectangleToScreen(ClientRectangle).Contains(MousePosition))
        //            {
        //                DialogResult = DialogResult.Cancel;
        //                Close();
        //            }
        //        }
        //    }
        //}
        //private bool isShown = false;
        //protected override void OnShown(EventArgs e)
        //{
        //    base.OnShown(e);
        //    isShown = true;

        //}
        #endregion

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    public class SeriesStyleObject
    {
        public MarkerStyle MarkerStyle { get; set; } = MarkerStyle.None;
        public int MarkerStep { get; set; } = 1;
        public int MarkerSize { get; set; } = 3;
        public int MarkerColor { get; set; } = Color.Red.ToArgb();
        public int MarkerBorderColor { get; set; } = Color.Black.ToArgb();
        public int MarkerBorderWidth { get; set; } = 1;

        public int LineColor { get; set; } = Color.Empty.ToArgb();
        public int LineWidth { get; set; } = 1;
        public ChartDashStyle LineStyle { get; set; } = ChartDashStyle.Solid;
    }
}
