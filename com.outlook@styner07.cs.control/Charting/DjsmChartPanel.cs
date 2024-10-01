namespace com.outlook_styner07.cs.control.Charting
{
    public class DjsmChartPanel : UserControl
    {
        public DjsmChart chart;

        private bool isMaximized;

        private ToolStrip tlb;
        private ToolStripButton snapToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel titleToolStripLabel;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton maximizeToolStripButton;

        public DjsmChartPanel(DjsmChart chart)
        {
            InitializeComponent();

            Dock = DockStyle.Fill;

            toolStripLabel1.BackColor = DjsmColorTable.SecondaryDark;

            tlb.MouseDown += Tlb_MouseDown;
            for (int len = tlb.Items.Count, i = 0; i < len; i ++)
            {
                if (!(tlb.Items[i] is ToolStripButton))
                {
                    tlb.Items[i].MouseDown += Tlb_MouseDown;
                }
            }

            isMaximized = false;

            this.chart = chart;
            Controls.Add(chart);
        }

        private void Tlb_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(this, DragDropEffects.Move);
        }

        private void MaximizeToolStripButton_Click(object sender, EventArgs e)
        {
            if (isMaximized) //to normal
            {
                OnMaximize(new MaximizedEventArgs() { Maximized = false });
            }
            else //to maximize
            {
                OnMaximize(new MaximizedEventArgs() { Maximized = true });
            }

            isMaximized = !isMaximized;
            maximizeToolStripButton.Image = isMaximized
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
                chart.SaveImage(sfd.FileName, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
            }
        }

        public void SetTitle(string title)
        {
            titleToolStripLabel.Text = title;
        }

        public void HideTitleBar()
        {
            tlb.Hide();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DjsmChartPanel));
            this.tlb = new System.Windows.Forms.ToolStrip();
            this.maximizeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.snapToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.titleToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.tlb.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlb
            // 
            this.tlb.BackColor = System.Drawing.Color.White;
            this.tlb.Font = new System.Drawing.Font("Arial", 9F);
            this.tlb.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maximizeToolStripButton,
            this.toolStripSeparator1,
            this.snapToolStripButton,
            this.toolStripLabel1,
            this.titleToolStripLabel});
            this.tlb.Location = new System.Drawing.Point(0, 0);
            this.tlb.Name = "tlb";
            this.tlb.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tlb.Size = new System.Drawing.Size(525, 31);
            this.tlb.TabIndex = 0;
            this.tlb.Text = "toolStrip1";
            // 
            // maximizeToolStripButton
            // 
            this.maximizeToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.maximizeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.maximizeToolStripButton.Image =  Properties.Resources.Enlarge;
            this.maximizeToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.maximizeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.maximizeToolStripButton.Name = "maximizeToolStripButton";
            this.maximizeToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.maximizeToolStripButton.Text = "Maximize";
            this.maximizeToolStripButton.ToolTipText = "Maximize/Restore";
            this.maximizeToolStripButton.Click += new System.EventHandler(this.MaximizeToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // snapToolStripButton
            // 
            this.snapToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.snapToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.snapToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("snapToolStripButton.Image")));
            this.snapToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.snapToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.snapToolStripButton.Name = "snapToolStripButton";
            this.snapToolStripButton.Size = new System.Drawing.Size(28, 28);
            this.snapToolStripButton.Text = "Snap";
            this.snapToolStripButton.Click += new System.EventHandler(this.SnapToolStripButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(5, 26);
            // 
            // titleToolStripLabel
            // 
            this.titleToolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.titleToolStripLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleToolStripLabel.Name = "titleToolStripLabel";
            this.titleToolStripLabel.Size = new System.Drawing.Size(74, 28);
            this.titleToolStripLabel.Text = "Chart Name";
            // 
            // NntChartPanel
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tlb);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Name = "NntChartPanel";
            this.Size = new System.Drawing.Size(525, 460);
            this.tlb.ResumeLayout(false);
            this.tlb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public event EventHandler<MaximizedEventArgs> Maximized;
        protected virtual void OnMaximize(MaximizedEventArgs args)
        {
            Maximized?.Invoke(this, args);
        }

        public class MaximizedEventArgs : EventArgs
        {
            public bool Maximized { get; set; }
        }
    }
}
