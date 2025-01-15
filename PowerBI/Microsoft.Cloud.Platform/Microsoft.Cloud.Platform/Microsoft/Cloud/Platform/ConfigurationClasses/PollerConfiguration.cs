using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses
{
	// Token: 0x02000439 RID: 1081
	[Serializable]
	public sealed class PollerConfiguration : ConfigurationClass
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0007CFF7 File Offset: 0x0007B1F7
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x0007CFFF File Offset: 0x0007B1FF
		[ConfigurationProperty]
		public TimeSpan DelayInterval { get; set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0007D008 File Offset: 0x0007B208
		// (set) Token: 0x06002195 RID: 8597 RVA: 0x0007D010 File Offset: 0x0007B210
		[ConfigurationProperty]
		public TimeSpan Timeout { get; set; }
	}
}
