using System.Globalization;

namespace ExpenseControl.Converters
{
    internal class TransactionNameColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Color.FromArgb("#FFFFFF");

            var randon = new Random();
            var color = String.Format("#FF{0:X6}", randon.Next(0x1000000));
            return Color.FromArgb(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
