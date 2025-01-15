using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x0200009D RID: 157
	public sealed class ResolvedDataReduction
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x0000B4B5 File Offset: 0x000096B5
		public ResolvedDataReduction(int? dataVolume, ResolvedDataReductionLimit primary, ResolvedDataReductionLimit secondary, ResolvedDataReductionLimit intersection, IReadOnlyList<ResolvedScopedDataReduction> scoped)
		{
			this.DataVolume = dataVolume;
			this.Primary = primary;
			this.Secondary = secondary;
			this.Intersection = intersection;
			this.Scoped = scoped;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000B4E2 File Offset: 0x000096E2
		public int? DataVolume { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000B4EA File Offset: 0x000096EA
		public ResolvedDataReductionLimit Primary { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000B4F2 File Offset: 0x000096F2
		public ResolvedDataReductionLimit Secondary { get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000B4FA File Offset: 0x000096FA
		public ResolvedDataReductionLimit Intersection { get; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000B502 File Offset: 0x00009702
		public IReadOnlyList<ResolvedScopedDataReduction> Scoped { get; }
	}
}
