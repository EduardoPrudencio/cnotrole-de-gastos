
using ExpenseControl.Views;

namespace ExpenseControl;

public partial class App : Application
{
    public App(TransactionList transactionList)
    {
        InitializeComponent();

        //MainPage = new AppShell();

        MainPage = new NavigationPage(transactionList);
    }
}
