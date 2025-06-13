using System;
using System.Data.SQLite;
using System.IO;

namespace CoreLibrary.Data
{
    public static class DatabaseHelper
    {
        //private static readonly string dbDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
        //private static readonly string dbPath = Path.Combine(dbDirectory, "memory.db");
        //private static readonly string connectionString = $"Data Source={dbPath};Version=3;";
        private static readonly string dbDirectory = @"G:\Programming\AiMemory\CoreLibrary\Database";
        private static readonly string dbPath = Path.Combine(dbDirectory, "memory.db");
        private static readonly string connectionString = $"Data Source={dbPath};Version=3;";

        public static string GetDbPath()
        {
            return dbPath;
        }

        /// <summary>
        /// Initializes the database and creates tables if they don't exist.
        /// </summary>
        public static void InitializeDatabase()
        {
            if (!Directory.Exists(dbDirectory))
                Directory.CreateDirectory(dbDirectory);

            bool newDb = !File.Exists(dbPath);
            if (newDb)
                SQLiteConnection.CreateFile(dbPath);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                if (newDb)
                {
                    using (var cmdEncoding = new SQLiteCommand("PRAGMA encoding = 'UTF-8';", conn))
                        cmdEncoding.ExecuteNonQuery();
                }

                string createTablesQuery = @"
                CREATE TABLE IF NOT EXISTS Notes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT,
                    Content TEXT NOT NULL,
                    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                );
                CREATE TABLE IF NOT EXISTS Tags (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS UserPreferences (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Key TEXT NOT NULL,
                    Value TEXT
                );";

                using var cmd = new SQLiteCommand(createTablesQuery, conn);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns an open connection to the database.
        /// </summary>
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}