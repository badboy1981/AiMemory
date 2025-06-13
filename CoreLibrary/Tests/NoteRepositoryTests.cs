using Xunit;
using CoreLibrary.Data;
using CoreLibrary.Models;
using System.Data.SQLite;
using System.Collections.Generic;

namespace CoreLibrary.Tests
{
    public class NoteRepositoryTests
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly NoteRepository _noteRepository;

        public NoteRepositoryTests()
        {
            //_connectionFactory = new SQLiteConnectionFactory("Data Source=ai_memory.db;Version=3;");
            _connectionFactory = new SQLiteConnectionFactory();
            _noteRepository = new NoteRepository(_connectionFactory);
        }

        [Fact]
        public void InsertNote_ShouldStoreNoteCorrectly()
        {
            var newNote = new Note { Title = "Test Title", Content = "This is a test note." };
            _noteRepository.InsertNote(newNote);

            using var conn = _connectionFactory.CreateConnection();
            conn.Open();

            using var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Notes WHERE Title = 'Test Title' AND Content = 'This is a test note.';", conn);
            var count = Convert.ToInt32(cmd.ExecuteScalar());

            Assert.True(count > 0, "❌ Note 'Test Title' was not found in the database!");
        }

        [Fact]
        public void GetAllNotes_ShouldReturnInsertedNotes()
        {
            var notes = _noteRepository.GetAllNotes();
            Assert.NotEmpty(notes);
        }
    }
}