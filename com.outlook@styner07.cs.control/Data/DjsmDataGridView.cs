using com.outlook_styner07.cs.control.Data.CellTemplates;

namespace com.outlook_styner07.cs.control.Data
{
    public class DjsmDataGridView : DataGridView
    {
        #region Constructors
        public DjsmDataGridView()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

            RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            RowHeadersDefaultCellStyle.BackColor = HEADER_BACK_COLOR;
            RowHeadersDefaultCellStyle.SelectionBackColor = SELECTION_BACK_COLOR;

            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.BackColor = HEADER_BACK_COLOR;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = SELECTION_BACK_COLOR;
            ColumnHeadersDefaultCellStyle.Padding = new Padding(0);

            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            DefaultCellStyle.SelectionBackColor = SELECTION_BACK_COLOR;
            DefaultCellStyle.BackColor = SystemColors.ControlLight;

            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            BackgroundColor = Color.White;

            CellBorderStyle = DataGridViewCellBorderStyle.Single;

            BorderStyle = BorderStyle.None;
            SelectionMode = DataGridViewSelectionMode.CellSelect;

            AllowUserToResizeRows = false;
            AllowUserToAddRows = false;
            AllowUserToResizeColumns = false;
            ShowEditingIcon = false;
            ShowRowErrors = false;
            ShowCellErrors = false;
            ShowCellToolTips = false;

            EnableHeadersVisualStyles = true;
            DoubleBuffered = true;  //for drawing performance

            MultiSelect = false;
            RowHeadersVisible = true;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private const string NO_DATA = "no data to display";

        public readonly Color SELECTION_BACK_COLOR = Color.FromArgb(0xA7, 0xA9, 0xAC);

        public static readonly Color COLOR_DIM_CELL = Color.LightGray;
        public static readonly Color HEADER_BACK_COLOR = SystemColors.Control;

        public static readonly Font DEFAULT_HEADER_FONT = new Font("Arial", 8f, FontStyle.Bold);
        public static readonly Font DEFAULT_CELL_FONT = new Font("Arial", 8f, FontStyle.Regular);

        private DataGridViewRow _dummyRow;

        private bool _isAllChecked = false;

        private List<ColMergeObject> _colMergeObjects;
        #endregion

        #region Properties
        public bool RemoveRowHeaderArrow { get; set; } = false;

        public bool IsAllChecked { get => _isAllChecked; set => _isAllChecked = value; }

        public Color MergeCellTextColor { get; set; } = Color.Black;
        #endregion

