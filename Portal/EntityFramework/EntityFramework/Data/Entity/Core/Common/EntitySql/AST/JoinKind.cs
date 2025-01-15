using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000689 RID: 1673
	internal enum JoinKind
	{
		// Token: 0x04001CE8 RID: 7400
		Cross,
		// Token: 0x04001CE9 RID: 7401
		Inner,
		// Token: 0x04001CEA RID: 7402
		LeftOuter,
		// Token: 0x04001CEB RID: 7403
		FullOuter,
		// Token: 0x04001CEC RID: 7404
		RightOuter
	}
}
