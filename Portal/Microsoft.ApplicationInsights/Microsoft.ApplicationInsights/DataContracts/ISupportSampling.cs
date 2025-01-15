using System;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D4 RID: 212
	public interface ISupportSampling
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000745 RID: 1861
		// (set) Token: 0x06000746 RID: 1862
		double? SamplingPercentage { get; set; }
	}
}
