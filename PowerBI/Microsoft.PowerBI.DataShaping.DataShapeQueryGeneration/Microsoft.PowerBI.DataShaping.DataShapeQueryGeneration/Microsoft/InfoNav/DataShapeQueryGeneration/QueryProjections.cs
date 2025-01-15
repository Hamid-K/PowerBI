using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000BC RID: 188
	internal sealed class QueryProjections
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00019490 File Offset: 0x00017690
		internal QueryProjections(IReadOnlyList<QueryMember> primaryMembers, IReadOnlyList<QueryMember> secondaryMembers, IReadOnlyList<ProjectedDsqExpression> measures, IReadOnlyList<ProjectedDsqExpression> dataShapeProjections, IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> projectionFiltersBySelectIndex, bool hasSortByMeasure)
		{
			this.PrimaryMembers = primaryMembers;
			this.SecondaryMembers = secondaryMembers;
			this.Measures = measures;
			this.DataShapeProjections = dataShapeProjections;
			this.ProjectionFiltersBySelectIndex = projectionFiltersBySelectIndex;
			this.HasSortByMeasure = hasSortByMeasure;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x000194C5 File Offset: 0x000176C5
		internal IReadOnlyList<QueryMember> PrimaryMembers { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x000194CD File Offset: 0x000176CD
		internal IReadOnlyList<QueryMember> SecondaryMembers { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x000194D5 File Offset: 0x000176D5
		internal IReadOnlyList<ProjectedDsqExpression> Measures { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x000194DD File Offset: 0x000176DD
		internal IReadOnlyList<ProjectedDsqExpression> DataShapeProjections { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000194E5 File Offset: 0x000176E5
		internal bool HasGroups
		{
			get
			{
				return this.PrimaryMembers.Count > 0 || this.SecondaryMembers.Count > 0;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00019505 File Offset: 0x00017705
		internal bool HasSortByMeasure { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001950D File Offset: 0x0001770D
		internal IReadOnlyDictionary<int, HashSet<ResolvedQueryFilter>> ProjectionFiltersBySelectIndex { get; }

		// Token: 0x060006C6 RID: 1734 RVA: 0x00019515 File Offset: 0x00017715
		internal QueryProjections CloneWithOverrides(IReadOnlyList<QueryMember> primaryMembers = null, IReadOnlyList<QueryMember> secondaryMembers = null, IReadOnlyList<ProjectedDsqExpression> measures = null, IReadOnlyList<ProjectedDsqExpression> dataShapeProjections = null)
		{
			return new QueryProjections(primaryMembers ?? this.PrimaryMembers, secondaryMembers ?? this.SecondaryMembers, measures ?? this.Measures, dataShapeProjections ?? this.DataShapeProjections, this.ProjectionFiltersBySelectIndex, this.HasSortByMeasure);
		}
	}
}
