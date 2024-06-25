using System.Drawing;

namespace com.outlook_styner07.cs.control
{
    public class DjsmColorTable
    {
        public static readonly Color DEFAULT_BORDER_COLOR = Color.Transparent;
        public static readonly Color DEFAULT_BACKGROUND_COLOR = Color.White;
        public static readonly Color DEFAULT_BACKGROUND_SELECTED_COLOR = Color.FromArgb(40, SecondaryLight);
        public static readonly Color DEFAULT_BACKGROUND_FOCUSED_COLOR = Color.FromArgb(60, SecondaryLight);
        public static readonly Color DEFAULT_TOOLSTRIP_BORDER_COLOR = Color.FromArgb(50, Secondary);

        public static Color Primary = Color.FromArgb(0xF5, 0x82, 0x1F);
        public static Color PrimaryDark = Color.FromArgb(0xFF, 0x66, 0x19);
        public static Color PrimaryLight = Color.FromArgb(80, Primary);

        public static Color Secondary = Color.FromArgb(0x77, 0x78, 0x7B);
        public static Color SecondaryDark = Color.FromArgb(0x59, 0x59, 0x59);
        public static Color SecondaryLight = Color.FromArgb(80, Secondary);

        public static Color
            BackColorSecondaryLightOpacity10 = Color.FromArgb(90, 91, 91),
            BackColorSecondaryDarkOpacity10 = Color.FromArgb(237, 237, 237),
            BackColorSecondaryDarkOpacity20 = Color.FromArgb(221, 221, 221);

        public static Color
            IndicatorGreen = Color.Lime, //Color.FromArgb(/*192, */62, 187, 69),
            IndicatorYellow = Color.Yellow, //Color.FromArgb(/*192, */255, 214, 21),
            IndicatorOrange = Color.Orange,//Color.FromArgb(/*192, */246, 126, 4),
            IndicatorRed = Color.Red,//Color.FromArgb(/*192, */222, 44, 40),
            IndicatorDisabled = Color.LightGray;

        public static Color[] SeriesColors = new Color[] {
            Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Navy, Color.Violet
            //Color.Red, Color.DarkRed, Color.IndianRed, Color.MediumVioletRed, Color.OrangeRed, Color.PaleVioletRed,
            //Color.Orange, Color.DarkOrange, 
            //Color.Yellow, Color.GreenYellow, Color.LightGoldenrodYellow, Color.LightYellow,
            //Color.Green, Color.DarkGreen, Color.DarkOliveGreen, Color.DarkSeaGreen, Color.ForestGreen, Color.LawnGreen, Color.LightGreen, Color.LightSeaGreen, Color.LimeGreen, Color.MediumSeaGreen, Color.MediumSpringGreen, Color.PaleGreen, Color.SeaGreen, Color.SpringGreen, Color.YellowGreen,
            //Color.Blue, Color.AliceBlue, Color.CadetBlue, Color.CornflowerBlue, Color.DarkBlue, Color.DarkSlateBlue, Color.DeepSkyBlue, Color.DodgerBlue, Color.LightBlue, Color.LightSkyBlue, Color.LightSteelBlue, Color.MediumBlue, Color.MediumSlateBlue, Color.MidnightBlue, Color.PowderBlue, Color.RoyalBlue, Color.SkyBlue, Color.SlateBlue, Color.SteelBlue,
            //Color.Navy, 
            //Color.Violet, Color.BlueViolet, Color.DarkViolet
        };

        public static Color
            StripLineStartAlgorithmStart = Color.FromArgb(152, 203, 49),
            StripLineEndAlgorithmStart = Color.FromArgb(136, 180, 44),
            StripLineStartJustEtch = Color.FromArgb(91, 77, 179),
            StripLineEndJustEtch = Color.FromArgb(74, 62, 150),
            StripLineStartEndPoint = Color.FromArgb(168, 75, 164),
            StripLineEndEndPoint = Color.FromArgb(160, 70, 157);
    }
}
