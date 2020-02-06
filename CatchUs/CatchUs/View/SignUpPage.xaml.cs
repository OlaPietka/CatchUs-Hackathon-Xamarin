using CatchUs.Custom;
using CatchUs.Data;
using CatchUs.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        Repository repo = new Repository();
        string imagePicked;

        public SignUpPage()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                AppImage theImage = (AppImage)sender;
                Image1.Opacity = 0.4;
                Image2.Opacity = 0.4;
                imagePicked = theImage.ImageName;
                theImage.Opacity = 1;
            };

            Image1.GestureRecognizers.Add(tapGestureRecognizer);
            Image2.GestureRecognizers.Add(tapGestureRecognizer);
        }

        async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            if (IsNameCorrect()
               && IsLastNameCorrect()
               && IsDateCorrect()
               && IsEmailCorrect()
               && IsPasswordCorrect()
               && IsPasswordDuplicationCorrect()
               && IsPhotoChosen())
            {
                var newUser = new User() { Name = Name.Text, LastName = LastName.Text, Age = DatePicker.Date, Email = Email.Text, Password = Password.Text, Photo = imagePicked };

                if (!repo.IsUserEmailExist(newUser.Email))
                {
                    repo.InsertUser(newUser);

                    await Navigation.PushModalAsync(new MainPage());
                }
                else
                    if (repo.IsUserEmailExist(newUser.Email)) ErrorMessage(Email, LabelEmail, "This e-mail is aattached to exist account.");
            }
            else
            {
                if (!IsNameCorrect()) ErrorMessage(Name, LabelName, "First name must have more then 2 characters.");
                if (!IsLastNameCorrect()) ErrorMessage(Name, LabelName, "Last name must have more then 2 characters.");
                if (!IsPasswordCorrect()) ErrorMessage(Password, LabelPassword, "Password must have more then 5 characters.");
                else if (!IsPasswordDuplicationCorrect()) ErrorMessage(RepeatPassword, LabelRepeatPassword, "Passwords must have to be identical.");
                if (!IsEmailCorrect()) ErrorMessage(Email, LabelEmail, "E-mail is not valid.");
                if (!IsPhotoChosen()) ErrorMessage(new AppEntry(), LabelImage, "No profile image has been choosen.");
            }
        }

        async void ErrorMessage(AppEntry field, Label label, string errorMessage)
        {
            field.Text = string.Empty;
            field.BorderColor = Color.Red;
            label.Text = errorMessage;

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            int count = errorMessage.Length;

            if (count > 40)
                label.HeightRequest = 8 + 7 * ((count / 40) - 1);

            await ErrorAnimation(field, label);
        }

        async Task ErrorAnimation(Custom.AppEntry field, Label label)
        {
            if (label.Opacity == 0)
            {
                await Task.WhenAll(
                Vibration(field),
                label.FadeTo(1, 300));
            }
            else
                await Vibration(field);
        }

        async Task Vibration(Custom.AppEntry field)
        {
            const uint animateTime = 60;
            Easing easing = Easing.BounceOut;

            await field.TranslateTo(2, 0, animateTime, easing);
            await field.TranslateTo(-2, 0, animateTime, easing);
            await field.TranslateTo(1, 0, animateTime, easing);
            await field.TranslateTo(-1, 0, animateTime, easing);
            await field.TranslateTo(0, 0, animateTime, easing);
        }

        void Date_Selected(object sender, EventArgs e)
        {
            if (!IsDateCorrect())
                TextDatePicker.Text = "You are to young. Sorry!";
        }

        bool IsNameCorrect()
        {
            if (Name.Text == null)
                return false;

            return Name.Text.Length > 2 && Name.Text.Length < 17;
        }

        bool IsLastNameCorrect()
        {
            if (Name.Text == null)
                return false;

            return Name.Text.Length > 2 && Name.Text.Length < 17;
        }

        bool IsDateCorrect()
        {
            return 2019 - DatePicker.Date.Year > 14;
        }

        bool IsEmailCorrect()
        {
            if (Email.Text == null)
                return false;

            return Email.Text.Length > 0;
        }
        bool IsPasswordCorrect()
        {
            if (Password.Text == null)
                return false;

            return Password.Text.Length > 5;
        }

        bool IsPasswordDuplicationCorrect()
        {
            if (Password.Text == null || RepeatPassword.Text == null)
                return false;

            return Password.Text.Equals(RepeatPassword.Text);
        }

        bool IsPhotoChosen()
        {
            if (imagePicked == null)
                return false;

            return true;
        }
    }
}
