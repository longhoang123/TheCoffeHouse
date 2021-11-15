using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TheCoffeHouse.Droid;
using TheCoffeHouse.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Droid))]

namespace TheCoffeHouse.Droid
{
    public class SQLite_Droid : IDatabaseConnection
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