namespace ShareCode.src.Platform
{
    public class MacOSPlatformHandler : PlatformHandler
    {
        public override async Task OnOpenButtonClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("macOS: Open Button Clicked");
            // macOS 전용 로직 추가
            await Task.CompletedTask;
        }

        public override async Task OnExportButtonClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("macOS: Export Button Clicked");
            // macOS 전용 로직 추가
            await Task.CompletedTask;
        }
    }
}
