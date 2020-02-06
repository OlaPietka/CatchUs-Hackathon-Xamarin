using Xamarin.Forms;

namespace CatchUs.Custom
{
    public class AppButton : Button
    {
        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create("StartColor", typeof(Color), typeof(AppButton), Color.Transparent);

        public Color StartColor
        {
            get { return (Color)this.GetValue(StartColorProperty); }
            set { this.SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty MiddleColorProperty =
        BindableProperty.Create("MiddleColor", typeof(Color), typeof(AppButton), Color.Transparent);

        public Color MiddleColor
        {
            get { return (Color)this.GetValue(MiddleColorProperty); }
            set { this.SetValue(MiddleColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create("EndColor", typeof(Color), typeof(AppButton), Color.Transparent);

        public Color EndColor
        {
            get { return (Color)this.GetValue(EndColorProperty); }
            set { this.SetValue(EndColorProperty, value); }
        }
        

        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation", typeof(string), typeof(AppButton), "TopBottom");

        public string Orientation
        {
            get { return (string)this.GetValue(OrientationProperty); }
            set { this.SetValue(OrientationProperty, value); }
        }

        public static readonly BindableProperty MiddleColorEnabledProperty =
            BindableProperty.Create("MiddleColorEnabled", typeof(bool), typeof(AppButton), false);

        public bool MiddleColorEnabled
        {
            get { return (bool)this.GetValue(MiddleColorEnabledProperty); }
            set { this.SetValue(MiddleColorEnabledProperty, value); }
        }

        public static readonly BindableProperty FixedBorderRadiusProperty =
            BindableProperty.Create("BorderRadius", typeof(int), typeof(AppButton), 25);

        public int FixedBorderRadius
        {
            get { return (int)this.GetValue(FixedBorderRadiusProperty); }
            set { this.SetValue(FixedBorderRadiusProperty, value); }
        }


        public static readonly BindableProperty LongPressEnabledProperty =
            BindableProperty.Create("LongPressEnabled", typeof(bool), typeof(AppButton), false);

        public bool LongPressEnabled
        {
            get { return (bool)this.GetValue(LongPressEnabledProperty); }
            set { this.SetValue(LongPressEnabledProperty, value); }
        }

        public static readonly BindableProperty GradientEnabledProperty =
            BindableProperty.Create("GradientEnabled", typeof(bool), typeof(AppButton), true);

        public bool GradientEnabled
        {
            get { return (bool)this.GetValue(GradientEnabledProperty); }
            set { this.SetValue(GradientEnabledProperty, value); }
        }

        public Command LongPressCommand { get; set; }
    }
}