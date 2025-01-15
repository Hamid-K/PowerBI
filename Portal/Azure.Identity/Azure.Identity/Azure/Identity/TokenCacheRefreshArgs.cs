using System;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000085 RID: 133
	public class TokenCacheRefreshArgs
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000DB95 File Offset: 0x0000BD95
		public string SuggestedCacheKey { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000DB9D File Offset: 0x0000BD9D
		public bool IsCaeEnabled { get; }

		// Token: 0x06000473 RID: 1139 RVA: 0x0000DBA5 File Offset: 0x0000BDA5
		internal TokenCacheRefreshArgs(TokenCacheNotificationArgs args, bool enableCae)
		{
			this.SuggestedCacheKey = args.SuggestedCacheKey;
			this.IsCaeEnabled = enableCae;
		}
	}
}
