using System;
using System.ServiceModel.Channels;
using Xamarin.Forms;

namespace SmashIt
{
    public partial class TaskCell : ViewCell
    {
        //private Switch doneStatus;

        public TaskCell()
        {
            //InitializeComponent();
            var smashTask = (SmashTask)BindingContext;


            // Task Name
            var nameLabel = new Label
            {
                YAlign = TextAlignment.Center,
                //TextColor = Color.FromRgb(75, 57, 57)
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");
            //nameLabel.TextColor = GetLabelColor(smashTask.CurrentProgress);
            
            // Done tick
            var doneStatusImage = new Image{};
            doneStatusImage.SetBinding(Image.IsVisibleProperty, "Done");
            doneStatusImage.BindingContextChanged += DoneStatusOnBindingContextChanged;
            
            // Pushing everything on page
            var layout = new StackLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = {doneStatusImage, nameLabel  }
            };
            View = layout;
        }

        private Color GetLabelColor(float progressValue)
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