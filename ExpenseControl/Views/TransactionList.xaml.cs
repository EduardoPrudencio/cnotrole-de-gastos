using CommunityToolkit.Mvvm.Messaging;
using ExpenseControl.Models;
using ExpenseControl.Repositories;

namespace ExpenseControl.Views;

public partial class TransactionList : ContentPage
{
    private ITransactionRepository _repository;

    public TransactionList(ITransactionRepository transactionRepository)
    {
        InitializeComponent();

        _repository = transactionRepository;
        GetFinancialValues();

        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            Transactions.ItemsSource = _repository.GetAll();
        });
    }

    private void GetFinancialValues()
    {
        var transactions = _repository.GetAll();
        Transactions.ItemsSource = transactions;

        decimal expenses = transactions
            .Where(t => t.Type == Models.TransactionType.Expenses)
            .Sum(t => t.Value);

        decimal incomes = transactions
            .Where(t => t.Type == Models.TransactionType.Icome)
            .Sum(t => t.Value);

        LabelReceita.Text = incomes.ToString("C");
        LabelDespesa.Text = expenses.ToString("C");
        LabelTotal.Text = (incomes - expenses).ToString("C");
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(transactionAdd);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

        var grid = (Grid)sender;
        var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
        Transaction transaction = (Transaction)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        transactionEdit.SetTransactionToEdit(transaction);
        Navigation.PushModalAsync(transactionEdit);
    }

}