using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000171 RID: 369
	internal sealed class QueryDataSourceVariablesDeclarationExpression : QueryBaseDeclarationExpression
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x0003AEB9 File Offset: 0x000390B9
		internal QueryDataSourceVariablesDeclarationExpression(QueryExpression expression)
			: base(expression.ConceptualResultType)
		{
			this.Expression = expression;
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0003AECE File Offset: 0x000390CE
		public QueryExpression Expression { get; }

		// Token: 0x06001472 RID: 5234 RVA: 0x0003AED6 File Offset: 0x000390D6
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0003AEEC File Offset: 0x000390EC
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryDataSourceVariablesDeclarationExpression queryDataSourceVariablesDeclarationExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryDataSourceVariablesDeclarationExpression>(this, other, out flag, out queryDataSourceVariablesDeclarationExpression))
			{
				return flag;
			}
			return this.Expression.Equals(queryDataSourceVariablesDeclarationExpression.Expression);
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0003AF19 File Offset: 0x00039119
		public override int GetHashCode()
		{
			return Hashing.GetHashCode<QueryExpression>(this.Expression, null);
		}
	}
}
