using System;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.AuthScheme.PoP
{
	// Token: 0x020002C9 RID: 713
	internal static class PoPProviderFactory
	{
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00056BC5 File Offset: 0x00054DC5
		public static TimeSpan KeyRotationInterval { get; } = TimeSpan.FromHours(8.0);

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x00056BCC File Offset: 0x00054DCC
		// (set) Token: 0x06001AAB RID: 6827 RVA: 0x00056BD3 File Offset: 0x00054DD3
		internal static ITimeService TimeService { get; set; } = new TimeService();

		// Token: 0x06001AAC RID: 6828 RVA: 0x00056BDC File Offset: 0x00054DDC
		public static InMemoryCryptoProvider GetOrCreateProvider()
		{
			object obj = PoPProviderFactory.s_lock;
			InMemoryCryptoProvider inMemoryCryptoProvider;
			lock (obj)
			{
				DateTime utcNow = PoPProviderFactory.TimeService.GetUtcNow();
				if (PoPProviderFactory.s_currentProvider != null && PoPProviderFactory.s_providerExpiration > utcNow)
				{
					inMemoryCryptoProvider = PoPProviderFactory.s_currentProvider;
				}
				else
				{
					PoPProviderFactory.s_currentProvider = new InMemoryCryptoProvider();
					PoPProviderFactory.s_providerExpiration = PoPProviderFactory.TimeService.GetUtcNow() + PoPProviderFactory.KeyRotationInterval;
					inMemoryCryptoProvider = PoPProviderFactory.s_currentProvider;
				}
			}
			return inMemoryCryptoProvider;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00056C68 File Offset: 0x00054E68
		public static void Reset()
		{
			PoPProviderFactory.s_currentProvider = null;
			PoPProviderFactory.TimeService = new TimeService();
		}

		// Token: 0x04000C26 RID: 3110
		private static InMemoryCryptoProvider s_currentProvider;

		// Token: 0x04000C27 RID: 3111
		private static DateTime s_providerExpiration;

		// Token: 0x04000C29 RID: 3113
		private static object s_lock = new object();
	}
}
