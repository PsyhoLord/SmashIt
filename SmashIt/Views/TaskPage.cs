using Xamarin.Forms;
using SmashIt;

namespace Todo
{
    public class TaskPage : ContentPage
    {
        public TaskPage()
        {
            this.SetBinding(ContentPage.TitleProperty, "Name");

            NavigationPage.SetHasNavigationBar(this, true);
            var nameLabel = new Label { Text = "Name" };
            var nameEntry = new Entry();

            nameEntry.SetBinding(Entry.TextProperty, "Name");

            var notesLabel = new Label { Text = "Notes" };
            var notesEntry = new Entry();
            notesEntry.SetBinding(Entry.TextProperty, "Notes");

            var doneLabel = new Label { Text = "Done" };
            var doneEntry = new Switch();
            doneEntry.SetBinding(Switch.IsToggledProperty, "Done");

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += (sender, e) =>
            {
                var SmashTask = (SmashTask)BindingContext;
                App.Database.SaveItem(SmashTask);
                this.Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += (sender, e) =>
            {
                var SmashTask = (SmashTask)BindingContext;
                App.Database.DeleteItem(SmashTask.ID);
                this.Navigation.PopAsync();
            };

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += (sender, e) =>
            {
                var SmashTask = (SmashTask)BindingContext;
                this.Navigation.PopAsync();
            };


            //var speakButton = new Button { Text = "Speak" };
            //speakButton.Clicked += (sender, e) =>
            //{
            //    var SmashTask = (SmashTask)BindingContext;
            //    DependencyService.Get<ITextToSpeech>().Speak(SmashTask.Name + " " + SmashTask.Notes);
            //};

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(20),
                Children = {
					nameLabel, nameEntry, 
					notesLabel, notesEntry,
					doneLabel, doneEntry,
					saveButton, deleteButton, cancelButton,
					//speakButton
				}
            };
        }
    }
}