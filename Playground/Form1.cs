using com.outlook_styner07.cs.control;
using com.outlook_styner07.cs.control.Data;
using System.ComponentModel;
using System.Drawing.Design;

namespace Playground
{
    public partial class Form1 : DjsmForm
    {
        public Form1()
        {
            InitializeComponent();
            djsmPropertyGrid1.SelectedObject = new TestObject();
        }

        private void djsmButton1_Click(object sender, EventArgs e)
        {
            djsmImagePanel1.FitToFrame();
        }
    }

    public class TestObject
    {
        [TypeConverter(typeof(UppercaseTypeConverter))]
        [Category("Demo")]
        [Description("영문 대문자(A-Z)만 허용")]
        public string Text1 { get; set; }

        [TypeConverter(typeof(NumericNegativeTypeConverter))]
        [Category("Demo")]
        public string Text2 { get; set; }

        [TypeConverter(typeof(DecimalNegativeTypeConverter))]
        [Category("Demo")]

        public string Text3 { get; set; }

        public string Text4 { get; set; }

        public string Text5 { get; set; }
    }
}
