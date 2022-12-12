using CalorieTracker5000.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CalorieTracker5000.Views
{
    public class AddFoodPage : ContentPage
    {
      
        public AddFoodPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
        public AddFoodPage(string inputMeal, string date)
        {
            MainPage mainPage = new MainPage();
            var db = mainPage.myDataBase;
            Grid PrimaryGrid = new Grid
            {
                VerticalOptions = LayoutOptions.Center,
                RowDefinitions =
                {
                new RowDefinition {Height = GridLength.Auto},
                new RowDefinition {Height = GridLength.Auto},
                new RowDefinition {Height = GridLength.Auto},
                new RowDefinition {Height = GridLength.Auto},
                },
                ColumnDefinitions =
                {
                new ColumnDefinition {Width = 110},
                new ColumnDefinition {Width = 110},
                new ColumnDefinition {Width = 110},
                },
            };
            PrimaryGrid.Children.Add(new Label
            {
                Text = "Protein",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            });
            PrimaryGrid.Children.Add(new Label
            {
                Text = "Carbs",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 0);
            PrimaryGrid.Children.Add(new Label
            {
                Text = "Fats",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            }, 2, 0);

            PrimaryGrid.Children.Add(new Label
            {
                Text = "Sugars",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            },0,2);
            PrimaryGrid.Children.Add(new Label
            {
                Text = "Fiber",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 2);
            PrimaryGrid.Children.Add(new Label
            {
                Text = "Sodium",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            }, 2, 2);

            Entry proEntry = new Entry();
            Entry carbEntry = new Entry();
            Entry fatEntry = new Entry();
            Entry sugarEntry = new Entry();
            Entry fiberEntry = new Entry();
            Entry sodiumEntry = new Entry();
            PrimaryGrid.Children.Add(proEntry, 0, 1);
            PrimaryGrid.Children.Add(carbEntry, 1, 1);
            PrimaryGrid.Children.Add(fatEntry, 2, 1);
            PrimaryGrid.Children.Add(sugarEntry, 0, 3);
            PrimaryGrid.Children.Add(fiberEntry, 1, 3);
            PrimaryGrid.Children.Add(sodiumEntry, 2, 3);

            Entry foodName = new Entry { Placeholder = "Food Name", PlaceholderColor = Color.White};
            Entry grams = new Entry { Placeholder = "Serving Size", PlaceholderColor = Color.White };
            Entry servings = new Entry { Placeholder = "Servings", PlaceholderColor = Color.White };

            Button addFood = new Button
            {
                Text = "Add Food",
                TextColor = Color.White,
                BackgroundColor = Color.FromRgb(254, 205, 170),
                Margin = new Thickness(0,10,0,10),
            };

            addFood.Clicked += async (args, e) =>
            {
                var foodInfo = new FoodData_Model
                {
                    Meal = inputMeal,
                    FoodName = foodName.Text,
                    ServingSize = grams.Text,
                    Servings = Convert.ToInt32(servings.Text),
                    Proteins = Convert.ToInt32(proEntry.Text),
                    Carbs = Convert.ToInt32(carbEntry.Text),
                    Fats = Convert.ToInt32(fatEntry.Text),
                    Sugars = Convert.ToInt32(sugarEntry.Text),
                    Fibers = Convert.ToInt32(fiberEntry.Text),
                    Sodium = Convert.ToInt32(sodiumEntry.Text),
                    Calories = (Convert.ToInt32(proEntry.Text) * 4) + (Convert.ToInt32(carbEntry.Text) * 4) + (Convert.ToInt32(fatEntry.Text) * 9),
                    Date = date,
                };            
                
                db.Insert(foodInfo);
                await DisplayAlert("Success!", inputMeal + " has been added to the database!", "OK");
                await Navigation.PushAsync(new MainPage());
                await Navigation.PopToRootAsync();
            };

            Content = new StackLayout
            {
                BackgroundColor = Color.FromRgb(55, 80, 92),
                Children =
                {
                    new Frame
                    {
                        Margin = new Thickness(10),
                        HeightRequest = 55,
                        BackgroundColor = Color.FromRgb(155, 193, 188),
                        HasShadow = true,
                        Content = new StackLayout
                        {
                            Children =
                            {
                                new Label
                                {
                                    Text = "Add to " + inputMeal,
                                    TextColor = Color.White,
                                    FontSize = 18,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,
                                },
                                new Image
                                {
                                    Source = "meal.png",
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,
                                },
                                new BoxView
                                {
                                    Color = Color.FromRgb(148, 131, 146),
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    HeightRequest = 2

                                },
                                
                            }
                        }
                    },
                     new Frame
                    {
                        Margin = new Thickness(10),
                        HeightRequest = 425,
                        BackgroundColor = Color.FromRgb(155, 193, 188),
                        HasShadow = true,
                        Content = new StackLayout
                        {
                            Children =
                            {
                                foodName,
                                grams,
                                servings,
                                new BoxView
                                {
                                    Color = Color.FromRgb(148, 131, 146),
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    HeightRequest = 2,
                                    Margin = new Thickness(0,10,0,10),
                                },
                                PrimaryGrid,
                                addFood
                            }
                        }
                    },
                 }
            };
        }
    }
}