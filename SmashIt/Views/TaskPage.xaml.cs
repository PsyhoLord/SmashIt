using System;
using System.ComponentModel;

using Xamarin.Forms;

namespace SmashIt
{
    public partial class TaskPage 
    {
        public TaskPage()
        {
            InitializeComponent();

            this.SetBinding(TitleProperty, "Name"); // Title got from name
            NavigationPage.SetHasNavigationBar(this, true); // Navigation bar = on
            
            EntryTaskName.SetBinding(Entry.TextProperty, "Name");
            EntryTaskNotes.SetBinding(Entry.TextProperty, "Notes");
            ProgressSlider.SetBinding(Slider.ValueProperty, "CurrentProgress", BindingMode.TwoWay);
            DoneSwitch.SetBinding(Switch.IsToggledProperty, "Done");
            DateField.SetBinding(DatePicker.DateProperty, "Deadline");
           //ew DateElement("The Date", DateTime.Today).Bind(this, "Value TheDate")
        }

        void OnSlidedProgressChanged(object sender,
                          ValueChangedEventArgs args)
        {
            var sliderValue = ((Slider)sender).Value;
            ProgressLabel.Text = sliderValue.ToString("F1") + "%";
            int R, G, B;
            R = 255;
            G = 0;
            B = 0;
            // changing color from red to green (0 -> 100)
            R -= (int)(sliderValue * 2.55);
            G += (int)(sliderValue * 2.55);
            
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
            ProgressSlider.Value = 100;
            ProgressSlider.IsEnabled = !DoneSwitch.IsToggled;
        }
    }
}
