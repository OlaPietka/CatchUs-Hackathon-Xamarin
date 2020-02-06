using System;
using Android.Content;
using Xamarin.Forms;
using CatchUs.Droid.Custom;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using CatchUs.Custom;
using Android.Util;

[assembly: ExportRenderer(typeof(AppButton), typeof(MyButtonRenderer))]
namespace CatchUs.Droid.Custom
{
    class MyButtonRenderer : ButtonRenderer
    {
        public MyButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            UpdateGradientBackground();
            var view = (AppButton)Element;

            if (view.LongPressEnabled && view.LongPressCommand != null)
            {
                this.Control.LongClick += (s, args) =>
                {
                    view.LongPressCommand.Execute(new object());
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null) return;

            UpdateGradientBackground();
        }

        protected override void UpdateBackgroundColor()
        {
            base.UpdateBackgroundColor();

            UpdateGradientBackground();
        }

        private void UpdateGradientBackground()
        {
            var button = this.Element as AppButton;
            if (button != null && button.GradientEnabled)
            {
                int[] colors;

                if (button.MiddleColorEnabled)
                {
                    colors = new int[] { button.StartColor.ToAndroid(), button.MiddleColor.ToAndroid(), button.EndColor.ToAndroid() };
                } else
                {
                    colors = new int[] { button.StartColor.ToAndroid(), button.EndColor.ToAndroid() };
                }

                var gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors);

                switch (button.Orientation)
                {
                    case "TopBottom":
                        {
                            gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors);
                            break;
                        }
                    case "BottomTop":
                        {
                            gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.BottomTop, colors);
                            break;
                        }
                    case "RightLeft":
                        {
                            gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.RightLeft, colors);
                            break;
                        }
                    case "LeftRight":
                        {
                            gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.LeftRight, colors);
                            break;
                        }
                }
                
                gradientDrawable.SetGradientType(GradientType.LinearGradient);
                gradientDrawable.SetShape(ShapeType.Rectangle);
                gradientDrawable.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(button.FixedBorderRadius)));
                gradientDrawable.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
                this.Control.Background = gradientDrawable;
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            Android.Util.DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return Android.Util.TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}