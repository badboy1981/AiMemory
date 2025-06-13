/// <summary>
/// Represents a tag that can be associated with notes.
/// </summary>

namespace CoreLibrary.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public required string Name { get; set; }  // Must be set (required)
    }
}