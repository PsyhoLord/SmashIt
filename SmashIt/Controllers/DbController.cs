using SQLite.Net;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SmashIt
{
    public class DbController
    {
        static object locker = new object();

        SQLiteConnection database;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public DbController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            database.CreateTable<SmashTask>();
        }

        public IEnumerable<SmashTask> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<SmashTask>() select i).ToList();
            }
        }

        public IEnumerable<SmashTask> GetItemsNotDone()
        {
            lock (locker)
            {
                return database.Query<SmashTask>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
            }
        }

        public SmashTask GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<SmashTask>().FirstOrDefault(x => x.ID == id);
            }
        }

        public int SaveItem(SmashTask item)
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    database.Update(item);
                    return item.ID;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<SmashTask>(id);
            }
        }
    }
}

