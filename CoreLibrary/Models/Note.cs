/// <summary>
/// Represents a note entity in the system.
/// </summary>

namespace CoreLibrary.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}