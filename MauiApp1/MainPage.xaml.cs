using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private TimeSpan countdownDuration = TimeSpan.FromMinutes(5);
        private DateTime countdownEndTime;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}