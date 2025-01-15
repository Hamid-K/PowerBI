using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000281 RID: 641
	internal sealed class TopNPerLevelWindowExpansionBuilder
	{
		// Token: 0x06001B7C RID: 7036 RVA: 0x0004D14C File Offset: 0x0004B34C
		public QueryTopNPerLevelWindowExpansion Build()
		{
			if (this._values.IsNullOrEmpty<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>() && this._windowValues.IsNullOrEmpty<QueryTopNPerLevelWindowExpansionValue>() && this._childBuilders.IsNullOrEmpty<TopNPerLevelWindowExpansionBuilder>())
			{
				return null;
			}
			List<QueryTopNPerLevelWindowExpansion> list = null;
			if (!this._childBuilders.IsNullOrEmpty<TopNPerLevelWindowExpansionBuilder>())
			{
				list = new List<QueryTopNPerLevelWindowExpansion>(this._childBuilders.Count);
				foreach (TopNPerLevelWindowExpansionBuilder topNPerLevelWindowExpansionBuilder in this._childBuilders)
				{
					list.Add(topNPerLevelWindowExpansionBuilder.Build());
				}
			}
			return new QueryTopNPerLevelWindowExpansion(this._values, this._windowValues, list);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0004D200 File Offset: 0x0004B400
		public TopNPerLevelWindowExpansionBuilder WithValues(IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> values)
		{
			Util.AddToLazyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(ref this._values, values);
			return this;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0004D20F File Offset: 0x0004B40F
		public TopNPerLevelWindowExpansionBuilder WithValues(params Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] values)
		{
			Util.AddToLazyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(ref this._values, values);
			return this;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0004D220 File Offset: 0x0004B420
		public TopNPerLevelWindowExpansionBuilder WithWindow(IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> windowValues, WindowKind windowKind)
		{
			QueryTopNPerLevelWindowExpansionValue queryTopNPerLevelWindowExpansionValue = new QueryTopNPerLevelWindowExpansionValue(windowValues, windowKind);
			Util.AddToLazyList<QueryTopNPerLevelWindowExpansionValue>(ref this._windowValues, queryTopNPerLevelWindowExpansionValue);
			return this;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0004D244 File Offset: 0x0004B444
		public TopNPerLevelWindowExpansionBuilder WithWindow(WindowKind windowKind, params Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] windowValues)
		{
			QueryTopNPerLevelWindowExpansionValue queryTopNPerLevelWindowExpansionValue = new QueryTopNPerLevelWindowExpansionValue(windowValues, windowKind);
			Util.AddToLazyList<QueryTopNPerLevelWindowExpansionValue>(ref this._windowValues, queryTopNPerLevelWindowExpansionValue);
			return this;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0004D268 File Offset: 0x0004B468
		public TopNPerLevelWindowExpansionBuilder WithChild()
		{
			TopNPerLevelWindowExpansionBuilder topNPerLevelWindowExpansionBuilder = new TopNPerLevelWindowExpansionBuilder();
			Util.AddToLazyList<TopNPerLevelWindowExpansionBuilder>(ref this._childBuilders, topNPerLevelWindowExpansionBuilder);
			return topNPerLevelWindowExpansionBuilder;
		}

		// Token: 0x04000F09 RID: 3849
		private List<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> _values;

		// Token: 0x04000F0A RID: 3850
		private List<QueryTopNPerLevelWindowExpansionValue> _windowValues;

		// Token: 0x04000F0B RID: 3851
		private List<TopNPerLevelWindowExpansionBuilder> _childBuilders;
	}
}
