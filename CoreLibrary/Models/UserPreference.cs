/// <summary>
/// Represents user preferences for the application.
/// </summary>

namespace CoreLibrary.Models
{
    public class UserPreference
    {
        public int Id { get; set; }
        public required string Key { get; set; }  // Must be set (required)
        public string? Value { get; set; }  // Optional (nullable)
    }
}