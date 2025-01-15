using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001B9 RID: 441
	internal sealed class QuerySortExpression : QueryExpression
	{
		// Token: 0x0600161A RID: 5658 RVA: 0x0003D4E1 File Offset: 0x0003B6E1
		internal QuerySortExpression(ConceptualResultType conceptualResultType, QueryExpressionBinding input, IEnumerable<QuerySortClause> sortOrder)
			: base(conceptualResultType)
		{
			this._input = ArgumentValidation.CheckNotNull<QueryExpressionBinding>(input, "input");
			this._sortOrder = ArgumentValidation.CheckNotNull<IEnumerable<QuerySortClause>>(sortOrder, "sortOrder").ToReadOnlyCollection<QuerySortClause>();
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0003D511 File Offset: 0x0003B711
		public QueryExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0003D519 File Offset: 0x0003B719
		public ReadOnlyCollection<QuerySortClause> SortOrder
		{
			get
			{
				return this._sortOrder;
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0003D521 File Offset: 0x0003B721
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0003D534 File Offset: 0x0003B734
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QuerySortExpression querySortExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QuerySortExpression>(this, other, out flag, out querySortExpression))
			{
				return flag;
			}
			return this.Input.Equals(querySortExpression.Input) && this.SortOrder.SequenceEqual(querySortExpression.SortOrder);
		}

		// Token: 0x04000BD1 RID: 3025
		private readonly QueryExpressionBinding _input;

		// Token: 0x04000BD2 RID: 3026
		private readonly ReadOnlyCollection<QuerySortClause> _sortOrder;
	}
}
