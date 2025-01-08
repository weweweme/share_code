namespace ShareCode.src.Platform
{
    public class WindowsPlatformHandler : PlatformHandler
    {
        public override void ConfigureWindow()
        {
            var window = Application.Current!.Windows[0];
            window.Height = GlobalConstants.WINDOW_HEIGHT;
            window.Width = GlobalConstants.WINDOW_WIDTH;
            window.MinimumHeight = GlobalConstants.WINDOW_MIN_HEIGHT;
            window.MinimumWidth = GlobalConstants.WINDOW_MIN_WIDTH;
        }
    }
}
