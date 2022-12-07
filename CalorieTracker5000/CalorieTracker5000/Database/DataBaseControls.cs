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
using System.Collections.ObjectModel;

namespace CalorieTracker5000.Database
{
    public class DataBaseControls

    { 

        public static void AddFood(SQLiteConnection db, FoodData_Model var)
        {
            
        }

        public static void RemoveFood(SQLiteConnection db, FoodData_Model var)
        {

        }

        public static void AddWater(SQLiteConnection db, Water_Model var)
        {

        }

        public static void RemoveWater(SQLiteConnection db, Water_Model var)
        {

        }

        public static void AddExercise(SQLiteConnection db, string TodaysDate, string Exercise, int Mins)
        {
            var exercise = new Exercise_Model
            {
                ExerciseName = Exercise,
                ExerciseTime = Mins,
                Date = TodaysDate
            };

            db.Insert(exercise);
        }

        public static ObservableCollection<FoodData_Model> GetTodaysFoods(SQLiteConnection db, string TodaysDate)
        {
            ObservableCollection<FoodData_Model> foodData = new ObservableCollection<FoodData_Model>();
            var foods = new List<FoodData_Model>();

            var todaysFoods = from i in db.Table<FoodData_Model>()
                              where i.Date == TodaysDate
                              select i;
                            
            foreach(FoodData_Model i in todaysFoods) { foods.Add(i);}
            return foodData;
            
        }
        public static int GetTodaysCals(SQLiteConnection db, string TodaysDate)
        {
          //  MainPage main = new MainPage();
            int totalcals = 0;
            var testcal = from i in db.Table<FoodData_Model>()
                          where i.Date == TodaysDate
                          select i.Calories;

            foreach (int i in testcal) { totalcals += i; }
            return totalcals;
        }

        public static int GetBreakFastCals(SQLiteConnection db, string TodaysDate)
        {
            int BreakCals = 0;
            var breakfast = from i in db.Table<FoodData_Model>()
                            where i.Meal == "Breakfast"
                            where i.Date == TodaysDate
                            select i.Calories;

            foreach (int i in breakfast) { BreakCals += i; }
            return BreakCals;

        }

        public static int GetLunchCals(SQLiteConnection db, string TodaysDate)
        {
            int lunchCals = 0;
            var lunch = from i in db.Table<FoodData_Model>()
                            where i.Meal == "Lunch"
                            where i.Date == TodaysDate
                            select i.Calories;

            foreach (int i in lunch) { lunchCals += i; }
            return lunchCals;

        }

        public static int GetDinnerCals(SQLiteConnection db, string TodaysDate)
        {
            int dinnerCals = 0;
            var dinner = from i in db.Table<FoodData_Model>()
                            where i.Meal == "Dinner"
                            where i.Date == TodaysDate
                            select i.Calories;

            foreach (int i in dinner) { dinnerCals += i; }
            return dinnerCals;

        }

        public static int GetSnackCals(SQLiteConnection db, string TodaysDate)
        {
            int snackCals = 0;
            var snacks = from i in db.Table<FoodData_Model>()
                            where i.Meal == "Snacks"
                            where i.Date == TodaysDate
                            select i.Calories;

            foreach (int i in snacks) { snackCals += i; }
            return snackCals;

        }


        //This is needed to prevent the app from crashing... We are getting total cals from the main page... if we try and get
        // a value that doesn't exist, the app crashes.  There is a better way to prevent this but Im a noob
        public static void GenDayStatingCals(SQLiteConnection db, string TodaysDate)
        {
            var foodInfo = new FoodData_Model
            {
                Meal = "",
                FoodName = "",
                ServingSize = "",
                Servings = 0,
                Proteins = 0,
                Carbs =0,
                Fats = 0,
                Sugars = 0,
                Fibers = 0,
                Sodium = 0,
                Calories = 0,
                Date = TodaysDate,
            };
            db.Insert(foodInfo);
        }

        public static void GenUser(SQLiteConnection db)
        {
            var userInfo = new UserInfo_Model
            {
                Id = 1,
                FirstName = "New User",
                LastName = "New User",
                WeightGoal = "Not Set",
                WaterGoal = 0,
                CurrentWeight = 0,
                goalProtein = 0,
                goalCarbs = 0,
                goalFats = 0,
                goalCalories = 0
            };
            db.Insert(userInfo);
        }

    }
}
