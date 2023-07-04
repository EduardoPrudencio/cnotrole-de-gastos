using CommunityToolkit.Mvvm.Messaging;
using ExpenseControl.Models;
using ExpenseControl.Repositories;

namespace ExpenseControl.Views;

public partial class TransactionEdit : ContentPage
{
    private ITransactionRepository _repository;
    private Transaction _transaction;

    public TransactionEdit(ITransactionRepository repository)
    {
        InitializeComponent();
        _repository = repository;
    }

    public void SetTransactionToEdit(Transaction transaction)
    {
        _transaction = transaction;

        RadioExpense.IsChecked = transaction.Type == TransactionType.Expenses;
        RadioIncome.IsChecked = transaction.Type == TransactionType.Icome;
        EntryName.Text = transaction.Name;
        EntryValue.Text = transaction.Value.ToString();
        DatePickerDate.Date = _transaction.Date.Date;
    }

    private void SaveTransaction()
    {
        Models.Transaction transaction = new()
        {
            Id = _transaction.Id,
            Type = RadioIncome.IsChecked ? TransactionType.Icome
                                        : TransactionType.Expenses,
            Name = EntryName.Text,
            Value = decimal.Parse(EntryValue.Text),
            Date = DatePickerDate.Date,
        };

        var repository = this.Handler.MauiContext.Services.GetService<ITransactionRepository>();
        _repository.Update(transaction);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private bool IsValid() =>
        !string.IsNullOrWhiteSpace(EntryName.Text) &&
           !string.IsNullOrWhiteSpace(EntryValue.Text) &&
            decimal.Parse(EntryValue.Text) > 0;

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (!IsValid())
        {
            LabelError.IsVisible = true;
            return;
        }

        SaveTransaction();

        WeakReferenceMessenger.Default.Send<string>(string.Empty);

        // int transactionsCount = _repository.GetAll().Count();
        App.Current.MainPage.DisplayAlert("Mensagem", "Transação registrada", "Ok");
        Navigation.PopModalAsync();
    }
}