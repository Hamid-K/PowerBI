using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001AE RID: 430
	internal sealed class QueryProjectExpression : QueryExpression
	{
		// Token: 0x060015E0 RID: 5600 RVA: 0x0003CDD9 File Offset: 0x0003AFD9
		internal QueryProjectExpression(ConceptualResultType conceptualResultType, QueryExpressionBinding input, QueryExpression projection, ProjectSubsetStrategy projectSubsetStrategy)
			: base(conceptualResultType)
		{
			this.Input = input;
			this.Projection = projection;
			this.ProjectSubsetStrategy = projectSubsetStrategy;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0003CDF8 File Offset: 0x0003AFF8
		public QueryExpressionBinding Input { get; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0003CE00 File Offset: 0x0003B000
		public QueryExpression Projection { get; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0003CE08 File Offset: 0x0003B008
		public ProjectSubsetStrategy ProjectSubsetStrategy { get; }

		// Token: 0x060015E4 RID: 5604 RVA: 0x0003CE10 File Offset: 0x0003B010
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x0003CE1C File Offset: 0x0003B01C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryProjectExpression queryProjectExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryProjectExpression>(this, other, out flag, out queryProjectExpression))
			{
				return flag;
			}
			return this.Input.Equals(queryProjectExpression.Input) && this.Projection.Equals(queryProjectExpression.Projection) && this.ProjectSubsetStrategy == queryProjectExpression.ProjectSubsetStrategy;
		}
	}
}
