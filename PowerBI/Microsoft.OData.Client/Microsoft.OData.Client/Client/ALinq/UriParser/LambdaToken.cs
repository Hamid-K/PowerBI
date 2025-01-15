using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200012A RID: 298
	public abstract class LambdaToken : QueryToken
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x0002CECA File Offset: 0x0002B0CA
		protected LambdaToken(QueryToken expression, string parameter, QueryToken parent)
		{
			this.expression = expression;
			this.parameter = parameter;
			this.parent = parent;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0002CEE7 File Offset: 0x0002B0E7
		public QueryToken Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0002CEEF File Offset: 0x0002B0EF
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002CEF7 File Offset: 0x0002B0F7
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002CEFF File Offset: 0x0002B0FF
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000672 RID: 1650
		private readonly QueryToken parent;

		// Token: 0x04000673 RID: 1651
		private readonly string parameter;

		// Token: 0x04000674 RID: 1652
		private readonly QueryToken expression;
	}
}
