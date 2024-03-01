namespace MauiRepeatButton;

public partial class MainPage : ContentPage
{
    private static readonly object _lock = new object();

    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void ButtonUpPressed(object sender, EventArgs e)
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct = cts.Token;

        using (var repeatActionTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(100)))
        {
            while (await repeatActionTimer.WaitForNextTickAsync(ct))
            {
                count++;
                LabelRepeats.Text = count.ToString();
                if (!UpButton.IsPressed)
                {
                    cts.Cancel();
                    break;
                }
            }
        }
    }

    private async void ButtonDownPressed(object sender, EventArgs e)
    {

        // use the cancellation token to stop the timer
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct = cts.Token;

        using (var repeatActionTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(100)))
        {

            while (await repeatActionTimer.WaitForNextTickAsync(ct))
            {

                count--;
                LabelRepeats.Text = count.ToString();

                if (!DownButton.IsPressed)
                {
                    cts.Cancel();
                    break;
                }
            }

        }

    }

}

