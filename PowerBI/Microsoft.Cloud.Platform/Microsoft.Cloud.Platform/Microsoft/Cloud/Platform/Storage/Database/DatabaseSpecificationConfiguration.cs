using System;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.ConfigurationClasses.Resources;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public sealed class DatabaseSpecificationConfiguration : EncryptedConfigurationClass
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004506 File Offset: 0x00002706
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000450E File Offset: 0x0000270E
		[ConfigurationProperty]
		public string Key { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004517 File Offset: 0x00002717
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000451F File Offset: 0x0000271F
		[ConfigurationProperty]
		public DatabaseStorageUnitConfiguration Unit { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004528 File Offset: 0x00002728
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004530 File Offset: 0x00002730
		[ConfigurationProperty]
		public int CommandTimeout { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004539 File Offset: 0x00002739
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004541 File Offset: 0x00002741
		[ConfigurationProperty]
		public DatabaseRetriesProfile RetryProfiles { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000454A File Offset: 0x0000274A
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004552 File Offset: 0x00002752
		[ConfigurationProperty]
		public ResourceThrottlerConfiguration ThrottlerConfiguration { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000455B File Offset: 0x0000275B
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004563 File Offset: 0x00002763
		[ConfigurationProperty]
		public int BulkInsertBatchSize { get; set; }
	}
}
