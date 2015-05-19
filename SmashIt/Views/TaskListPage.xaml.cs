using System.Diagnostics;
using Xamarin.Forms;

namespace SmashIt
{
    public partial class TaskListPage : ContentPage
    {
        private ListView tasksListView;
        //private DataTemplate cell;

        public TaskListPage()
        {
            //InitializeComponent();

            Title = "Smash it!";
            
            tasksListView = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(TaskCell))//,
                //ItemsSource = App.Database.GetItems()
            };

            tasksListView.ItemSelected += (sender, e) =>
            {
                var taskItem = (SmashTask)e.SelectedItem;
                var taskPage = new TaskPage { BindingContext = taskItem };

                ((App)App.Current).ResumeAtTaskId = taskItem.ID;

                Navigation.PushAsync(taskPage);
            };

            // Push the list view down below the status bar on iOS.
            if (Device.OS == TargetPlatform.iOS)
            Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 5);

            // Set the content for the page.
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    tasksListView
                }
            };

            #region toolbar
            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.Android) // BUG: Android doesn't support the icon being null
            {
                tbi = new ToolbarItem("+", "plus", () =>
                {
                    var taskItem = new SmashTask();
                    var taskPage = new TaskPage {BindingContext = taskItem};
                    Navigation.PushAsync(taskPage);
                }, 0, 0);
            }
            ToolbarItems.Add(tbi);
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTaskId = -1;
            tasksListView.ItemsSource = App.Database.GetItems();
        }
    }
}