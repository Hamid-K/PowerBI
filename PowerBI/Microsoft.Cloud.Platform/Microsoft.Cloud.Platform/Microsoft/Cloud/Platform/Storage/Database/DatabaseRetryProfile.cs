using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public sealed class DatabaseRetryProfile : ConfigurationClass
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004596 File Offset: 0x00002796
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000459E File Offset: 0x0000279E
		[ConfigurationProperty]
		public int RetryCount { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000045A7 File Offset: 0x000027A7
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000045AF File Offset: 0x000027AF
		[ConfigurationProperty]
		public int IntervalInMilliseconds { get; set; }
	}
}
