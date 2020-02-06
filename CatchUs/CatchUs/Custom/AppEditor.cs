using Xamarin.Forms;

namespace CatchUs.Custom
{
    public class AppEditor : Editor
    {

        public static readonly BindableProperty TextAlignmentProperty =
               BindableProperty.Create("TextAlignmentCenter", typeof(bool), typeof(AppEditor), false);

        public bool TextAlignmentCenter
        {
            get { return (bool)this.GetValue(TextAlignmentProperty); }
            set { this.SetValue(TextAlignmentProperty, value); }
        }

        public static readonly BindableProperty BorderRadiusProperty =
        BindableProperty.Create("BorderRadius", typeof(int), typeof(AppEditor), 25);

        public int BorderRadius
        {
            get { return (int)this.GetValue(BorderRadiusProperty); }
            set { this.SetValue(BorderRadiusProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create("BorderColor", typeof(Color), typeof(AppEditor), Color.Gray);

        public Color BorderColor
        {
            get { return (Color)this.GetValue(BorderColorProperty); }
            set { this.SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create("BorderWidth", typeof(int), typeof(AppEditor), 0);

        public int BorderWidth
        {
            get { return (int)this.GetValue(BorderWidthProperty); }
            set { this.SetValue(BorderWidthProperty, value); }
        }

        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create("StartColor", typeof(Color), typeof(AppEditor), Color.Gray);

        public Color StartColor
        {
            get { return (Color)this.GetValue(StartColorProperty); }
            set { this.SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create("EndColor", typeof(Color), typeof(AppEditor), Color.Black);

        public Color EndColor
        {
            get { return (Color)this.GetValue(EndColorProperty); }
            set { this.SetValue(EndColorProperty, value); }
        }
    }
}