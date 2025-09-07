using System.ComponentModel;
using System.Text;

namespace com.outlook_styner07.cs.control.Data
{
    public class UppercaseTypeConverter : StringConverter
    {
        private string _lastValidValue = "";

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            string s = value as string ?? "";
            var sb = new StringBuilder();

            foreach (char c in s)
            {
                if (c >= 'a' && c <= 'z')
                {
                    sb.Append(char.ToUpperInvariant(c));
                }
                else if ((c >= 'A' && c <= 'Z') || c == '_')
                {
                    sb.Append(c);
                }
            }

            string result = sb.ToString();

            if (result.Length != s.Length)
            {
                return _lastValidValue;
            }

            _lastValidValue = result;
            return result;
        }
    }

    public class NumericNegativeTypeConverter : StringConverter
    {
        private string _lastValidValue = "";

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            string s = value as string ?? "";
            var sb = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if ((c >= '0' && c <= '9') || (c == '-' && i == 0))
                {
                    sb.Append(c);
                }
            }

            string result = sb.ToString();

            if (result.Length != s.Length)
            {
                return _lastValidValue;
            }

            _lastValidValue = result;
            return result;
        }
    }

    public class DecimalNegativeTypeConverter : StringConverter
    {
        private string _lastValidValue = "";

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
        {
            string s = value as string ?? "";
            var sb = new StringBuilder();
            bool hasDot = false;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if ((c >= '0' && c <= '9') || (c == '-' && i == 0))
                {
                    sb.Append(c);
                }
                else if (c == '.' && !hasDot)
                {
                    sb.Append(c);
                    hasDot = true;
                }
            }

            string result = sb.ToString();

            if (result.Length != s.Length)
                return _lastValidValue;

            _lastValidValue = result;
            return result;
        }
    }
}
