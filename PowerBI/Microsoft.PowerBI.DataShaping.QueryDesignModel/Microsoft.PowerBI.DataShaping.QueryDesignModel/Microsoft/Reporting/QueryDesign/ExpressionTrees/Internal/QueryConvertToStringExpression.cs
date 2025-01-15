using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200016D RID: 365
	internal sealed class QueryConvertToStringExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001462 RID: 5218 RVA: 0x0003AD75 File Offset: 0x00038F75
		internal QueryConvertToStringExpression(QueryExpression input)
			: base(ConceptualPrimitiveResultType.Text)
		{
			this._input = input;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0003AD89 File Offset: 0x00038F89
		public QueryExpression Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0003AD91 File Offset: 0x00038F91
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0003ADA4 File Offset: 0x00038FA4
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryConvertToStringExpression queryConvertToStringExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryConvertToStringExpression>(this, other, out flag, out queryConvertToStringExpression))
			{
				return flag;
			}
			return this.Input.Equals(queryConvertToStringExpression.Input);
		}

		// Token: 0x04000B2A RID: 2858
		private readonly QueryExpression _input;
	}
}
