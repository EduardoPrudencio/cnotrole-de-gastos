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
            GetFinancialValues();
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

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        //var border = (Border) sender;
        //var gesture = (TapGestureRecognizer) border.GestureRecognizers.First();
        //Transaction transaction = (Transaction)gesture.CommandParameter;

        bool response = await App.Current.MainPage.DisplayAlert("Excluir", "Deseja excluir esse item?", "sim", "não");

        if (response)
        {
            Transaction transaction = (Transaction)e.Parameter;
            _repository.Delete(transaction);
            GetFinancialValues();
        }
    }
}