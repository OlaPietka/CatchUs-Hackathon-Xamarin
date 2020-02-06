using CatchUs.Data;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyCodePage : ContentPage
    {
        Repository repo = new Repository();

        public VerifyCodePage()
        {
            InitializeComponent();
        }

        async void VerifyCode_Clicked(object sender, EventArgs e)
        {
            var emailRef = Preferences.Get("resetPass_Email", "");
            var codeRef = Preferences.Get("resetPass_Code", "");

            if (VerificationCode.Text == null || VerificationCode.Text == string.Empty || codeRef == string.Empty || emailRef == string.Empty)
                LabelVerifyCode.Text = "Please, enter valid datas";
            else if (VerificationCode.Text.Equals(repo.GetPasswordRecoveryByEmail(emailRef).Code))
            {
                if (DateTime.Now.Date.Subtract(repo.GetPasswordRecoveryByEmail(emailRef).Time).Minutes <= 5)
                    await Navigation.PushModalAsync(new CreateNewPasswordPage());
                else
                    LabelVerifyCode.Text = "Your code has expired! Try again.";
            }
            else
                LabelVerifyCode.Text = "Given code is not valid.";
        }
    }
}