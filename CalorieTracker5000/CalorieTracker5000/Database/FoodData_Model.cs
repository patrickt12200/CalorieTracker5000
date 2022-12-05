using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTracker5000.Database
{
    public class FoodData_Model
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(25)]
        public string Meal { get; set; }
        [MaxLength(25)]
        public string FoodName { get; set; }
        [MaxLength(25)]
        public string ServingSize { get; set; }
        public int Servings { get; set; }
        public int Proteins { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public int Sugars { get; set; }
        public int Fibers { get; set; }
        public int Sodium { get; set; }

        public int Calories { get; set; }

        public string Date { get; set; }
    }
}
