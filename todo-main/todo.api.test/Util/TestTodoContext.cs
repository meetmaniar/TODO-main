using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using todo.data;

namespace todo.api.test.Util
{
    public class TestTodoContext : TodoContext
    {
        private readonly SqliteConnection _connection;

        public TestTodoContext()
        {
            var dbName = Guid.NewGuid().ToString();
            _connection = new SqliteConnection($"Data Source={dbName};Mode=Memory;");
            _connection.Open();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connection);
        }

        public void DetachAll() => base.DetachAll();

        public override void Dispose()
        {
            _connection.Dispose();
            base.Dispose();
        }
    }
}
