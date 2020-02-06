using CatchUs.Custom;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CatchUs.Validation
{
    public class EmailValidation : Behavior<AppEntry>
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

            var email = e.NewTextValue;

            string emailPatten = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            var emailEntry = sender as AppEntry;

            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (!Regex.IsMatch(email, emailPatten, RegexOptions.IgnoreCase))
                    emailEntry.BorderColor = Color.Red;
                else
                    emailEntry.BorderColor = Color.FromHex("#212526");
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
