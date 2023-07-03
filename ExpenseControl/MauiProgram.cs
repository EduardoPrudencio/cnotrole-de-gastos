using ExpenseControl.Repositories;
using ExpenseControl.Views;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace ExpenseControl;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterDataBaseAndRepositories()
            .RegisterViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterDataBaseAndRepositories(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<LiteDatabase>
            (
                options =>
                {
                    string connectionString = $"Filename={Path.Combine(FileSystem.AppDataDirectory)}database.db;connection=Shared";
                    return new LiteDatabase(connectionString);
                }
            );
        mauiAppBuilder.Services.AddTransient<ITransactionRepository, TransactionRepositoryLiteDb>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<TransactionList>();
        mauiAppBuilder.Services.AddTransient<TransactionEdit>();
        mauiAppBuilder.Services.AddTransient<TransactionAdd>();
        return mauiAppBuilder;
    }
}
