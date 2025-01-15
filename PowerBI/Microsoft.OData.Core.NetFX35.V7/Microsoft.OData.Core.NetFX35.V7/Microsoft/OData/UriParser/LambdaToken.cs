using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000175 RID: 373
	public abstract class LambdaToken : QueryToken
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x0002C088 File Offset: 0x0002A288
		protected LambdaToken(QueryToken expression, string parameter, QueryToken parent)
		{
			this.expression = expression;
			this.parameter = parameter;
			this.parent = parent;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0002C0A5 File Offset: 0x0002A2A5
		public QueryToken Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0002C0AD File Offset: 0x0002A2AD
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0002C0B5 File Offset: 0x0002A2B5
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0002C0BD File Offset: 0x0002A2BD
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007D2 RID: 2002
		private readonly QueryToken parent;

		// Token: 0x040007D3 RID: 2003
		private readonly string parameter;

		// Token: 0x040007D4 RID: 2004
		private readonly QueryToken expression;
	}
}
