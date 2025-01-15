using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x0200009C RID: 156
	internal abstract class LambdaToken : QueryToken
	{
		// Token: 0x060003AE RID: 942 RVA: 0x0000BD23 File Offset: 0x00009F23
		protected LambdaToken(QueryToken expression, string parameter, QueryToken parent)
		{
			this.expression = expression;
			this.parameter = parameter;
			this.parent = parent;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000BD40 File Offset: 0x00009F40
		public QueryToken Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000BD48 File Offset: 0x00009F48
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000BD50 File Offset: 0x00009F50
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000BD58 File Offset: 0x00009F58
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400011C RID: 284
		private readonly QueryToken parent;

		// Token: 0x0400011D RID: 285
		private readonly string parameter;

		// Token: 0x0400011E RID: 286
		private readonly QueryToken expression;
	}
}
