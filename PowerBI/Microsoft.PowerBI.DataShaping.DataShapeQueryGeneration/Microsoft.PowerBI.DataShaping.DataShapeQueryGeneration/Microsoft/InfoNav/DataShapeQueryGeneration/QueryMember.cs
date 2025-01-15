using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B6 RID: 182
	internal sealed class QueryMember
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x00018FC0 File Offset: 0x000171C0
		internal QueryMember(QueryGroup group, IReadOnlyList<QueryGroupValue> values, IReadOnlyList<ProjectedDsqExpression> measureCalcs, IReadOnlyList<ResolvedQueryExpression> columnProjectionExpressions, IReadOnlyList<FilterDefinition> instanceFilters, bool hasExplicitSubtotal, bool isContextOnly)
		{
			this.Group = group;
			this.Values = values;
			this.MeasureCalculations = measureCalcs;
			this.ColumnProjectionExpressions = columnProjectionExpressions;
			this.InstanceFilters = instanceFilters;
			this.HasExplicitSubtotal = hasExplicitSubtotal;
			this.IsContextOnly = isContextOnly;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00018FFD File Offset: 0x000171FD
		internal QueryGroup Group { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00019005 File Offset: 0x00017205
		internal IReadOnlyList<QueryGroupValue> Values { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001900D File Offset: 0x0001720D
		internal IReadOnlyList<ProjectedDsqExpression> MeasureCalculations { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00019015 File Offset: 0x00017215
		internal IReadOnlyList<ResolvedQueryExpression> ColumnProjectionExpressions { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001901D File Offset: 0x0001721D
		internal IReadOnlyList<FilterDefinition> InstanceFilters { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00019025 File Offset: 0x00017225
		public bool HasExplicitSubtotal { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001902D File Offset: 0x0001722D
		internal bool IsContextOnly { get; }

		// Token: 0x0600069D RID: 1693 RVA: 0x00019035 File Offset: 0x00017235
		internal QueryMember CloneWithOverrides(QueryGroup group = null, IReadOnlyList<QueryGroupValue> values = null, IReadOnlyList<ProjectedDsqExpression> measureCalcs = null)
		{
			return new QueryMember(group ?? this.Group, values ?? values, measureCalcs ?? measureCalcs, this.ColumnProjectionExpressions, this.InstanceFilters, this.HasExplicitSubtotal, this.IsContextOnly);
		}
	}
}
