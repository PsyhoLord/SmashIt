using SQLite.Net;

namespace SmashIt
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}