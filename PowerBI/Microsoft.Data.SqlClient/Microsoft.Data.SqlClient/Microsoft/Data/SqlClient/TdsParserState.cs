using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000100 RID: 256
	internal enum TdsParserState
	{
		// Token: 0x04000847 RID: 2119
		Closed,
		// Token: 0x04000848 RID: 2120
		OpenNotLoggedIn,
		// Token: 0x04000849 RID: 2121
		OpenLoggedIn,
		// Token: 0x0400084A RID: 2122
		Broken
	}
}
