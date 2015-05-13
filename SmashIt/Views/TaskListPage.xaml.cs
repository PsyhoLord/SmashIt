using System.Diagnostics;
using Xamarin.Forms;

namespace SmashIt
{
    public partial class TaskListPage : ContentPage
    {
        ListView listView;

        public TaskListPage()
        {
            //InitializeComponent();

            Title = "iNeed To!";

            listView = new ListView
            {
                ItemTemplate = new DataTemplate (typeof(TaskCell))
            };

            listView.ItemSelected += (sender, e) =>
            {
                var taskItem = (SmashTask)e.SelectedItem;
                var taskPage = new TaskPage { BindingContext = taskItem };

                ((App)App.Current).ResumeAtTaskId = taskItem.ID;
                Debug.WriteLine("setting ResumeAtNeedId = " + taskItem.ID);

                Navigation.PushAsync(taskPage);
            };

            var layout = new StackLayout();

            layout.Children.Add(listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            Content = layout;

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
            listView.ItemsSource = App.Database.GetItems();
        }
    }
}