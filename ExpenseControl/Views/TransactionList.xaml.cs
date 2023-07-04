using CommunityToolkit.Mvvm.Messaging;
using ExpenseControl.Models;
using ExpenseControl.Repositories;

namespace ExpenseControl.Views;

public partial class TransactionList : ContentPage
{
    private ITransactionRepository _repository;
    private Color _borderOriginalBackgroundColor;
    private string _labelOriginalText;

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

    private async Task AnimationBorder(Border border, bool isDeleteAnimation)
    {
        var label = (Label)border.Content;

        if (isDeleteAnimation)
        {
            _borderOriginalBackgroundColor = border.BackgroundColor;
            _labelOriginalText = label.Text;

            await border.RotateYTo(90, 200);
            border.BackgroundColor = Colors.Red;

            label.Text = "X";
            label.TextColor = Colors.White;

            await border.RotateYTo(180, 200);

        }
        else
        {
            await border.RotateYTo(90, 200);
            border.BackgroundColor = _borderOriginalBackgroundColor;

            label.Text = _labelOriginalText;
            label.TextColor = Colors.White;
            await border.RotateYTo(0, 200);
        }
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

        await AnimationBorder((Border)sender, true);
        bool response = await App.Current.MainPage.DisplayAlert("Excluir", "Deseja excluir esse item?", "sim", "não");

        await AnimationBorder((Border)sender, false);

        if (response)
        {
            Transaction transaction = (Transaction)e.Parameter;
            _repository.Delete(transaction);
            GetFinancialValues();
        }
    }
}