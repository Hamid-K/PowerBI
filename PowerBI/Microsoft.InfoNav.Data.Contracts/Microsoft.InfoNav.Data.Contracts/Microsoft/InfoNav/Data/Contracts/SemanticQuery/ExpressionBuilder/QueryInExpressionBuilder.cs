using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder
{
	// Token: 0x02000098 RID: 152
	public sealed class QueryInExpressionBuilder
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0000A251 File Offset: 0x00008451
		public QueryInExpressionBuilder(QueryEqualitySemanticsKind? equalityKind = null)
		{
			this._expressions = new List<QueryExpressionContainer>();
			this._values = new List<List<QueryExpressionContainer>>();
			this._equalityKind = equalityKind;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000A276 File Offset: 0x00008476
		public QueryInExpressionBuilder WithExpressions(params QueryExpressionContainer[] exprs)
		{
			this._expressions.AddRange(exprs);
			return this;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000A285 File Offset: 0x00008485
		public QueryInExpressionBuilder WithValues(params QueryExpressionContainer[] vals)
		{
			return this.WithValues(vals.ToList<QueryExpressionContainer>());
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000A293 File Offset: 0x00008493
		public QueryInExpressionBuilder WithValues(List<QueryExpressionContainer> vals)
		{
			this._values.Add(vals);
			return this;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000A2A2 File Offset: 0x000084A2
		public QueryInExpression Build()
		{
			return new QueryInExpression
			{
				Expressions = this._expressions,
				Values = this._values,
				EqualityKind = this._equalityKind
			};
		}

		// Token: 0x040001D3 RID: 467
		private List<QueryExpressionContainer> _expressions;

		// Token: 0x040001D4 RID: 468
		private List<List<QueryExpressionContainer>> _values;

		// Token: 0x040001D5 RID: 469
		private QueryEqualitySemanticsKind? _equalityKind;
	}
}
