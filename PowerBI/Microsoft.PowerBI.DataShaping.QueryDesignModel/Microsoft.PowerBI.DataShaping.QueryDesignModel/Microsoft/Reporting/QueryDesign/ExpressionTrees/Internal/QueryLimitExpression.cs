using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200019C RID: 412
	internal sealed class QueryLimitExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600158D RID: 5517 RVA: 0x0003C391 File Offset: 0x0003A591
		internal QueryLimitExpression(QueryLimitOperator limitOperator, ConceptualResultType conceptualResultType, QueryExpressionBinding input, QueryExpression count, QueryExpression skipCount, IEnumerable<QuerySortClause> sortOrder)
			: base(conceptualResultType)
		{
			this._limitOperator = limitOperator;
			this._input = input;
			this._count = count;
			this._skipCount = skipCount;
			this._sortOrder = sortOrder.ToReadOnlyCollection<QuerySortClause>();
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0003C3C5 File Offset: 0x0003A5C5
		public QueryLimitOperator LimitKind
		{
			get
			{
				return this._limitOperator;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0003C3CD File Offset: 0x0003A5CD
		public QueryExpression Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0003C3D5 File Offset: 0x0003A5D5
		public QueryExpression SkipCount
		{
			get
			{
				return this._skipCount;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0003C3DD File Offset: 0x0003A5DD
		public QueryExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0003C3E5 File Offset: 0x0003A5E5
		public ReadOnlyCollection<QuerySortClause> SortOrder
		{
			get
			{
				return this._sortOrder;
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0003C3ED File Offset: 0x0003A5ED
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0003C400 File Offset: 0x0003A600
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryLimitExpression queryLimitExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryLimitExpression>(this, other, out flag, out queryLimitExpression))
			{
				return flag;
			}
			return this.LimitKind == queryLimitExpression.LimitKind && this.Count == queryLimitExpression.Count && this.SkipCount == queryLimitExpression.SkipCount && this.Input.Equals(queryLimitExpression.Input) && this.SortOrder.SequenceEqual(queryLimitExpression.SortOrder);
		}

		// Token: 0x04000B72 RID: 2930
		private readonly QueryLimitOperator _limitOperator;

		// Token: 0x04000B73 RID: 2931
		private readonly QueryExpression _count;

		// Token: 0x04000B74 RID: 2932
		private readonly QueryExpression _skipCount;

		// Token: 0x04000B75 RID: 2933
		private readonly QueryExpressionBinding _input;

		// Token: 0x04000B76 RID: 2934
		private readonly ReadOnlyCollection<QuerySortClause> _sortOrder;
	}
}
