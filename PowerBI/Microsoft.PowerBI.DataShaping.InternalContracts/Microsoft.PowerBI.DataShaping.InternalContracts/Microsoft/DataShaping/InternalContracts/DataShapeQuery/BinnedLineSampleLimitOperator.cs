using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000AA RID: 170
	internal sealed class BinnedLineSampleLimitOperator : LimitOperator
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00007627 File Offset: 0x00005827
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000762F File Offset: 0x0000582F
		public Candidate<int> MinPointsPerSeries { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00007638 File Offset: 0x00005838
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00007640 File Offset: 0x00005840
		public Candidate<int> MaxPointsPerSeries { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00007649 File Offset: 0x00005849
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x00007651 File Offset: 0x00005851
		public Candidate<int> MaxDynamicSeriesCount { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000765A File Offset: 0x0000585A
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00007662 File Offset: 0x00005862
		public Expression PrimaryScalarKey { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000766B File Offset: 0x0000586B
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00007673 File Offset: 0x00005873
		public List<Expression> Measures { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000767C File Offset: 0x0000587C
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.BinnedLineSampleLimitOperator;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000767F File Offset: 0x0000587F
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
