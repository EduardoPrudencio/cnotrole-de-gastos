namespace ExpenseControl.Views;

public partial class TransactionList : ContentPage
{
    private TransactionAdd _transactionAdd;
    private TransactionEdit _transactionEdit;

    public TransactionList(TransactionAdd transactionAdd, TransactionEdit transactionEdit)
    {
        InitializeComponent();

        _transactionAdd = transactionAdd;
        _transactionEdit = transactionEdit;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(_transactionAdd);
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(_transactionEdit);
    }
}