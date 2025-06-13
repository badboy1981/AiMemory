using System.Data.SQLite;
using CoreLibrary.Models;
using CoreLibrary.Utilities;

namespace CoreLibrary.Data
{
    public class NoteRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public NoteRepository(IDatabaseConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Inserts a new note into the database.
        /// </summary>
        public void InsertNote(Note note)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(note.Content))
                {
                    LogService.Error("Attempted to insert a note with empty content.");
                    throw new ArgumentException("Content cannot be null or empty.");
                }

                using (var conn = _connectionFactory.CreateConnection())
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Notes (Title, Content) VALUES (@title, @content);";

                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", note.Title);
                        cmd.Parameters.AddWithValue("@content", note.Content);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.Error("Error inserting note into database.", ex);
            }
        }

        /// <summary>
        /// Retrieves all notes from the database.
        /// </summary>
        public List<Note> GetAllNotes()
        {
            var notes = new List<Note>();

            using (var conn = _connectionFactory.CreateConnection())
            {
                conn.Open();
                string selectQuery = "SELECT Id, Title, Content, CreatedAt FROM Notes;";

                using (var cmd = new SQLiteCommand(selectQuery, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        notes.Add(new Note
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Content = reader.GetString(2),
                            CreatedAt = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return notes;
        }
    }
}