using System.IO;
using CineAntibes.Interface;
using CineAntibes.iOS.Interface;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteImpl))]
namespace CineAntibes.iOS.Interface
{
    public class SQLiteImpl : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "cineantibesDatabase.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            SQLiteConnection conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}