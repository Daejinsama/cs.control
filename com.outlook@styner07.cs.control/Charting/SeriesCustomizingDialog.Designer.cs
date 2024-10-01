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
            this.nntToolStrip1 = new Container.DjsmToolStrip();
            this.titleToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnApply = new System.Windows.Forms.ToolStripButton();
            this.gbxLineStyle = new Container.DjsmGroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLineDash = new System.Windows.Forms.ComboBox();
            this.cmbLineWidth = new System.Windows.Forms.ComboBox();
            this.btnLineColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbxPreview = new Container.DjsmGroupBox();
            this.gbxMarkerStyle = new Container.DjsmGroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnMarkerBorderColor = new System.Windows.Forms.Button();
            this.cmbMarkerBorderWidth = new System.Windows.Forms.ComboBox();
            this.btnMarkerColor = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbMarkerShape = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbMarkerSize = new System.Windows.Forms.ComboBox();
            this.cmbMarkerStep = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nntToolStrip1.SuspendLayout();
            this.gbxLineStyle.SuspendLayout();
            this.gbxMarkerStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // nntToolStrip1
            // 
            this.nntToolStrip1.BackColor = System.Drawing.Color.DimGray;
            this.nntToolStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nntToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.nntToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.titleToolStripLabel,
            this.btnCancel,
            this.toolStripSeparator1,
            this.btnApply});
            this.nntToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.nntToolStrip1.Name = "nntToolStrip1";
            this.nntToolStrip1.Size = new System.Drawing.Size(309, 25);
            this.nntToolStrip1.TabIndex = 0;
            this.nntToolStrip1.Text = "nntToolStrip1";
            this.nntToolStrip1.WindowDragEnabled = true;
            // 
            // titleToolStripLabel
            // 
            this.titleToolStripLabel.AutoSize = false;
            this.titleToolStripLabel.BackColor = System.Drawing.SystemColors.Control;
            this.titleToolStripLabel.ForeColor = System.Drawing.Color.White;
            this.titleToolStripLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.titleToolStripLabel.Name = "titleToolStripLabel";
            this.titleToolStripLabel.Size = new System.Drawing.Size(200, 22);
            this.titleToolStripLabel.Text = "Series Customizing";
            this.titleToolStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 22);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnApply
            // 
            this.btnApply.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnApply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnApply.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.Image")));
            this.btnApply.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(23, 22);
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // groupLineStyle
            // 
            this.gbxLineStyle.Align = control.Container.DjsmGroupBox.TitleAlign.TOP_LEFT;
            this.gbxLineStyle.Controls.Add(this.label3);
            this.gbxLineStyle.Controls.Add(this.label2);
            this.gbxLineStyle.Controls.Add(this.cmbLineDash);
            this.gbxLineStyle.Controls.Add(this.cmbLineWidth);
            this.gbxLineStyle.Controls.Add(this.btnLineColor);
            this.gbxLineStyle.Controls.Add(this.label1);
            this.gbxLineStyle.DrawRoundRect = true;
            this.gbxLineStyle.Font = new System.Drawing.Font("Arial", 9F);
            this.gbxLineStyle.Location = new System.Drawing.Point(0, 28);
            this.gbxLineStyle.Name = "groupLineStyle";
            this.gbxLineStyle.Radius = 10;
            this.gbxLineStyle.Size = new System.Drawing.Size(150, 93);
            this.gbxLineStyle.TabIndex = 1;
            this.gbxLineStyle.TabStop = false;
            this.gbxLineStyle.Text = "Line Style";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F);
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Dash Style";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Width";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLineDash
            // 
            this.cmbLineDash.BackColor = System.Drawing.SystemColors.Window;
            this.cmbLineDash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineDash.Font = new System.Drawing.Font("Arial", 9F);
            this.cmbLineDash.FormattingEnabled = true;
            this.cmbLineDash.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbLineDash.Location = new System.Drawing.Point(89, 22);
            this.cmbLineDash.Name = "cmbLineDash";
            this.cmbLineDash.Size = new System.Drawing.Size(50, 23);
            this.cmbLineDash.TabIndex = 3;
            // 
            // cmbLineWidth
            // 
            this.cmbLineWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineWidth.Font = new System.Drawing.Font("Arial", 9F);
            this.cmbLineWidth.FormattingEnabled = true;
            this.cmbLineWidth.Location = new System.Drawing.Point(89, 43);
            this.cmbLineWidth.Name = "cmbLineWidth";
            this.cmbLineWidth.Size = new System.Drawing.Size(50, 23);
            this.cmbLineWidth.TabIndex = 2;
            // 
            // btnLineColor
            // 
            this.btnLineColor.BackColor = System.Drawing.Color.Red;
            this.btnLineColor.FlatAppearance.BorderSize = 0;
            this.btnLineColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLineColor.Font = new System.Drawing.Font("Arial", 9F);
            this.btnLineColor.Location = new System.Drawing.Point(89, 64);
            this.btnLineColor.Name = "btnLineColor";
            this.btnLineColor.Size = new System.Drawing.Size(50, 20);
            this.btnLineColor.TabIndex = 1;
            this.btnLineColor.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(6, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Color";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupPreview
            // 
            this.gbxPreview.Align = control.Container.DjsmGroupBox.TitleAlign.TOP_LEFT;
            this.gbxPreview.BackColor = System.Drawing.Color.White;
            this.gbxPreview.DrawRoundRect = true;
            this.gbxPreview.Font = new System.Drawing.Font("Arial", 9F);
            this.gbxPreview.Location = new System.Drawing.Point(0, 127);
            this.gbxPreview.MaximumSize = new System.Drawing.Size(369, 68);
            this.gbxPreview.Name = "groupPreview";
            this.gbxPreview.Radius = 10;
            this.gbxPreview.Size = new System.Drawing.Size(150, 59);
            this.gbxPreview.TabIndex = 2;
            this.gbxPreview.TabStop = false;
            this.gbxPreview.Text = "Preview";
            // 
            // groupMarkerStyle
            // 
            this.gbxMarkerStyle.Align = control.Container.DjsmGroupBox.TitleAlign.TOP_LEFT;
            this.gbxMarkerStyle.Controls.Add(this.label8);
            this.gbxMarkerStyle.Controls.Add(this.btnMarkerBorderColor);
            this.gbxMarkerStyle.Controls.Add(this.cmbMarkerBorderWidth);
            this.gbxMarkerStyle.Controls.Add(this.btnMarkerColor);
            this.gbxMarkerStyle.Controls.Add(this.label9);
            this.gbxMarkerStyle.Controls.Add(this.label7);
            this.gbxMarkerStyle.Controls.Add(this.cmbMarkerShape);
            this.gbxMarkerStyle.Controls.Add(this.label4);
            this.gbxMarkerStyle.Controls.Add(this.label5);
            this.gbxMarkerStyle.Controls.Add(this.cmbMarkerSize);
            this.gbxMarkerStyle.Controls.Add(this.cmbMarkerStep);
            this.gbxMarkerStyle.Controls.Add(this.label6);
            this.gbxMarkerStyle.DrawRoundRect = true;
            this.gbxMarkerStyle.Font = new System.Drawing.Font("Arial", 9F);
            this.gbxMarkerStyle.Location = new System.Drawing.Point(156, 28);
            this.gbxMarkerStyle.Name = "groupMarkerStyle";
            this.gbxMarkerStyle.Radius = 10;
            this.gbxMarkerStyle.Size = new System.Drawing.Size(150, 158);
            this.gbxMarkerStyle.TabIndex = 3;
            this.gbxMarkerStyle.TabStop = false;
            this.gbxMarkerStyle.Text = "Marker Style";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(6, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "Border Color";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMarkerBorderColor
            // 
            this.btnMarkerBorderColor.BackColor = System.Drawing.Color.Red;
            this.btnMarkerBorderColor.FlatAppearance.BorderSize = 0;
            this.btnMarkerBorderColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkerBorderColor.Font = new System.Drawing.Font("Arial", 9F);
            this.btnMarkerBorderColor.Location = new System.Drawing.Point(89, 127);
            this.btnMarkerBorderColor.Name = "btnMarkerBorderColor";
            this.btnMarkerBorderColor.Size = new System.Drawing.Size(50, 20);
            this.btnMarkerBorderColor.TabIndex = 17;
            this.btnMarkerBorderColor.UseVisualStyleBackColor = false;
            // 
            // cmbMarkerBorderWidth
            // 
            this.cmbMarkerBorderWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarkerBorderWidth.Font = new System.Drawing.Font("Arial", 9F);
            this.cmbMarkerBorderWidth.FormattingEnabled = true;
            this.cmbMarkerBorderWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbMarkerBorderWidth.Location = new System.Drawing.Point(89, 106);
            this.cmbMarkerBorderWidth.Name = "cmbMarkerBorderWidth";
            this.cmbMarkerBorderWidth.Size = new System.Drawing.Size(50, 23);
            this.cmbMarkerBorderWidth.TabIndex = 18;
            // 
            // btnMarkerColor
            // 
            this.btnMarkerColor.BackColor = System.Drawing.Color.Red;
            this.btnMarkerColor.FlatAppearance.BorderSize = 0;
            this.btnMarkerColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkerColor.Font = new System.Drawing.Font("Arial", 9F);
            this.btnMarkerColor.Location = new System.Drawing.Point(89, 85);
            this.btnMarkerColor.Name = "btnMarkerColor";
            this.btnMarkerColor.Size = new System.Drawing.Size(50, 20);
            this.btnMarkerColor.TabIndex = 16;
            this.btnMarkerColor.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F);
            this.label9.Location = new System.Drawing.Point(6, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "Border width";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.Location = new System.Drawing.Point(6, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Color";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMarkerShape
            // 
            this.cmbMarkerShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarkerShape.Font = new System.Drawing.Font("Arial", 9F);
            this.cmbMarkerShape.FormattingEnabled = true;
            this.cmbMarkerShape.Location = new System.Drawing.Point(89, 22);
            this.cmbMarkerShape.Name = "cmbMarkerShape";
            this.cmbMarkerShape.Size = new System.Drawing.Size(50, 23);
            this.cmbMarkerShape.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9F);
            this.label4.Location = new System.Drawing.Point(6, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Size";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F);
            this.label5.Location = new System.Drawing.Point(6, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Step";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMarkerSize
            // 
            this.cmbMarkerSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarkerSize.Font = new System.Drawing.Font("Arial", 9F);
            this.cmbMarkerSize.FormattingEnabled = true;
            this.cmbMarkerSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbMarkerSize.Location = new System.Drawing.Point(89, 64);
            this.cmbMarkerSize.Name = "cmbMarkerSize";
            this.cmbMarkerSize.Size = new System.Drawing.Size(50, 23);
            this.cmbMarkerSize.TabIndex = 9;
            // 
            // cmbMarkerStep
            // 
            this.cmbMarkerStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarkerStep.Font = new System.Drawing.Font("Arial", 9F);
            this.cmbMarkerStep.FormattingEnabled = true;
            this.cmbMarkerStep.Location = new System.Drawing.Point(89, 43);
            this.cmbMarkerStep.Name = "cmbMarkerStep";
            this.cmbMarkerStep.Size = new System.Drawing.Size(50, 23);
            this.cmbMarkerStep.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Shape";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SeriesCustomizingDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(309, 188);
            this.Controls.Add(this.gbxMarkerStyle);
            this.Controls.Add(this.gbxPreview);
            this.Controls.Add(this.gbxLineStyle);
            this.Controls.Add(this.nntToolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(315, 194);
            this.Name = "SeriesCustomizingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SeriesCustomizingDialog";
            this.nntToolStrip1.ResumeLayout(false);
            this.nntToolStrip1.PerformLayout();
            this.gbxLineStyle.ResumeLayout(false);
            this.gbxMarkerStyle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Container.DjsmToolStrip nntToolStrip1;
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