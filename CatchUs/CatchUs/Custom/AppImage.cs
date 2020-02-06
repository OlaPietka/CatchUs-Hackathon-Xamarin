using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CatchUs.Custom
{
    public class AppImage : Image
    {
        public static readonly BindableProperty ImageSourceProperty =
          BindableProperty.Create(propertyName: nameof(ImageSource),
              returnType: typeof(string),
              declaringType: typeof(AppImage),
              defaultValue: "");

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly BindableProperty ImageNameProperty =
          BindableProperty.Create(propertyName: nameof(ImageName),
              returnType: typeof(string),
              declaringType: typeof(AppImage),
              defaultValue: "");

        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        public static readonly BindableProperty BorderThicknessProperty =
          BindableProperty.Create(propertyName: nameof(BorderThickness),
              returnType: typeof(float),
              declaringType: typeof(AppImage),
              defaultValue: 0F);

        public float BorderThickness
        {
            get { return (float)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(propertyName: nameof(BorderColor),
              returnType: typeof(Color),
              declaringType: typeof(AppImage),
              defaultValue: Color.White);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(propertyName: nameof(FillColor),
              returnType: typeof(Color),
              declaringType: typeof(AppImage),
              defaultValue: Color.Transparent);

        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }
    }
}
