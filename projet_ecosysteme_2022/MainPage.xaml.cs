namespace projet_ecosysteme_2022;

public partial class MainPage : ContentPage
{
    IDispatcherTimer timer;
    Simulation simulation;

    public MainPage()
    {
        InitializeComponent();

        simulation = Resources["simulation"] as Simulation;

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(200);
        timer.Tick += this.OnTimeEvent;
        timer.Start();
    }

    private void OnTimeEvent(object source, EventArgs e)
    {
        simulation.Update();
        graphics.Invalidate();
    }
}