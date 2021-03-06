﻿using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace SmashIt
{
    public class App : Application
    {
        static DbController database;

        public App()
        {
            var mainNav = new NavigationPage(new TaskListPage());

            MainPage = mainNav;
        }

        public static DbController Database
        {
            get
            {
                if (database == null)
                {
                    database = new DbController();
                }
                return database;
            }
        }

        public int ResumeAtTaskId { get; set; }

        protected override void OnStart()
        {
            Debug.WriteLine("OnStart");

            // always re-set when the app starts
            // users expect this (usually)
            //			Properties ["ResumeAtTodoId"] = "";
            if (Properties.ContainsKey("ResumeAtTodoId"))
            {
                var rati = Properties["ResumeAtTodoId"].ToString();
                Debug.WriteLine("   rati=" + rati);
                if (!String.IsNullOrEmpty(rati))
                {
                    Debug.WriteLine("   rati = " + rati);
                    ResumeAtTaskId = int.Parse(rati);

                    if (ResumeAtTaskId >= 0)
                    {
                        var taskPage = new TaskPage();
                        taskPage.BindingContext = Database.GetItem(ResumeAtTaskId);

                        MainPage.Navigation.PushAsync(
                            taskPage,
                            false); // no animation
                    }
                }
            }
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep saving ResumeAtTodoId = " + ResumeAtTaskId);
            // the app should keep updating this value, to
            // keep the "state" in case of a sleep/resume
            Properties["ResumeAtTodoId"] = ResumeAtTaskId;
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
            //			if (Properties.ContainsKey ("ResumeAtTodoId")) {
            //				var rati = Properties ["ResumeAtTodoId"].ToString();
            //				Debug.WriteLine ("   rati="+rati);
            //				if (!String.IsNullOrEmpty (rati)) {
            //					Debug.WriteLine ("   rati = " + rati);
            //					ResumeAtTodoId = int.Parse (rati);
            //
            //					if (ResumeAtTodoId >= 0) {
            //						var todoPage = new TodoItemPage ();
            //						todoPage.BindingContext = Database.GetItem (ResumeAtTodoId);
            //
            //						MainPage.Navigation.PushAsync (
            //							todoPage,
            //							false); // no animation
            //					}
            //				}
            //			}
        }
    }
}