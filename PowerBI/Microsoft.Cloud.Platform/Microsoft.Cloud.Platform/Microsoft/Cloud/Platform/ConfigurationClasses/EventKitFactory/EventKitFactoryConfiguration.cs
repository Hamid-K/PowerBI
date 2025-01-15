using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.EventKitFactory
{
	// Token: 0x02000448 RID: 1096
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.None)]
	[Serializable]
	public sealed class EventKitFactoryConfiguration : ConfigurationClass
	{
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x0007DDB4 File Offset: 0x0007BFB4
		// (set) Token: 0x06002203 RID: 8707 RVA: 0x0007DDBC File Offset: 0x0007BFBC
		[ConfigurationProperty]
		public string EventLogSourceName { get; set; }
	}
}
