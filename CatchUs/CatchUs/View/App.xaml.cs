using CatchUs.Data;
using CatchUs.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CatchUs.View
{
    public partial class App : Application
    {
        static DatabaseController database;
        Repository repo = new Repository();

        public App()
        {
            #if DEBUG
            LiveReload.Init();
            #endif

            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            Preferences.Clear();
            string id = Preferences.Get("id", "");
            string email = Preferences.Get("email", "");
            string pass = Preferences.Get("pass", "");

            if (email == string.Empty || pass == string.Empty || id == string.Empty)
                Current.MainPage = new MainPage();
            else
            {
                if (repo.GetUserById(Convert.ToInt32(id)).Email.Equals(email) &&
                    repo.GetUserById(Convert.ToInt32(id)).Password.Equals(pass))
                {
                    Current.MainPage = new HomePage();
                }
                else
                    Preferences.Clear();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static DatabaseController Database
        {
            get
            {
                if (database == null)
                    database = new DatabaseController();

                return database;
            }
        }
    }
}
