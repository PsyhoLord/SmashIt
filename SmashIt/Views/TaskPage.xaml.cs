using System;
using System.ComponentModel;

using Xamarin.Forms;

namespace SmashIt
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();

            this.SetBinding(ContentPage.TitleProperty, "Name"); // Title got from name
            NavigationPage.SetHasNavigationBar(this, true); // Navigation bar = on
            
            EntryTaskName.SetBinding(Entry.TextProperty, "Name");
            EntryTaskNotes.SetBinding(Entry.TextProperty, "Notes");
            ProgressSlider.SetBinding(Slider.ValueProperty, "CurrentProgress", BindingMode.TwoWay);
            DoneSwitch.SetBinding(Switch.IsToggledProperty, "Done");
        }

        void OnSlidedProgressChanged(object sender,
                          ValueChangedEventArgs args)
        {
            ProgressLabel.Text = ((Slider)sender).Value.ToString("F1")+"%";
            double R = 255;
            R -= (((Slider) sender).Value/100*255);
            double G = 0;
            G += ((Slider) sender).Value/100*255;
            double B = 75;
            ProgressLabel.TextColor = Color.FromRgb(R,G,B);
        }

        async void SaveButtonClicked(object sender, EventArgs args)
        {
            //Button button = (Button)sender;
            var smashTask = (SmashTask)BindingContext;
            //smashTask.Name = EntryTaskName.ToString();
            //smashTask.Notes = EntryTaskNotes.ToString();
            //smashTask.CurrentProgress = Convert.ToInt16(ProgressLabel.Text);

            App.Database.SaveItem(smashTask);
            this.Navigation.PopAsync();
        }

        async private void DeleteButtonClicked(object sender, EventArgs e)
        {
            var smashTask = (SmashTask)BindingContext;
            App.Database.DeleteItem(smashTask.ID);
            this.Navigation.PopAsync();
        }

        async private void CancelButtonClicked(object sender, EventArgs e)
        {
            var smashTask = (SmashTask)BindingContext;
            this.Navigation.PopAsync();
        }

        private void IsDoneStateChanged(object sender, PropertyChangedEventArgs e)
        {
            ProgressSlider.IsVisible = !DoneSwitch.IsToggled;
        }
    }
}
