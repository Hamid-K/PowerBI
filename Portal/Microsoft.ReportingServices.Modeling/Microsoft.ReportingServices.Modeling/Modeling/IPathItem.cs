using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000AC RID: 172
	public interface IPathItem
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000936 RID: 2358
		Cardinality Cardinality { get; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000937 RID: 2359
		Optionality Optionality { get; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000938 RID: 2360
		Cardinality ReverseCardinality { get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000939 RID: 2361
		Optionality ReverseOptionality { get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600093A RID: 2362
		IQueryEntity TargetEntity { get; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600093B RID: 2363
		IQueryEntity SourceEntity { get; }
	}
}
