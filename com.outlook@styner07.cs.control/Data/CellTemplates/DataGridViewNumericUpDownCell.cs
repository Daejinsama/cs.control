using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace com.outlook_styner07.cs.control.Data.CellTemplates
{
    /// <summary>
    /// Defines a NumericUpDown cell type for the System.Windows.Forms.DataGridView control
    /// </summary>
    public class DataGridViewNumericUpDownCell : DataGridViewTextBoxCell
    {
        #region Constructors
        /// <summary>
        /// Constructor for the DataGridViewNumericUpDownCell cell type
        /// </summary>
        public DataGridViewNumericUpDownCell()
        {
            // Create a thread specific bitmap used for the painting of the non-edited cells
            if (_renderingBitmap == null)
            {
                _renderingBitmap = new Bitmap(DEFAULT_RENDERING_BITMAP_WIDTH, DEFAULT_RENDERING_BITMAP_HEIGHT);
            }

            // Create a thread specific NumericUpDown control used for the painting of the non-edited cells
            if (_paintingNumericUpDown == null)
            {
                _paintingNumericUpDown = new NumericUpDown
                {
                    BorderStyle = BorderStyle.None,
                    Maximum = decimal.MaxValue / 10,
                    Minimum = decimal.MinValue / 10,
                };
            }

            // Set the default values of the properties:
            _decimalPlaces = DEFAULT_DECIMAL_PLACES;
            _increment = DEFAULT_INCREMENT;
            _minimum = DEFAULT_MINIMUM;
            _maximum = DEFAULT_MAXIMUM;
            _thousandsSeparator = DEFAULT_THOUSANDS_SEPARATOR;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private static readonly DataGridViewContentAlignment anyRight = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
        private static readonly DataGridViewContentAlignment anyCenter = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;

        // Default dimensions of the static rendering bitmap used for the painting of the non-edited cells
        private const int DEFAULT_RENDERING_BITMAP_WIDTH = 100;
        private const int DEFAULT_RENDERING_BITMAP_HEIGHT = 22;

        // Default value of the DecimalPlaces property
        internal const int DEFAULT_DECIMAL_PLACES = 0;
        // Default value of the Increment property
        internal const decimal DEFAULT_INCREMENT = decimal.One;
        // Default value of the Maximum property
        internal const decimal DEFAULT_MAXIMUM = (decimal)100.0;
        // Default value of the Minimum property
        internal const decimal DEFAULT_MINIMUM = decimal.Zero;
        // Default value of the ThousandsSeparator property
        internal const bool DEFAULT_THOUSANDS_SEPARATOR = false;

        // Type of this cell's editing control
        private readonly static Type defaultEditType = typeof(DataGridViewNumericUpDownEditingControl);
        // Type of this cell's value. The formatted value type is string, the same as the base class DataGridViewTextBoxCell
        private readonly static Type defaultValueType = typeof(decimal);

        // The bitmap used to paint the non-edited cells via a call to NumericUpDown.DrawToBitmap
        [ThreadStatic]
        private static Bitmap _renderingBitmap;

        // The NumericUpDown control used to paint the non-edited cells via a call to NumericUpDown.DrawToBitmap
        [ThreadStatic]
        private static NumericUpDown _paintingNumericUpDown;

        private int _decimalPlaces;       // Caches the value of the DecimalPlaces property
        private decimal _increment;       // Caches the value of the Increment property
        private decimal _minimum;         // Caches the value of the Minimum property
        private decimal _maximum;         // Caches the value of the Maximum property
        private bool _thousandsSeparator; // Caches the value of the ThousandsSeparator property
        #endregion

        #region Properties
        /// <summary>
        /// The DecimalPlaces property replicates the one from the NumericUpDown control
        /// </summary>
        [DefaultValue(DEFAULT_DECIMAL_PLACES)]
        public int DecimalPlaces
        {
            get
            {
                return _decimalPlaces;
            }
            set
            {
                if (value < 0 || value > 99)
                {
                    throw new ArgumentOutOfRangeException("The DecimalPlaces property cannot be smaller than 0 or larger than 99.");
                }

                if (_decimalPlaces != value)
                {
                    _decimalPlaces = value;
                    SetDecimalPlaces(RowIndex, value);
                    OnCommonChange();  // Assure that the cell or column gets repainted and autosized if needed
                }
            }
        }

        /// <summary>
        /// Returns the current DataGridView EditingControl as a DataGridViewNumericUpDownEditingControl control
        /// </summary>
        private DataGridViewNumericUpDownEditingControl EditingNumericUpDown
        {
            get
            {
                return (DataGridViewNumericUpDownEditingControl)DataGridView.EditingControl;
            }
        }

        /// <summary>
        /// Define the type of the cell's editing control
        /// </summary>
        public override Type EditType
        {
            get
            {
                return defaultEditType; // the type is DataGridViewNumericUpDownEditingControl
            }
        }

        /// <summary>
        /// The Increment property replicates the one from the NumericUpDown control
        /// </summary>
        public decimal Increment
        {
            get
            {
                return _increment;
            }
            set
            {
                if (value < (decimal)0.0)
                {
                    throw new ArgumentOutOfRangeException("The Increment property cannot be smaller than 0.");
                }

                if (_increment != value)
                {
                    _increment = value;
                    SetIncrement(RowIndex, value);
                    // No call to OnCommonChange is needed since the increment value does not affect the rendering of the cell.
                }
            }
        }

        /// <summary>
        /// The Maximum property replicates the one from the NumericUpDown control
        /// </summary>
        public decimal Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                if (_maximum != value)
                {
                    _maximum = value;
                    SetMaximum(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The Minimum property replicates the one from the NumericUpDown control
        /// </summary>
        public decimal Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                if (_minimum != value)
                {
                    _minimum = value;
                    SetMinimum(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// The ThousandsSeparator property replicates the one from the NumericUpDown control
        /// </summary>
        [DefaultValue(DEFAULT_THOUSANDS_SEPARATOR)]
        public bool ThousandsSeparator
        {
            get
            {
                return _thousandsSeparator;
            }
            set
            {
                if (_thousandsSeparator != value)
                {
                    _thousandsSeparator = value;
                    SetThousandsSeparator(RowIndex, value);
                    OnCommonChange();
                }
            }
        }

        /// <summary>
        /// Returns the type of the cell's Value property
        /// </summary>
        public override Type ValueType
        {
            get
            {
                if (base.ValueType != null)
                {
                    return base.ValueType;
                }

                return defaultValueType;
            }
        }
        #endregion

        #region Methods
        // Used in KeyEntersEditMode function
        [System.Runtime.InteropServices.DllImport("USER32.DLL", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern short VkKeyScan(char key);

        protected override void Dispose(bool disposing)
        {
            disposing = true;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Clones a DataGridViewNumericUpDownCell cell, copies all the custom properties.
        /// </summary>
        public override object Clone()
        {
            var dataGridViewCell = base.Clone() as DataGridViewNumericUpDownCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.DecimalPlaces = DecimalPlaces;
                dataGridViewCell.Increment = Increment;
                dataGridViewCell.Maximum = Maximum;
                dataGridViewCell.Minimum = Minimum;
                dataGridViewCell.ThousandsSeparator = ThousandsSeparator;
            }

            return dataGridViewCell;
        }

        /// <summary>
        /// Returns the provided value constrained to be within the min and max. 
        /// </summary>
        private decimal Constrain(decimal value)
        {
            Debug.Assert(_minimum <= _maximum);
            if (value < _minimum)
            {
                value = _minimum;
            }

            if (value > _maximum)
            {
                value = _maximum;
            }

            return value;
        }

        /// <summary>
        /// DetachEditingControl gets called by the DataGridView control when the editing session is ending
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            var dataGridView = DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
            {
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
            }

            var numericUpDown = (NumericUpDown)dataGridView.EditingControl;
            if (numericUpDown != null)
            {
                // Editing controls get recycled. Indeed, when a DataGridViewNumericUpDownCell cell gets edited
                // after another DataGridViewNumericUpDownCell cell, the same editing control gets reused for 
                // performance reasons (to avoid an unnecessary control destruction and creation). 
                // Here the undo buffer of the TextBox inside the NumericUpDown control gets cleared to avoid
                // interferences between the editing sessions.
                var textBox = (TextBox)numericUpDown.Controls[1];
                if (textBox != null)
                {
                    textBox.Text = numericUpDown.Value.ToString();  /// 키보드 입력값 반영

                    textBox.ClearUndo();
                }
            }

            base.DetachEditingControl();
        }

        /// <summary>
        /// Adjusts the location and size of the editing control given the alignment characteristics of the cell
        /// </summary>
        private Rectangle GetAdjustedEditingControlBounds(Rectangle editingControlBounds, DataGridViewCellStyle cellStyle)
        {
            // Add a 1 pixel padding on the left and right of the editing control
            editingControlBounds.X += 1;
            editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 2);

            // Adjust the vertical location of the editing control:
            var preferredHeight = cellStyle.Font.Height + 3;
            if (preferredHeight < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - preferredHeight) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - preferredHeight;
                        break;
                }
            }

            return editingControlBounds;
        }

        /// <summary>
        /// Customized implementation of the GetErrorIconBounds function in order to draw the potential 
        /// error icon next to the up/down buttons and not on top of them.
        /// </summary>
        protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        {
            int buttonsWidth = 16;

            var errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            if (DataGridView.RightToLeft == RightToLeft.Yes)
            {
                errorIconBounds.X = errorIconBounds.Left + buttonsWidth;
            }
            else
            {
                errorIconBounds.X = errorIconBounds.Left - buttonsWidth;
            }

            return errorIconBounds;
        }

        /// <summary>
        /// Customized implementation of the GetFormattedValue function in order to include the decimal and thousand separator
        /// characters in the formatted representation of the cell value.
        /// </summary>
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            // By default, the base implementation converts the Decimal 1234.5 into the string "1234.5"
            var formattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
            var formattedNumber = formattedValue as string;
            if (!string.IsNullOrEmpty(formattedNumber) && value != null)
            {
                var unformattedDecimal = Convert.ToDecimal(value);
                var formattedDecimal = Convert.ToDecimal(formattedNumber);
                if (unformattedDecimal == formattedDecimal)
                {
                    // The base implementation of GetFormattedValue (which triggers the CellFormatting event) did nothing else than 
                    // the typical 1234.5 to "1234.5" conversion. But depending on the values of ThousandsSeparator and DecimalPlaces,
                    // this may not be the actual string displayed. The real formatted value may be "1,234.500"
                    return formattedDecimal.ToString((ThousandsSeparator ? "N" : "F") + DecimalPlaces.ToString());
                }
            }

            return formattedValue;
        }

        /// <summary>
        /// Custom implementation of the GetPreferredSize function. This implementation uses the preferred size of the base 
        /// DataGridViewTextBoxCell cell and adds room for the up/down buttons.
        /// </summary>
        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
            {
                return new Size(-1, -1);
            }

            var preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
            {
                int buttonsWidth = 16; // Account for the width of the up/down buttons.
                int buttonMargin = 8;  // Account for some blank pixels between the text and buttons.
                preferredSize.Width += buttonsWidth + buttonMargin;
            }

            return preferredSize;
        }

        /// <summary>
        /// Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
        /// at the beginning of an editing session. It makes sure that the properties of the NumericUpDown editing control are 
        /// set according to the cell properties.
        /// </summary>
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            var numericUpDown = (NumericUpDown)DataGridView.EditingControl;
            if (numericUpDown != null)
            {
                numericUpDown.BorderStyle = BorderStyle.None;
                numericUpDown.DecimalPlaces = DecimalPlaces;
                numericUpDown.Increment = Increment;
                numericUpDown.Maximum = Maximum;
                numericUpDown.Minimum = Minimum;
                numericUpDown.ThousandsSeparator = ThousandsSeparator;
                var initialFormattedValueStr = (string)initialFormattedValue;
                if (initialFormattedValueStr == null)
                {
                    numericUpDown.Text = string.Empty;
                }
                else
                {
                    numericUpDown.Text = initialFormattedValueStr;
                }
            }
        }

        /// <summary>
        /// Custom implementation of the KeyEntersEditMode function. This function is called by the DataGridView control
        /// to decide whether a keystroke must start an editing session or not. In this case, a new session is started when
        /// a digit or negative sign key is hit.
        /// </summary>
        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var negativeSignKey = Keys.None;
            var negativeSignStr = numberFormatInfo.NegativeSign;
            if (!string.IsNullOrEmpty(negativeSignStr) && negativeSignStr.Length == 1)
            {
                negativeSignKey = (Keys)VkKeyScan(negativeSignStr[0]);
            }

            if ((char.IsDigit((char)e.KeyCode) || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || negativeSignKey == e.KeyCode || Keys.Subtract == e.KeyCode) && !e.Shift && !e.Alt && !e.Control)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Called when a cell characteristic that affects its rendering and/or preferred size has changed.
        /// This implementation only takes care of repainting the cells. The DataGridView's autosizing methods
        /// also need to be called in cases where some grid elements autosize.
        /// </summary>
        private void OnCommonChange()
        {
            if (DataGridView != null && !DataGridView.IsDisposed && !DataGridView.Disposing)
            {
                if (RowIndex == -1)
                {
                    // Invalidate and autosize column
                    DataGridView.InvalidateColumn(ColumnIndex);

                    // TODO: Add code to autosize the cell's column, the rows, the column headers 
                    // and the row headers depending on their autosize settings.
                    // The DataGridView control does not expose a public method that takes care of this.
                }
                else
                {
                    // The DataGridView control exposes a public method called UpdateCellValue
                    // that invalidates the cell so that it gets repainted and also triggers all
                    // the necessary autosizing: the cell's column and/or row, the column headers
                    // and the row headers are autosized depending on their autosize settings.
                    DataGridView.UpdateCellValue(ColumnIndex, RowIndex);
                }
            }
        }

        /// <summary>
        /// Determines whether this cell, at the given row index, shows the grid's editing control or not.
        /// The row index needs to be provided as a parameter because this cell may be shared among multiple rows.
        /// </summary>
        private bool OwnsEditingNumericUpDown(int rowIndex)
        {
            if (rowIndex == -1 || DataGridView == null)
            {
                return false;
            }

            var numericUpDownEditingControl = (DataGridViewNumericUpDownEditingControl)DataGridView.EditingControl;
            return numericUpDownEditingControl != null && rowIndex == ((IDataGridViewEditingControl)numericUpDownEditingControl).EditingControlRowIndex;
        }

        /// <summary>
        /// Custom paints the cell. The base implementation of the DataGridViewTextBoxCell type is called first,
        /// dropping the icon error and content foreground parts. Those two parts are painted by this custom implementation.
        /// In this sample, the non-edited NumericUpDown control is painted by using a call to Control.DrawToBitmap. This is
        /// an easy solution for painting controls but it's not necessarily the most performant. An alternative would be to paint
        /// the NumericUpDown control piece by piece (text and up/down buttons).
        /// </summary>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                                      object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (DataGridView == null)
            {
                return;
            }

            // First paint the borders and background of the cell.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
                       paintParts & ~(DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.ContentForeground));

            var ptCurrentCell = DataGridView.CurrentCellAddress;
            var cellCurrent = ptCurrentCell.X == ColumnIndex && ptCurrentCell.Y == rowIndex;
            var cellEdited = cellCurrent && DataGridView.EditingControl != null;

            int buttonsWidth = 16; // Account for the width of the up/down buttons.
                                   //const int ButtonMargin = 8;  // Account for some blank pixels between the text and buttons.
                                   //~jdj

            // If the cell is in editing mode, there is nothing else to paint
            if (!cellEdited)
            {
                if (PartPainted(paintParts, DataGridViewPaintParts.ContentForeground))
                {
                    // Paint a NumericUpDown control
                    // Take the borders into account
                    var borderWidths = BorderWidths(advancedBorderStyle);
                    var valBounds = cellBounds;
                    valBounds.Offset(borderWidths.X, borderWidths.Y);
                    valBounds.Width -= borderWidths.Right;
                    valBounds.Height -= borderWidths.Bottom;
                    // Also take the padding into account
                    if (cellStyle.Padding != Padding.Empty)
                    {
                        if (DataGridView.RightToLeft == RightToLeft.Yes)
                        {
                            valBounds.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
                        }
                        else
                        {
                            valBounds.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                        }

                        valBounds.Width -= cellStyle.Padding.Horizontal;
                        valBounds.Height -= cellStyle.Padding.Vertical;
                    }
                    // Determine the NumericUpDown control location
                    valBounds = GetAdjustedEditingControlBounds(valBounds, cellStyle);

                    var cellSelected = (cellState & DataGridViewElementStates.Selected) != 0;

                    if (_renderingBitmap.Width < valBounds.Width ||
                        _renderingBitmap.Height < valBounds.Height)
                    {
                        // The static bitmap is too small, a bigger one needs to be allocated.
                        _renderingBitmap.Dispose();
                        _renderingBitmap = new Bitmap(valBounds.Width, valBounds.Height);
                    }
                    // Make sure the NumericUpDown control is parented to a visible control

                    /// 재생성시 이 부분에서 ObjectDisposedException 발생 => 주석처리
                    //if (paintingNumericUpDown.Parent == null || !paintingNumericUpDown.Parent.Visible)
                    //{
                    //    paintingNumericUpDown.Parent = this.DataGridView;
                    //}
                    // Set all the relevant properties
                    _paintingNumericUpDown.TextAlign = TranslateAlignment(cellStyle.Alignment);
                    _paintingNumericUpDown.DecimalPlaces = DecimalPlaces;
                    _paintingNumericUpDown.ThousandsSeparator = ThousandsSeparator;
                    _paintingNumericUpDown.Font = cellStyle.Font;
                    _paintingNumericUpDown.Width = valBounds.Width;
                    _paintingNumericUpDown.Height = valBounds.Height;
                    _paintingNumericUpDown.RightToLeft = DataGridView.RightToLeft;
                    _paintingNumericUpDown.Location = new Point(0, -_paintingNumericUpDown.Height - 100);
                    _paintingNumericUpDown.Text = formattedValue as string;

                    Color backColor;
                    if (PartPainted(paintParts, DataGridViewPaintParts.SelectionBackground) && cellSelected)
                    {
                        backColor = cellStyle.SelectionBackColor;
                    }
                    else
                    {
                        backColor = cellStyle.BackColor;
                    }

                    if (PartPainted(paintParts, DataGridViewPaintParts.Background))
                    {
                        if (backColor.A < 255)
                        {
                            // The NumericUpDown control does not support transparent back colors
                            backColor = Color.FromArgb(255, backColor);
                        }

                        _paintingNumericUpDown.BackColor = backColor;
                    }
                    //20180829 add foreColor variable
                    Color foreColor;
                    if (PartPainted(paintParts, DataGridViewPaintParts.ContentForeground) && cellSelected)
                    {
                        foreColor = cellStyle.SelectionForeColor;
                    }
                    else
                    {
                        foreColor = cellStyle.ForeColor;
                    }
                    if (PartPainted(paintParts, DataGridViewPaintParts.ContentForeground))
                    {
                        if (foreColor.A < 255)
                        {
                            foreColor = Color.FromArgb(255, backColor);
                        }

                        _paintingNumericUpDown.ForeColor = foreColor;
                    }

                    // Finally paint the NumericUpDown control //jdj_-buttonswidth로 에디팅에서만 버튼 출력
                    //OwningColumn.dis
                    var srcRect = new Rectangle(0, 0, ((DataGridViewNumericUpDownColumn)OwningColumn).DisplayStyleForCurrentCellOnly ? valBounds.Width - buttonsWidth : valBounds.Width, valBounds.Height);
                    if (srcRect.Width > 0 && srcRect.Height > 0)
                    {
                        _paintingNumericUpDown.DrawToBitmap(_renderingBitmap, srcRect);
                        graphics.DrawImage(_renderingBitmap, new Rectangle(valBounds.Location, valBounds.Size), srcRect, GraphicsUnit.Pixel);
                    }
                }

                if (PartPainted(paintParts, DataGridViewPaintParts.ErrorIcon))
                {

                    // Paint the potential error icon on top of the NumericUpDown control
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText,
                           cellStyle, advancedBorderStyle, DataGridViewPaintParts.ErrorIcon);

                }
            }
        }

        /// <summary>
        /// Little utility function called by the Paint function to see if a particular part needs to be painted. 
        /// </summary>
        private static bool PartPainted(DataGridViewPaintParts paintParts, DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != 0;
        }

        /// <summary>
        /// Custom implementation of the PositionEditingControl method called by the DataGridView control when it
        /// needs to relocate and/or resize the editing control.
        /// </summary>
        public override void PositionEditingControl(bool setLocation,
                                            bool setSize,
                                            Rectangle cellBounds,
                                            Rectangle cellClip,
                                            DataGridViewCellStyle cellStyle,
                                            bool singleVerticalBorderAdded,
                                            bool singleHorizontalBorderAdded,
                                            bool isFirstDisplayedColumn,
                                            bool isFirstDisplayedRow)
        {
            var editingControlBounds = PositionEditingPanel(cellBounds,
                                                        cellClip,
                                                        cellStyle,
                                                        singleVerticalBorderAdded,
                                                        singleHorizontalBorderAdded,
                                                        isFirstDisplayedColumn,
                                                        isFirstDisplayedRow);
            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);
            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }

        /// <summary>
        /// Utility function that sets a new value for the DecimalPlaces property of the cell. This function is used by
        /// the cell and column DecimalPlaces property. The column uses this method instead of the DecimalPlaces
        /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
        /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
        /// this cell may be shared among multiple rows.
        /// </summary>
        internal void SetDecimalPlaces(int rowIndex, int value)
        {
            Debug.Assert(value >= 0 && value <= 99);
            _decimalPlaces = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.DecimalPlaces = value;
            }
        }

        /// Utility function that sets a new value for the Increment property of the cell. This function is used by
        /// the cell and column Increment property. A row index needs to be provided as a parameter because
        /// this cell may be shared among multiple rows.
        internal void SetIncrement(int rowIndex, decimal value)
        {
            Debug.Assert(value >= (decimal)0.0);
            _increment = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.Increment = value;
            }
        }

        /// Utility function that sets a new value for the Maximum property of the cell. This function is used by
        /// the cell and column Maximum property. The column uses this method instead of the Maximum
        /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
        /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
        /// this cell may be shared among multiple rows.
        internal void SetMaximum(int rowIndex, decimal value)
        {
            _maximum = value;
            if (_minimum > _maximum)
            {
                _minimum = _maximum;
            }

            var cellValue = GetValue(rowIndex);
            if (cellValue != null)
            {
                var currentValue = Convert.ToDecimal(cellValue);
                var constrainedValue = Constrain(currentValue);
                if (constrainedValue != currentValue)
                {
                    SetValue(rowIndex, constrainedValue);
                }
            }

            Debug.Assert(_maximum == value);
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.Maximum = value;
            }
        }

        /// Utility function that sets a new value for the Minimum property of the cell. This function is used by
        /// the cell and column Minimum property. The column uses this method instead of the Minimum
        /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
        /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
        /// this cell may be shared among multiple rows.
        internal void SetMinimum(int rowIndex, decimal value)
        {
            _minimum = value;
            if (_minimum > _maximum)
            {
                _maximum = value;
            }

            var cellValue = GetValue(rowIndex);
            if (cellValue != null)
            {
                var currentValue = Convert.ToDecimal(cellValue);
                var constrainedValue = Constrain(currentValue);
                if (constrainedValue != currentValue)
                {
                    SetValue(rowIndex, constrainedValue);
                }
            }

            Debug.Assert(_minimum == value);
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.Minimum = value;
            }
        }

        /// Utility function that sets a new value for the ThousandsSeparator property of the cell. This function is used by
        /// the cell and column ThousandsSeparator property. The column uses this method instead of the ThousandsSeparator
        /// property for performance reasons. This way the column can invalidate the entire column at once instead of 
        /// invalidating each cell of the column individually. A row index needs to be provided as a parameter because
        /// this cell may be shared among multiple rows.
        internal void SetThousandsSeparator(int rowIndex, bool value)
        {
            _thousandsSeparator = value;
            if (OwnsEditingNumericUpDown(rowIndex))
            {
                EditingNumericUpDown.ThousandsSeparator = value;
            }
        }

        /// <summary>
        /// Returns a standard textual representation of the cell.
        /// </summary>
        public override string ToString()
        {
            return "DataGridViewNumericUpDownCell { ColumnIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) + ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }

        /// <summary>
        /// Little utility function used by both the cell and column types to translate a DataGridViewContentAlignment value into
        /// a HorizontalAlignment value.
        /// </summary>
        internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            if ((align & anyRight) != 0)
            {
                return HorizontalAlignment.Right;
            }
            else if ((align & anyCenter) != 0)
            {
                return HorizontalAlignment.Center;
            }
            else
            {
                return HorizontalAlignment.Left;
            }
        }
        #endregion
    }
}
