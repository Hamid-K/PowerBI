using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000174 RID: 372
	internal class QueryDaxTextExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x0003B06C File Offset: 0x0003926C
		internal QueryDaxTextExpression(ConceptualResultType conceptualResultType, string text)
			: base(conceptualResultType)
		{
			this.Text = text;
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0003B07C File Offset: 0x0003927C
		public string Text { get; }

		// Token: 0x06001483 RID: 5251 RVA: 0x0003B084 File Offset: 0x00039284
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0003B090 File Offset: 0x00039290
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryDaxTextExpression queryDaxTextExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryDaxTextExpression>(this, other, out flag, out queryDaxTextExpression))
			{
				return flag;
			}
			return this.Text.Equals(queryDaxTextExpression.Text, StringComparison.Ordinal);
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0003B0BE File Offset: 0x000392BE
		public override int GetHashCode()
		{
			return this.Text.GetHashCode();
		}
	}
}
