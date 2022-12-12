using CalorieTracker5000.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace CalorieTracker5000.Views
{
    public class Cookbook : ContentPage
    {
        public Cookbook(SQLiteConnection db, string TodaysDate)
        {
            Grid grid = new Grid()
            {
                BackgroundColor = Color.FromRgb(155, 193, 188)
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition()
            {
                Height = 20,
                
                
            }); 

            Label food = new Label { Text = "Food", FontSize = 16, TextColor = Color.White };
            Label cals = new Label { Text = "calories", FontSize = 16, TextColor = Color.White };
            Label Del = new Label { Text = "Meal", FontSize = 16, TextColor = Color.White };
            grid.Children.Add(food, 0, 0);
            grid.Children.Add(cals, 1, 0);
            grid.Children.Add(Del, 2, 0);
            ObservableCollection<string> foodData = new ObservableCollection<string>();
           // var foods = new List<FoodData_Model>();

            var todaysFoods = from i in db.Table<FoodData_Model>()
                              where i.Date == TodaysDate
                              select i;
            int row = 1;
            //int counter = 0;
            var btn = new Button
            {
                Text = "Delete Food",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(254, 205, 170),
                TextColor = Color.White
            };

            btn.Clicked += async (e, args) =>
            {
                string result = await DisplayPromptAsync("CONFIRM", "Confirm the name of the food you want to delete");
                DataBaseControls.RemoveFood(db, result, TodaysDate);
                await DisplayAlert("Success", result + " has been deleted!", "OK");
                await Navigation.PushAsync(new MainPage());
   
            };

            foreach (FoodData_Model i in todaysFoods)
            {
                if(i.Calories != 0)
                {
                    foodData.Add(i.FoodName + " \n" + i.Calories);
                    grid.RowDefinitions.Add(new RowDefinition { Height = 50,});
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
                    var lbl3 = new Label
                    {
                        Text = i.Meal.ToString(),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                    };

                    grid.Children.Add(lbl, 0, row);
                    grid.Children.Add(lbl2, 1, row);
                    grid.Children.Add(lbl3, 2, row);
                    row++;
                }

            }

            StackLayout stack = new StackLayout
            {
                Children =
                {
                grid,
                btn
                },
               BackgroundColor = Color.FromRgb(155, 193, 188)
                
            };
            this.Content = stack;
            
        }
    }
}
