namespace MauiRepeatButton;

public partial class MainPage : ContentPage
{
    CancellationTokenSource _tokenSource;

    int _count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void ButtonUpPressed(object sender, EventArgs e)
    {
        // Check the cancellation token to stop the timer if a button is stuck in  pressed
        if (_tokenSource != null && !_tokenSource.IsCancellationRequested) { _tokenSource.Cancel(); }

        _tokenSource = new CancellationTokenSource();
        CancellationToken ct = _tokenSource.Token;

        using (var repeatActionTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(100)))
        {
            try
            {
                while (await repeatActionTimer.WaitForNextTickAsync(ct))
                {
                    _count++;
                    LabelRepeats.Text = _count.ToString();
                    if (!UpButton.IsPressed)
                    {
                        _tokenSource.Cancel();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // do nothing
            }
        }
    }

    private async void ButtonDownPressed(object sender, EventArgs e)
    {

        // Check the cancellation token to stop the timer if a button is stuck in  pressed mode
        if (_tokenSource != null && !_tokenSource.IsCancellationRequested) { _tokenSource.Cancel(); }

        _tokenSource = new CancellationTokenSource();
        CancellationToken ct = _tokenSource.Token;

        using (var repeatActionTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(100)))
        {
            try
            {
                while (await repeatActionTimer.WaitForNextTickAsync(ct))
                {
                    _count--;
                    LabelRepeats.Text = _count.ToString();

                    if (!DownButton.IsPressed)
                    {
                        _tokenSource.Cancel();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // do nothing
            }

        }

    }
}


