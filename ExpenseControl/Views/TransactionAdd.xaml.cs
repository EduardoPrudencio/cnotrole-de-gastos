using CommunityToolkit.Mvvm.Messaging;
using ExpenseControl.Models;
using ExpenseControl.Repositories;

namespace ExpenseControl.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionRepository _repository;

    public TransactionAdd(ITransactionRepository repository)
    {
        InitializeComponent();

        _repository = repository;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

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

    private void SaveTransaction()
    {
        Models.Transaction transaction = new()
        {
            Type = RadioIcome.IsChecked ? TransactionType.Icome
                                        : TransactionType.Expenses,
            Name = EntryName.Text,
            Value = decimal.Parse(EntryValue.Text),
            Date = DataPickerDate.Date,
        };

        var repository = this.Handler.MauiContext.Services.GetService<ITransactionRepository>();
        _repository.Add(transaction);
    }

    private bool IsValid() =>
        !string.IsNullOrWhiteSpace(EntryName.Text) &&
           !string.IsNullOrWhiteSpace(EntryValue.Text) &&
            decimal.Parse(EntryValue.Text) > 0;
}