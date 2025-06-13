using Xunit;
using CoreLibrary.Data;
using CoreLibrary.Models;
using CoreLibrary.Utilities;
using System.Data.SQLite;
using System.IO;

namespace CoreLibrary.Tests
{
    public class UserPreferenceRepositoryTests
    {
        private readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        [Fact]
        public void SetPreference_ShouldInsertOrUpdatePreference_AndLogIt()
        {
            // حذف لاگ‌های قبلی قبل از شروع تست
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            var preference = new UserPreference { Key = "Theme", Value = "Dark" };
            UserPreferenceRepository.SetPreference(preference);

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using var cmd = new SQLiteCommand("SELECT Value FROM UserPreferences WHERE Key = 'Theme';", conn);
            var result = cmd.ExecuteScalar()?.ToString();

            Assert.Equal("Dark", result);

            // بررسی می‌کنیم که لاگ ذخیره شده
            Assert.True(File.Exists(logFilePath), "❌ Log file was not created!");
            var logContent = FileHelper.ReadFromFile(logFilePath);
            Assert.Contains("✅ Preference saved", logContent);

            // حالا مقدار را تغییر داده و دوباره بررسی می‌کنیم
            var updatedPreference = new UserPreference { Key = "Theme", Value = "Light" };
            UserPreferenceRepository.SetPreference(updatedPreference);

            using var cmd2 = new SQLiteCommand("SELECT Value FROM UserPreferences WHERE Key = 'Theme';", conn);
            var updatedResult = cmd2.ExecuteScalar()?.ToString();

            Assert.Equal("Light", updatedResult);

            // بررسی می‌کنیم که به‌روزرسانی هم داخل لاگ ثبت شده باشد
            //logContent = File.ReadAllText(logFilePath);
            logContent=FileHelper.ReadFromFile(logFilePath);
            Assert.Contains("✅ Preference saved", logContent);
        }
    }
}