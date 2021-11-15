using SQLite;

namespace TheCoffeHouse.Services
{
    public interface IDatabaseConnection
    {
        SQLiteConnection SqliteConnection(string databaseName);
        long GetSize(string databaseName);
        string GetDatabasePath();
    }
}
