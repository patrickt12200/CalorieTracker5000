using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CalorieTracker5000.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;
using System.IO;
using CalorieTracker5000.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Android_DB_Connect))]
namespace CalorieTracker5000.Droid
{
    public class Android_DB_Connect : IDatabase
    {
        public Android_DB_Connect() { }
            public SQLiteConnection DBConnect()
            {
                var filename = "UserData.db3";
                string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                var path = Path.Combine(folder, filename);
                var connection = new SQLiteConnection(path);
                return connection;
            }
        
    }
}