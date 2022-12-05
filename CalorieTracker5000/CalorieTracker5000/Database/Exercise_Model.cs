using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTracker5000.Database
{
     class Exercise_Model
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ExerciseName { get; set; }
        public int ExerciseTime { get; set; }

        public string Date { get; set; }
    }
}
