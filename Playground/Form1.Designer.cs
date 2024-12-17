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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            djsmImagePanel1 = new com.outlook_styner07.cs.control.Container.DjsmImagePanel();
            djsmProgressBar1 = new com.outlook_styner07.cs.control.Gauge.DjsmProgressBar();
            djsmRadioButton1 = new com.outlook_styner07.cs.control.Button.DjsmRadioButton();
            djsmCheckBox1 = new com.outlook_styner07.cs.control.Button.DjsmCheckBox();
            djsmRadioButton2 = new com.outlook_styner07.cs.control.Button.DjsmRadioButton();
            djsmImagePanel1.SuspendLayout();
            SuspendLayout();
            // 
            // djsmImagePanel1
            // 
            djsmImagePanel1.AllowDrop = true;
            djsmImagePanel1.Controls.Add(djsmRadioButton2);
            djsmImagePanel1.Controls.Add(djsmProgressBar1);
            djsmImagePanel1.Controls.Add(djsmRadioButton1);
            djsmImagePanel1.Controls.Add(djsmCheckBox1);
            djsmImagePanel1.Dock = DockStyle.Fill;
            djsmImagePanel1.Image = (Image)resources.GetObject("djsmImagePanel1.Image");
            djsmImagePanel1.Location = new Point(0, 0);
            djsmImagePanel1.Name = "djsmImagePanel1";
            djsmImagePanel1.PanEnabled = true;
            djsmImagePanel1.Size = new Size(800, 450);
            djsmImagePanel1.TabIndex = 0;
            djsmImagePanel1.Paint += djsmImagePanel1_Paint;
            // 
            // djsmProgressBar1
            // 
            djsmProgressBar1.IsFixedLabel = true;
            djsmProgressBar1.LabelDrawing = true;
            djsmProgressBar1.LabelText = "test";
            djsmProgressBar1.Location = new Point(227, 32);
            djsmProgressBar1.Name = "djsmProgressBar1";
            djsmProgressBar1.ProgressBarColor = Color.Orange;
            djsmProgressBar1.ProgressFont = new Font("Arial", 9F, FontStyle.Bold);
            djsmProgressBar1.ProgressFontColor = Color.Black;
            djsmProgressBar1.Size = new Size(351, 23);
            djsmProgressBar1.TabIndex = 2;
            djsmProgressBar1.Value = 30;
            // 
            // djsmRadioButton1
            // 
            djsmRadioButton1.AutoSize = true;
            djsmRadioButton1.BorderSides = ToolStripStatusLabelBorderSides.Bottom;
            djsmRadioButton1.DeselectedForeColor = Color.Black;
            djsmRadioButton1.Font = new Font("맑은 고딕", 9F);
            djsmRadioButton1.Location = new Point(407, 398);
            djsmRadioButton1.Name = "djsmRadioButton1";
            djsmRadioButton1.SelectedForeColor = Color.Black;
            djsmRadioButton1.Size = new Size(200, 19);
            djsmRadioButton1.TabIndex = 1;
            djsmRadioButton1.TabStop = true;
            djsmRadioButton1.Text = "Save the recently used sequence";
            djsmRadioButton1.UseVisualStyleBackColor = false;
            // 
            // djsmCheckBox1
            // 
            djsmCheckBox1.AutoSize = true;
            djsmCheckBox1.CheckedForeColor = Color.Black;
            djsmCheckBox1.Font = new Font("Arial", 9F);
            djsmCheckBox1.Location = new Point(89, 398);
            djsmCheckBox1.Name = "djsmCheckBox1";
            djsmCheckBox1.Size = new Size(207, 19);
            djsmCheckBox1.TabIndex = 0;
            djsmCheckBox1.Text = "Save the recently used sequence";
            djsmCheckBox1.UncheckedForeColor = Color.Black;
            djsmCheckBox1.UseVisualStyleBackColor = false;
            // 
            // djsmRadioButton2
            // 
            djsmRadioButton2.AutoSize = true;
            djsmRadioButton2.BorderSides = ToolStripStatusLabelBorderSides.Bottom;
            djsmRadioButton2.DeselectedForeColor = Color.Black;
            djsmRadioButton2.Font = new Font("맑은 고딕", 9F);
            djsmRadioButton2.Location = new Point(407, 423);
            djsmRadioButton2.Name = "djsmRadioButton2";
            djsmRadioButton2.SelectedForeColor = Color.Black;
            djsmRadioButton2.Size = new Size(200, 19);
            djsmRadioButton2.TabIndex = 3;
            djsmRadioButton2.TabStop = true;
            djsmRadioButton2.Text = "Save the recently used sequence";
            djsmRadioButton2.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(djsmImagePanel1);
            Name = "Form1";
            Text = "Form1";
            djsmImagePanel1.ResumeLayout(false);
            djsmImagePanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private com.outlook_styner07.cs.control.Container.DjsmImagePanel djsmImagePanel1;
        private com.outlook_styner07.cs.control.Button.DjsmCheckBox djsmCheckBox1;
        private com.outlook_styner07.cs.control.Button.DjsmRadioButton djsmRadioButton1;
        private com.outlook_styner07.cs.control.Gauge.DjsmProgressBar djsmProgressBar1;
        private com.outlook_styner07.cs.control.Button.DjsmRadioButton djsmRadioButton2;
    }
}
