using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTracker5000.Database
{
    class Meal_Model
    {
        [PrimaryKey, AutoIncrement]
        public int ID   { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Proteins { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public int Sugars { get; set; }
        public int Fibers { get; set; }
        public int Sodium { get; set; }

        public string Date { get; set; }

    }
}
