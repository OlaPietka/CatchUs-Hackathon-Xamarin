using CatchUs.Custom;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CatchUs.Validation
{
    class NameValidation : Behavior<AppEntry>
    {
        AppEntry control;
        string _placeHolder;
        Color _placeHolderColor;

        protected override void OnAttachedTo(AppEntry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            bindable.PropertyChanged += OnPropertyChanged;
            control = bindable;
            _placeHolder = bindable.Placeholder;
            _placeHolderColor = bindable.PlaceholderColor;
        }

        void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {

            var username = e.NewTextValue;

            var usernameEntry = sender as AppEntry;

            var usernamePattern = "^[a-zA-Z0-9]+$";

            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (!Regex.IsMatch(username, usernamePattern, RegexOptions.IgnoreCase))
                    usernameEntry.BorderColor = Color.Red;
                else
                    usernameEntry.BorderColor = Color.FromHex("#212526");
            }
        }

        protected override void OnDetachingFrom(AppEntry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= Bindable_TextChanged;
        }

        void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AppEntry.BorderColorProperty.PropertyName && control != null)
            {


            }
        }
    }
}
