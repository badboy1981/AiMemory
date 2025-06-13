using System.Data.SQLite;
using System.IO;
using System.Text;
using CoreLibrary.Data;
using CoreLibrary.Utilities;
using Xunit;

namespace CoreLibrary.Tests
{
    public class DatabaseManagerTests
    {
        [Fact]
        public void Database_ShouldBeInitializedCorrectly()
        {
            var logBuilder = new StringBuilder();
            logBuilder.AppendLine("🚀 Running Database Initialization Test...");

            DatabaseManager.InitializeDatabase();

            if (File.Exists("ai_memory.db"))
            {
                logBuilder.AppendLine("✅ Database file exists.");
            }
            else
            {
                logBuilder.AppendLine("❌ Database file is missing!");
                Assert.Fail("Database file does not exist!");
            }

            using var conn = DatabaseManager.GetConnection();
            conn.Open();
            logBuilder.AppendLine("✅ Connection opened successfully.");

            using var cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Notes';", conn);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                logBuilder.AppendLine("✅ Table 'Notes' exists in the database.");
            }
            else
            {
                logBuilder.AppendLine("❌ Table 'Notes' is missing!");
                Assert.Fail("Table 'Notes' does not exist!");
            }

            // ذخیره لاگ تست در فایل
            FileHelper.SaveToFile("database_test_log.txt", logBuilder.ToString());
        }
    }
}