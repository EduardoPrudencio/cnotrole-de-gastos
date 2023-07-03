using CommunityToolkit.Mvvm.Messaging;
using ExpenseControl.Repositories;

namespace ExpenseControl.Views;

public partial class TransactionList : ContentPage
{
    private ITransactionRepository _repository;

    public TransactionList(ITransactionRepository transactionRepository)
    {
        InitializeComponent();

        _repository = transactionRepository;
        Transactions.ItemsSource = _repository.GetAll();

        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            Transactions.ItemsSource = _repository.GetAll();
        });
    }


    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        Navigation.PushModalAsync(transactionEdit);
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(transactionAdd);
    }
}