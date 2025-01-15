using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000167 RID: 359
	public sealed class AllToken : LambdaToken
	{
		// Token: 0x06000F47 RID: 3911 RVA: 0x0002BB85 File Offset: 0x00029D85
		public AllToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0002BB90 File Offset: 0x00029D90
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.All;
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0002BB94 File Offset: 0x00029D94
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
