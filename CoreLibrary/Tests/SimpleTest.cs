using Xunit;
using CoreLibrary.Utilities;

namespace CoreLibrary.Tests
{
    public class SimpleTest
    {
        [Fact]
        public void SaveMessageUsingFileHelper()
        {
            var message = "🎯 Saved using FileHelper during test.";
            var path = "output_via_helper.txt";

            FileHelper.SaveToFile(path, message);
        }
    }
}