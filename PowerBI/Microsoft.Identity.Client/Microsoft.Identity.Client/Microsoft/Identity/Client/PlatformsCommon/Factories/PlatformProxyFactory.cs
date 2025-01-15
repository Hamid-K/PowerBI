using System;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Logger;
using Microsoft.Identity.Client.Platforms.netdesktop;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client.PlatformsCommon.Factories
{
	// Token: 0x02000200 RID: 512
	internal static class PlatformProxyFactory
	{
		// Token: 0x060015B3 RID: 5555 RVA: 0x00047D7C File Offset: 0x00045F7C
		public static IPlatformProxy CreatePlatformProxy(ILoggerAdapter logger)
		{
			return new NetDesktopPlatformProxy(logger ?? LoggerHelper.NullLogger);
		}
	}
}
