using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001CA RID: 458
	public sealed class AggregateExpressionToken : QueryToken
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x00031FF9 File Offset: 0x000301F9
		public AggregateExpressionToken(QueryToken expression, AggregationMethod method, string alias)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<string>(alias, "alias");
			this.expression = expression;
			this.method = method;
			this.alias = alias;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0003202E File Offset: 0x0003022E
		public AggregateExpressionToken(QueryToken expression, AggregationMethodDefinition methodDefinition, string alias)
			: this(expression, methodDefinition.MethodKind, alias)
		{
			this.methodDefinition = methodDefinition;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00028E49 File Offset: 0x00027049
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.AggregateExpression;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00032045 File Offset: 0x00030245
		public AggregationMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0003204D File Offset: 0x0003024D
		public AggregationMethodDefinition MethodDefinition
		{
			get
			{
				return this.methodDefinition;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00032055 File Offset: 0x00030255
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0003205D File Offset: 0x0003025D
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00032065 File Offset: 0x00030265
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000917 RID: 2327
		private readonly QueryToken expression;

		// Token: 0x04000918 RID: 2328
		private readonly AggregationMethod method;

		// Token: 0x04000919 RID: 2329
		private readonly AggregationMethodDefinition methodDefinition;

		// Token: 0x0400091A RID: 2330
		private readonly string alias;
	}
}
