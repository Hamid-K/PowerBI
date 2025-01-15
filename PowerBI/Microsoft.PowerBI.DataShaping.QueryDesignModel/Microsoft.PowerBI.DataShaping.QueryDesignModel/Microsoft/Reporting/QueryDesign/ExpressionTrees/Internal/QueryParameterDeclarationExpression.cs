using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001AC RID: 428
	internal sealed class QueryParameterDeclarationExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x0003CCFF File Offset: 0x0003AEFF
		internal QueryParameterDeclarationExpression(QueryParameterReferenceExpression parameterRef)
			: base(parameterRef.ConceptualResultType)
		{
			this.Parameter = parameterRef;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0003CD14 File Offset: 0x0003AF14
		public QueryParameterReferenceExpression Parameter { get; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		public string Name
		{
			get
			{
				return this.Parameter.Name;
			}
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0003CD2C File Offset: 0x0003AF2C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryParameterDeclarationExpression queryParameterDeclarationExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryParameterDeclarationExpression>(this, other, out flag, out queryParameterDeclarationExpression))
			{
				return flag;
			}
			return this.Parameter.Equals(queryParameterDeclarationExpression.Parameter);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0003CD59 File Offset: 0x0003AF59
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
