using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011E0 RID: 4576
	internal enum JoinOperator
	{
		// Token: 0x040041C4 RID: 16836
		InnerJoin = 1,
		// Token: 0x040041C5 RID: 16837
		LeftOuterJoin,
		// Token: 0x040041C6 RID: 16838
		RightOuterJoin,
		// Token: 0x040041C7 RID: 16839
		FullOuterJoin,
		// Token: 0x040041C8 RID: 16840
		CrossJoin,
		// Token: 0x040041C9 RID: 16841
		CrossApply
	}
}
