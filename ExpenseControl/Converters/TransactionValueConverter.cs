using ExpenseControl.Models;
using System.Globalization;

namespace ExpenseControl.Converters
{
    internal class TransactionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Transaction transaction = (Transaction)value;
            if (transaction == null) return string.Empty;

            return transaction.Type == TransactionType.Expenses ? $"- {transaction.Value.ToString("C")}" : transaction.Value.ToString("C");

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


