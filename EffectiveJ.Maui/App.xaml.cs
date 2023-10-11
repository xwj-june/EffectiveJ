namespace EffectiveJ.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Width = 400;
            window.Height= 300;
            window.Created += (s, e) =>
            {
                // Custom logic
            };

            return window;
        }
    }
}
