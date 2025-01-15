using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B5 RID: 437
	public sealed class AllToken : LambdaToken
	{
		// Token: 0x0600147F RID: 5247 RVA: 0x0003BC41 File Offset: 0x00039E41
		public AllToken(QueryToken expression, string parameter, QueryToken parent)
			: base(expression, parameter, parent)
		{
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00038D0A File Offset: 0x00036F0A
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.All;
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0003BC4C File Offset: 0x00039E4C
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
