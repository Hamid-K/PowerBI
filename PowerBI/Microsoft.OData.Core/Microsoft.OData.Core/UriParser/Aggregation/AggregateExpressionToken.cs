using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001FB RID: 507
	public sealed class AggregateExpressionToken : AggregateTokenBase
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x0003F2C1 File Offset: 0x0003D4C1
		public AggregateExpressionToken(QueryToken expression, AggregationMethod method, string alias)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			this.expression = expression;
			this.method = method;
			this.alias = alias;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0003F2F6 File Offset: 0x0003D4F6
		public AggregateExpressionToken(QueryToken expression, AggregationMethodDefinition methodDefinition, string alias)
			: this(expression, methodDefinition.MethodKind, alias)
		{
			this.methodDefinition = methodDefinition;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0003877D File Offset: 0x0003697D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.AggregateExpression;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0003F30D File Offset: 0x0003D50D
		public AggregationMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x0003F315 File Offset: 0x0003D515
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0003F31D File Offset: 0x0003D51D
		public AggregationMethodDefinition MethodDefinition
		{
			get
			{
				return this.methodDefinition;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0003F325 File Offset: 0x0003D525
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0003F32D File Offset: 0x0003D52D
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A2B RID: 2603
		private readonly QueryToken expression;

		// Token: 0x04000A2C RID: 2604
		private readonly AggregationMethod method;

		// Token: 0x04000A2D RID: 2605
		private readonly AggregationMethodDefinition methodDefinition;

		// Token: 0x04000A2E RID: 2606
		private readonly string alias;
	}
}
