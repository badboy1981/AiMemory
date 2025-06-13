using System.Data.SQLite;

namespace CoreLibrary.Data
{
    public static class DatabaseManager
    {
        private static readonly string _connectionString = "Data Source=ai_memory.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public static void InitializeDatabase()
        {
            using var conn = GetConnection();
            conn.Open();

            const string createTablesQuery = @"
    CREATE TABLE IF NOT EXISTS Notes (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Title TEXT NOT NULL,
        Content TEXT NOT NULL,
        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
    );

    CREATE TABLE IF NOT EXISTS Tags (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Name TEXT NOT NULL UNIQUE
    );

    CREATE TABLE IF NOT EXISTS UserPreferences (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        UserId INTEGER NOT NULL,
        PreferenceKey TEXT NOT NULL,
        PreferenceValue TEXT NOT NULL
    );
";

            using var cmd = new SQLiteCommand(createTablesQuery, conn);
            cmd.ExecuteNonQuery();

            Console.WriteLine("✅ Database initialized: ai_memory.db");
        }
    }
}