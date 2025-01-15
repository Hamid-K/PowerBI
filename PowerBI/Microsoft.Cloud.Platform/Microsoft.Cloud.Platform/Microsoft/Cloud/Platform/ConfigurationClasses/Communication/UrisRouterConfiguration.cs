using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x02000456 RID: 1110
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.None)]
	[Serializable]
	public sealed class UrisRouterConfiguration : ConfigurationClass
	{
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x0007E3F3 File Offset: 0x0007C5F3
		// (set) Token: 0x060022A2 RID: 8866 RVA: 0x0007E3FB File Offset: 0x0007C5FB
		[ConfigurationProperty]
		public ConfigurationCollection<UrisRouterSectionConfiguration> Routers { get; set; }
	}
}
