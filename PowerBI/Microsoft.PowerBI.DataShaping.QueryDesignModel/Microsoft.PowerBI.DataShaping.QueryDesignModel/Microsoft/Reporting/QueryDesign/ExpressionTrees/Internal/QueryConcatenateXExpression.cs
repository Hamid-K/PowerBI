using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200016C RID: 364
	internal sealed class QueryConcatenateXExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x0003AC83 File Offset: 0x00038E83
		internal QueryConcatenateXExpression(QueryExpressionBinding table, QueryExpression expression, QueryExpression delimiter, QuerySortClause orderBy)
			: base(ConceptualPrimitiveResultType.Text)
		{
			this.Table = table;
			this.Expression = expression;
			this.Delimiter = delimiter;
			this.OrderBy = orderBy;
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0003ACAD File Offset: 0x00038EAD
		public QueryExpressionBinding Table { get; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0003ACB5 File Offset: 0x00038EB5
		public QueryExpression Expression { get; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0003ACBD File Offset: 0x00038EBD
		public QueryExpression Delimiter { get; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0003ACC5 File Offset: 0x00038EC5
		public QuerySortClause OrderBy { get; }

		// Token: 0x0600145F RID: 5215 RVA: 0x0003ACCD File Offset: 0x00038ECD
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0003ACD8 File Offset: 0x00038ED8
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryConcatenateXExpression queryConcatenateXExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryConcatenateXExpression>(this, other, out flag, out queryConcatenateXExpression))
			{
				return flag;
			}
			return this.Table.Equals(queryConcatenateXExpression.Table) && this.Expression.Equals(queryConcatenateXExpression.Expression) && object.Equals(this.Delimiter, queryConcatenateXExpression.Delimiter) && object.Equals(this.OrderBy, queryConcatenateXExpression.OrderBy);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0003AD40 File Offset: 0x00038F40
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Table.GetHashCode(), this.Expression.GetHashCode(), Hashing.GetHashCode<QueryExpression>(this.Delimiter, null), Hashing.GetHashCode<QuerySortClause>(this.OrderBy, null));
		}
	}
}
