namespace Playground
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            djsmImagePanel1 = new com.outlook_styner07.cs.control.Container.DjsmImagePanel();
            djsmButton1 = new com.outlook_styner07.cs.control.Button.DjsmButton();
            djsmSplitContainer1 = new com.outlook_styner07.cs.control.Container.DjsmSplitContainer();
            djsmButton2 = new com.outlook_styner07.cs.control.Button.DjsmButton();
            ((System.ComponentModel.ISupportInitialize)djsmSplitContainer1).BeginInit();
            djsmSplitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // djsmImagePanel1
            // 
            djsmImagePanel1.AllowDrop = true;
            djsmImagePanel1.BackColor = SystemColors.ActiveCaption;
            djsmImagePanel1.Image = null;
            djsmImagePanel1.Location = new Point(4, 70);
            djsmImagePanel1.Name = "djsmImagePanel1";
            djsmImagePanel1.PanEnabled = true;
            djsmImagePanel1.Size = new Size(515, 453);
            djsmImagePanel1.TabIndex = 1;
            // 
            // djsmButton1
            // 
            djsmButton1.BackColor = Color.Yellow;
            djsmButton1.Location = new Point(814, 74);
            djsmButton1.Name = "djsmButton1";
            djsmButton1.Radius = 11;
            djsmButton1.RenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            djsmButton1.Size = new Size(107, 55);
            djsmButton1.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            djsmButton1.TabIndex = 2;
            djsmButton1.Text = "fittoframe";
            djsmButton1.UseVisualStyleBackColor = false;
            djsmButton1.Click += djsmButton1_Click;
            // 
            // djsmSplitContainer1
            // 
            djsmSplitContainer1.BorderColor = Color.FromArgb(80, 119, 120, 123);
            djsmSplitContainer1.Location = new Point(689, 186);
            djsmSplitContainer1.Name = "djsmSplitContainer1";
            djsmSplitContainer1.Size = new Size(343, 269);
            djsmSplitContainer1.SplitterBorderColor = Color.White;
            djsmSplitContainer1.SplitterColor = Color.FromArgb(119, 120, 123);
            djsmSplitContainer1.SplitterDistance = 177;
            djsmSplitContainer1.SplitterHandleColor = Color.White;
            djsmSplitContainer1.SplitterWidth = 0;
            djsmSplitContainer1.TabIndex = 3;
            // 
            // djsmButton2
            // 
            djsmButton2.BackColor = Color.Green;
            djsmButton2.Location = new Point(614, 74);
            djsmButton2.Name = "djsmButton2";
            djsmButton2.Radius = 11;
            djsmButton2.RenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            djsmButton2.Size = new Size(150, 75);
            djsmButton2.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            djsmButton2.TabIndex = 4;
            djsmButton2.Text = "djsmButton2";
            djsmButton2.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1221, 540);
            Controls.Add(djsmButton2);
            Controls.Add(djsmSplitContainer1);
            Controls.Add(djsmButton1);
            Controls.Add(djsmImagePanel1);
            Name = "Form1";
            Text = "Form1";
            Controls.SetChildIndex(djsmImagePanel1, 0);
            Controls.SetChildIndex(djsmButton1, 0);
            Controls.SetChildIndex(djsmSplitContainer1, 0);
            Controls.SetChildIndex(djsmButton2, 0);
            ((System.ComponentModel.ISupportInitialize)djsmSplitContainer1).EndInit();
            djsmSplitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private com.outlook_styner07.cs.control.Container.DjsmImagePanel djsmImagePanel1;
        private com.outlook_styner07.cs.control.Button.DjsmButton djsmButton1;
        private com.outlook_styner07.cs.control.Container.DjsmSplitContainer djsmSplitContainer1;
        private com.outlook_styner07.cs.control.Button.DjsmButton djsmButton2;
    }
}
