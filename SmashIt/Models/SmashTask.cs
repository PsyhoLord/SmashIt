using System;
using SQLite.Net.Attributes;
using Xamarin.Forms;

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

        public String TimeLeft()
        {
            var CurrentDate = DateTime.Now;
            var diff = Deadline.Subtract(CurrentDate);
            return String.Format("{0}", diff.TotalDays);
        }

        public String DoneImage()
        {
            return Done ? FileImageSource.FromFile("Done.png").ToString() : FileImageSource.FromFile("unDone.png").ToString();
        }
    }
}

