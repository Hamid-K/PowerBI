using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000113 RID: 275
	public sealed class ComputeExpressionToken : QueryToken
	{
		// Token: 0x06000F72 RID: 3954 RVA: 0x00026564 File Offset: 0x00024764
		public ComputeExpressionToken(QueryToken expression, string alias)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.expression = expression;
			this.alias = alias;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00026039 File Offset: 0x00024239
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.ComputeExpression;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00026591 File Offset: 0x00024791
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00026599 File Offset: 0x00024799
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x000265A4 File Offset: 0x000247A4
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			SyntacticTreeVisitor<T> syntacticTreeVisitor = visitor as SyntacticTreeVisitor<T>;
			if (syntacticTreeVisitor != null)
			{
				return syntacticTreeVisitor.Visit(this);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0400078A RID: 1930
		private QueryToken expression;

		// Token: 0x0400078B RID: 1931
		private string alias;
	}
}
