using System.Diagnostics;
using Xamarin.Forms;

namespace SmashIt
{
    public partial class TaskListPage : ContentPage
    {
        ListView listView;
        
        public TaskListPage()
        {
            InitializeComponent();

            Title = "iNeed To!";
            var cell = new DataTemplate(typeof(ImageCell));

            cell.SetBinding(TextCell.TextProperty, "Name");
            cell.SetBinding(TextCell.DetailProperty, new Binding("TimeLeft"));
            cell.SetBinding(ImageCell.ImageSourceProperty, "DoneImage");

            listView  = new ListView
            {
            //    ItemsSource = tasksListView
                ItemTemplate = cell
            };

            tasksListView.ItemSelected += (sender, e) =>
            {
                var taskItem = (SmashTask)e.SelectedItem;
                var taskPage = new TaskPage { BindingContext = taskItem };

                ((App)App.Current).ResumeAtTaskId = taskItem.ID;
                Debug.WriteLine("setting ResumeAtNeedId = " + taskItem.ID);

                Navigation.PushAsync(taskPage);
            };

            // Push the list view down below the status bar on iOS.
            if (Device.OS == TargetPlatform.iOS)
            Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 5);

            // Set the content for the page.
            Content = new StackLayout
            {
                Children =
                {
                    listView
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