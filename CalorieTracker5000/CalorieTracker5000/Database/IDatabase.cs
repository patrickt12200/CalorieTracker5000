using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace CalorieTracker5000.Database
{
    public interface IDatabase
    {
        SQLiteConnection DBConnect();
    }
}
