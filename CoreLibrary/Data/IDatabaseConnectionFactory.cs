using System.Data.SQLite;

namespace CoreLibrary.Data
{
    public interface IDatabaseConnectionFactory
    {
        SQLiteConnection CreateConnection();
    }
}
