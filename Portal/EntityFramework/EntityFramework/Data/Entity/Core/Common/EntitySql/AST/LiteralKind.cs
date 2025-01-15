using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200068C RID: 1676
	internal enum LiteralKind
	{
		// Token: 0x04001CF9 RID: 7417
		Number,
		// Token: 0x04001CFA RID: 7418
		String,
		// Token: 0x04001CFB RID: 7419
		UnicodeString,
		// Token: 0x04001CFC RID: 7420
		Boolean,
		// Token: 0x04001CFD RID: 7421
		Binary,
		// Token: 0x04001CFE RID: 7422
		DateTime,
		// Token: 0x04001CFF RID: 7423
		Time,
		// Token: 0x04001D00 RID: 7424
		DateTimeOffset,
		// Token: 0x04001D01 RID: 7425
		Guid,
		// Token: 0x04001D02 RID: 7426
		Null
	}
}
