using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000184 RID: 388
	internal sealed class QueryFilterExpression : QueryExpression
	{
		// Token: 0x06001515 RID: 5397 RVA: 0x0003B738 File Offset: 0x00039938
		internal QueryFilterExpression(ConceptualResultType conceptualResultType, QueryExpressionBinding input, QueryExpression predicate)
			: base(conceptualResultType)
		{
			this._input = ArgumentValidation.CheckNotNull<QueryExpressionBinding>(input, "input");
			this._predicate = ArgumentValidation.CheckNotNull<QueryExpression>(predicate, "predicate");
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0003B763 File Offset: 0x00039963
		public QueryExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0003B76B File Offset: 0x0003996B
		public QueryExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0003B773 File Offset: 0x00039973
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0003B788 File Offset: 0x00039988
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryFilterExpression queryFilterExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryFilterExpression>(this, other, out flag, out queryFilterExpression))
			{
				return flag;
			}
			return this.Input.Equals(queryFilterExpression.Input) && this.Predicate.Equals(queryFilterExpression.Predicate);
		}

		// Token: 0x04000B47 RID: 2887
		private readonly QueryExpressionBinding _input;

		// Token: 0x04000B48 RID: 2888
		private readonly QueryExpression _predicate;
	}
}
