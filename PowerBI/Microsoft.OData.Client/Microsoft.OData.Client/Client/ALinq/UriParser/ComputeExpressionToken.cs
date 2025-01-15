using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200011A RID: 282
	public sealed class ComputeExpressionToken : QueryToken
	{
		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002C9B0 File Offset: 0x0002ABB0
		public ComputeExpressionToken(QueryToken expression, string alias)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.expression = expression;
			this.alias = alias;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0002C9DD File Offset: 0x0002ABDD
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.ComputeExpression;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0002C9E1 File Offset: 0x0002ABE1
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002C9E9 File Offset: 0x0002ABE9
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002C9F4 File Offset: 0x0002ABF4
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			SyntacticTreeVisitor<T> syntacticTreeVisitor = visitor as SyntacticTreeVisitor<T>;
			if (syntacticTreeVisitor != null)
			{
				return syntacticTreeVisitor.Visit(this);
			}
			throw new NotImplementedException();
		}

		// Token: 0x04000656 RID: 1622
		private QueryToken expression;

		// Token: 0x04000657 RID: 1623
		private string alias;
	}
}
