using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Resources
{
	// Token: 0x02000458 RID: 1112
	[Serializable]
	public sealed class ResourceThrottlerConfiguration : ConfigurationClass
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x0007E448 File Offset: 0x0007C648
		// (set) Token: 0x060022AE RID: 8878 RVA: 0x0007E450 File Offset: 0x0007C650
		[ConfigurationProperty]
		public int MaxThrottledConcurrentOperations { get; set; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0007E459 File Offset: 0x0007C659
		// (set) Token: 0x060022B0 RID: 8880 RVA: 0x0007E461 File Offset: 0x0007C661
		[ConfigurationProperty]
		public int MaxThrottledPendingOperations { get; set; }
	}
}
