using CatchUs.Data;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CatchUs.View
{
    public partial class MainPage : ContentPage
    {
        Repository repo = new Repository();

        public MainPage()
        {
            InitializeComponent();
        }

        async void LogInButton_Clicked(object sender, EventArgs e)
        {
            if (!repo.IsUserEmailExist(Login.Text))
            {
                var action = await DisplayAlert("Błąd logowania", "Nieprawidłowe hasło lub nazwa użytkownika", "Zapomniałem/am hasła", "Wróć");
                
                if (action) // action jest bool. Jezeli ktos kliknie wroc to action jest false (nic sie nie dzieje) jezeli zapomni hasla to action jest true
                    await Navigation.PushModalAsync(new ForgotPassPage());
            }
            else
            {
                Preferences.Set("id", repo.GetUserByEmail(Login.Text).Id.ToString());
                Preferences.Set("email", Login.Text);
                Preferences.Set("pass", Password.Text);

                await Navigation.PushModalAsync(new HomePage());
            }
        }

        async void ForgotPassword_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgotPassPage());
        }

        async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignUpPage());
        }
    }
}
