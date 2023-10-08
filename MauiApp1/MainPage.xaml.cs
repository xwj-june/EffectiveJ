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

        private async void StartButton_Clicked(object sender, EventArgs e)
        {
            // Calculate the end time of the countdown
            countdownEndTime = DateTime.Now.Add(countdownDuration);

            // Disable the button during the countdown
            startButton.IsEnabled = false;

            // Update the countdown label every second
            while (DateTime.Now < countdownEndTime)
            {
                TimeSpan remainingTime = countdownEndTime - DateTime.Now;
                countdownLabel.Text = $"Countdown: {remainingTime:mm\\:ss}";
                await Task.Delay(1000); // Delay for 1 second
            }

            // Re-enable the button when the countdown is finished
            startButton.IsEnabled = true;
            countdownLabel.Text = "Countdown: Done!";
        }
    }
}