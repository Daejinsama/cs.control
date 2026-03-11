namespace com.outlook_styner07.cs.control.Charting
{
    public class DjsmChartPanel : UserControl
    {
        #region Constructors
        public DjsmChartPanel()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;

            tsl.BackColor = DjsmColorTable.SecondaryDark;
            tlb.MouseDown += tlb_MouseDown;

            for (int len = tlb.Items.Count, i = 0; i < len; i++)
            {
                if (!(tlb.Items[i] is ToolStripButton))
                {
                    tlb.Items[i].MouseDown += tlb_MouseDown;
                }
            }

            _isMaximized = false;

            Chart = new DjsmChart { Dock = DockStyle.Fill };
            Controls.Add(Chart);
        }

        #endregion

        #region Types
        #endregion

        #region Fields
        private bool _isMaximized;

        protected ToolStrip tlb;
        protected ToolStripButton tsbSnap;
        protected ToolStripSeparator toolStripSeparator1;
        protected ToolStripLabel tslTitle;
        protected ToolStripLabel tsl;
        protected ToolStripButton tlbMaximize;

        public event EventHandler<MaximizedEventArgs> Maximized;
        #endregion

        #region Properties
        public DjsmChart Chart { get; set; }
        #endregion

        #region Methods
        private void tlb_MouseDown(object? sender, MouseEventArgs e)
        {
            DoDragDrop(this, DragDropEffects.Move);
        }

        private void MaximizeToolStripButton_Click(object sender, EventArgs e)
        {
            if (_isMaximized) //to normal
            {
                OnMaximize(new MaximizedEventArgs() { Maximized = false });
            }
            else //to maximize
            {
                OnMaximize(new MaximizedEventArgs() { Maximized = true });
            }

            _isMaximized = !_isMaximized;
            tlbMaximize.Image = _isMaximized
                ? Properties.Resources.Compress
                : Properties.Resources.Enlarge;
        }

        private void SnapToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Chart Image Format(*.png)|*.png",
                DefaultExt = "png"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Chart.SaveImage(sfd.FileName, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
            }
        }

        public void SetTitle(string title)
        {
            tslTitle.Text = title;
        }

        public void HideTitleBar()
        {
            tlb.Hide();
        }

        protected virtual void OnMaximize(MaximizedEventArgs args)
        {
            Maximized?.Invoke(this, args);
        }

        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(DjsmChartPanel));
            tlb = new ToolStrip();
            tlbMaximize = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsbSnap = new ToolStripButton();
            tsl = new ToolStripLabel();
            tslTitle = new ToolStripLabel();
            tlb.SuspendLayout();
            SuspendLayout();
            // 
            // tlb
            // 
            tlb.BackColor = Color.White;
            tlb.Font = new Font("Arial", 9F);
            tlb.GripStyle = ToolStripGripStyle.Hidden;
            tlb.Items.AddRange(new ToolStripItem[] { tlbMaximize, toolStripSeparator1, tsbSnap, tsl, tslTitle });
            tlb.Location = new Point(0, 0);
            tlb.Name = "tlb";
            tlb.RenderMode = ToolStripRenderMode.System;
            tlb.Size = new Size(525, 31);
            tlb.TabIndex = 0;
            tlb.Text = "toolStrip1";
            // 
            // tlbMaximize
            // 
            tlbMaximize.Alignment = ToolStripItemAlignment.Right;
            tlbMaximize.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tlbMaximize.Image = Properties.Resources.Enlarge;
            tlbMaximize.ImageScaling = ToolStripItemImageScaling.None;
            tlbMaximize.ImageTransparentColor = Color.Magenta;
            tlbMaximize.Name = "tlbMaximize";
            tlbMaximize.Size = new Size(28, 28);
            tlbMaximize.Text = "Maximize";
            tlbMaximize.ToolTipText = "Maximize/Restore";
            tlbMaximize.Click += MaximizeToolStripButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator1.Margin = new Padding(0, 3, 0, 3);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // tsbSnap
            // 
            tsbSnap.Alignment = ToolStripItemAlignment.Right;
            tsbSnap.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbSnap.Image = (Image)resources.GetObject("tsbSnap.Image");
            tsbSnap.ImageScaling = ToolStripItemImageScaling.None;
            tsbSnap.ImageTransparentColor = Color.Magenta;
            tsbSnap.Name = "tsbSnap";
            tsbSnap.Size = new Size(28, 28);
            tsbSnap.Text = "Snap";
            tsbSnap.Click += SnapToolStripButton_Click;
            // 
            // tsl
            // 
            tsl.AutoSize = false;
            tsl.BackColor = SystemColors.ControlDark;
            tsl.Name = "tsl";
            tsl.Size = new Size(5, 26);
            // 
            // tslTitle
            // 
            tslTitle.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tslTitle.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tslTitle.Name = "tslTitle";
            tslTitle.Size = new Size(74, 28);
            tslTitle.Text = "Chart Name";
            // 
            // DjsmChartPanel
            // 
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(tlb);
            DoubleBuffered = true;
            Font = new Font("Arial", 9F);
            Name = "DjsmChartPanel";
            Size = new Size(525, 460);
            tlb.ResumeLayout(false);
            tlb.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion

        public class MaximizedEventArgs : EventArgs
        {
            public bool Maximized { get; set; }
        }
    }
}
