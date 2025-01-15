using System;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200013E RID: 318
	public sealed class AggregateExpressionToken : AggregateTokenBase
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x0002D539 File Offset: 0x0002B739
		public AggregateExpressionToken(QueryToken expression, AggregationMethod method, string alias)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			this.expression = expression;
			this.method = method;
			this.alias = alias;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002D56E File Offset: 0x0002B76E
		public AggregateExpressionToken(QueryToken expression, AggregationMethodDefinition methodDefinition, string alias)
			: this(expression, methodDefinition.MethodKind, alias)
		{
			this.methodDefinition = methodDefinition;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0002D585 File Offset: 0x0002B785
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.AggregateExpression;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0002D589 File Offset: 0x0002B789
		public AggregationMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0002D591 File Offset: 0x0002B791
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0002D599 File Offset: 0x0002B799
		public AggregationMethodDefinition MethodDefinition
		{
			get
			{
				return this.methodDefinition;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0002D5A1 File Offset: 0x0002B7A1
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002D5A9 File Offset: 0x0002B7A9
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040006B0 RID: 1712
		private readonly QueryToken expression;

		// Token: 0x040006B1 RID: 1713
		private readonly AggregationMethod method;

		// Token: 0x040006B2 RID: 1714
		private readonly AggregationMethodDefinition methodDefinition;

		// Token: 0x040006B3 RID: 1715
		private readonly string alias;
	}
}
