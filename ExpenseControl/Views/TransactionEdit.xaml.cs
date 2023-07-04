using ExpenseControl.Models;

namespace ExpenseControl.Views;

public partial class TransactionEdit : ContentPage
{
    private Transaction _transaction;

    public TransactionEdit()
    {
        InitializeComponent();
    }

    public void SetTransactionToEdit(Transaction transaction)
    {
        _transaction = transaction;

        RadioExpense.IsChecked = transaction.Type == TransactionType.Expenses;
        RadioIncome.IsChecked = transaction.Type == TransactionType.Icome;
        EntryName.Text = transaction.Name;
        EntryValue.Text = transaction.Value.ToString("C");
        DatePickerDate.Date = _transaction.Date.Date;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

}