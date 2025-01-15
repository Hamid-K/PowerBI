using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017F RID: 383
	public sealed class StarToken : PathToken
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x0002C25E File Offset: 0x0002A45E
		public StarToken(QueryToken nextToken)
		{
			this.nextToken = nextToken;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0002B681 File Offset: 0x00029881
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Star;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0002C26D File Offset: 0x0002A46D
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x0002C275 File Offset: 0x0002A475
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

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x0002C27E File Offset: 0x0002A47E
		public override string Identifier
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0002C285 File Offset: 0x0002A485
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007FB RID: 2043
		private QueryToken nextToken;
	}
}
