namespace com.outlook_styner07.cs.control.Data
{
    public class DjsmListView : ListView
    {
        #region Constructors
        public DjsmListView()
        {
            OwnerDraw = true;
            AllowColumnReorder = false;
            DoubleBuffered = true;
            FullRowSelect = true;
            MultiSelect = false;

            BorderStyle = BorderStyle.None;
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            View = View.Details;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        #endregion

        #region Properties
        public int[] ColumnFillWeight { get; set; }
        #endregion

        #region Methods
        public void SetColumnFillWeight(int[] weight)
        {
            ColumnFillWeight = weight;
        }

        protected override void OnResize(EventArgs e)
        {
            if (ColumnFillWeight != null)
            {
                int totalSplit = 0;

                foreach (int i in ColumnFillWeight)
                {
                    totalSplit += i;
                }

                int totalColumnsWidth = 0;
                for (int len = ColumnFillWeight.Length, i = 0; i < len; i++)
                {
                    Columns[i].Width = Width / totalSplit * ColumnFillWeight[i];
                    Columns[i].TextAlign = HorizontalAlignment.Center;
                    totalColumnsWidth += Columns[i].Width;
                }

                // fill lefted gap
                if (totalColumnsWidth < Width)
                {
                    Columns[Columns.Count - 1].Width += Width - totalColumnsWidth;
                }
            }
        }

        protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
        {
            if (ColumnFillWeight != null)
            {
                e.Cancel = true;
                e.NewWidth = Columns[e.ColumnIndex].Width;
            }
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), e.Bounds);

            Font f = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);
            SizeF s = e.Graphics.MeasureString(e.Header.Text, f);
            PointF p = new PointF(e.Bounds.X + ((e.Bounds.Width - s.Width) / 2), e.Bounds.Y);

            using (var b = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(e.Header.Text, f, b, p.X, p.Y);
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.Item.BackColor = e.Item.Selected ? DjsmColorTable.PrimaryLight : BackColor;
            e.Item.ForeColor = e.Item.Selected ? Color.White : Color.Black;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            e.SubItem.BackColor = e.Item.Selected ? DjsmColorTable.PrimaryLight : BackColor;
            e.SubItem.ForeColor = e.Item.Selected ? Color.White : Color.Black;
            e.DrawBackground();

            Graphics g = e.Graphics;
            if (e.Item.IndentCount > 0 && e.ColumnIndex == 0)
            {
                Rectangle rect = e.SubItem.Bounds;
                rect.X += e.Item.IndentCount * 25;
                using (var b = new SolidBrush(e.Item.ForeColor))
                {
                    g.DrawString(e.SubItem.Text, e.Item.Font, b, rect);
                }
            }
            else
            {
                e.DrawText();
            }
        }
        #endregion
    }
}
