namespace MauiApp1.Controls;

using System;
using Microsoft.Maui.Controls;

public class CountdownControl : ContentView
{
    public Button StartButton { get; private set; } = new Button();
    private Label countdownLabel = new Label();

    public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(5);

    public CountdownControl()
    {
        // Initialize the UI components
        countdownLabel.Text = "Countdown: ";
        StartButton.Text = "Start Countdown";
        StartButton.Clicked += StartButton_Clicked;

        // Create the layout
        var stackLayout = new StackLayout
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        stackLayout.Children.Add(StartButton);
        stackLayout.Children.Add(countdownLabel);

        Content = stackLayout;
    }

    private void StartButton_Clicked(object sender, EventArgs e)
    {
        // Find the parent MainPage
        MainPage parentPage = FindParent<MainPage>(this);

        // Start the countdown if the parent is found
        if (parentPage != null)
        {
            parentPage.StartCountdown(Duration);
        }
    }

    private T FindParent<T>(VisualElement element) where T : VisualElement
    {
        var parent = element.Parent;
        while (parent != null)
        {
            if (parent is T result)
            {
                return result;
            }
            parent = parent.Parent;
        }
        return null;
    }

    public void UpdateCountdownLabel(string newText)
    {
        countdownLabel.Text = newText;
    }
}
