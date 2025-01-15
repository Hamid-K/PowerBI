using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A3 RID: 163
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ServicePointHelpers
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
		public static void SetLimits(ServicePoint requestServicePoint)
		{
			try
			{
				if (requestServicePoint.ConnectionLimit == 2)
				{
					requestServicePoint.ConnectionLimit = 50;
				}
				if (requestServicePoint.ConnectionLeaseTimeout == -1)
				{
					requestServicePoint.ConnectionLeaseTimeout = 300000;
				}
			}
			catch (NotImplementedException)
			{
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000FC30 File Offset: 0x0000DE30
		public static void SetLimits(HttpMessageHandler messageHandler)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
			{
				return;
			}
			try
			{
				HttpClientHandler httpClientHandler = messageHandler as HttpClientHandler;
				if (httpClientHandler != null && httpClientHandler.MaxConnectionsPerServer == 2)
				{
					httpClientHandler.MaxConnectionsPerServer = 50;
				}
			}
			catch (NotSupportedException)
			{
			}
			catch (NotImplementedException)
			{
			}
		}

		// Token: 0x04000219 RID: 537
		private const int RuntimeDefaultConnectionLimit = 2;

		// Token: 0x0400021A RID: 538
		private const int IncreasedConnectionLimit = 50;

		// Token: 0x0400021B RID: 539
		private const int IncreasedConnectionLeaseTimeout = 300000;

		// Token: 0x0400021C RID: 540
		private static TimeSpan DefaultConnectionLeaseTimeoutTimeSpan = Timeout.InfiniteTimeSpan;

		// Token: 0x0400021D RID: 541
		private static TimeSpan IncreasedConnectionLeaseTimeoutTimeSpan = TimeSpan.FromMilliseconds(300000.0);

		// Token: 0x0400021E RID: 542
		private const int DefaultConnectionLeaseTimeout = -1;
	}
}
