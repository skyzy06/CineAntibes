using SQLite;

namespace CineAntibes.Interface
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
