using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public sealed class DatabaseRetriesProfile : ConfigurationClass
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000456C File Offset: 0x0000276C
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004574 File Offset: 0x00002774
		[ConfigurationProperty]
		public DatabaseRetryProfile QueryRetryProfile { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000457D File Offset: 0x0000277D
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004585 File Offset: 0x00002785
		[ConfigurationProperty]
		public DatabaseRetryProfile ConnectionRetryProfile { get; set; }
	}
}
