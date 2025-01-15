using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200025C RID: 604
	internal sealed class QdmCurrentGroupPlaceholderExpression : QueryExtensionExpression
	{
		// Token: 0x06001A38 RID: 6712 RVA: 0x000482A5 File Offset: 0x000464A5
		internal QdmCurrentGroupPlaceholderExpression(QueryTable table)
			: base(table.Expression.ConceptualResultType)
		{
			this._table = table;
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x000482BF File Offset: 0x000464BF
		public QueryTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000482C8 File Offset: 0x000464C8
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QdmCurrentGroupPlaceholderExpression qdmCurrentGroupPlaceholderExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QdmCurrentGroupPlaceholderExpression>(this, other, out flag, out qdmCurrentGroupPlaceholderExpression))
			{
				return flag;
			}
			return this.Table.Equals(qdmCurrentGroupPlaceholderExpression.Table);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000482F5 File Offset: 0x000464F5
		public override int GetHashCode()
		{
			return this.Table.GetHashCode();
		}

		// Token: 0x04000E82 RID: 3714
		private readonly QueryTable _table;
	}
}
