using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001CB RID: 459
	internal sealed class QueryVariableDeclarationExpression : QueryBaseDeclarationExpression
	{
		// Token: 0x06001677 RID: 5751 RVA: 0x0003E0ED File Offset: 0x0003C2ED
		internal QueryVariableDeclarationExpression(QueryExpression input, QueryVariableReferenceExpression varRef)
			: base(input.ConceptualResultType)
		{
			this.Expression = input;
			this.Variable = varRef;
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0003E109 File Offset: 0x0003C309
		public QueryExpression Expression { get; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0003E111 File Offset: 0x0003C311
		public string VariableName
		{
			get
			{
				return this.Variable.VariableName;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x0003E11E File Offset: 0x0003C31E
		public QueryVariableReferenceExpression Variable { get; }

		// Token: 0x0600167B RID: 5755 RVA: 0x0003E128 File Offset: 0x0003C328
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryVariableDeclarationExpression queryVariableDeclarationExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryVariableDeclarationExpression>(this, other, out flag, out queryVariableDeclarationExpression))
			{
				return flag;
			}
			return this.Expression.Equals(queryVariableDeclarationExpression.Expression) && this.Variable.Equals(queryVariableDeclarationExpression.Variable);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0003E16A File Offset: 0x0003C36A
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Expression.GetHashCode(), this.Variable.GetHashCode());
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0003E187 File Offset: 0x0003C387
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
