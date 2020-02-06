using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();

            AddRecognizerToNavigationBar();
		}
        void AddRecognizerToNavigationBar()
        {
            var tapGestureRecognizerLeft = new TapGestureRecognizer();
            tapGestureRecognizerLeft.Tapped += async (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopModalAsync();
                    Navigation.PushModalAsync(new MeetingsViewPage(), false);
                });
            };

            var tapGestureRecognizerMiddle = new TapGestureRecognizer();
            tapGestureRecognizerMiddle.Tapped += async (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopModalAsync();
                    Navigation.PushModalAsync(new HomePage(), false);
                });
            };

            var tapGestureRecognizerRight = new TapGestureRecognizer();
            tapGestureRecognizerRight.Tapped += async (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopModalAsync();
                    Navigation.PushModalAsync(new SettingsPage(), false);
                });
            };

            Left.GestureRecognizers.Add(tapGestureRecognizerLeft);
            Middle.GestureRecognizers.Add(tapGestureRecognizerMiddle);
            Right.GestureRecognizers.Add(tapGestureRecognizerRight);
        }

        private void SignOut_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            Device.BeginInvokeOnMainThread(() => {
                Navigation.PopModalAsync();
                Navigation.PushModalAsync(new MainPage(), false);
            });
        }

        private void Authors_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Authors", "Ola Pietka, Rafal Tecza", "Close");
        }

        private void Contact_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Contact", "This feature is not ready yet, sorry.", "Close");
        }

        private void Avatar_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Contact", "This feature is not ready yet, sorry.", "Close");
        }
    }
}