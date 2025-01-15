using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000519 RID: 1305
	[Serializable]
	public sealed class ThreadPoolRangeConfiguration : ConfigurationClass
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600286E RID: 10350 RVA: 0x00091F3D File Offset: 0x0009013D
		// (set) Token: 0x0600286F RID: 10351 RVA: 0x00091F45 File Offset: 0x00090145
		[ConfigurationProperty]
		public int Min { get; set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x00091F4E File Offset: 0x0009014E
		// (set) Token: 0x06002871 RID: 10353 RVA: 0x00091F56 File Offset: 0x00090156
		[ConfigurationProperty]
		public int Max { get; set; }
	}
}
