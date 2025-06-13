using System;
using System.Collections.Generic;
using System.Data.SQLite;
using CoreLibrary.Models;
using CoreLibrary.Utilities;

namespace CoreLibrary.Data
{
    public static class UserPreferenceRepository
    {
        /// <summary>
        /// Inserts or updates a user preference.
        /// </summary>
        public static void SetPreference(UserPreference preference)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(preference.Key))
                {
                    LogService.Error("Attempted to insert a preference with an empty key.");
                    throw new ArgumentException("Preference key cannot be null or empty.");
                }

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string upsertQuery = "INSERT INTO UserPreferences (Key, Value) VALUES (@key, @value) ON CONFLICT(Key) DO UPDATE SET Value = @value;";

                    using (var cmd = new SQLiteCommand(upsertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@key", preference.Key);
                        cmd.Parameters.AddWithValue("@value", preference.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.Error("Error inserting/updating user preference.", ex);
            }
        }
    }
}