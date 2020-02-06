using System;
using System.Reflection;
using CatchUs.Custom;
using CatchUs.Data;
using CatchUs.Model;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMeetingPopupPage : PopupPage
    {
        Repository repo = new Repository();
        string imagePicked;
        string activityPicked;
        Xamarin.Forms.GoogleMaps.Pin pin;
        Xamarin.Forms.GoogleMaps.Map map;

        public CreateMeetingPopupPage(Xamarin.Forms.GoogleMaps.Pin pin, Xamarin.Forms.GoogleMaps.Map map)
        {
            InitializeComponent();

            this.pin = pin;
            this.map = map;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                AppImage theImage = (AppImage)sender;
                imagePicked = theImage.ImageSource;

                Icon_1.Opacity = 0.4; 
                Icon_2.Opacity = 0.4; 
                Icon_3.Opacity = 0.4; 
                Icon_4.Opacity = 0.4; 
                Icon_5.Opacity = 0.4; 
                Icon_6.Opacity = 0.4; 
                Icon_7.Opacity = 0.4; 
                Icon_8.Opacity = 0.4; 
                Icon_9.Opacity = 0.4; 
                Icon_10.Opacity = 0.4; 

                activityPicked = theImage.ImageName;
                theImage.Opacity = 1;
            };

            Editor.Focused += (sender, e) =>
            {
                ChoseActivity.IsVisible = false;
                DateAndTime.IsVisible = false;
                DatePicker.IsVisible = false;
                TimePicker.IsVisible = false;
                Icon_1.IsVisible = false;
                Icon_2.IsVisible = false;
                Icon_3.IsVisible = false;
                Icon_4.IsVisible = false;
                Icon_5.IsVisible = false;
                Icon_6.IsVisible = false;
                Icon_7.IsVisible = false;
                Icon_8.IsVisible = false;
                Icon_9.IsVisible = false;
                Icon_10.IsVisible = false;
            };

            Editor.Completed += (sender, e) =>
            {
                ChoseActivity.IsVisible = true;
                DateAndTime.IsVisible = true;
                DatePicker.IsVisible = true;
                TimePicker.IsVisible = true;
                Icon_1.IsVisible = true;
                Icon_2.IsVisible = true;
                Icon_3.IsVisible = true;
                Icon_4.IsVisible = true;
                Icon_5.IsVisible = true;
                Icon_6.IsVisible = true;
                Icon_7.IsVisible = true;
                Icon_8.IsVisible = true;
                Icon_9.IsVisible = true;
                Icon_10.IsVisible = true;
            };

            Icon_1.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_2.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_3.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_4.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_5.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_6.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_7.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_8.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_9.GestureRecognizers.Add(tapGestureRecognizer);
            Icon_10.GestureRecognizers.Add(tapGestureRecognizer);
        }


        async void AcceptButton_Clicked(object sender, EventArgs e)
        {
            if ((IsDateAndTimeCorrect()
                && IsActivityPicked()
                && IsDescription()))
            {
                var user = repo.GetUserById(int.Parse(Preferences.Get("id", "")));

                repo.UpdateUser("OrganisedMeetings", (user.OrganisedMeetings + 1).ToString(), user.Id);

                var newPin = new Model.Pin { Latitude = pin.Position.Latitude, Longitude = pin.Position.Longitude, Icon = imagePicked };
                repo.InsertPin(newPin);

                Meeting meeting = new Meeting
                {
                    OrganizerUser_Id = user.Id,
                    OrganizerName = user.Name,
                    OrganizerLastName = user.LastName,
                    OrganizerAge = user.Age,
                    ActivityName = activityPicked,
                    ActivityIcon = imagePicked,
                    Date = DatePicker.Date,
                    Time = TimePicker.Time,
                    Participants = 1,
                    Pin_Id = repo.GetIdByPin(newPin),
                    Description = Editor.Text
                };
                repo.InsertMeeting(meeting);

                UserMeeting userMeeting = new UserMeeting
                {
                    User_Id = user.Id,
                    Meeting_Id = meeting.Id
                };

                repo.InsertUserMeeting(userMeeting);

                string path = "Assets";
                var assembly = typeof(MainPage).GetTypeInfo().Assembly;
                var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}.{imagePicked}");
                pin.Icon = BitmapDescriptorFactory.FromStream(stream, id: imagePicked);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, map.VisibleRegion.Radius));

                pin.Label = activityPicked;
                pin.Address = "Show more";

                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                if (!IsDateAndTimeCorrect()) await DisplayAlert("Something went wrong.", "The date and time must be greater than today.", "Back");
                if (!IsActivityPicked()) await DisplayAlert("Something went wrong.", "You have to choose activity.", "Back");
                if (!IsDescription()) await DisplayAlert("Something went wrong.", "You have to write something in the description.", "Back");
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        async private void CancelButton_Clicked(object sender, EventArgs e)
        {
            map.Pins.Remove(pin);
            await PopupNavigation.Instance.PopAsync();
        }

        #region Check If It's Correct
        bool IsDateAndTimeCorrect()
        {
            if (DatePicker.Date.Date == DateTime.Now.Date)
                return TimePicker.Time > DateTime.Now.TimeOfDay;

            return (DatePicker.Date > DateTime.Now.Date);
        }

        bool IsActivityPicked()
        {
            return activityPicked != null;
        }

        bool IsDescription()
        {
            return Editor.Text != string.Empty;
        }
        #endregion
    }
}