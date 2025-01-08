using System.Diagnostics;
using UIKit;

namespace ShareCode.src.Platform
{
    public class MacOSPlatformHandler : PlatformHandler
    {
        public override void ConfigureWindow()
        {
            if (UIApplication.SharedApplication.ConnectedScenes.FirstOrDefault<object>() is not UIWindowScene windowScene) return;
            Debug.Assert(windowScene.SizeRestrictions != null);
            
            windowScene.SizeRestrictions.MinimumSize = new CoreGraphics.CGSize(
                GlobalConstants.WINDOW_MIN_WIDTH, 
                GlobalConstants.WINDOW_MIN_HEIGHT);
            windowScene.SizeRestrictions.MaximumSize = new CoreGraphics.CGSize(
                GlobalConstants.WINDOW_WIDTH, 
                GlobalConstants.WINDOW_HEIGHT);
        }
    }
}
