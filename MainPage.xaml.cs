using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace SnappApp.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var connection = NetworkInformation.GetInternetConnectionProfile();
            bool status = (connection != null && connection.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            if (status == true)
            {
                cnAsync();
                WebViewPage wp = new WebViewPage();
                Frame.Navigate(typeof(WebViewPage));
            }
            else
            {
                MessageDialog msgbox = new MessageDialog("خطای اتصال به اینترنت");
            }

        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var connection = NetworkInformation.GetInternetConnectionProfile();
            bool status = (connection != null && connection.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            if (status == true)
            {
                cnAsync();
                WebViewPage wp = new WebViewPage();
                Frame.Navigate(typeof(WebViewPage));
            }
            else
            {
                MessageDialog msgbox = new MessageDialog("خطای اتصال به اینترنت");
            }
        }
        private void webView_PermissionRequested(WebView sender, WebViewPermissionRequestedEventArgs args)
        {
            if (args.PermissionRequest.PermissionType == WebViewPermissionType.Geolocation)
                args.PermissionRequest.Allow();
        }
        public async void cnAsync()
        {
                var locator = new Windows.Devices.Geolocation.Geolocator();
                var location = await locator.GetGeopositionAsync();
                var position = location.Coordinate.Point.Position;
            var uriMe = new Uri(@"ms-settings:privacy-location");
        }
    }
}
