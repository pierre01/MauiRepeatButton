namespace MauiRepeatButton
{
    public partial class MainPage : ContentPage
    {
        private static readonly object _lock = new object();

        int count = 0;
        private PeriodicTimer _repeatActionTimer;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ButtonUpPressed(object sender, EventArgs e)
        {

            // use CancellationToken to stop the timer

            using (_repeatActionTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(100)))
            {
                while (await _repeatActionTimer.WaitForNextTickAsync())
                {
                    lock (_lock)
                    {
                        count++;
                        LabelRepeats.Text = count.ToString();
                        if (!UpButton.IsPressed)
                        {
                            break;
                        }
                    }

                }
            }

        }

        private async void ButtonDownPressed(object sender, EventArgs e)
        {

            // use the cancellation token to stop the timer

            using (_repeatActionTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(100)))
            {

                while (await _repeatActionTimer.WaitForNextTickAsync())
                {
                    lock (_lock)
                    {
                        count--;
                        LabelRepeats.Text = count.ToString();

                        if (!DownButton.IsPressed)
                        {

                            break;
                        }
                    }

                }

            }

        }

        private void ButtonReleased(object sender, EventArgs e)
        {
            //ClearTimer();
            //using (_repeatActionTimer)
            //{
            //    if (_repeatActionTimer != null)
            //    {
            //        _repeatActionTimer.Dispose();
            //        _repeatActionTimer = null;

            //    }
            //}
        }

        private void ClearTimer()
        {

        }
    }

}
