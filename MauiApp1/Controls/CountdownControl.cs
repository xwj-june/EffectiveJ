namespace MauiApp1.Controls;

using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

public class CountdownControl : ContentView
{
    public Button StartButton { get; private set; } = new Button();

    public TimeSpan Duration { get; set; } = TimeSpan.FromMinutes(5);

    public string DisplayText { get; set; }

    public CountdownControl()
    {
        // Initialize the UI components
        StartButton.Text = Duration.ToString();
        StartButton.FontSize = 12;
        StartButton.Clicked += StartButton_Clicked;

        // Create the layout
        var stackLayout = new StackLayout
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand,
            VerticalOptions = LayoutOptions.CenterAndExpand
        };
        stackLayout.Children.Add(StartButton);

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
}
