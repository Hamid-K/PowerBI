using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000168 RID: 360
	public sealed class AnyToken : LambdaToken
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x0002BB85 File Offset: 0x00029D85
		public AnyToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0002BB9D File Offset: 0x00029D9D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.Any;
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002BBA1 File Offset: 0x00029DA1
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
