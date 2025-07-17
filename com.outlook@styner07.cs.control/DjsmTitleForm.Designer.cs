namespace com.outlook_styner07.cs.control
{
    partial class DjsmTitleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            djsmToolStrip1 = new com.outlook_styner07.cs.control.Container.DjsmToolStrip();
            lblWindowTitle = new ToolStripLabel();
            btnCloseWindow = new ToolStripButton();
            btnMaximizeWindow = new ToolStripButton();
            btnMinimizeWindow = new ToolStripButton();
            djsmToolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // djsmToolStrip1
            // 
            djsmToolStrip1.BorderColor = Color.FromArgb(80, 119, 120, 123);
            djsmToolStrip1.BorderSides = ToolStripStatusLabelBorderSides.Bottom;
            djsmToolStrip1.Font = new Font("Arial", 9F);
            djsmToolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            djsmToolStrip1.Items.AddRange(new ToolStripItem[] { lblWindowTitle, btnCloseWindow, btnMaximizeWindow, btnMinimizeWindow });
            djsmToolStrip1.Location = new Point(1, 1);
            djsmToolStrip1.Name = "djsmToolStrip1";
            djsmToolStrip1.Padding = new Padding(3);
            djsmToolStrip1.Size = new Size(798, 34);
            djsmToolStrip1.TabIndex = 0;
            djsmToolStrip1.Text = "djsmToolStrip1";
            djsmToolStrip1.WindowDragEnabled = true;
            // 
            // lblWindowTitle
            // 
            lblWindowTitle.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblWindowTitle.Name = "lblWindowTitle";
            lblWindowTitle.Padding = new Padding(10, 0, 0, 0);
            lblWindowTitle.Size = new Size(85, 25);
            lblWindowTitle.Text = "window title";
            // 
            // btnCloseWindow
            // 
            btnCloseWindow.Alignment = ToolStripItemAlignment.Right;
            btnCloseWindow.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnCloseWindow.Image = Properties.Resources.Close_Window;
            btnCloseWindow.ImageScaling = ToolStripItemImageScaling.None;
            btnCloseWindow.ImageTransparentColor = Color.Magenta;
            btnCloseWindow.Margin = new Padding(0);
            btnCloseWindow.Name = "btnCloseWindow";
            btnCloseWindow.Size = new Size(28, 28);
            // 
            // btnMaximizeWindow
            // 
            btnMaximizeWindow.Alignment = ToolStripItemAlignment.Right;
            btnMaximizeWindow.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnMaximizeWindow.Image = Properties.Resources.Maximize_Window;
            btnMaximizeWindow.ImageScaling = ToolStripItemImageScaling.None;
            btnMaximizeWindow.ImageTransparentColor = Color.Magenta;
            btnMaximizeWindow.Margin = new Padding(0);
            btnMaximizeWindow.Name = "btnMaximizeWindow";
            btnMaximizeWindow.Size = new Size(28, 28);
            // 
            // btnMinimizeWindow
            // 
            btnMinimizeWindow.Alignment = ToolStripItemAlignment.Right;
            btnMinimizeWindow.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnMinimizeWindow.Image = Properties.Resources.Minimize_Window;
            btnMinimizeWindow.ImageScaling = ToolStripItemImageScaling.None;
            btnMinimizeWindow.ImageTransparentColor = Color.Magenta;
            btnMinimizeWindow.Margin = new Padding(0);
            btnMinimizeWindow.Name = "btnMinimizeWindow";
            btnMinimizeWindow.Size = new Size(28, 28);
            // 
            // DjsmTitleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(djsmToolStrip1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DjsmTitleForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DjsmTitleForm";
            djsmToolStrip1.ResumeLayout(false);
            djsmToolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Container.DjsmToolStrip djsmToolStrip1;
        private ToolStripLabel lblWindowTitle;
        private ToolStripButton btnCloseWindow;
        private ToolStripButton btnMaximizeWindow;
        private ToolStripButton btnMinimizeWindow;
    }
}