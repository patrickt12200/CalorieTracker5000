using CalorieTracker5000.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


using Xamarin.Forms;

namespace CalorieTracker5000.Views
{
    public class Cookbook : ContentPage
    {
        public Cookbook(SQLiteConnection db, string TodaysDate)
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());


            ObservableCollection<string> foodData = new ObservableCollection<string>();
           // var foods = new List<FoodData_Model>();

            var todaysFoods = from i in db.Table<FoodData_Model>()
                              where i.Date == TodaysDate
                              select i;
            int row = 0;
            foreach (FoodData_Model i in todaysFoods)
            {
                if(i.Calories != 0)
                {
                    foodData.Add(i.FoodName + " \n" + i.Calories);
                    grid.RowDefinitions.Add(new RowDefinition { Height = 50 });
                    var lbl = new Label
                    {
                        Text = i.FoodName,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    var lbl2 = new Label
                    {
                        Text = i.Calories.ToString(),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                    };
                    grid.Children.Add(lbl, 0, row);
                    grid.Children.Add(lbl2, 1, row);
                    row++;
                }

            }
            this.Content = grid;
            
        }
    }
}
