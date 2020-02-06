using System.IO;
using CatchUs.Droid.Services;
using CatchUs.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace CatchUs.Droid.Services
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFileName = "TestDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);
            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}