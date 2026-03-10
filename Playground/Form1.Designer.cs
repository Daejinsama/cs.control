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
            djsmPropertyGrid1 = new com.outlook_styner07.cs.control.Data.DjsmPropertyGrid();
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
            djsmButton1.BackColor = Color.Gray;
            djsmButton1.Location = new Point(4, 41);
            djsmButton1.Name = "djsmButton1";
            djsmButton1.Radius = 11;
            djsmButton1.RenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            djsmButton1.Size = new Size(75, 23);
            djsmButton1.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            djsmButton1.TabIndex = 2;
            djsmButton1.Text = "fittoframe";
            djsmButton1.UseVisualStyleBackColor = false;
            djsmButton1.Click += djsmButton1_Click;
            // 
            // djsmPropertyGrid1
            // 
            djsmPropertyGrid1.CategoryForeColor = Color.White;
            djsmPropertyGrid1.CellHeight = 80;
            djsmPropertyGrid1.Font = new Font("Arial", 9F);
            djsmPropertyGrid1.HelpVisible = false;
            djsmPropertyGrid1.LineColor = SystemColors.ControlDark;
            djsmPropertyGrid1.Location = new Point(671, 126);
            djsmPropertyGrid1.Name = "djsmPropertyGrid1";
            djsmPropertyGrid1.ReadOnly = false;
            djsmPropertyGrid1.Size = new Size(279, 311);
            djsmPropertyGrid1.TabIndex = 3;
            djsmPropertyGrid1.ToolbarVisible = false;
            djsmPropertyGrid1.ViewBackColor = SystemColors.Control;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1221, 540);
            Controls.Add(djsmPropertyGrid1);
            Controls.Add(djsmButton1);
            Controls.Add(djsmImagePanel1);
            Name = "Form1";
            Text = "Form1";
            Controls.SetChildIndex(djsmImagePanel1, 0);
            Controls.SetChildIndex(djsmButton1, 0);
            Controls.SetChildIndex(djsmPropertyGrid1, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private com.outlook_styner07.cs.control.Container.DjsmImagePanel djsmImagePanel1;
        private com.outlook_styner07.cs.control.Button.DjsmButton djsmButton1;
        private com.outlook_styner07.cs.control.Data.DjsmPropertyGrid djsmPropertyGrid1;
    }
}
