using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000116 RID: 278
	public sealed class AllToken : LambdaToken
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002C8E8 File Offset: 0x0002AAE8
		public AllToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002C8F3 File Offset: 0x0002AAF3
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.All;
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002C8F7 File Offset: 0x0002AAF7
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
