using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001BC RID: 444
	internal sealed class QuerySubstituteWithIndexExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600162A RID: 5674 RVA: 0x0003D854 File Offset: 0x0003BA54
		internal QuerySubstituteWithIndexExpression(ConceptualResultType conceptualResultType, QueryExpressionBinding table, string indexColumnName, QueryExpressionBinding indexTable, ReadOnlyCollection<QuerySortClause> indexTableSortOrder)
			: base(conceptualResultType)
		{
			this._table = ArgumentValidation.CheckNotNull<QueryExpressionBinding>(table, "table");
			this._indexColumnName = ArgumentValidation.CheckNotNullOrEmpty(indexColumnName, "indexColumnName");
			this._indexTable = ArgumentValidation.CheckNotNull<QueryExpressionBinding>(indexTable, "indexTable");
			this._indexTableSortOrder = ArgumentValidation.CheckNotNull<ReadOnlyCollection<QuerySortClause>>(indexTableSortOrder, "indexTableSortOrder");
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0003D8AE File Offset: 0x0003BAAE
		public QueryExpressionBinding Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x0003D8B6 File Offset: 0x0003BAB6
		public string IndexColumnName
		{
			get
			{
				return this._indexColumnName;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0003D8BE File Offset: 0x0003BABE
		public QueryExpressionBinding IndexTable
		{
			get
			{
				return this._indexTable;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0003D8C6 File Offset: 0x0003BAC6
		public ReadOnlyCollection<QuerySortClause> IndexTableSortOrder
		{
			get
			{
				return this._indexTableSortOrder;
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0003D8CE File Offset: 0x0003BACE
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0003D8E4 File Offset: 0x0003BAE4
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QuerySubstituteWithIndexExpression querySubstituteWithIndexExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QuerySubstituteWithIndexExpression>(this, other, out flag, out querySubstituteWithIndexExpression))
			{
				return flag;
			}
			return this.Table.Equals(querySubstituteWithIndexExpression.Table) && this.IndexColumnName == querySubstituteWithIndexExpression.IndexColumnName && this.IndexTable.Equals(querySubstituteWithIndexExpression.IndexTable) && this.IndexTableSortOrder.SequenceEqual(querySubstituteWithIndexExpression.IndexTableSortOrder);
		}

		// Token: 0x04000BD4 RID: 3028
		private readonly QueryExpressionBinding _table;

		// Token: 0x04000BD5 RID: 3029
		private readonly string _indexColumnName;

		// Token: 0x04000BD6 RID: 3030
		private readonly QueryExpressionBinding _indexTable;

		// Token: 0x04000BD7 RID: 3031
		private readonly ReadOnlyCollection<QuerySortClause> _indexTableSortOrder;
	}
}
