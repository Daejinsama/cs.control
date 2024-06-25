namespace com.outlook_styner07.cs.control.Common
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DjsmCalendar : UserControl
    {
        private DateTime currentMonth;
        private DateTime selectedDate;
        private Color backgroundColor;
        private Color selectedDateColor;
        private Font dateFont;

        public DjsmCalendar()
        {
            this.DoubleBuffered = true;
            this.currentMonth = DateTime.Now;
            this.selectedDate = DateTime.Now;
            this.backgroundColor = Color.LightBlue;
            this.selectedDateColor = Color.Red;
            this.dateFont = new Font("Arial", 10);
            this.Size = new Size(250, 200);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Draw background
            using (SolidBrush brush = new SolidBrush(this.backgroundColor))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }

            // Draw the month and year
            string monthYear = currentMonth.ToString("MMMM yyyy");
            SizeF monthYearSize = g.MeasureString(monthYear, this.dateFont);
            g.DrawString(monthYear, this.dateFont, Brushes.Black, (this.Width - monthYearSize.Width) / 2, 10);

            // Draw the days of the week
            string[] daysOfWeek = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                g.DrawString(daysOfWeek[i], this.dateFont, Brushes.Black, i * (this.Width / 7), 40);
            }

            // Draw the days of the month
            int daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
            DateTime firstDay = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            int startDay = (int)firstDay.DayOfWeek;

            for (int day = 1; day <= daysInMonth; day++)
            {
                int row = (startDay + day - 1) / 7;
                int col = (startDay + day - 1) % 7;

                Rectangle dayRect = new Rectangle(col * (this.Width / 7), 60 + row * 30, this.Width / 7, 30);

                if (new DateTime(currentMonth.Year, currentMonth.Month, day) == selectedDate)
                {
                    using (SolidBrush brush = new SolidBrush(this.selectedDateColor))
                    {
                        g.FillRectangle(brush, dayRect);
                    }
                }

                g.DrawString(day.ToString(), this.dateFont, Brushes.Black, dayRect);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int x = e.X / (this.Width / 7);
            int y = (e.Y - 60) / 30;

            int day = y * 7 + x - (int)new DateTime(currentMonth.Year, currentMonth.Month, 1).DayOfWeek + 1;

            if (day > 0 && day <= DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month))
            {
                selectedDate = new DateTime(currentMonth.Year, currentMonth.Month, day);
                this.Invalidate();
            }
        }
    }

}