        #region Methods
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
        {
            base.OnCurrentCellDirtyStateChanged(e);

            if (IsCurrentCellDirty)
            {
                if (CurrentCell is DataGridViewCheckBoxCell || CurrentCell is DataGridViewComboBoxCell)
                {
                    CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
                else if (CurrentCell is DataGridViewNumericUpDownCell)
                {
                    CommitEdit(DataGridViewDataErrorContexts.Display);
                }
            }
        }

        public DataGridViewRow GetDummyRow()
        {
            if (_dummyRow == null)
            {
                _dummyRow = new DataGridViewRow();
                DataGridViewCell[] cells = new DataGridViewCell[Columns.Count];
                for (int len = Columns.Count, i = 0; i < len; i++)
                {
                    cells[i] = Columns[i].CellTemplate;
                }

                _dummyRow.Cells.AddRange(cells);
            }

            return (DataGridViewRow)_dummyRow.Clone();
        }

        public DataGridViewNumericUpDownColumn AddNumericUpDownColumn(string colName, DataGridViewContentAlignment align)
        {
            DataGridViewNumericUpDownColumn col = new DataGridViewNumericUpDownColumn()
            {
                HeaderText = colName,
                Width = 100,
            };
            col.DefaultCellStyle.Alignment = align;
            col.DefaultCellStyle.Font = DEFAULT_CELL_FONT;
            col.HeaderCell.Style.Font = DEFAULT_HEADER_FONT;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Columns.Add(col);
            return col;
        }
        public DataGridViewCheckBoxColumn AddCheckBoxColumn(string colName, bool enabledAllCheck = true)
        {
            DataGridViewCheckBoxColumn ret = AddCheckBoxColumn(enabledAllCheck);
            ret.HeaderText = colName;
            return ret;
        }

        public DataGridViewCheckBoxColumn AddCheckBoxColumn(bool enabledAllCheck)
        {
            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn()
            {
                Width = 20,
            };

            col.DefaultCellStyle.Font = DEFAULT_CELL_FONT;
            col.HeaderCell.Style.Font = DEFAULT_HEADER_FONT;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Columns.Add(col);

            if (enabledAllCheck)
            {
                CellPainting += (sender, e) =>
                {
                    if (e.RowIndex == -1 && col.Index == e.ColumnIndex)
                    {
                        e.PaintBackground(e.ClipBounds, false);

                        Point pt = e.CellBounds.Location;

                        Size s = CheckBoxRenderer.GetGlyphSize(e.Graphics, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);

                        int offsetX = (e.CellBounds.Width - s.Width) / 2;
                        int offsetY = (e.CellBounds.Height - s.Height) / 2;

                        pt.X += offsetX;
                        pt.Y += offsetY;

                        CheckBoxRenderer.DrawCheckBox(e.Graphics, pt,
                            _isAllChecked
                            ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal
                            : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

                        e.Handled = true;
                    }
                };

                ColumnHeaderMouseClick += (sender, e) =>
                {
                    if (e.ColumnIndex == col.Index)
                    {
                        _isAllChecked = !_isAllChecked;
                        InvalidateColumn(col.Index);

                        /// begin/end edit 호출로 화면상 비갱신 셀들도 업데이트 함
                        if (CurrentCell != null)
                        {
                            BeginEdit(false);

                            for (int len = Rows.Count, i = 0; i < len; i++)
                            {
                                Rows[i].Cells[col.Index].Value = _isAllChecked;
                            }

                            EndEdit();
                        }
                    }
                };
            }

            return col;
        }

        public DataGridViewComboBoxColumn AddComboBoxColumn(string colName, Type type, object dataSource, DataGridViewContentAlignment align)
        {
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn()
            {
                HeaderText = colName,
                Width = 80,
                DataSource = dataSource,
                ValueType = type,
                FlatStyle = FlatStyle.Flat,
                //DisplayStyleForCurrentCellOnly = true,
            };
            col.DefaultCellStyle.Alignment = align;
            col.DefaultCellStyle.Font = DEFAULT_CELL_FONT;
            col.HeaderCell.Style.Font = DEFAULT_HEADER_FONT;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Columns.Add(col);
            return col;
        }

        public DataGridViewButtonColumn AddButtonColumn(string colName)
        {
            DataGridViewButtonColumn col = new DataGridViewButtonColumn()
            {
                HeaderText = colName,
                Width = 80,
            };
            col.DefaultCellStyle.BackColor = BackgroundColor;
            col.DefaultCellStyle.Font = DEFAULT_CELL_FONT;
            col.HeaderCell.Style.Font = DEFAULT_HEADER_FONT;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Columns.Add(col);
            return col;
        }

        public DataGridViewColumn AddColumn(string colName)
        {
            DataGridViewColumn col = new DataGridViewColumn()
            {
                HeaderText = colName,
            };
            col.DefaultCellStyle.BackColor = BackgroundColor;
            col.DefaultCellStyle.Font = DEFAULT_CELL_FONT;
            col.HeaderCell.Style.Font = DEFAULT_HEADER_FONT;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Columns.Add(col);
            return col;
        }

        public DataGridViewImageColumn AddClickableImageColumn(string colName, DataGridViewClickableImageCell template)
        {
            DataGridViewImageColumn col = new DataGridViewImageColumn()
            {
                HeaderText = colName,
                Width = 80,
                CellTemplate = template
            };
            col.DefaultCellStyle.BackColor = BackgroundColor;
            col.DefaultCellStyle.Font = DEFAULT_CELL_FONT;
            col.HeaderCell.Style.Font = DEFAULT_HEADER_FONT;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Columns.Add(col);
            return col;
        }

        public DataGridViewTextBoxColumn AddTextBoxColumn(string colName, Type type, DataGridViewContentAlignment align)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn()
            {
                HeaderText = colName,
                ValueType = type,
            };
            col.DefaultCellStyle.Alignment = align;
            col.DefaultCellStyle.Font = DEFAULT_CELL_FONT;
            col.HeaderCell.Style.Font = DEFAULT_HEADER_FONT;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Columns.Add(col);
            return col;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>return false, when table row count = 0</returns>
        public bool ExportTable(string path)
        {
            if (Rows.Count > 0)
            {
                bool oldState = MultiSelect;
                MultiSelect = true;

                ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                SelectAll();
                DataObject dataObject = GetClipboardContent();
                string writedObject = dataObject.GetText(TextDataFormat.CommaSeparatedValue);
                File.WriteAllText(path, writedObject);

                MultiSelect = oldState;

                DjsmToastMessage.Show("Table export complete.", 3000, DjsmToastMessage.Theme.DARK);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Rows.Count == 0)
            {
                Graphics g = e.Graphics;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                SizeF textSize = g.MeasureString(NO_DATA, Font);
                using (var b = new SolidBrush(ForeColor))
                {
                    g.DrawString(NO_DATA, Font, b, new Point(ClientRectangle.X + (int)(ClientRectangle.Width - textSize.Width) / 2, ClientRectangle.Y + (int)(ClientRectangle.Height - textSize.Height) / 2));
                }
            }
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
            //base.OnDataError(displayErrorDialogIfNoHandler, e);
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);
            CurrentCell = this[e.ColumnIndex, e.RowIndex];
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);
            Invalidate();
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (_colMergeObjects != null && _colMergeObjects.Count > 0 && e.RowIndex == -1)
            {
                for (int len = _colMergeObjects.Count, i = 0; i < len; i++)
                {
                    int startColIndex = _colMergeObjects[i].StartColIndex;
                    int endColIndex = _colMergeObjects[i].EndColIndex;

                    int mergedCellWidth = 0;

                    if (e.ColumnIndex == endColIndex)
                    {
                        Rectangle[] rects = new Rectangle[endColIndex - startColIndex + 1];

                        int rectIndex = 0;
                        int totalWidth = 0;

                        for (int j = startColIndex; j <= endColIndex; j++)
                        {
                            mergedCellWidth += Columns[j].Width;
                            rects[rectIndex] = GetCellDisplayRectangle(j, -1, false);
                            totalWidth += rects[rectIndex].Width;

                            rectIndex += 1;
                        }

                        mergedCellWidth -= 1;

                        Rectangle rect = new Rectangle(rects[0].X, rects[0].Y, totalWidth, rects[0].Height);
                        e.PaintBackground(rect, false);

                        if (ColumnHeadersBorderStyle != DataGridViewHeaderBorderStyle.None)
                        {
                            rect.Y += 1;
                            rect.Width -= 1;
                            rect.Height -= 2;
                        }

                        Graphics g = e.Graphics;

                        g.SetClip(rect);

                        using (var b = new SolidBrush(_colMergeObjects[i].BackColor))
                        {
                            g.FillRectangle(b, rect);
                        }

                        string title = _colMergeObjects[i].ColTitle;

                        SizeF stringSize = g.MeasureString(title, DEFAULT_HEADER_FONT);

                        int textRenderOffsetX = 0;

                        if (mergedCellWidth > 0 && rect.Width < mergedCellWidth)
                        {
                            textRenderOffsetX = mergedCellWidth - rect.Width;
                        }

                        PointF textRenderPosition = new PointF(
                                rect.X + (rect.Width - textRenderOffsetX - stringSize.Width) / 2,
                                rect.Y + (rect.Height - stringSize.Height) / 2);

                        using (var b = new SolidBrush(MergeCellTextColor))
                        {
                            g.DrawString(title, DEFAULT_HEADER_FONT, b, textRenderPosition);
                        }

                        g.ResetClip();

                        e.Handled = true;
                    }
                }
            }

            base.OnCellPainting(e);
        }

        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            if (RemoveRowHeaderArrow)
            {
                e.PaintCells(e.ClipBounds, DataGridViewPaintParts.All);
                // remove header arrow
                e.PaintHeader(DataGridViewPaintParts.Background
                    | DataGridViewPaintParts.Border
                    | DataGridViewPaintParts.Focus
                    | DataGridViewPaintParts.SelectionBackground
                    | DataGridViewPaintParts.ContentForeground);

                e.Handled = true;
            }

