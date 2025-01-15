using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000117 RID: 279
	public sealed class AnyToken : LambdaToken
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002C8E8 File Offset: 0x0002AAE8
		public AnyToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0002C900 File Offset: 0x0002AB00
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Any;
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002C904 File Offset: 0x0002AB04
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
