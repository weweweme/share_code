namespace ShareCode.src.Platform
{
    public abstract class PlatformHandler
    {
        public abstract Task OnOpenButtonClicked(object? sender, EventArgs e);
        public abstract Task OnExportButtonClicked(object? sender, EventArgs e);
    }
}
