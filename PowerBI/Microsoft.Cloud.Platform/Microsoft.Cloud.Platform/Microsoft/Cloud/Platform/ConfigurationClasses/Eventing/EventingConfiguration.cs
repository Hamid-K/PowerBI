using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Eventing
{
	// Token: 0x0200044A RID: 1098
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class EventingConfiguration : ConfigurationClass
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x0007DEF7 File Offset: 0x0007C0F7
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x0007DEFF File Offset: 0x0007C0FF
		[ConfigurationProperty]
		public EnabledEventTypesConfiguration EnabledEventTypesConfiguration { get; set; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x0007DF08 File Offset: 0x0007C108
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x0007DF10 File Offset: 0x0007C110
		[ConfigurationProperty]
		public ConfigurationCollection<SinkConfiguration> EnabledSinksConfiguration { get; set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x0007DF19 File Offset: 0x0007C119
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x0007DF21 File Offset: 0x0007C121
		[ConfigurationProperty]
		public int MaxMillisecondsBetweenBatches { get; set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x0007DF2A File Offset: 0x0007C12A
		// (set) Token: 0x06002231 RID: 8753 RVA: 0x0007DF32 File Offset: 0x0007C132
		[ConfigurationProperty]
		public int LoadModeHighWatermark { get; set; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x0007DF3B File Offset: 0x0007C13B
		// (set) Token: 0x06002233 RID: 8755 RVA: 0x0007DF43 File Offset: 0x0007C143
		[ConfigurationProperty]
		public int LoadModeLowWatermark { get; set; }
	}
}
