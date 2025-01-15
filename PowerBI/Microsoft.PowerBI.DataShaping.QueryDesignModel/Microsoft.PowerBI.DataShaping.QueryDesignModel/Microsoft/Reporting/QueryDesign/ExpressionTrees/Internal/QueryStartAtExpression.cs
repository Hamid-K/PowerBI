using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001BA RID: 442
	internal sealed class QueryStartAtExpression : QueryUnaryExtensionExpression
	{
		// Token: 0x0600161F RID: 5663 RVA: 0x0003D576 File Offset: 0x0003B776
		internal QueryStartAtExpression(QuerySortExpression input, IEnumerable<QueryExpression> values)
			: base(input.ConceptualResultType, input)
		{
			ArgumentValidation.CheckNotNullOrEmpty<QueryExpression>(values, "values");
			this._values = values.ToReadOnlyCollection<QueryExpression>();
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0003D59D File Offset: 0x0003B79D
		public QuerySortExpression OrderBy
		{
			get
			{
				return (QuerySortExpression)base.Argument;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0003D5AA File Offset: 0x0003B7AA
		public ReadOnlyCollection<QueryExpression> Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0003D5B2 File Offset: 0x0003B7B2
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0003D5C7 File Offset: 0x0003B7C7
		public override bool Equals(QueryExpression other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000BD3 RID: 3027
		private readonly ReadOnlyCollection<QueryExpression> _values;
	}
}
