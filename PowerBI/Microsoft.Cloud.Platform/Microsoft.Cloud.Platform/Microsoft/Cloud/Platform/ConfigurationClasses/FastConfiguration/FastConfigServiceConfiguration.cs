using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.FastConfiguration
{
	// Token: 0x02000459 RID: 1113
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class FastConfigServiceConfiguration : ConfigurationClass
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0007E46A File Offset: 0x0007C66A
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x0007E472 File Offset: 0x0007C672
		[ConfigurationProperty]
		public bool EnablePolling { get; set; }
	}
}
