using CatchUs.Data;
using CatchUs.Model;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetingPage : ContentPage
    {
        Repository repo = new Repository();

        public MeetingPage()
        {
            InitializeComponent();

            AddRecognizerToNavigationBar();

            InitializeView(repo.GetMeetingById(int.Parse(Preferences.Get("meeting_Id", ""))));
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

        void InitializeView(Meeting meeting)
        {
            banner.Source = "banner" + meeting.ActivityIcon;
            activityIcon.Source = meeting.ActivityIcon;
            activityName.Text = meeting.ActivityName;
            date.Text = meeting.Date.ToShortDateString();
            time.Text = meeting.Time.ToString();
            Organizer.Text = meeting.OrganizerName + " " + meeting.OrganizerLastName + ", " + GetAge(meeting.OrganizerAge);
            Participants.Text = meeting.Participants + "";
            DescriptionText.Text = meeting.Description;

            ChatMessage.Text = meeting.Chat;

            ChatEntry.Completed += (sender, e) =>
            {
                var entry = (Entry)sender;
                repo.UpdateMeeting("Chat", repo.GetUserById(int.Parse(Preferences.Get("id", ""))).Name + ": " + meeting.Chat + entry.Text + ", " + DateTime.Now.Date.TimeOfDay + "\n", int.Parse(Preferences.Get("meeting_Id", "")));
                ChatMessage.Text += repo.GetUserById(int.Parse(Preferences.Get("id", ""))).Name + ": " + entry.Text + "\n";
                ChatEntry.Text = string.Empty;
            };
        }

        string GetAge(DateTime date)
        {
            return (DateTime.Now.Year - date.Year).ToString();
        }

        async void ShowOnMapButton_Clicked(object sender, EventArgs e)
        {
            var meeting = repo.GetMeetingById(int.Parse(Preferences.Get("meeting_Id", "")));
            var pin = repo.GetPinById(meeting.Pin_Id);

            await Navigation.PushModalAsync(new HomePage(pin.Latitude, pin.Longitude), false);
        }

        async void RejectButton_Clicked(object sender, EventArgs e)
        {
            var user = repo.GetUserById(int.Parse(Preferences.Get("id", "")));
            var meeting = repo.GetMeetingById(int.Parse(Preferences.Get("meeting_Id", "")));
            var pin = repo.GetPinById(meeting.Pin_Id);

            User organizerUser = new User();

            foreach (UserMeeting um in repo.GetAllUserMeetings())
            {
                if (um.User_Id == user.Id && um.Meeting_Id == meeting.Id)
                    organizerUser = repo.GetUserById(repo.GetMeetingById(meeting.Id).OrganizerUser_Id);
            }

            if (organizerUser.Id == user.Id)
            {
                foreach (UserMeeting um in repo.GetAllUserMeetings())
                {
                    if (um.User_Id == user.Id && um.Meeting_Id == meeting.Id)
                        repo.DeleteUserMeetingByMeetingId(meeting.Id);
                }
                repo.UpdateUser("OrganisedMeetings", (--user.OrganisedMeetings).ToString(), user.Id);
                return;
            }

            foreach (UserMeeting um in repo.GetAllUserMeetings())
            {
                if (um.User_Id == user.Id && um.Meeting_Id == meeting.Id)
                    repo.DeleteUserMeetingByUserId(um.User_Id);
            }
            repo.UpdateUser("ParticipateIn", (--user.ParticipateIn).ToString(), user.Id);

            await Navigation.PushModalAsync(new MeetingsViewPage(), false);
        }
    }
}