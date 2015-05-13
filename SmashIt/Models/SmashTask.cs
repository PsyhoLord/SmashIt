using System;
using SQLite.Net.Attributes;

namespace SmashIt
{
    public class SmashTask
    {
        public SmashTask()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
        public float CurrentProgress { get; set; }
        public DateTime Deadline { get; set; }
        //public int Year;
        //public int Month;
        //public int Day;
    }
}

