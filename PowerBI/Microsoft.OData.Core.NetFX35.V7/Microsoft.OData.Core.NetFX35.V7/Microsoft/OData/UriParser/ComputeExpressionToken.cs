using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016B RID: 363
	public sealed class ComputeExpressionToken : QueryToken
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x0002BC50 File Offset: 0x00029E50
		public ComputeExpressionToken(QueryToken expression, string alias)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(expression, "expression");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.expression = expression;
			this.alias = alias;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00029054 File Offset: 0x00027254
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.ComputeExpression;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0002BC7D File Offset: 0x00029E7D
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0002BC85 File Offset: 0x00029E85
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002BC90 File Offset: 0x00029E90
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			SyntacticTreeVisitor<T> syntacticTreeVisitor = visitor as SyntacticTreeVisitor<T>;
			if (syntacticTreeVisitor != null)
			{
				return syntacticTreeVisitor.Visit(this);
			}
			throw new NotImplementedException();
		}

		// Token: 0x040007B3 RID: 1971
		private QueryToken expression;

		// Token: 0x040007B4 RID: 1972
		private string alias;
	}
}
