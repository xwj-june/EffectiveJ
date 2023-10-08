using MauiApp1.Controls;
using MauiApp1.Services;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private CountdownTimerService timerService = new CountdownTimerService();


        public MainPage()
        {
            InitializeComponent();
            // Subscribe to the timer service's TimeChanged event

            timerService.TimeChanged += TimerService_TimeChanged;
        }


        private void TimerService_TimeChanged(TimeSpan remainingTime)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                // Update the countdown label by calling the UpdateCountdownLabel method in CountdownControl
                foreach (var control in stackLayout.Children)
                {
                    if (control is CountdownControl countdownControl)
                    {
                        countdownControl.UpdateCountdownLabel($"Countdown: {remainingTime:mm\\:ss}");
                    }
                }
            });
        }

        public void StartCountdown(TimeSpan duration)
        {
            // Start the countdown with the given duration
            timerService.Start(duration);

            // Disable all buttons during the countdown
            foreach (var control in stackLayout.Children)
            {
                if (control is CountdownControl countdownControl)
                {
                    countdownControl.StartButton.IsEnabled = false;
                }
            }

            // Subscribe to the countdown finished event
            timerService.CountdownFinished += () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Re-enable all buttons when the countdown is finished
                    foreach (var control in stackLayout.Children)
                    {
                        if (control is CountdownControl countdownControl)
                        {
                            countdownControl.StartButton.IsEnabled = true;
                        }
                    }
                });
            };
        }
    }
}