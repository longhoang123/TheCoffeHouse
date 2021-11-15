using Foundation;
using TheCoffeHouse.iOS;
using TheCoffeHouse.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace TheCoffeHouse.iOS
{
    public class SQLite_iOS : IDatabaseConnection
    {
        string IDatabaseConnection.GetDatabasePath()
        {
            throw new NotImplementedException();
        }

        long IDatabaseConnection.GetSize(string databaseName)
        {
            throw new NotImplementedException();
        }

        SQLiteConnection IDatabaseConnection.SqliteConnection(string databaseName)
        {
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dbPath, databaseName);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}