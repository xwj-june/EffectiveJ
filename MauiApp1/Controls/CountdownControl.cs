namespace MauiApp1.Controls;

using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

public class CountdownControl : ContentView
{
    public Button StartButton { get; private set; } = new Button();
    private Label countdownLabel = new Label();
    private Entry entry = new Entry();

    public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(5);

    private DateTimeOffset StartDateTime { get; set; }
    private DateTimeOffset EndDateTime { get; set; }


    public CountdownControl()
    {
        // Initialize the UI components
        countdownLabel.Text = "Countdown: ";
        StartButton.Text = "Start Countdown";
        StartButton.Clicked += StartButton_Clicked;

        entry.Placeholder = "Enter task details";

        // Create the layout
        var stackLayout = new StackLayout
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        stackLayout.Children.Add(StartButton);
        stackLayout.Children.Add(countdownLabel);
        stackLayout.Children.Add(entry);

        Content = stackLayout;
    }

    private void StartButton_Clicked(object sender, EventArgs e)
    {
        // Find the parent MainPage
        MainPage parentPage = FindParent<MainPage>(this);

        // Start the countdown if the parent is found
        if (parentPage != null)
        {
            StartDateTime = DateTimeOffset.Now;
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

    public void Record()
    {
        EndDateTime = DateTimeOffset.Now;
        SaveTimestampToFile();
    }

    private string FormatText()
    {
        var timeDifference = EndDateTime.AddMinutes(1) - StartDateTime;
        return $"({timeDifference.Minutes})[{StartDateTime}-{EndDateTime}] Task: {entry.Text}";
    }

    private async void SaveTimestampToFile()
    {
        string formattedTimestamp = FormatText();

        try
        {
            var path = @"c:\EffectiveJ-Records";

            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            
            //TEMP: create one file for each record, please check the TODO below
            var fullPath = Path.Combine(path, $"records-{DateTimeOffset.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.txt");

            //TODO: investigate why this has been append more than once
            File.AppendAllText(fullPath, formattedTimestamp + Environment.NewLine);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during file operations
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

}
