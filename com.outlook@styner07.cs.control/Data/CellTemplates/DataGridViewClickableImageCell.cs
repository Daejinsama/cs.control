namespace com.outlook_styner07.cs.control.Data.CellTemplates
{
    public class DataGridViewClickableImageCell : DataGridViewImageCell
    {
        #region Constructors
        public DataGridViewClickableImageCell()
        {
            ValueIsIcon = false;
            ImageLayout = DataGridViewImageCellLayout.Normal;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        #endregion

        #region Properties
        public Image ImgDefault { get; set; }
        public Image ImgPressed { get; set; }
        #endregion

        #region Methods
        protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Value = ImgPressed;
            }
        }

        protected override void OnMouseLeave(int rowIndex)
        {
            base.OnMouseLeave(rowIndex);
            Value = ImgDefault;
        }

        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                Value = ImgDefault;
            }
        }
        #endregion
    }
}
