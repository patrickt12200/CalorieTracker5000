using CalorieTracker5000.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Xamarin.Forms;

namespace CalorieTracker5000.Views
{
    public class UserSettings : ContentPage
    {
        public UserSettings()
        {

            //==========
            //Macro Grid
            //==========
            MainPage mainPage = new MainPage();
            var db = mainPage.myDataBase;
            Grid mainGrid = new Grid
            {
                VerticalOptions = LayoutOptions.Center,
                RowDefinitions =
                {
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
            mainGrid.Children.Add(new Label
            {
                Text = "Protein",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            });
            mainGrid.Children.Add(new Label
            {
                Text = "Carbs",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            },1,0);
            mainGrid.Children.Add(new Label
            {
                Text = "Fats",
                FontSize = 16,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            }, 2, 0);

            Entry proEntry = new Entry ();
            Entry carbEntry = new Entry();
            Entry fatEntry = new Entry();
            mainGrid.Children.Add(proEntry,0,1);
            mainGrid.Children.Add(carbEntry, 1, 1);
            mainGrid.Children.Add(fatEntry, 2, 1);

            //
            //Cal grid
            //
            Entry calGoal = new Entry
            {
                Placeholder = "Calorie Goal",
                FontSize = 16,
            };
            CheckBox TrackByMacros = new CheckBox
            {

            };

            //=========
            //
            //=========
            Entry firstName = new Entry
            {
                Placeholder = "FirstName",
                PlaceholderColor = Color.White,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
            };
            
            Entry lastName = new Entry
            { 
                Placeholder = "LastName",
                PlaceholderColor = Color.White,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
            };

            Entry inputWeight = new Entry
            {
                Placeholder = "Current Weight",
                PlaceholderColor = Color.White,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
            };

            Picker goals = new Picker
            {
                Title = "Weight Goal",
                TextColor = Color.White,
                TitleColor = Color.White,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),

            };
            goals.Items.Add("Fat Loss");
            goals.Items.Add("Build Muslce");
            goals.Items.Add("Maintain Weight");


            Entry waterGoal = new Entry
            {
                Placeholder = "Water Goal(mL)",
                PlaceholderColor = Color.White,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Keyboard = Keyboard.Numeric,
            };

            Label CaloriesLbl = new Label
            {
                Text = "Calorie Goal: 0",
                FontSize = 24,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            Button SaveSetting = new Button
            {
                Text = "Save",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromRgb(254, 205, 170),
            };
          
            SaveSetting.Clicked += async (args, e) =>
            {
                int calsGoal = (Convert.ToInt32(proEntry.Text) * 4) + (Convert.ToInt32(carbEntry.Text) * 4) + (Convert.ToInt32(fatEntry.Text) * 9);
                var userInfo = new UserInfo_Model {
                    Id = 1,
                    FirstName = firstName.Text,
                    LastName = lastName.Text,
                    WeightGoal = goals.SelectedItem.ToString(),
                    WaterGoal = Convert.ToInt32(waterGoal.Text),
                    CurrentWeight = Convert.ToInt32(inputWeight.Text),                    
                    goalProtein = Convert.ToInt32(proEntry.Text),
                    goalCarbs = Convert.ToInt32(carbEntry.Text),
                    goalFats = Convert.ToInt32(fatEntry.Text),
                    goalCalories = calsGoal
                };
                //===
                //==
                firstName.Placeholder = firstName.Text;
                lastName.Placeholder = lastName.Text;
                goals.Title = goals.SelectedItem.ToString();
                waterGoal.Placeholder = waterGoal.Text;
                inputWeight.Placeholder = inputWeight.Text;
                proEntry.Placeholder = proEntry.Text;
                fatEntry.Placeholder = fatEntry.Text;
                carbEntry.Placeholder = carbEntry.Text;

                firstName.Text = "";
                lastName.Text = "";
                inputWeight.Text = "";
                proEntry.Text = "";
                fatEntry.Text = "";
                carbEntry.Text = "";
                //==
                //==
                db.Update(userInfo);
                await DisplayAlert("Success!", "Settings have been Updated!\nNew Calorie Goal: " + calGoal, "Close");
                await Navigation.PushAsync(new MainPage());
            };

            StackLayout settingsStack = new StackLayout
            {
                BackgroundColor = Color.FromRgb(55, 80, 92),
                Children = {

            new Frame
            {

                Content = new StackLayout
                {
                    Children =
                    {
                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2

                        },
                        new Label
                        {
                            Text = "User Settings/Info",
                            FontSize = 24,
                            TextColor= Color.White,
                            VerticalOptions= LayoutOptions.Center,
                            HorizontalTextAlignment = TextAlignment.Center,

                        },
                        new Image
                        {
                          Source = "notebook.png",
                          HorizontalOptions = LayoutOptions.Center,
                          VerticalOptions = LayoutOptions.Center,
                         },

                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2,
                            Margin = new Thickness(0,0,0,20),                           
                        },
                        firstName,
                        lastName,
                        goals,
                        inputWeight,
                        waterGoal,

                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2,
                            Margin = new Thickness(0,20,0,20),
                        },
                        mainGrid,

                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2,
                            Margin = new Thickness(0,20,0,20),

                        },
                        CaloriesLbl,
                        SaveSetting,
                         new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2,
                            Margin = new Thickness(0,20,0,20),

                        },

                    }
                },
                Margin = new Thickness(10),
                BackgroundColor = Color.FromRgb(155, 193, 188),
                HasShadow = true,
                HeightRequest = 800

            },

                }
            };
            this.Content =  new ScrollView { Content = settingsStack};
        }
    }
}