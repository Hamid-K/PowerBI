using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001AA RID: 426
	internal sealed class QueryNullExpression : QueryExpression
	{
		// Token: 0x060015CC RID: 5580 RVA: 0x0003CBE4 File Offset: 0x0003ADE4
		internal QueryNullExpression(ConceptualResultType conceptualResultType)
			: base(conceptualResultType)
		{
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0003CBED File Offset: 0x0003ADED
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0003CC00 File Offset: 0x0003AE00
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryNullExpression queryNullExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryNullExpression>(this, other, out flag, out queryNullExpression))
			{
				return flag;
			}
			return queryNullExpression != null;
		}
	}
}
