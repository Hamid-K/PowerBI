using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C1 RID: 449
	public abstract class LambdaToken : QueryToken
	{
		// Token: 0x060014CA RID: 5322 RVA: 0x0003C058 File Offset: 0x0003A258
		protected LambdaToken(QueryToken expression, string parameter, QueryToken parent)
		{
			this.expression = expression;
			this.parameter = parameter;
			this.parent = parent;
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0003C075 File Offset: 0x0003A275
		public QueryToken Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0003C07D File Offset: 0x0003A27D
		public QueryToken Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0003C085 File Offset: 0x0003A285
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0003C08D File Offset: 0x0003A28D
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000915 RID: 2325
		private readonly QueryToken parent;

		// Token: 0x04000916 RID: 2326
		private readonly string parameter;

		// Token: 0x04000917 RID: 2327
		private readonly QueryToken expression;
	}
}
