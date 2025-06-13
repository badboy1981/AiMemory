using Xunit;
using CoreLibrary.Data;
using CoreLibrary.Models;
using CoreLibrary.Utilities;
using System.Data.SQLite;
using System.Text;

namespace CoreLibrary.Tests
{
    public class TagRepositoryTests
    {
        [Fact]
        public void InsertTag_ShouldStoreTagCorrectly()
        {
            var logBuilder = new StringBuilder();
            logBuilder.AppendLine("🚀 Running TagRepository Test...");

            DatabaseManager.InitializeDatabase();
            logBuilder.AppendLine("✅ Database initialized.");

            var newTag = new Tag { Name = "AI" };
            TagRepository.InsertTag(newTag);
            logBuilder.AppendLine("✅ InsertTag executed.");

            using var conn = DatabaseManager.GetConnection();
            conn.Open();
            logBuilder.AppendLine("✅ Connection opened successfully.");

            using var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Tags WHERE Name = 'AI';", conn);
            var count = Convert.ToInt32(cmd.ExecuteScalar());

            Assert.True(count > 0, "❌ Tag 'AI' was not found in the database!");

            logBuilder.AppendLine($"✅ Tag 'AI' exists in database, count: {count}");

            // ذخیره لاگ تست در فایل
            FileHelper.SaveToFile("tag_repository_test_log.txt", logBuilder.ToString());
        }
    }
}