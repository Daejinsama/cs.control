using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace com.outlook_styner07.cs.control.Data
{
    public class DjsmPropertyGrid : PropertyGrid
    {
        #region Constructors
        public DjsmPropertyGrid()
        {
            ToolbarVisible = false;
            HelpVisible = false;

            LineColor = SystemColors.ControlDark;
            ViewBackColor = SystemColors.Control;
            HelpBackColor = SystemColors.Control;
            CategoryForeColor = Color.White;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private bool _readOnly;
        private int _cellHeight = 30;
        #endregion

        #region Properties
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                this.SetObjectAsReadOnly();
            }
        }

        public int CellHeight
        {
            get { return _cellHeight; }
            set
            {
                _cellHeight = value;
                this.SetObjectAsReadOnly();
            }
        }
        #endregion

        #region Methods
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            SetRowHeight(this, _cellHeight);
            //SetValueTextAlign(this, HorizontalAlignment.Center);
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

        public static bool SetRowHeight(PropertyGrid grid, int height)
        {
            if (grid == null || height < 10 || height > 200)
            {
                return false;
            }

            try
            {
                var gridView = GetGridView(grid);
                if (gridView == null)
                {
                    return false;
                }

                // _cachedRowHeight 필드 찾기 및 수정
                var cachedRowHeightField = gridView.GetType().GetField("_cachedRowHeight", BindingFlags.Instance | BindingFlags.NonPublic);

                if (cachedRowHeightField != null)
                {
                    // 캐시된 값을 무효화하고 새 높이 설정
                    cachedRowHeightField.SetValue(gridView, height);
                    grid.Refresh();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SetValueTextAlign(PropertyGrid grid, HorizontalAlignment alignment)
        {
            try
            {
                var gridView = GetGridView(grid);
                if (gridView == null)
                {
                    return false;
                }

                var editField = gridView.GetType().GetField("_editTextBox", BindingFlags.Instance | BindingFlags.NonPublic);

                if (editField == null)
                {
                    return false;
                }

                var editTextBox = editField.GetValue(gridView) as TextBox;
                if (editTextBox != null)
                {
                    editTextBox.TextAlign = alignment;

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TextAlign 설정 오류: {ex.Message}");
                return false;
            }
        }

        private static Control? GetGridView(PropertyGrid grid)
        {
            Control? gridView = null;
            foreach (Control control in grid.Controls)
            {
                if (control.GetType().Name == "PropertyGridView")
                {
                    gridView = control;
                    break;
                }
            }

            return gridView;
        }
        #endregion
    }
}
