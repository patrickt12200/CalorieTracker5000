using CalorieTracker5000.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CalorieTracker5000.Views
{
    public class TrackExercise : ContentPage
    {
        public TrackExercise(SQLiteConnection db, string TodaysDate)
        {
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

            Entry ExerciseName = new Entry { Placeholder = "Exercise Name", PlaceholderColor = Color.White };
            Entry WorkoutDuration = new Entry { Placeholder = "Workout Duration in minutes", PlaceholderColor = Color.White, Keyboard = Keyboard.Numeric};

            Button addExercise = new Button
            {
                Text = "Add Exercise",
                TextColor = Color.White,
                BackgroundColor = Color.FromRgb(254, 205, 170),
                Margin = new Thickness(0, 10, 0, 10),
            };
             addExercise.Clicked += (args, e) =>
            {
                int mins = Convert.ToInt32(WorkoutDuration.Text);
                string exercise = ExerciseName.Text;
                DataBaseControls.AddExercise(db, TodaysDate, exercise , mins);
                DisplayAlert("Success!", ExerciseName.Text + " has been added to the database!", "OK");
            };

            Content = new StackLayout
            {
                BackgroundColor = Color.FromRgb(55, 80, 92),
                Children =
                {
                    new Frame
                    {
                        Margin = new Thickness(10),
                        HeightRequest = 85,
                        BackgroundColor = Color.FromRgb(155, 193, 188),
                        HasShadow = true,
                        Content = new StackLayout
                        {
                            Children =
                            {
                                new Label
                                {
                                    Text = "Add Exercise",
                                    TextColor = Color.White,
                                    FontSize = 24,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,
                                },
                                new Image
                                {
                                    Source = "workout.png",
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
                                ExerciseName,
                                WorkoutDuration,
                                addExercise,
                                 new Frame
                                {
                                    Margin = new Thickness(10),
                                    HeightRequest = 95,
                                    BackgroundColor = Color.FromRgb(155, 193, 188),
                                    HasShadow = true,
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
                                                Text = "Exercise is beneficial for weightloss\n" +
                                                "For best results, a workout regimen is recommended.",
                                                TextColor = Color.White,
                                                FontSize = 18,
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

                            }
                        }
                    },
                 }
            };
        }
    }
}