            base.OnRowPrePaint(e);
        }

        public void CellMerge(int startColIndex, int endColIndex, string colTitle, Color backColor)
        {
            if (_colMergeObjects == null)
            {
                _colMergeObjects = new List<ColMergeObject>();
            }

            _colMergeObjects.Add(new ColMergeObject
            {
                StartColIndex = startColIndex,
                EndColIndex = endColIndex,
                ColTitle = colTitle,
                BackColor = backColor,
            });
        }
        #endregion

        private class ItemComparer : System.Collections.IComparer
        {
            #region Constructors
            public ItemComparer(int[] indexes)
            {
                _indexes = indexes;
            }

            public ItemComparer(int[] indexes, SortOrder[] orders) : this(indexes)
            {
                _orders = orders;
            }

            public ItemComparer(int[] indexes, SortOrder order) : this(indexes, new SortOrder[] { order }) { }
            #endregion

            #region Types
            #endregion

            #region Fields
            private int[] _indexes;

            private SortOrder[] _orders;
            #endregion

            #region Properties
            #endregion

            #region Methods
            public int Compare(object x, object y)
            {
                DataGridViewRow row1 = (DataGridViewRow)x;
                DataGridViewRow row2 = (DataGridViewRow)y;

                int compareResult = 0;
                int cellIndex;
                int ordering = 1;

                for (int len = _indexes.Length, i = 0; i < len; i++)
                {
                    cellIndex = _indexes[i];

                    if (_orders == null)
                    {
                        ordering = 1;
                    }
                    else
                    {
                        if (i < _orders.Length)
                        {
                            ordering = _orders[i] == SortOrder.Descending ? -1 : 1;
                        }
                    }

                    if (row1.Cells[cellIndex].ValueType == typeof(int)
                        || row1.Cells[cellIndex].ValueType == typeof(double)
                        || row1.Cells[cellIndex].ValueType == typeof(float))
                    {
                        decimal c1 = decimal.Parse(row1.Cells[cellIndex].Value.ToString());
                        decimal c2 = decimal.Parse(row2.Cells[cellIndex].Value.ToString());

                        compareResult = c1 < c2 ? -1 : c1 > c2 ? 1 : 0;
                    }
                    else
                    {
                        compareResult = string.Compare(
                            row1.Cells[cellIndex].Value.ToString(),
                            row2.Cells[cellIndex].Value.ToString());
                    }

                    if (compareResult != 0)
                    {
                        compareResult *= ordering;
                        break;
                    }
                }

                return compareResult;
            }
            #endregion
        }

        private class ColMergeObject
        {
            public int StartColIndex { get; set; }
            public int EndColIndex { get; set; }
            public string ColTitle { get; set; } = string.Empty;
            public Color BackColor { get; set; }
        }
    }
}
