using SQLite;

namespace CatchUs.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
