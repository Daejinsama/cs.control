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
            ppgTemp = new com.outlook_styner07.cs.control.Data.DjsmPropertyGrid();
            SuspendLayout();
            // 
            // ppgTemp
            // 
            ppgTemp.CategoryForeColor = Color.White;
            ppgTemp.HelpVisible = false;
            ppgTemp.LineColor = SystemColors.ControlDark;
            ppgTemp.Location = new Point(120, 156);
            ppgTemp.Name = "ppgTemp";
            ppgTemp.ReadOnly = false;
            ppgTemp.Size = new Size(240, 340);
            ppgTemp.TabIndex = 1;
            ppgTemp.ToolbarVisible = false;
            ppgTemp.ViewBackColor = SystemColors.Control;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1221, 599);
            Controls.Add(ppgTemp);
            Name = "Form1";
            Text = "Form1";
            Controls.SetChildIndex(ppgTemp, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private com.outlook_styner07.cs.control.Data.DjsmPropertyGrid ppgTemp;
    }
}
