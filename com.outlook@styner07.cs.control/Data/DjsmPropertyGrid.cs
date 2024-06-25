using System.ComponentModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Data
{
    public class DjsmPropertyGrid : PropertyGrid
    {
        private bool _readOnly;
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                this.SetObjectAsReadOnly();
            }
        }

        public DjsmPropertyGrid()
        {
            ToolbarVisible = false;
            HelpVisible = false;

            LineColor = SystemColors.ControlDark;
            ViewBackColor = SystemColors.Control;
            HelpBackColor = SystemColors.Control;
            CategoryForeColor = Color.White;
        }

        protected override void OnSelectedObjectsChanged(EventArgs e)
        {
            this.SetObjectAsReadOnly();
            base.OnSelectedObjectsChanged(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void SetObjectAsReadOnly()
        {
            if (SelectedObject != null)
            {
                TypeDescriptor.AddAttributes(this.SelectedObject, new Attribute[] { new ReadOnlyAttribute(_readOnly) });
                this.Refresh();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NntPropertyGrid
            // 
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.ResumeLayout(false);
        }
    }
}
