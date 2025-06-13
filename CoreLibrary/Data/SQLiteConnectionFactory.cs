using System.Data.SQLite;

namespace CoreLibrary.Data
{
    public class SQLiteConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString = "Data Source=ai_memory.db;Version=3;";

        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

    }
}