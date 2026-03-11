namespace com.outlook_styner07.cs.control.Charting
{
    partial class SeriesCustomizingDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriesCustomizingDialog));
            ToolStrip1 = new com.outlook_styner07.cs.control.Container.DjsmToolStrip();
            titleToolStripLabel = new ToolStripLabel();
            btnCancel = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnApply = new ToolStripButton();
            gbxLineStyle = new com.outlook_styner07.cs.control.Container.DjsmGroupBox();
            label3 = new Label();
            label2 = new Label();
            cmbLineDash = new ComboBox();
            cmbLineWidth = new ComboBox();
            btnLineColor = new System.Windows.Forms.Button();
            label1 = new Label();
            gbxPreview = new com.outlook_styner07.cs.control.Container.DjsmGroupBox();
            gbxMarkerStyle = new com.outlook_styner07.cs.control.Container.DjsmGroupBox();
            label8 = new Label();
            btnMarkerBorderColor = new System.Windows.Forms.Button();
            cmbMarkerBorderWidth = new ComboBox();
            btnMarkerColor = new System.Windows.Forms.Button();
            label9 = new Label();
            label7 = new Label();
            cmbMarkerShape = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            cmbMarkerSize = new ComboBox();
            cmbMarkerStep = new ComboBox();
            label6 = new Label();
            ToolStrip1.SuspendLayout();
            gbxLineStyle.SuspendLayout();
            gbxMarkerStyle.SuspendLayout();
            SuspendLayout();
            // 
            // ToolStrip1
            // 
            ToolStrip1.BackColor = Color.DimGray;
            ToolStrip1.BorderColor = Color.FromArgb(80, 119, 120, 123);
            ToolStrip1.BorderSides = ToolStripStatusLabelBorderSides.Bottom;
            ToolStrip1.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ToolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            ToolStrip1.Items.AddRange(new ToolStripItem[] { titleToolStripLabel, btnCancel, toolStripSeparator1, btnApply });
            ToolStrip1.Location = new Point(0, 0);
            ToolStrip1.Name = "ToolStrip1";
            ToolStrip1.Padding = new Padding(0);
            ToolStrip1.Size = new Size(299, 25);
            ToolStrip1.TabIndex = 0;
            ToolStrip1.Text = "ToolStrip1";
            ToolStrip1.WindowDragEnabled = true;
            // 
            // titleToolStripLabel
            // 
            titleToolStripLabel.AutoSize = false;
            titleToolStripLabel.BackColor = SystemColors.Control;
            titleToolStripLabel.ForeColor = Color.White;
            titleToolStripLabel.ImageScaling = ToolStripItemImageScaling.None;
            titleToolStripLabel.Name = "titleToolStripLabel";
            titleToolStripLabel.Size = new Size(200, 22);
            titleToolStripLabel.Text = "Series Customizing";
            titleToolStripLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Alignment = ToolStripItemAlignment.Right;
            btnCancel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnCancel.Image = (Image)resources.GetObject("btnCancel.Image");
            btnCancel.ImageScaling = ToolStripItemImageScaling.None;
            btnCancel.ImageTransparentColor = Color.Magenta;
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(23, 22);
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // btnApply
            // 
            btnApply.Alignment = ToolStripItemAlignment.Right;
            btnApply.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnApply.Image = (Image)resources.GetObject("btnApply.Image");
            btnApply.ImageScaling = ToolStripItemImageScaling.None;
            btnApply.ImageTransparentColor = Color.Magenta;
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(23, 22);
            btnApply.Text = "Apply";
            btnApply.Click += btnApply_Click;
            // 
            // gbxLineStyle
            // 
            gbxLineStyle.Align = control.Container.DjsmGroupBox.TitleAlign.TOP_LEFT;
            gbxLineStyle.Controls.Add(label3);
            gbxLineStyle.Controls.Add(label2);
            gbxLineStyle.Controls.Add(cmbLineDash);
            gbxLineStyle.Controls.Add(cmbLineWidth);
            gbxLineStyle.Controls.Add(btnLineColor);
            gbxLineStyle.Controls.Add(label1);
            gbxLineStyle.DrawRoundRect = true;
            gbxLineStyle.Font = new Font("Arial", 9F);
            gbxLineStyle.Location = new Point(0, 28);
            gbxLineStyle.Name = "gbxLineStyle";
            gbxLineStyle.Radius = 10;
            gbxLineStyle.Size = new Size(150, 93);
            gbxLineStyle.TabIndex = 1;
            gbxLineStyle.TabStop = false;
            gbxLineStyle.Text = "Line Style";
            // 
            // label3
            // 
            label3.Font = new Font("Arial", 9F);
            label3.Location = new Point(6, 22);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 5;
            label3.Text = "Dash Style";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Font = new Font("Arial", 9F);
            label2.Location = new Point(6, 43);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 4;
            label2.Text = "Width";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbLineDash
            // 
            cmbLineDash.BackColor = SystemColors.Window;
            cmbLineDash.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLineDash.Font = new Font("Arial", 9F);
            cmbLineDash.FormattingEnabled = true;
            cmbLineDash.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            cmbLineDash.Location = new Point(89, 22);
            cmbLineDash.Name = "cmbLineDash";
            cmbLineDash.Size = new Size(50, 23);
            cmbLineDash.TabIndex = 3;
            // 
            // cmbLineWidth
            // 
            cmbLineWidth.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLineWidth.Font = new Font("Arial", 9F);
            cmbLineWidth.FormattingEnabled = true;
            cmbLineWidth.Location = new Point(89, 43);
            cmbLineWidth.Name = "cmbLineWidth";
            cmbLineWidth.Size = new Size(50, 23);
            cmbLineWidth.TabIndex = 2;
            // 
            // btnLineColor
            // 
            btnLineColor.BackColor = Color.Red;
            btnLineColor.FlatAppearance.BorderSize = 0;
            btnLineColor.FlatStyle = FlatStyle.Flat;
            btnLineColor.Font = new Font("Arial", 9F);
            btnLineColor.Location = new Point(89, 64);
            btnLineColor.Name = "btnLineColor";
            btnLineColor.Size = new Size(50, 20);
            btnLineColor.TabIndex = 1;
            btnLineColor.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Font = new Font("Arial", 9F);
            label1.Location = new Point(6, 64);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 0;
            label1.Text = "Color";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // gbxPreview
            // 
            gbxPreview.Align = control.Container.DjsmGroupBox.TitleAlign.TOP_LEFT;
            gbxPreview.BackColor = Color.White;
            gbxPreview.DrawRoundRect = true;
            gbxPreview.Font = new Font("Arial", 9F);
            gbxPreview.Location = new Point(0, 127);
            gbxPreview.MaximumSize = new Size(369, 68);
            gbxPreview.Name = "gbxPreview";
            gbxPreview.Radius = 10;
            gbxPreview.Size = new Size(150, 59);
            gbxPreview.TabIndex = 2;
            gbxPreview.TabStop = false;
            gbxPreview.Text = "Preview";
            // 
            // gbxMarkerStyle
            // 
            gbxMarkerStyle.Align = control.Container.DjsmGroupBox.TitleAlign.TOP_LEFT;
            gbxMarkerStyle.Controls.Add(label8);
            gbxMarkerStyle.Controls.Add(btnMarkerBorderColor);
            gbxMarkerStyle.Controls.Add(cmbMarkerBorderWidth);
            gbxMarkerStyle.Controls.Add(btnMarkerColor);
            gbxMarkerStyle.Controls.Add(label9);
            gbxMarkerStyle.Controls.Add(label7);
            gbxMarkerStyle.Controls.Add(cmbMarkerShape);
            gbxMarkerStyle.Controls.Add(label4);
            gbxMarkerStyle.Controls.Add(label5);
            gbxMarkerStyle.Controls.Add(cmbMarkerSize);
            gbxMarkerStyle.Controls.Add(cmbMarkerStep);
            gbxMarkerStyle.Controls.Add(label6);
            gbxMarkerStyle.DrawRoundRect = true;
            gbxMarkerStyle.Font = new Font("Arial", 9F);
            gbxMarkerStyle.Location = new Point(156, 28);
            gbxMarkerStyle.Name = "gbxMarkerStyle";
            gbxMarkerStyle.Radius = 10;
            gbxMarkerStyle.Size = new Size(150, 158);
            gbxMarkerStyle.TabIndex = 3;
            gbxMarkerStyle.TabStop = false;
            gbxMarkerStyle.Text = "Marker Style";
            // 
            // label8
            // 
            label8.Font = new Font("Arial", 9F);
            label8.Location = new Point(6, 127);
            label8.Name = "label8";
            label8.Size = new Size(77, 20);
            label8.TabIndex = 14;
            label8.Text = "Border Color";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnMarkerBorderColor
            // 
            btnMarkerBorderColor.BackColor = Color.Red;
            btnMarkerBorderColor.FlatAppearance.BorderSize = 0;
            btnMarkerBorderColor.FlatStyle = FlatStyle.Flat;
            btnMarkerBorderColor.Font = new Font("Arial", 9F);
            btnMarkerBorderColor.Location = new Point(89, 127);
            btnMarkerBorderColor.Name = "btnMarkerBorderColor";
            btnMarkerBorderColor.Size = new Size(50, 20);
            btnMarkerBorderColor.TabIndex = 17;
            btnMarkerBorderColor.UseVisualStyleBackColor = false;
            // 
            // cmbMarkerBorderWidth
            // 
            cmbMarkerBorderWidth.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarkerBorderWidth.Font = new Font("Arial", 9F);
            cmbMarkerBorderWidth.FormattingEnabled = true;
            cmbMarkerBorderWidth.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            cmbMarkerBorderWidth.Location = new Point(89, 106);
            cmbMarkerBorderWidth.Name = "cmbMarkerBorderWidth";
            cmbMarkerBorderWidth.Size = new Size(50, 23);
            cmbMarkerBorderWidth.TabIndex = 18;
            // 
            // btnMarkerColor
            // 
            btnMarkerColor.BackColor = Color.Red;
            btnMarkerColor.FlatAppearance.BorderSize = 0;
            btnMarkerColor.FlatStyle = FlatStyle.Flat;
            btnMarkerColor.Font = new Font("Arial", 9F);
            btnMarkerColor.Location = new Point(89, 85);
            btnMarkerColor.Name = "btnMarkerColor";
            btnMarkerColor.Size = new Size(50, 20);
            btnMarkerColor.TabIndex = 16;
            btnMarkerColor.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            label9.Font = new Font("Arial", 9F);
            label9.Location = new Point(6, 106);
            label9.Name = "label9";
            label9.Size = new Size(77, 20);
            label9.TabIndex = 15;
            label9.Text = "Border width";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.Font = new Font("Arial", 9F);
            label7.Location = new Point(6, 85);
            label7.Name = "label7";
            label7.Size = new Size(77, 20);
            label7.TabIndex = 13;
            label7.Text = "Color";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbMarkerShape
            // 
            cmbMarkerShape.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarkerShape.Font = new Font("Arial", 9F);
            cmbMarkerShape.FormattingEnabled = true;
            cmbMarkerShape.Location = new Point(89, 22);
            cmbMarkerShape.Name = "cmbMarkerShape";
            cmbMarkerShape.Size = new Size(50, 23);
            cmbMarkerShape.TabIndex = 12;
            // 
            // label4
            // 
            label4.Font = new Font("Arial", 9F);
            label4.Location = new Point(6, 64);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 11;
            label4.Text = "Size";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.Font = new Font("Arial", 9F);
            label5.Location = new Point(6, 43);
            label5.Name = "label5";
            label5.Size = new Size(77, 20);
            label5.TabIndex = 10;
            label5.Text = "Step";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbMarkerSize
            // 
            cmbMarkerSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarkerSize.Font = new Font("Arial", 9F);
            cmbMarkerSize.FormattingEnabled = true;
            cmbMarkerSize.Items.AddRange(new object[] { "1", "2", "3", "4", "5" });
            cmbMarkerSize.Location = new Point(89, 64);
            cmbMarkerSize.Name = "cmbMarkerSize";
            cmbMarkerSize.Size = new Size(50, 23);
            cmbMarkerSize.TabIndex = 9;
            // 
            // cmbMarkerStep
            // 
            cmbMarkerStep.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarkerStep.Font = new Font("Arial", 9F);
            cmbMarkerStep.FormattingEnabled = true;
            cmbMarkerStep.Location = new Point(89, 43);
            cmbMarkerStep.Name = "cmbMarkerStep";
            cmbMarkerStep.Size = new Size(50, 23);
            cmbMarkerStep.TabIndex = 8;
            // 
            // label6
            // 
            label6.Font = new Font("Arial", 9F);
            label6.Location = new Point(6, 22);
            label6.Name = "label6";
            label6.Size = new Size(77, 20);
            label6.TabIndex = 6;
            label6.Text = "Shape";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SeriesCustomizingDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(299, 155);
            Controls.Add(gbxMarkerStyle);
            Controls.Add(gbxPreview);
            Controls.Add(gbxLineStyle);
            Controls.Add(ToolStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximumSize = new Size(315, 194);
            Name = "SeriesCustomizingDialog";
            StartPosition = FormStartPosition.Manual;
            Text = "SeriesCustomizingDialog";
            ToolStrip1.ResumeLayout(false);
            ToolStrip1.PerformLayout();
            gbxLineStyle.ResumeLayout(false);
            gbxMarkerStyle.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Container.DjsmToolStrip ToolStrip1;
        private System.Windows.Forms.ToolStripLabel titleToolStripLabel;
        private System.Windows.Forms.ToolStripButton btnApply;
        private Container.DjsmGroupBox gbxLineStyle;
        private Container.DjsmGroupBox gbxPreview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLineDash;
        private System.Windows.Forms.ComboBox cmbLineWidth;
        private System.Windows.Forms.Button btnLineColor;
        private System.Windows.Forms.Label label1;
        private Container.DjsmGroupBox gbxMarkerStyle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbMarkerShape;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbMarkerSize;
        private System.Windows.Forms.ComboBox cmbMarkerStep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbMarkerBorderWidth;
        private System.Windows.Forms.Button btnMarkerBorderColor;
        private System.Windows.Forms.Button btnMarkerColor;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}