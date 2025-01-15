using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public sealed class DatabaseStorageUnitConfiguration : EncryptedConfigurationClass
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000045B8 File Offset: 0x000027B8
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000045C0 File Offset: 0x000027C0
		[ConfigurationProperty]
		public string DatabaseName { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000045C9 File Offset: 0x000027C9
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000045D1 File Offset: 0x000027D1
		[ConfigurationProperty]
		public DatabaseConnectionStringConfiguration Primary { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000045DA File Offset: 0x000027DA
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000045E2 File Offset: 0x000027E2
		[ConfigurationProperty]
		public DatabaseConnectionStringConfiguration Secondary { get; set; }
	}
}
