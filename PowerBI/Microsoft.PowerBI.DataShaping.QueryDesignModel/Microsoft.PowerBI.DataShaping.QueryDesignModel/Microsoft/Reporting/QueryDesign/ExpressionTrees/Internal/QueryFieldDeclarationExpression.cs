using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000181 RID: 385
	internal sealed class QueryFieldDeclarationExpression : QueryBaseDeclarationExpression
	{
		// Token: 0x06001501 RID: 5377 RVA: 0x0003B547 File Offset: 0x00039747
		internal QueryFieldDeclarationExpression(QueryExpression expr, QueryFieldExpression fieldRef)
			: base(expr.ConceptualResultType)
		{
			this.Expression = expr;
			this.FieldRef = fieldRef;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0003B563 File Offset: 0x00039763
		public QueryExpression Expression { get; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0003B56B File Offset: 0x0003976B
		public QueryFieldExpression FieldRef { get; }

		// Token: 0x06001504 RID: 5380 RVA: 0x0003B574 File Offset: 0x00039774
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryFieldDeclarationExpression queryFieldDeclarationExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryFieldDeclarationExpression>(this, other, out flag, out queryFieldDeclarationExpression))
			{
				return flag;
			}
			return this.Expression.Equals(queryFieldDeclarationExpression.Expression) && this.FieldRef.Equals(queryFieldDeclarationExpression.FieldRef);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0003B5B6 File Offset: 0x000397B6
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Expression.GetHashCode(), this.FieldRef.GetHashCode());
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0003B5D3 File Offset: 0x000397D3
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
