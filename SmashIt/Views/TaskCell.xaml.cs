using System;
using Xamarin.Forms;

namespace SmashIt
{
    public partial class TaskCell : ViewCell
    {
        //private Switch doneStatus;
        private Label nameLabel;
        private Label timeLeftLabel;

        private ProgressBar progress;
        private DatePicker deadline;

        public TaskCell()
        {
            // Done tick (image) that show Done/Undone status
            var doneStatusImage = new Image();
            doneStatusImage.SetBinding(Image.IsVisibleProperty, "Done");
            doneStatusImage.BindingContextChanged += DoneStatusOnBindingContextChanged;

            // Task Name
            nameLabel = new Label
            {
                YAlign = TextAlignment.Center,
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            // time left label
            timeLeftLabel = new Label
            {
                YAlign = TextAlignment.Center,
            };

            /////>>> Invisible fields <<</////
            // Invisible to get current progress to set color for text
            progress = new ProgressBar
            {
                IsVisible = false
            };
            progress.SetBinding(ProgressBar.ProgressProperty, "CurrentProgress");
            progress.PropertyChanging += ProgressOnBindingContextChanged;

            // Invisible to get deadline date to show days left to deadline
            deadline = new DatePicker()
            {
                IsVisible = false
            };
            deadline.SetBinding(DatePicker.DateProperty, "Deadline");
            deadline.BindingContextChanged += DeadlineOnBindingContextChanged;

            // Pushing everything on page
            var layout = new StackLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    // visible
                    doneStatusImage,
                    nameLabel,
                    timeLeftLabel,
                    // invisible
                    progress,
                    deadline
                }
            };
            View = layout;
        }

        private void DeadlineOnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            DateTime currentDateTime = DateTime.Today;
            var timeLeft = ((DatePicker)sender).Date - currentDateTime;
            timeLeftLabel.Text = String.Format("{0} days", timeLeft.TotalDays);
        }

        private void ProgressOnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            var progress = ((ProgressBar)sender).Progress;
            nameLabel.TextColor = GetLabelColor(progress);
        }

        private Color GetLabelColor(double progressValue)
        {
            //var smashTask = (SmashTask) BindingContext;

            //float progressValue = smashTask.CurrentProgress;
            
            int R, G, B;
            R = 255;
            G = 0;
            B = 0;

            R -= (int)(progressValue * 2.55);
            G += (int)(progressValue * 2.55);

            return Color.FromRgb(R, G, B);
        }

        private void DoneStatusOnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            var doneStatusImage = (Image)sender;
            doneStatusImage.Source =
                FileImageSource.FromFile(doneStatusImage.IsVisible ?
                "Done.png" : "unDone.png");
            doneStatusImage.IsVisible = true;
        }
        
        protected override void OnBindingContextChanged()
        {
            // Fixme : this is happening because the View.Parent is getting 
            // set after the Cell gets the binding context set on it. Then it is inheriting
            // the parents binding context.
            View.BindingContext = BindingContext;
            base.OnBindingContextChanged();
        }
    }
}