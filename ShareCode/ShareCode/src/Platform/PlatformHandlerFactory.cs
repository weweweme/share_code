﻿namespace ShareCode.Platform
{
    public static class PlatformHandlerFactory
    {
        public static PlatformHandler GetPlatformHandler()
        {
            if (OperatingSystem.IsMacCatalyst())
            {
                return new MacOSPlatformHandler();
            }
            else if (OperatingSystem.IsWindows())
            {
                return new WindowsPlatformHandler();
            }
            else
            {
                throw new PlatformNotSupportedException("지원하지 않는 플랫폼입니다.");
            }
        }
    }
}
