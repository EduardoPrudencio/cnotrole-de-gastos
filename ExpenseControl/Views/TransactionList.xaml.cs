using ExpenseControl.Repositories;

namespace ExpenseControl.Views;

public partial class TransactionList : ContentPage
{
    private TransactionAdd _transactionAdd;
    private TransactionEdit _transactionEdit;
    private ITransactionRepository _repository;

    public TransactionList(TransactionAdd transactionAdd, TransactionEdit transactionEdit, ITransactionRepository transactionRepository)
    {
        InitializeComponent();

        _transactionAdd = transactionAdd;
        _transactionEdit = transactionEdit;
        _repository = transactionRepository;

        Transactions.ItemsSource = _repository.GetAll();
    }


    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(_transactionEdit);
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(_transactionAdd);
    }
}