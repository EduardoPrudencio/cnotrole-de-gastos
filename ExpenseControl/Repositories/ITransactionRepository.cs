namespace ExpenseControl.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Models.Transaction> GetAll();
        void Add(Models.Transaction transaction);
        void Update(Models.Transaction transaction);
        void Delete(Models.Transaction transaction);

    }
}
