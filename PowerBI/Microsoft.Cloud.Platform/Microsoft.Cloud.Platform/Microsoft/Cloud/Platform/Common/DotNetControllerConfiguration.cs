using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000518 RID: 1304
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class DotNetControllerConfiguration : ConfigurationClass
	{
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x00091F0A File Offset: 0x0009010A
		// (set) Token: 0x06002868 RID: 10344 RVA: 0x00091F12 File Offset: 0x00090112
		[ConfigurationProperty]
		public ThreadPoolRangeConfiguration WorkerRange { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x00091F1B File Offset: 0x0009011B
		// (set) Token: 0x0600286A RID: 10346 RVA: 0x00091F23 File Offset: 0x00090123
		[ConfigurationProperty]
		public ThreadPoolRangeConfiguration IoRange { get; set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x00091F2C File Offset: 0x0009012C
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x00091F34 File Offset: 0x00090134
		[ConfigurationProperty]
		public int ServicePointConnections { get; set; }
	}
}
