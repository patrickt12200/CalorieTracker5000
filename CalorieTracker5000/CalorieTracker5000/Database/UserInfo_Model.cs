using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTracker5000.Database
{
     class UserInfo_Model
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WeightGoal { get; set; }
        public int WaterGoal { get; set; }
        public int CurrentWeight { get; set; }
        public int goalProtein { get; set; }
        public int goalCarbs { get; set; }
        public int goalFats { get; set; }

        public int goalCalories { get; set; }
    }
}
