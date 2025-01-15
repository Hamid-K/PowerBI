using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses
{
	// Token: 0x0200043A RID: 1082
	[Serializable]
	public sealed class RetryConfiguration : ConfigurationClass
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x0007D019 File Offset: 0x0007B219
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x0007D021 File Offset: 0x0007B221
		[ConfigurationProperty]
		public int MaxRetries { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0007D02A File Offset: 0x0007B22A
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x0007D032 File Offset: 0x0007B232
		[ConfigurationProperty]
		public TimeSpan RetryInterval { get; set; }
	}
}
