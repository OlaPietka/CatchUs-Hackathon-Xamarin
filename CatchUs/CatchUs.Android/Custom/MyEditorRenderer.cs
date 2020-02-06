﻿using System;
using Android.Content;
using Xamarin.Forms;
using CatchUs.Droid.Custom;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using CatchUs.Custom;
using Android.Util;
using Android.Views;

[assembly: ExportRenderer(typeof(AppEditor), typeof(MyEditorRenderer))]
namespace CatchUs.Droid.Custom
{
    class MyEditorRenderer : EditorRenderer
    {
        public MyEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            UpdateBorders();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null) return;
            UpdateBorders();
            UpdateText();
        }

        void UpdateText()
        {
            var element = this.Element as AppEditor;
            if (element.TextAlignmentCenter)
            {
                Control.Gravity = GravityFlags.CenterHorizontal;
            }
        }

        void UpdateBorders()
        {
            var element = this.Element as AppEditor;

            int[] colors = new int[] { element.StartColor.ToAndroid(), element.EndColor.ToAndroid() };
            var gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors);

            gradientDrawable.SetGradientType(GradientType.LinearGradient);
            gradientDrawable.SetShape(ShapeType.Rectangle);
            gradientDrawable.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(element.BorderRadius)));
            gradientDrawable.SetStroke((int)element.BorderWidth, element.BorderColor.ToAndroid());
            this.Control.Background = gradientDrawable;
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            Android.Util.DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return Android.Util.TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}