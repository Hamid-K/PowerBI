using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000006 RID: 6
	internal abstract class ClientOperationsSupportProvider
	{
		// Token: 0x06000039 RID: 57
		public abstract bool AreRegionsSupported();

		// Token: 0x0600003A RID: 58
		public abstract bool AreNotificationsSupported(CacheEventType cacheEventType);

		// Token: 0x0400001A RID: 26
		public static readonly ClientOperationsSupportProvider OnPremise = new ClientOperationsSupportProvider.DefaultOperationsSupportProvider();

		// Token: 0x0400001B RID: 27
		public static readonly ClientOperationsSupportProvider VAS = new ClientOperationsSupportProvider.VasOperationsSupportProvider();

		// Token: 0x02000007 RID: 7
		private sealed class DefaultOperationsSupportProvider : ClientOperationsSupportProvider
		{
			// Token: 0x0600003D RID: 61 RVA: 0x00002B16 File Offset: 0x00000D16
			public override bool AreRegionsSupported()
			{
				return true;
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00002B16 File Offset: 0x00000D16
			public override bool AreNotificationsSupported(CacheEventType cacheEventType)
			{
				return true;
			}
		}

		// Token: 0x02000008 RID: 8
		private sealed class VasOperationsSupportProvider : ClientOperationsSupportProvider
		{
			// Token: 0x06000040 RID: 64 RVA: 0x00002B21 File Offset: 0x00000D21
			public override bool AreRegionsSupported()
			{
				return ConfigManager.IsTestingMode;
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00002B28 File Offset: 0x00000D28
			public override bool AreNotificationsSupported(CacheEventType cacheEventTypes)
			{
				return (cacheEventTypes & CacheEventType.AllCacheEvents) == CacheEventType.AllCacheEvents || (cacheEventTypes & CacheEventType.NotificationMissEvent) == CacheEventType.NotificationMissEvent;
			}
		}
	}
}
