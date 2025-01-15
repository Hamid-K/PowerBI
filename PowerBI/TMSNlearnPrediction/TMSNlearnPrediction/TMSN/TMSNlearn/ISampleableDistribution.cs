using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004B7 RID: 1207
	public interface ISampleableDistribution<TResult> : IDistribution<TResult>
	{
		// Token: 0x060018E4 RID: 6372
		TResult[] GetSupportSample(out TResult[] weights);
	}
}
