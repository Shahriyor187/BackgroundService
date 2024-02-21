using Background.Model;
using MongoDB.Driver;

namespace Background
{
    public class TodoDb
    {
        public IMongoDatabase _database { get; }
        public TodoDb(string connection, string database)
        {
            var client = new MongoClient(connection);
            _database = client.GetDatabase(database);
        }
        public IMongoCollection<Todo> Todos
             => _database.GetCollection<Todo>("Todos");
    }
}
