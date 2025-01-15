using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000BB RID: 187
	internal sealed class StarToken : PathToken
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x0000EBC5 File Offset: 0x0000CDC5
		public StarToken(QueryToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Star;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000EBD8 File Offset: 0x0000CDD8
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public override QueryToken NextToken
		{
			get
			{
				return this.nextToken;
			}
			set
			{
				this.nextToken = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000EBE9 File Offset: 0x0000CDE9
		public override string Identifier
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000188 RID: 392
		private QueryToken nextToken;
	}
}
