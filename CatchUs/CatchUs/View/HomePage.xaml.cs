using CatchUs.Custom;
using CatchUs.Data;
using CatchUs.Model;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace CatchUs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        Map map;
        Repository repo = new Repository();

        public HomePage()
        {
            InitializeComponent();

            AddRecognizerToNavigationBar();

            GetNeededPermissions();
            SetupMap();
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

        private void SetupMap()
        {
            if (map == null)
            {
                map = new Map
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    MapType = MapType.Street,
                    MapStyle = MapStyle.FromJson(GetJson("map_style.json")),
                    MyLocationEnabled = true,
                };

                HomePageLayout.Children.Add(map);
            }

            Position position = new Position(54.393, 18.595);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, new Distance(2500d)));

            map.UiSettings.ZoomGesturesEnabled = true;
            map.UiSettings.ScrollGesturesEnabled = true;
            map.UiSettings.IndoorLevelPickerEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;

            map.UiSettings.CompassEnabled = false;
            map.UiSettings.TiltGesturesEnabled = false;
            map.UiSettings.RotateGesturesEnabled = false;

            string fileName = "icon_waiting.png";
            string path = "Assets";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}.{fileName}");

            InitializePinsFromDb();

            // Map Clicked
            map.MapClicked += (sender, e) =>
            {
                var lat = e.Point.Latitude.ToString("0.000");
                var lng = e.Point.Longitude.ToString("0.000");

                this.DisplayAlert("MapClicked", $"{lat}/{lng}", "CLOSE");
            };

            // Map Long clicked
            map.MapLongClicked += (sender, e) =>
            {
                double lat = e.Point.Latitude;
                double lng = e.Point.Longitude;
                Console.WriteLine(lat + " " + lng);

                Xamarin.Forms.GoogleMaps.Pin pin = new Xamarin.Forms.GoogleMaps.Pin()
                {
                    Type = PinType.Place,
                    Label = "",
                    Address = "",
                    Position = new Position(lat, lng),
                    Icon = BitmapDescriptorFactory.FromStream(stream, id: fileName)
                };

                pin.Clicked += (sender_pin, e_pin) =>
                {
                    PopupNavigation.PushAsync(new ShowMeetingPopupPage(pin, map));
                };

                map.Pins.Add(pin);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, map.VisibleRegion.Radius));

                double top = CalculateBoundingCoordinates(map.VisibleRegion, "top");
                double bottom = CalculateBoundingCoordinates(map.VisibleRegion, "bottom");

                double y = lat - ((top - bottom) / 2);
                MapSpan span = new MapSpan(new Position(y, lng), map.VisibleRegion.LatitudeDegrees, map.VisibleRegion.LongitudeDegrees);

                map.MoveToRegion(span);

                PopupNavigation.PushAsync(new CreateMeetingPopupPage(pin, map));
            };
        }

        public HomePage(double _lat, double _lng)
        {

            InitializeComponent();

            AddRecognizerToNavigationBar();

            if (map == null)
            {
                map = new Map
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    MapType = MapType.Street,
                    MapStyle = MapStyle.FromJson(GetJson("map_style.json")),
                    MyLocationEnabled = true,
                };

                HomePageLayout.Children.Add(map);
            }

            Position position = new Position(_lat, _lng);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, new Distance(500d)));

            map.UiSettings.ZoomGesturesEnabled = true;
            map.UiSettings.ScrollGesturesEnabled = true;
            map.UiSettings.IndoorLevelPickerEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;

            map.UiSettings.CompassEnabled = false;
            map.UiSettings.TiltGesturesEnabled = false;
            map.UiSettings.RotateGesturesEnabled = false;

            string fileName = "icon_waiting.png";
            string path = "Assets";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}.{fileName}");

            InitializePinsFromDb();

            // Map Clicked
            map.MapClicked += (sender, e) =>
            {
                var lat = e.Point.Latitude.ToString("0.000");
                var lng = e.Point.Longitude.ToString("0.000");

                this.DisplayAlert("MapClicked", $"{lat}/{lng}", "CLOSE");
            };

            // Map Long clicked
            map.MapLongClicked += (sender, e) =>
            {
                double lat = e.Point.Latitude;
                double lng = e.Point.Longitude;
                Console.WriteLine(lat + " " + lng);

                Xamarin.Forms.GoogleMaps.Pin pin = new Xamarin.Forms.GoogleMaps.Pin()
                {
                    Type = PinType.Place,
                    Label = "",
                    Address = "",
                    Position = new Position(lat, lng),
                    Icon = BitmapDescriptorFactory.FromStream(stream, id: fileName)
                };

                pin.Clicked += (sender_pin, e_pin) =>
                {
                    PopupNavigation.PushAsync(new ShowMeetingPopupPage(pin, map));
                };

                map.Pins.Add(pin);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, map.VisibleRegion.Radius));

                double top = CalculateBoundingCoordinates(map.VisibleRegion, "top");
                double bottom = CalculateBoundingCoordinates(map.VisibleRegion, "bottom");

                double y = lat - ((top - bottom) / 2);
                MapSpan span = new MapSpan(new Position(y, lng), map.VisibleRegion.LatitudeDegrees, map.VisibleRegion.LongitudeDegrees);

                map.MoveToRegion(span);

                PopupNavigation.PushAsync(new CreateMeetingPopupPage(pin, map));
            };
        }

        void InitializePinsFromDb()
        {
            foreach (Model.Pin p in repo.GetAllPins())
            {
                Meeting meeting = new Meeting();
                foreach (Meeting m in repo.GetAllMeetings())
                {
                    if (m.Pin_Id == p.Id)
                    {
                        meeting = m;

                        string path = "Assets";
                        var assembly = typeof(MainPage).GetTypeInfo().Assembly;
                        var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}.{p.Icon}");

                        Xamarin.Forms.GoogleMaps.Pin newPin = new Xamarin.Forms.GoogleMaps.Pin()
                        {
                            Type = PinType.Place,
                            Label = meeting.ActivityName,
                            Address = "Show more",
                            Position = new Position(p.Latitude, p.Longitude),
                            Icon = BitmapDescriptorFactory.FromStream(stream, id: p.Icon)
                        };

                        map.Pins.Add(newPin);

                        newPin.Clicked += (sender_pin, e_pin) =>
                        {
                            PopupNavigation.PushAsync(new ShowMeetingPopupPage(newPin, map));
                        };
                    }
                }
            }
        }

        private double CalculateBoundingCoordinates(MapSpan region, string value)
        {
            var center = region.Center;
            var halfheightDegrees = region.LatitudeDegrees / 2.50;
            var halfwidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - halfwidthDegrees;
            var right = center.Longitude + halfwidthDegrees;
            var top = center.Latitude + halfheightDegrees;
            var bottom = center.Latitude - halfheightDegrees;

            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;

            switch (value)
            {
                case "top":
                    return top;
                case "left":
                    return left;
                case "right":
                    return right;
                case "bottom":
                    return bottom;
                default:
                    return 0;
            }
        }

        public string GetJson(string file)
        {
            string jsonFileName = file;
            string path = "Assets";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{path}.{jsonFileName}");

            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();
                Console.WriteLine(jsonString);
                return jsonString;
            }
        }

        async private void GetNeededPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Permission Notification", "We need access to your current location.", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    await DisplayAlert("Permission Notification", "Permissions have been assigned correctly.", "OK");
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Permission Notification", "We couldn't add permissions, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
