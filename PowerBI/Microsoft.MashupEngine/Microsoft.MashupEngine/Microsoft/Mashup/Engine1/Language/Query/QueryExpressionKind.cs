using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200187E RID: 6270
	internal enum QueryExpressionKind
	{
		// Token: 0x04005386 RID: 21382
		Binary,
		// Token: 0x04005387 RID: 21383
		Constant,
		// Token: 0x04005388 RID: 21384
		ColumnAccess,
		// Token: 0x04005389 RID: 21385
		If,
		// Token: 0x0400538A RID: 21386
		Invocation,
		// Token: 0x0400538B RID: 21387
		Unary,
		// Token: 0x0400538C RID: 21388
		ArgumentAccess,
		// Token: 0x0400538D RID: 21389
		Count
	}
}
