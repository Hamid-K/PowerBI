using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000155 RID: 341
	internal enum IsolationLevel
	{
		// Token: 0x04001869 RID: 6249
		None,
		// Token: 0x0400186A RID: 6250
		ReadCommitted,
		// Token: 0x0400186B RID: 6251
		ReadUncommitted,
		// Token: 0x0400186C RID: 6252
		RepeatableRead,
		// Token: 0x0400186D RID: 6253
		Serializable,
		// Token: 0x0400186E RID: 6254
		Snapshot
	}
}
