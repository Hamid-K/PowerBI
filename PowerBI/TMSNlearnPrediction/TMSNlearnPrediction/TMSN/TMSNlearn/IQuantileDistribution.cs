using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004B8 RID: 1208
	public interface IQuantileDistribution<TResult> : ISampleableDistribution<TResult>, IDistribution<TResult>
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060018E5 RID: 6373
		TResult Median { get; }

		// Token: 0x060018E6 RID: 6374
		TResult GetQuantile(float p);
	}
}
