using com.outlook_styner07.cs.control.Charting;

namespace Playground
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Controls.Clear();
            DjsmFullSpectrumChart chart = new DjsmFullSpectrumChart();
            DjsmChartPanel pnlChart = new DjsmChartPanel(chart);
            Controls.Add(pnlChart);

        }
    }
}
