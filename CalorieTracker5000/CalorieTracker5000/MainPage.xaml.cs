using CalorieTracker5000.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using SQLite;
using CalorieTracker5000.Database;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace CalorieTracker5000
{
    public partial class MainPage : ContentPage
    {
        public SQLiteConnection myDataBase;
        public MainPage()
        {
            InitializeComponent();
            myDataBase = DependencyService.Get<IDatabase>().DBConnect();
            myDataBase.CreateTable<FoodData_Model>();
            myDataBase.CreateTable<UserInfo_Model>();
            myDataBase.CreateTable<Exercise_Model>();
            //myDataBase.CreateTable<FoodData_Model>();

            //=============
            //Define Tap Gesture Recognizer
            //and handles
            //=============
            // We want our Frames Clickable

            //=========
            //Meal Menu Labels


            ToolbarItem Settings = new ToolbarItem
            {          
                IconImageSource = ImageSource.FromFile("settings.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };

            Settings.Clicked += async (args, e) =>
            {
                await Navigation.PushAsync(new UserSettings());
            };

            ToolbarItem Calendar = new ToolbarItem
            {
                IconImageSource = ImageSource.FromFile("calendar.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };

             DatePicker datePicker = new DatePicker
            {
                BackgroundColor = Color.FromRgb(254, 205, 170),
                MinimumDate = new DateTime(2022, 11, 29),
                MaximumDate = new DateTime(2023, 11, 29),
                Date = DateTime.Now,       
                
                IsVisible = false
            };
            Calendar.Clicked += (args, e) =>
            {
                datePicker.Focus();
            };
            var userName = myDataBase.Get<UserInfo_Model>(1);
            var TodaysDate = datePicker.Date.Month.ToString() + "/" + datePicker.Date.Day.ToString() + "/" + datePicker.Date.Year.ToString();
            int todaysCals = DataBaseControls.GetTodaysCals(myDataBase, TodaysDate);
            
            //These labels need to be constatntly updated
            Label calsProgLbl = new Label
            {
                Text = "Calories Consumed: " + todaysCals + "/" + userName.goalCalories.ToString(),
                //Text = test.Calories.ToString(),
                TextColor = Color.White,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,

            };
          
            Label DateLabel = new Label
            {
                Text =  datePicker.Date.Month.ToString() + "/" + datePicker.Date.Day.ToString() + "/" + datePicker.Date.Year.ToString(),
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            Label BreakfastLbl = new Label
            {
                Text = "Calories: " + DataBaseControls.GetBreakFastCals(myDataBase, TodaysDate),
                TextColor = Color.White,
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Start,
            };

            Label LunchLbl = new Label
            {
                Text = "Calories: " + DataBaseControls.GetLunchCals(myDataBase, TodaysDate),
                TextColor = Color.White,
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Start,
            };

            Label DinnerLbl = new Label
            {
                Text = "Calories: " + DataBaseControls.GetDinnerCals(myDataBase, TodaysDate),
                TextColor = Color.White,
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Start,
            };

            Label SnackLbl = new Label
            {
                Text = "Calories: " + DataBaseControls.GetSnackCals(myDataBase, TodaysDate),
                TextColor = Color.White,
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Start,
            };


            ProgressBar calsProgress = new ProgressBar
            {
                ProgressColor = Color.FromRgb(254, 205, 170)
            };
            double p = Convert.ToDouble(todaysCals) / Convert.ToDouble(userName.goalCalories);
            calsProgress.ProgressTo(p, 500, Easing.Linear);

            //These labels need to be constantly updated
            
            datePicker.DateSelected += async (args, e) =>
            {
                
                TodaysDate = datePicker.Date.Month.ToString() + "/" + datePicker.Date.Day.ToString() + "/" + datePicker.Date.Year.ToString();
                todaysCals = DataBaseControls.GetTodaysCals(myDataBase, TodaysDate);
                DataBaseControls.GenDayStatingCals(myDataBase, TodaysDate);
                p = todaysCals / userName.goalCalories;
                Math.Round(p, 2);
                await calsProgress.ProgressTo(p, 500, Easing.Linear);
                ReFreshData();
            };

            ToolbarItem Cookbook = new ToolbarItem
            {
                IconImageSource = ImageSource.FromFile("cookbook.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };

            Cookbook.Clicked += async (args, e) =>
            {
                await Navigation.PushAsync(new Cookbook(myDataBase, TodaysDate));
            };

            this.ToolbarItems.Add(Settings);
            this.ToolbarItems.Add(Calendar);
            this.ToolbarItems.Add(Cookbook);


            //=============
            //Define Frames
            //=============

            //=============
            //Welcome Frame
            //=============

            //============
            // Getting Data
            //============

            //==Gets user data and stores it in this var

            try
            {
                myDataBase.Get<UserInfo_Model>(1);
            }
            catch (Exception e)
            {
                DataBaseControls.GenUser(myDataBase);
                DisplayAlert("ALERT", "NEW USER DETECTED\n Please edit the user settings!\n" + e.Message.ToString(), "OK");
            }


            Label welcomeLabel = new Label
            {
                Text = userName.FirstName + " " + userName.LastName,
                TextColor = Color.White,
                FontSize = 36,
                FontAttributes = FontAttributes.Bold,
                //FontFamily
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,

            };
            var test = myDataBase.Get<FoodData_Model>(1);

            Frame WelcomeFrame = new Frame
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        welcomeLabel,
                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2

                        },
                        DateLabel,
                        calsProgLbl,
                        calsProgress,
                        
                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2

                        },
                        new Label
                        {
                            Text =    
                            "\nWeightGoal: " + userName.WeightGoal + 
                            "\nCurrentWeight: " + userName.CurrentWeight +
                            "\nProtein Goal: " + userName.goalProtein +
                            "\nCarb Goal: " + userName.goalCarbs +
                            "\nFats Goal : " + userName.goalFats,


                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            TextColor = Color.White,
                            FontSize = 16,
                        }
                    }
                }, 
                Margin = new Thickness(10),
                BackgroundColor = Color.FromRgb(155, 193, 188),
                HasShadow = true,
                HeightRequest = 300,
                BorderColor = Color.FromRgb(92, 164, 169),            
                
            };

                //=============
                //Welcome Frame Event Handler
                //=============
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (sender, e) =>
                {
                    DisplayAlert("Test", "Test", "Test");
                };
                WelcomeFrame.GestureRecognizers.Add(tapGestureRecognizer);


            //=============
            //Breakfast Frame
            //=============



            Frame Breakfast = new Frame
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
                            Text = "Breakfast",
                            TextColor = Color.White,
                            FontSize = 18,
                            HorizontalOptions = LayoutOptions.Start,
                        },
                        //new Image
                        //{
                        //    Source="plus.png",
                        //    HorizontalOptions = LayoutOptions.End,
                        //    VerticalOptions = LayoutOptions.Start,
                        //},
                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2

                        },
                        BreakfastLbl
                    }
                }
            };
                //=============
                //Breakfast Frame Event Handler
                //=============

                var BreakFastTapRecognizer = new TapGestureRecognizer();
                BreakFastTapRecognizer.Tapped += async (sender, e) =>
                {
                   // await calsProgress.ProgressTo(0.50, 1000, Easing.Linear);
                    await Navigation.PushAsync(new AddFoodPage("Breakfast", TodaysDate));
                };
                Breakfast.GestureRecognizers.Add(BreakFastTapRecognizer);

            //=============
            //Lunch Frame
            //=============
            Frame Lunch = new Frame
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
                            Text = "Lunch",
                            TextColor = Color.White,
                            FontSize = 18,
                            HorizontalOptions = LayoutOptions.Start,
                        },
                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2

                        },
                        LunchLbl
                    }
                }
            };
            var LunchTapRecognizer = new TapGestureRecognizer();
            LunchTapRecognizer.Tapped += async (sender, e) =>
            {
                await Navigation.PushAsync(new AddFoodPage("Lunch", TodaysDate));
            };

            Lunch.GestureRecognizers.Add(LunchTapRecognizer);

            //=============
            //Dinner Frame
            //=============

            Frame Dinner = new Frame
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
                            Text = "Dinner",
                            TextColor = Color.White,
                            FontSize = 18,
                            HorizontalOptions = LayoutOptions.Start,
                        },
                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2

                        },
                        DinnerLbl,
                    }
                }
            };
            var DinnerTapRecognizer = new TapGestureRecognizer();
            DinnerTapRecognizer.Tapped += async (sender, e) =>
            {
                await Navigation.PushAsync(new AddFoodPage("Dinner", TodaysDate));
            };

            Dinner.GestureRecognizers.Add(DinnerTapRecognizer);

            //=============
            //Snacks Frame
            //=============

            Frame Snacks = new Frame
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
                            Text = "Snacks",
                            TextColor = Color.White,
                            FontSize = 18,
                            HorizontalOptions = LayoutOptions.Start,
                        },
                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2

                        },
                        SnackLbl,
                    }
                }
            };
            var SnacksTapRecognizer = new TapGestureRecognizer();
            SnacksTapRecognizer.Tapped += async (sender, e) =>
            {
                await Navigation.PushAsync(new AddFoodPage("Snacks", TodaysDate));
            };

            Snacks.GestureRecognizers.Add(SnacksTapRecognizer);

            //=============
            //Summary Frame
            //=============

            Frame Exercise = new Frame
            {
                Margin = new Thickness(10),
                HeightRequest = 45,
                BackgroundColor = Color.FromRgb(155, 193, 188),
                HasShadow = true,
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = "Exercise/Steps",
                            TextColor = Color.White,
                            FontSize = 18,
                            HorizontalOptions = LayoutOptions.Start,
                        },
                        new BoxView
                        {
                            Color = Color.FromRgb(148, 131, 146),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 2

                        },
                    }
                }


            };
            var ExerciseTapRecognizer = new TapGestureRecognizer();
            ExerciseTapRecognizer.Tapped += async (sender, e) =>
            {
                await Navigation.PushAsync(new TrackExercise(myDataBase, TodaysDate));
            };

            Exercise.GestureRecognizers.Add(ExerciseTapRecognizer);


            //=============
            //Main Stack 
            //=============

            StackLayout mainStack = new StackLayout
            {
                
                BackgroundColor = Color.FromRgb(55, 80, 92),
                Children =
                {
                    WelcomeFrame,
                    Breakfast,
                    Lunch,
                    Dinner,
                    Snacks,
                    Exercise,
                    datePicker,
                }
            };


             this.Content = new ScrollView { Content = mainStack };

            void ReFreshData()
            {
                // Refresh Cals Progress Label
                calsProgLbl.Text = todaysCals.ToString() + "/" + userName.goalCalories.ToString();
                // Refresh the data label 
                DateLabel.Text = datePicker.Date.Month.ToString() + "/" + datePicker.Date.Day.ToString() + "/" + datePicker.Date.Year.ToString();
                BreakfastLbl.Text = "Breakfast Cals: " + DataBaseControls.GetBreakFastCals(myDataBase, TodaysDate);
                LunchLbl.Text = "Lunch Cals: " + DataBaseControls.GetLunchCals(myDataBase, TodaysDate);
                DinnerLbl.Text = "Dinner Cals: " + DataBaseControls.GetDinnerCals(myDataBase, TodaysDate);
                SnackLbl.Text = "Snack Cals: " + DataBaseControls.GetSnackCals(myDataBase, TodaysDate);
                //
                double pp = Convert.ToDouble(todaysCals) / Convert.ToDouble(userName.goalCalories);
                calsProgress.ProgressTo(pp, 500, Easing.Linear);

            }
        }

    }
}
