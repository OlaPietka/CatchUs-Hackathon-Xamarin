using System;
using CatchUs.Data;
using CatchUs.Model;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMeetingPopupPage : PopupPage
    {
        Repository repo = new Repository();
        Xamarin.Forms.GoogleMaps.Pin pin;
        Xamarin.Forms.GoogleMaps.Map map;
        User user;
        Meeting met;
        int meeting_Id;

        public ShowMeetingPopupPage(Xamarin.Forms.GoogleMaps.Pin pin, Xamarin.Forms.GoogleMaps.Map map)
        {
            InitializeComponent();

            this.pin = pin;
            this.map = map;

            user = repo.GetUserById(int.Parse(Preferences.Get("id", "")));
            meeting_Id = -1;
           
            int pin_Id = -1;

            bool alreadyIn = false;

            foreach (Pin p in repo.GetAllPins())
            {
                if (p.Latitude == pin.Position.Latitude && p.Longitude == pin.Position.Longitude)
                    pin_Id = p.Id;
            }
            foreach (Meeting m in repo.GetAllMeetings())
            {
                if (pin_Id != -1 && m.Pin_Id == pin_Id)
                {
                    meeting_Id = m.Id;
                    met = m;
                }
            }
            foreach (UserMeeting um in repo.GetAllUserMeetings())
            {
                if (um.User_Id == user.Id && um.Meeting_Id == meeting_Id)
                    alreadyIn = true;
            }

            if (alreadyIn)
            {
                AcceptButton.IsEnabled = false;
                CancelButton.Text = "Reject";
            }
            else
            {
                AcceptButton.IsEnabled = true;
                CancelButton.Text = "Cancel";
            }

            InitializeView(repo.GetMeetingById(meeting_Id));
        }

        void InitializeView(Meeting meeting)
        {
            User organizerUser = new User();

            foreach (UserMeeting um in repo.GetAllUserMeetings())
            {
                if (um.User_Id == met.OrganizerUser_Id && um.Meeting_Id == meeting_Id)
                    organizerUser = repo.GetUserById(met.OrganizerUser_Id);
            }

            ProfileImage.Source = organizerUser.Photo;
            Name.Text = organizerUser.Name;
            LastName.Text = organizerUser.LastName;
            Age.Text = $"AGE: {GetAge(organizerUser.Age)}";
            Likes.Text = $"LIKES: {organizerUser.Likes}";
            AcitvityName.Text = repo.GetMeetingById(meeting_Id).ActivityName;
            Date.Text = repo.GetMeetingById(meeting_Id).Date.Date.ToShortDateString();
            Time.Text = repo.GetMeetingById(meeting_Id).Time.ToString();
            Participants.Text = repo.GetMeetingById(meeting_Id).Participants.ToString();
            Discription.Text = repo.GetMeetingById(meeting_Id).Description;
        }

        string GetAge(DateTime date)
        {
            return (DateTime.Now.Year - date.Year).ToString();
        }

        async private void AcceptButton_Clicked(object sender, EventArgs e)
        {
            repo.InsertUserMeeting(new UserMeeting { User_Id = user.Id, Meeting_Id = meeting_Id });
            repo.UpdateMeeting("Participants", (++repo.GetMeetingById(meeting_Id).Participants).ToString(), meeting_Id);
            repo.UpdateUser("ParticipateIn", (++user.ParticipateIn).ToString(), user.Id);

            await PopupNavigation.Instance.PopAsync();
        }

        async private void CancelButton_Clicked(object sender, EventArgs e)
        {
            if (CancelButton.Text.Equals("Reject"))
            {
                User organizerUser = new User();

                foreach (UserMeeting um in repo.GetAllUserMeetings())
                {
                    if (um.User_Id == user.Id && um.Meeting_Id == meeting_Id)
                        organizerUser = repo.GetUserById(repo.GetMeetingById(meeting_Id).OrganizerUser_Id);
                }

                if (organizerUser.Id == user.Id)
                {
                    foreach (UserMeeting um in repo.GetAllUserMeetings())
                    {
                        if (um.User_Id == user.Id && um.Meeting_Id == meeting_Id)
                            repo.DeleteUserMeetingByMeetingId(meeting_Id);
                    }
                    repo.UpdateMeeting("Participants", (--repo.GetMeetingById(meeting_Id).Participants).ToString(), meeting_Id);
                    repo.UpdateUser("OrganisedMeetings", (--user.OrganisedMeetings).ToString(), user.Id);
                    return;
                }

                foreach (UserMeeting um in repo.GetAllUserMeetings())
                {
                    if (um.User_Id == user.Id && um.Meeting_Id == meeting_Id)
                        repo.DeleteUserMeetingByUserId(um.User_Id);
                }
                repo.UpdateUser("ParticipateIn", (--user.ParticipateIn).ToString(), user.Id);
            }

            await PopupNavigation.Instance.PopAsync();
        }
    }
}