using System.Windows.Forms.DataVisualization.Charting;

namespace com.outlook_styner07.cs.control.Charting
{
    public partial class SeriesCustomizingDialog : Form
    {
        #region Constructors
        public SeriesCustomizingDialog(DjsmCustomStyleSeries oldSeries)
        {
            _series = new DjsmCustomStyleSeries { ChartType = oldSeries.ChartType };
            _series.ApplyStyle(oldSeries.ExtractStyle());

            InitializeComponent();
            InitializeSeriesComponent();
            InitializePreviewChart();

            LoadOldSeriesStyle();
            RegisterControlEvent();
            titleToolStripLabel.Text = string.Format("{0}_{1}", titleToolStripLabel.Text, oldSeries.Name);
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private DjsmChart _chartPreview;
        private DjsmCustomStyleSeries _series;

        private readonly object[] WIDTH = { 1, 2, 3, 4, 5 };
        private readonly object[] STEP = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private readonly object[] SIZE = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void LoadOldSeriesStyle()
        {
            cmbLineDash.SelectedItem = _series.BorderDashStyle;
            cmbLineWidth.SelectedItem = _series.BorderWidth;
            btnLineColor.BackColor = _series.Color;

            cmbMarkerShape.SelectedItem = _series.MarkerStyle;
            cmbMarkerStep.SelectedItem = _series.MarkerStep;
            cmbMarkerSize.SelectedItem = _series.MarkerSize;
            btnMarkerColor.BackColor = _series.MarkerColor;
            cmbMarkerBorderWidth.SelectedItem = _series.MarkerBorderWidth;
            btnMarkerBorderColor.BackColor = _series.MarkerBorderColor;
        }

        private void InitializeSeriesComponent()
        {
            cmbLineDash.Items.Clear();
            cmbLineDash.Items.AddRange([
                ChartDashStyle.Dash,
                ChartDashStyle.DashDot,
                ChartDashStyle.DashDotDot,
                ChartDashStyle.Dot,
                ChartDashStyle.NotSet,
                ChartDashStyle.Solid]);

            cmbLineWidth.Items.Clear();
            cmbLineWidth.Items.AddRange(WIDTH);
            cmbLineWidth.SelectedIndex = 0;

            cmbMarkerShape.Items.Clear();
            cmbMarkerShape.Items.AddRange([
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
            ]);

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
                    _series.BorderDashStyle = (ChartDashStyle)cmbLineDash.SelectedItem;
                }
            };

            cmbLineWidth.SelectedIndexChanged += delegate
            {
                _series.BorderWidth = (int)WIDTH[cmbLineWidth.SelectedIndex];
            };

            btnLineColor.Click += (sender, e) =>
            {
                Color color = ((System.Windows.Forms.Button)sender).BackColor;
                GetColorFromPicker(ref color);
                btnLineColor.BackColor = color;
                _series.Color = color;
            };

            cmbMarkerShape.SelectedValueChanged += delegate
            {
                if (cmbMarkerShape.SelectedItem != null)
                {
                    _series.MarkerStyle = (MarkerStyle)(cmbMarkerShape.SelectedItem);
                }
            };

            cmbMarkerStep.SelectedIndexChanged += delegate
            {
                _series.MarkerStep = (int)STEP[cmbMarkerStep.SelectedIndex];
            };

            cmbMarkerSize.SelectedIndexChanged += delegate
            {
                _series.MarkerSize = (int)SIZE[cmbMarkerSize.SelectedIndex];
            };

            btnMarkerColor.Click += (sender, e) =>
            {
                Color color = ((System.Windows.Forms.Button)sender).BackColor;
                GetColorFromPicker(ref color);
                btnMarkerColor.BackColor = color;
                _series.MarkerColor = color;
            };

            cmbMarkerBorderWidth.SelectedIndexChanged += delegate
            {
                _series.MarkerBorderWidth = (int)WIDTH[cmbMarkerBorderWidth.SelectedIndex];
            };

            btnMarkerBorderColor.Click += (sender, e) =>
            {
                Color color = ((System.Windows.Forms.Button)sender).BackColor;
                GetColorFromPicker(ref color);
                btnMarkerBorderColor.BackColor = color;
                _series.MarkerBorderColor = color;
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
            _chartPreview = new DjsmChart
            {
                Dock = DockStyle.Fill,
                BackColor = this.BackColor,
            };

            _chartPreview.AreaMain.Visible = false;

            Legend legend = _chartPreview.Legends.Add("Style");
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

            gbxPreview.Controls.Add(_chartPreview);
            _chartPreview.Series.Add(_series);
        }

        public SeriesStyleObject GetSeriesStyle()
        {
            return _series.ExtractStyle();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

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
}
