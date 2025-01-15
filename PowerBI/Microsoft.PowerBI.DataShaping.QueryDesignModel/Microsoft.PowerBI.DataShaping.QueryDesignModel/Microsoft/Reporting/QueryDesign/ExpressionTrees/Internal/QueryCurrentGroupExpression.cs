using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000170 RID: 368
	internal sealed class QueryCurrentGroupExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600146C RID: 5228 RVA: 0x0003AE55 File Offset: 0x00039055
		internal QueryCurrentGroupExpression(ConceptualResultType conceptualResultType, QueryVariableReferenceExpression input)
			: base(conceptualResultType)
		{
			this._input = ArgumentValidation.CheckNotNull<QueryVariableReferenceExpression>(input, "input");
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0003AE6F File Offset: 0x0003906F
		public QueryVariableReferenceExpression Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0003AE77 File Offset: 0x00039077
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0003AE8C File Offset: 0x0003908C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryCurrentGroupExpression queryCurrentGroupExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryCurrentGroupExpression>(this, other, out flag, out queryCurrentGroupExpression))
			{
				return flag;
			}
			return this.Input.Equals(queryCurrentGroupExpression.Input);
		}

		// Token: 0x04000B2C RID: 2860
		private readonly QueryVariableReferenceExpression _input;
	}
}
