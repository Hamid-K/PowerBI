using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public sealed class DatabaseServerLogin : EncryptedConfigurationClass
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004615 File Offset: 0x00002815
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000461D File Offset: 0x0000281D
		[ItemPathComponent]
		[ConfigurationProperty]
		public string RoleName { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004626 File Offset: 0x00002826
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000462E File Offset: 0x0000282E
		[ConfigurationProperty]
		public string UserName { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004637 File Offset: 0x00002837
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000463F File Offset: 0x0000283F
		[ConfigurationProperty(Encrypt = true)]
		public string Password { get; set; }
	}
}
