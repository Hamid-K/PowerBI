using System;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200026D RID: 621
	internal abstract class LambdaToken : QueryToken
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x0004BFCA File Offset: 0x0004A1CA
		protected LambdaToken(QueryToken expression, string parameter, QueryToken parent)
		{
			this.expression = expression;
			this.parameter = parameter;
			this.parent = parent;
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0004BFE7 File Offset: 0x0004A1E7
		public QueryToken Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0004BFEF File Offset: 0x0004A1EF
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0004BFF7 File Offset: 0x0004A1F7
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0004BFFF File Offset: 0x0004A1FF
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000909 RID: 2313
		private readonly QueryToken parent;

		// Token: 0x0400090A RID: 2314
		private readonly string parameter;

		// Token: 0x0400090B RID: 2315
		private readonly QueryToken expression;
	}
}
