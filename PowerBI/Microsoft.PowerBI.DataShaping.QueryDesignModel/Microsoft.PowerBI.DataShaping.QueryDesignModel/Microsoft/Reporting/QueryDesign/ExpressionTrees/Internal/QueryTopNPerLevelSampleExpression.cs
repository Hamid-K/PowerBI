using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C0 RID: 448
	internal sealed class QueryTopNPerLevelSampleExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001647 RID: 5703 RVA: 0x0003DC28 File Offset: 0x0003BE28
		internal QueryTopNPerLevelSampleExpression(ConceptualResultType conceptualResultType, QueryExpressionBinding input, IReadOnlyList<TopNPerLevelLevelRow> levels, QueryExpression count, string restartIndicatorColumnName, QueryTopNPerLevelWindowExpansion windowExpansion)
			: base(conceptualResultType)
		{
			this.Input = input;
			this.Levels = levels;
			this.Count = count;
			this.RestartIndicatorColumnName = restartIndicatorColumnName;
			this.WindowExpansion = windowExpansion;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0003DC57 File Offset: 0x0003BE57
		public QueryExpression Count { get; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0003DC5F File Offset: 0x0003BE5F
		public QueryExpressionBinding Input { get; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0003DC67 File Offset: 0x0003BE67
		public IReadOnlyList<TopNPerLevelLevelRow> Levels { get; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0003DC6F File Offset: 0x0003BE6F
		public string RestartIndicatorColumnName { get; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0003DC77 File Offset: 0x0003BE77
		public QueryTopNPerLevelWindowExpansion WindowExpansion { get; }

		// Token: 0x0600164D RID: 5709 RVA: 0x0003DC7F File Offset: 0x0003BE7F
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0003DC94 File Offset: 0x0003BE94
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryTopNPerLevelSampleExpression queryTopNPerLevelSampleExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryTopNPerLevelSampleExpression>(this, other, out flag, out queryTopNPerLevelSampleExpression))
			{
				return flag;
			}
			return this.Count == queryTopNPerLevelSampleExpression.Count && this.Input.Equals(queryTopNPerLevelSampleExpression.Input) && this.Levels.Equals(queryTopNPerLevelSampleExpression.Levels) && this.RestartIndicatorColumnName.Equals(queryTopNPerLevelSampleExpression.RestartIndicatorColumnName) && this.WindowExpansion.Equals(queryTopNPerLevelSampleExpression.WindowExpansion);
		}
	}
}
