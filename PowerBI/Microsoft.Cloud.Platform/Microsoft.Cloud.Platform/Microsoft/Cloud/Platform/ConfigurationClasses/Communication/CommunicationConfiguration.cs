using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x0200044E RID: 1102
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.None)]
	[Serializable]
	public sealed class CommunicationConfiguration : ConfigurationClass
	{
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x0007E007 File Offset: 0x0007C207
		// (set) Token: 0x06002241 RID: 8769 RVA: 0x0007E00F File Offset: 0x0007C20F
		[ConfigurationProperty]
		public ConfigurationCollection<ServiceConfiguration> Services { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0007E018 File Offset: 0x0007C218
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x0007E020 File Offset: 0x0007C220
		[ConfigurationProperty]
		public ConfigurationCollection<ContractKnownTypesConfiguration> ContractKnownTypes { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x0007E029 File Offset: 0x0007C229
		// (set) Token: 0x06002245 RID: 8773 RVA: 0x0007E031 File Offset: 0x0007C231
		[ConfigurationProperty]
		public ConfigurationCollection<TypeIdentifier> KnownExceptions { get; set; }
	}
}
