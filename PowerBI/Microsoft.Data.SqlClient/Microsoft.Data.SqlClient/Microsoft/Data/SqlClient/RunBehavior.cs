using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000FF RID: 255
	internal enum RunBehavior
	{
		// Token: 0x04000842 RID: 2114
		UntilDone = 1,
		// Token: 0x04000843 RID: 2115
		ReturnImmediately,
		// Token: 0x04000844 RID: 2116
		Clean = 5,
		// Token: 0x04000845 RID: 2117
		Attention = 13
	}
}
