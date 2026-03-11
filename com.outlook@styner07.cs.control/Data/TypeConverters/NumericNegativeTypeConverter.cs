using System.ComponentModel;
using System.Text;

namespace com.outlook_styner07.cs.control.Data.TypeConverters
{
    public class NumericNegativeTypeConverter : StringConverter
    {
        #region Constructors
        private string _lastValidValue = "";
        #endregion

        #region Types
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            var s = value as string ?? "";
            var sb = new StringBuilder();

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                if (c >= '0' && c <= '9' || c == '-' && i == 0)
                {
                    sb.Append(c);
                }
            }

            var result = sb.ToString();

            if (result.Length != s.Length)
            {
                return _lastValidValue;
            }

            _lastValidValue = result;
            return result;
        }
        #endregion
    }
}
