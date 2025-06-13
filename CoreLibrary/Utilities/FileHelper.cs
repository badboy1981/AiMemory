using System.IO;
using System.Text;

namespace CoreLibrary.Utilities
{
    public static class FileHelper
    {
        /// <summary>
        /// Saves the given content to the specified file path.
        /// </summary>
        /// <param name="filePath">Path of the file to save.</param>
        /// <param name="content">Content to write to the file.</param>
        public static void SaveToFile(string filePath, string content)
        {
            try
            {
                using (StreamWriter writer = new(filePath, false, Encoding.UTF8))
                {
                    writer.Write(content);
                }

                Console.WriteLine($"✅ File saved successfully: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error saving file: {ex.Message}");
            }
        }

        /// <summary>
        /// Reads content from the specified file path.
        /// </summary>
        /// <param name="filePath">Path of the file to read.</param>
        /// <returns>File content as a string.</returns>
        public static string ReadFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"❌ File not found: {filePath}");

                using (StreamReader reader = new(filePath, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LogService.Error($"Error reading file: {filePath}", ex); // General Error Logging
                return string.Empty;
            }
        }
    }
}