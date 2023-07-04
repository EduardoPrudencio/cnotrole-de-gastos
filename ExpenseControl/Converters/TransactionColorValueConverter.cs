using ExpenseControl.Models;
using System.Globalization;

namespace ExpenseControl.Converters
{
    internal class TransactionColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Transaction transaction = (Transaction)value;

            if (transaction == null) return Colors.Black;

            return transaction.Type == TransactionType.Expenses ? Colors.Red : Colors.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


