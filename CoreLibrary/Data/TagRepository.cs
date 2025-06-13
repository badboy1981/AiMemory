using System.Data.SQLite;
using CoreLibrary.Models;
using CoreLibrary.Utilities;

namespace CoreLibrary.Data
{
    public static class TagRepository
    {
        /// <summary>
        /// Inserts a new tag into the database.
        /// </summary>
        public static void InsertTag(Tag tag)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tag.Name))
                {
                    LogService.Error("Attempted to insert a tag with an empty name.");
                    throw new ArgumentException("Tag name cannot be null or empty.");
                }

                using (var conn = DatabaseManager.GetConnection())
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Tags (Name) VALUES (@name);";

                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", tag.Name);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.Error("Error inserting tag into database.", ex);
            }
        }
    }
}