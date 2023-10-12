using EffectiveJ.Maui.Services;

namespace EffectiveJ.Maui.ViewModels
{
    public partial class MainPageViewModel : BasicViewModel
    {
        CountdownTimerService timerService;
        TaskRecordingService taskRecordingService;

        public MainPageViewModel(CountdownTimerService timerService, TaskRecordingService taskRecordingService)
        {
            this.timerService = timerService;
            this.taskRecordingService = taskRecordingService;
            Title = "Effective";
            CountdownTimerLabel = "00：00";
            timerService.TimeChanged += TimerService_TimeChanged;
        }

        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public string countdownTimerLabel;

        [ObservableProperty]
        public string taskDetails;

        public TimeSpan Duration { get; set; }
        private DateTimeOffset startDateTime;


        [RelayCommand]
        void StartTimer(string minute)
        {
            startDateTime = DateTimeOffset.Now;

            if(Int32.TryParse(minute, out int minuteInInt))
            {
                if (minute == "5")
                {
                    StartCountdown(TimeSpan.FromSeconds(minuteInInt));
                }
                StartCountdown(TimeSpan.FromMinutes(minuteInInt));
            }
        }

        private void TimerService_TimeChanged(TimeSpan remainingTime)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                CountdownTimerLabel = $"{remainingTime:mm\\:ss}";
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
                    taskRecordingService.Record(startDateTime, taskDetails);
                });
            };
        }
    }
}
