using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000107 RID: 263
	internal sealed class SqlLoginAck
	{
		// Token: 0x04000874 RID: 2164
		internal string programName;

		// Token: 0x04000875 RID: 2165
		internal byte majorVersion;

		// Token: 0x04000876 RID: 2166
		internal byte minorVersion;

		// Token: 0x04000877 RID: 2167
		internal short buildNum;

		// Token: 0x04000878 RID: 2168
		internal bool isVersion8;

		// Token: 0x04000879 RID: 2169
		internal uint tdsVersion;
	}
}
