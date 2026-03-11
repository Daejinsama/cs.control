namespace com.outlook_styner07.cs.control.Common
{
    public class DjsmCalendar : UserControl
    {
        #region Constructors
        public DjsmCalendar()
        {
            DoubleBuffered = true;
            Size = new Size(250, 200);

            _currentMonth = DateTime.Now;
            _selectedDate = DateTime.Now;
            _backgroundColor = Color.LightBlue;
            _selectedDateColor = Color.Red;
            _dateFont = new Font("Arial", 10);
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private DateTime _currentMonth;
        private DateTime _selectedDate;
        private Color _backgroundColor;
        private Color _selectedDateColor;
        private Font _dateFont;
        #endregion

        #region Properties
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            // Draw background
            using (SolidBrush brush = new SolidBrush(_backgroundColor))
            {
                g.FillRectangle(brush, ClientRectangle);
            }

            // Draw the month and year
            string monthYear = _currentMonth.ToString("MMMM yyyy");
            SizeF monthYearSize = g.MeasureString(monthYear, _dateFont);
            g.DrawString(monthYear, _dateFont, Brushes.Black, (Width - monthYearSize.Width) / 2, 10);

            // Draw the days of the week
            string[] daysOfWeek = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                g.DrawString(daysOfWeek[i], _dateFont, Brushes.Black, i * (Width / 7), 40);
            }

            // Draw the days of the month
            int daysInMonth = DateTime.DaysInMonth(_currentMonth.Year, _currentMonth.Month);
            DateTime firstDay = new DateTime(_currentMonth.Year, _currentMonth.Month, 1);
            int startDay = (int)firstDay.DayOfWeek;

            for (int day = 1; day <= daysInMonth; day++)
            {
                int row = (startDay + day - 1) / 7;
                int col = (startDay + day - 1) % 7;

                Rectangle dayRect = new Rectangle(col * (Width / 7), 60 + row * 30, Width / 7, 30);

                if (new DateTime(_currentMonth.Year, _currentMonth.Month, day) == _selectedDate)
                {
                    using (SolidBrush brush = new SolidBrush(_selectedDateColor))
                    {
                        g.FillRectangle(brush, dayRect);
                    }
                }

                g.DrawString(day.ToString(), _dateFont, Brushes.Black, dayRect);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int x = e.X / (Width / 7);
            int y = (e.Y - 60) / 30;

            int day = y * 7 + x - (int)new DateTime(_currentMonth.Year, _currentMonth.Month, 1).DayOfWeek + 1;

            if (day > 0 && day <= DateTime.DaysInMonth(_currentMonth.Year, _currentMonth.Month))
            {
                _selectedDate = new DateTime(_currentMonth.Year, _currentMonth.Month, day);
                Invalidate();
            }
        }
        #endregion
    }
}
