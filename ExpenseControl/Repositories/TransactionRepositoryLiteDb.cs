using LiteDB;
using System.Transactions;

namespace ExpenseControl.Repositories
{
    public class TransactionRepositoryLiteDb : ITransactionRepository
    {
        readonly LiteDatabase _database;
        readonly string _collectionName = "transactions";

        public TransactionRepositoryLiteDb(LiteDatabase database)
        {
            _database = database;
        }

        public IEnumerable<Models.Transaction> GetAll()
        {
            return _database.GetCollection<Models.Transaction>(_collectionName)
                .Query()
                .OrderByDescending(t => t.Date).ToList();
        }

        public Transaction GetById(int id) { return null; }

        public void Add(Models.Transaction transaction)
        {
            ILiteCollection<Models.Transaction> collection = _database.GetCollection<Models.Transaction>(_collectionName);
            collection.Insert(transaction);
            collection.EnsureIndex(t => t.Date);
        }
        public void Update(Models.Transaction transaction)
        {
            _database.GetCollection<Models.Transaction>(_collectionName).Update(transaction);
        }

        public void Delete(Models.Transaction transaction)
        {
            _database.GetCollection<Models.Transaction>(_collectionName).Delete(transaction.Id);
        }
    }
}
