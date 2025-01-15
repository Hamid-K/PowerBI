using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000182 RID: 386
	internal class QueryFieldExpression : QueryExpression
	{
		// Token: 0x06001507 RID: 5383 RVA: 0x0003B5DC File Offset: 0x000397DC
		internal QueryFieldExpression(QueryExpression instance, ConceptualTypeColumn columnType)
			: base(columnType.PrimitiveType)
		{
			this.Instance = instance;
			this.Column = columnType;
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0003B5F8 File Offset: 0x000397F8
		public QueryExpression Instance { get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0003B600 File Offset: 0x00039800
		public ConceptualTypeColumn Column { get; }

		// Token: 0x0600150A RID: 5386 RVA: 0x0003B608 File Offset: 0x00039808
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0003B61C File Offset: 0x0003981C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryFieldExpression queryFieldExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryFieldExpression>(this, other, out flag, out queryFieldExpression))
			{
				return flag;
			}
			return this.Column.Equals(queryFieldExpression.Column) && this.Instance.Equals(queryFieldExpression.Instance);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0003B65E File Offset: 0x0003985E
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Column.GetHashCode(), this.Instance.GetHashCode());
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0003B67B File Offset: 0x0003987B
		public KeyValuePair<string, QueryExpression> ToKeyValuePair()
		{
			return new KeyValuePair<string, QueryExpression>(this.Column.EdmName, this);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0003B68E File Offset: 0x0003988E
		public static implicit operator KeyValuePair<string, QueryExpression>(QueryFieldExpression value)
		{
			ArgumentValidation.CheckNotNull<QueryFieldExpression>(value, "value");
			return value.ToKeyValuePair();
		}
	}
}
