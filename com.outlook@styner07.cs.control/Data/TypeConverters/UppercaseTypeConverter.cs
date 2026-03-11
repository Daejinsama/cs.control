using System.ComponentModel;
using System.Text;

namespace com.outlook_styner07.cs.control.Data.TypeConverters
{
    public class UppercaseTypeConverter : StringConverter
    {
        #region Constructors
        #endregion

        #region Types
        #endregion

        #region Fields
        private string _lastValidValue = "";
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

            foreach (var c in s)
            {
                if (c >= 'a' && c <= 'z')
                {
                    sb.Append(char.ToUpperInvariant(c));
                }
                else if (c >= 'A' && c <= 'Z' || c == '_')
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
