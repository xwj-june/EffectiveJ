using MauiApp1.Services;

namespace MauiApp1.Views
{
    public partial class MainPage : ContentPage
    {
        private CountdownTimerService timerService = new CountdownTimerService();
        private TaskRecordingService taskRecordingService = new TaskRecordingService();
        private DateTimeOffset startDateTime;
        
        public TimeSpan Duration { get; set; }

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
                countdownTimer.Text = $"{remainingTime:mm\\:ss}";
            });
        }

        public void StartCountdown(TimeSpan duration)
        {
            // Start the countdown with the given duration
            timerService.Start(duration);
            startDateTime = DateTimeOffset.Now;

            // Subscribe to the countdown finished event
            timerService.CountdownFinished += () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    taskRecordingService.Record(startDateTime, taskDetails.Text);
                });

                

            };
        }

        private void FirstButton_Clicked(object sender, EventArgs e)
        {
            StartCountdown(TimeSpan.FromSeconds(5));
            //StartCountdown(TimeSpan.FromMinutes(30));
        }

        private void SecondButton_Clicked(object sender, EventArgs e)
        {
            StartCountdown(TimeSpan.FromMinutes(60));
        }

        private void stopButton_Clicked(object sender, EventArgs e)
        {
            timerService.Stop();
        }

        private void TestButton_Clicked(object sender, EventArgs e)
        {
            StartCountdown(TimeSpan.FromSeconds(5));
        }
    }
}