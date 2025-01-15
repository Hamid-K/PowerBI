using System;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004B5 RID: 1205
	public interface IDistribution<out TResult>
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060018DF RID: 6367
		TResult Minimum { get; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060018E0 RID: 6368
		TResult Maximum { get; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060018E1 RID: 6369
		TResult Mean { get; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060018E2 RID: 6370
		TResult StandardDeviation { get; }
	}
}
