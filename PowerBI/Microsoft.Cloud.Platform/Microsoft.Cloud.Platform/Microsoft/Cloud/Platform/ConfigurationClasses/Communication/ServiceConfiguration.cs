using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Communication
{
	// Token: 0x02000450 RID: 1104
	[Serializable]
	public sealed class ServiceConfiguration : ConfigurationClass
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0007E05C File Offset: 0x0007C25C
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x0007E064 File Offset: 0x0007C264
		[ConfigurationProperty]
		public string Name { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0007E06D File Offset: 0x0007C26D
		// (set) Token: 0x0600224F RID: 8783 RVA: 0x0007E075 File Offset: 0x0007C275
		[ConfigurationProperty]
		[NullConfigurationProperty]
		public ConsumerConfiguration ConsumerConfiguration { get; set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0007E07E File Offset: 0x0007C27E
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x0007E086 File Offset: 0x0007C286
		[ConfigurationProperty]
		[NullConfigurationProperty]
		public ProviderConfiguration ProviderConfiguration { get; set; }
	}
}
