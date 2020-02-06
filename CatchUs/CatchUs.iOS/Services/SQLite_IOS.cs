using System.IO;
using CatchUs.iOS.Services;
using CatchUs.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_IOS))]
namespace CatchUs.iOS.Services
{
    public class SQLite_IOS : ISQLite
    {
        public SQLite_IOS() { }
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