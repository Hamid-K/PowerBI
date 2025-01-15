using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000195 RID: 405
	internal sealed class QueryInTableExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001574 RID: 5492 RVA: 0x0003C155 File Offset: 0x0003A355
		internal QueryInTableExpression(ConceptualResultType conceptualResultType, QueryExpression leftExpression, QueryExpression rightExpression)
			: base(conceptualResultType)
		{
			this._leftExpression = leftExpression;
			this._rightExpression = rightExpression;
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0003C16C File Offset: 0x0003A36C
		public QueryExpression LeftExpression
		{
			get
			{
				return this._leftExpression;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0003C174 File Offset: 0x0003A374
		public QueryExpression RightExpression
		{
			get
			{
				return this._rightExpression;
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0003C17C File Offset: 0x0003A37C
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0003C188 File Offset: 0x0003A388
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryInTableExpression queryInTableExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryInTableExpression>(this, other, out flag, out queryInTableExpression))
			{
				return flag;
			}
			return this.LeftExpression.Equals(queryInTableExpression.LeftExpression) && this.RightExpression.Equals(queryInTableExpression.RightExpression);
		}

		// Token: 0x04000B6B RID: 2923
		private readonly QueryExpression _leftExpression;

		// Token: 0x04000B6C RID: 2924
		private readonly QueryExpression _rightExpression;
	}
}
