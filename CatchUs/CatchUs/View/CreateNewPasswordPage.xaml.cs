using CatchUs.Data;
using CatchUs.Model;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewPasswordPage : ContentPage
    {
        Repository repo = new Repository();

        public CreateNewPasswordPage()
        {
            InitializeComponent();
        }

        private async void ChangePassword_Clicked(object sender, EventArgs e)
        {
            if (IsPasswordCorrect() 
                && IsPasswordDuplicationCorrect())
            {
                var emailRef = Preferences.Get("resetPass_Email", "");

                if (emailRef != string.Empty)
                {
                    repo.UpdateUser("Password", NewPassword.Text, repo.GetUserByEmail(emailRef).Id);

                    repo.DeletePasswordRecovery(repo.GetPasswordRecoveryByEmail(emailRef).Id);

                    Preferences.Remove("resetPass_Email");

                    await Navigation.PushModalAsync(new MainPage());
                }
                else
                    Console.WriteLine("Something bad happend :<");
            }
            else
            {
                if (!IsPasswordCorrect()) ErrorMessage(NewPassword, LabelNewPassword, "Password lenght have to be mor then 6 characters.");
                else if (!IsPasswordCorrect()) ErrorMessage(NewRepeatPassword, LabelNewPassword, "Passwords dose not match.");
            }
        }

        async void ErrorMessage(Custom.AppEntry field, Label label, string errorMessage)
        {
            field.Text = string.Empty;
            field.BorderColor = Color.Red;

            label.Text = errorMessage;

            label.Opacity = 0;
            await label.FadeTo(1, 300);
        }

        #region Checking If It's Correct
        bool IsPasswordCorrect()
        {
            if (NewPassword.Text == null)
                return false;

            return NewPassword.Text.Length > 5;
        }

        bool IsPasswordDuplicationCorrect()
        {
            if (NewPassword.Text == null || NewRepeatPassword.Text == null)
                return false;

            return NewPassword.Text.Equals(NewRepeatPassword.Text);
        }
        #endregion
    }
}