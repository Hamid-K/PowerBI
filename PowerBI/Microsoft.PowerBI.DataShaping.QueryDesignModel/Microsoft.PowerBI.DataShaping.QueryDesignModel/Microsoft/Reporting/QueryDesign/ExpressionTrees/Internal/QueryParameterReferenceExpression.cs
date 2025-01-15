using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001AD RID: 429
	internal sealed class QueryParameterReferenceExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060015DB RID: 5595 RVA: 0x0003CD62 File Offset: 0x0003AF62
		internal QueryParameterReferenceExpression(string name, ConceptualResultType resultType)
			: base(resultType)
		{
			this.Name = name;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0003CD72 File Offset: 0x0003AF72
		public string Name { get; }

		// Token: 0x060015DD RID: 5597 RVA: 0x0003CD7C File Offset: 0x0003AF7C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryParameterReferenceExpression queryParameterReferenceExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryParameterReferenceExpression>(this, other, out flag, out queryParameterReferenceExpression))
			{
				return flag;
			}
			return QueryNamingContext.NameComparer.Equals(this.Name, queryParameterReferenceExpression.Name);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0003CDAE File Offset: 0x0003AFAE
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.ConceptualResultType.GetHashCode(), QueryNamingContext.NameComparer.GetHashCode(this.Name));
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
