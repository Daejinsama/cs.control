namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmButton : System.Windows.Forms.Button
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = DjsmColorTable.DEFAULT_BACKGROUND_SELECTED_COLOR;
            FlatAppearance.MouseDownBackColor = DjsmColorTable.DEFAULT_BACKGROUND_FOCUSED_COLOR;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

        }
    }
}
