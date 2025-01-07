namespace ShareCode.src.Platform
{
    public class WindowsPlatformHandler : PlatformHandler
    {
        public override async Task OnOpenButtonClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("Windows: Open Button Clicked");
            // Windows 전용 로직 추가
            await Task.CompletedTask;
        }

        public override async Task OnExportButtonClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("Windows: Export Button Clicked");
            // Windows 전용 로직 추가
            await Task.CompletedTask;
        }
    }
}
