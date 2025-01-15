using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000244 RID: 580
	internal sealed class DataMemberAnnotation
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x0004D192 File Offset: 0x0004B392
		internal DataMemberAnnotation(bool isPrimaryMember, int leafIndex, bool areContentsIncludedInOutput, SortByMeasureInfoCollection sortByMeasureInfos, IReadOnlyList<global::System.ValueTuple<Limit, bool>> limits)
		{
			this.m_isPrimaryMember = isPrimaryMember;
			this.m_leafIndex = leafIndex;
			this.AreContentsIncludedInOutput = areContentsIncludedInOutput;
			this.m_sortByMeasureInfos = sortByMeasureInfos;
			this.m_limits = limits;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0004D1BF File Offset: 0x0004B3BF
		public bool IsPrimaryMember
		{
			get
			{
				return this.m_isPrimaryMember;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0004D1C7 File Offset: 0x0004B3C7
		public int LeafIndex
		{
			get
			{
				return this.m_leafIndex;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0004D1CF File Offset: 0x0004B3CF
		public bool AreContentsIncludedInOutput { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0004D1D7 File Offset: 0x0004B3D7
		public SortByMeasureInfoCollection SortByMeasureInfos
		{
			get
			{
				return this.m_sortByMeasureInfos;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0004D1DF File Offset: 0x0004B3DF
		public bool HasSortOnMeasureKeys
		{
			get
			{
				return this.m_sortByMeasureInfos != null && this.m_sortByMeasureInfos.Count > 0;
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004D1FC File Offset: 0x0004B3FC
		public Limit GetLimit()
		{
			if (this.m_limits != null)
			{
				int num = this.m_limits.Count - 1;
				return this.m_limits[num].Item1;
			}
			return null;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004D232 File Offset: 0x0004B432
		public Limit GetLimitWithInnermostTarget()
		{
			if (this.m_limits != null)
			{
				return this.m_limits.LastOrDefault(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Limit", "IsInnermostTarget" })] global::System.ValueTuple<Limit, bool> l) => l.Item2).Item1;
			}
			return null;
		}

		// Token: 0x040008BE RID: 2238
		private readonly bool m_isPrimaryMember;

		// Token: 0x040008BF RID: 2239
		private readonly int m_leafIndex;

		// Token: 0x040008C0 RID: 2240
		private readonly SortByMeasureInfoCollection m_sortByMeasureInfos;

		// Token: 0x040008C1 RID: 2241
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Limit", "IsInnermostTarget" })]
		private readonly IReadOnlyList<global::System.ValueTuple<Limit, bool>> m_limits;
	}
}
