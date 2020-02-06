using Xamarin.Forms;

namespace CatchUs.Custom
{
    public class AppTimePicker : TimePicker
    {
        public static readonly BindableProperty TextAlignmentProperty =
               BindableProperty.Create("TextAlignmentCenter", typeof(bool), typeof(AppTimePicker), false);

        public bool TextAlignmentCenter
        {
            get { return (bool)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }

        public static readonly BindableProperty BorderRadiusProperty =
        BindableProperty.Create("BorderRadius", typeof(int), typeof(AppTimePicker), 25);

        public int BorderRadius
        {
            get { return (int)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create("BorderColor", typeof(Color), typeof(AppTimePicker), Color.Gray);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create("BorderWidth", typeof(int), typeof(AppTimePicker), 0);

        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create("StartColor", typeof(Color), typeof(AppTimePicker), Color.Gray);

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create("EndColor", typeof(Color), typeof(AppTimePicker), Color.Black);

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }
    }
}