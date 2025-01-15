using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public sealed class DatabaseConnectionStringConfiguration : EncryptedConfigurationClass
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000045EB File Offset: 0x000027EB
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000045F3 File Offset: 0x000027F3
		[ConfigurationProperty(Encrypt = true)]
		public string ConnectionString { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000045FC File Offset: 0x000027FC
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004604 File Offset: 0x00002804
		[ConfigurationProperty]
		public StorageOperationMode OperationMode { get; set; }
	}
}
