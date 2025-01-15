using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x0200044F RID: 1103
	[Serializable]
	public sealed class ContractKnownTypesConfiguration : ConfigurationClass
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x0007E03A File Offset: 0x0007C23A
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x0007E042 File Offset: 0x0007C242
		[ConfigurationProperty]
		public string Contract { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x0007E04B File Offset: 0x0007C24B
		// (set) Token: 0x0600224A RID: 8778 RVA: 0x0007E053 File Offset: 0x0007C253
		[ConfigurationProperty]
		public ConfigurationCollection<TypeIdentifier> KnownTypes { get; set; }
	}
}
