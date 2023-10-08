namespace MauiApp1.Controls;

public class CountdownControl : ContentView
{
    private Label countdownLabel = new Label();
    private Button startButton = new Button();

    public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(5);

    public CountdownControl()
    {
        // Initialize the UI components
        countdownLabel.Text = "Countdown: ";
        startButton.Text = "Start Countdown";
        startButton.Clicked += StartButton_Clicked;

        // Create the layout
        var stackLayout = new StackLayout
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        stackLayout.Children.Add(startButton);
        stackLayout.Children.Add(countdownLabel);

        Content = stackLayout;
    }

    private async void StartButton_Clicked(object sender, EventArgs e)
    {
        // Calculate the end time of the countdown
        DateTime countdownEndTime = DateTime.Now.Add(Duration);

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
