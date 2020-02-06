using CatchUs.Data;
using CatchUs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CatchUs.View
{
    public partial class MeetingsViewPage : ContentPage
    {
        public ObservableCollection<MeetingsView> Meetings { get; set; }
        Repository repo = new Repository();

        public MeetingsViewPage()
        {
            InitializeComponent();

            AddRecognizerToNavigationBar();

            List<MeetingsView> meetings = new List<MeetingsView>();
            foreach (UserMeeting u in repo.GetAllMeetingsFromUser(int.Parse(Preferences.Get("id", ""))))
            {
                foreach (Meeting m in repo.GetAllMeetings())
                {
                    if (u.Meeting_Id == m.Id)
                        meetings.Add(new MeetingsView { Id = m.Id, Banner = "banner" + m.ActivityIcon, Image = m.ActivityIcon, Name = m.ActivityName, Date = m.Date.ToShortDateString(), Time = m.Time.ToString(@"hh\:mm") });
                }
            }

            Meetings = new ObservableCollection<MeetingsView>(SortDescending(meetings));

            listView.ItemsSource = Meetings;
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

        private List<MeetingsView> SortDescending(List<MeetingsView> list)
        {
            list.Sort((a, b) => b.Date.CompareTo(a.Date));

            return list;
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MeetingsView)e.SelectedItem;
            Preferences.Set("meeting_Id", item.Id.ToString());

            await Navigation.PushModalAsync(new MeetingPage());
        }
    }
}