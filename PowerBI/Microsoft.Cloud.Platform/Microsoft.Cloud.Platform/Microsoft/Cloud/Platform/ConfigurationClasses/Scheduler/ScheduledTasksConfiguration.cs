using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Scheduler
{
	// Token: 0x0200043D RID: 1085
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class ScheduledTasksConfiguration : ConfigurationClass
	{
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0007D1B1 File Offset: 0x0007B3B1
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x0007D1B9 File Offset: 0x0007B3B9
		[ConfigurationProperty]
		public ConfigurationCollection<ScheduledTaskSettings> Settings { get; set; }
	}
}
