using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000A0 RID: 160
	public sealed class ResolvedDataReductionDataWindow : ResolvedDataReductionLimit
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000B576 File Offset: 0x00009776
		public ResolvedDataReductionDataWindow(int? count, IReadOnlyList<IReadOnlyList<PrimitiveValue>> restartTokens, RestartMatchingBehavior? restartMatchingBehavior)
		{
			this.Count = count;
			this.RestartTokens = restartTokens;
			this.RestartMatchingBehavior = restartMatchingBehavior;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000B593 File Offset: 0x00009793
		public int? Count { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000B59B File Offset: 0x0000979B
		public IReadOnlyList<IReadOnlyList<PrimitiveValue>> RestartTokens { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000B5A3 File Offset: 0x000097A3
		public RestartMatchingBehavior? RestartMatchingBehavior { get; }
	}
}
