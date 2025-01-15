using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000EE RID: 238
	internal class SessionStateRecord
	{
		// Token: 0x04000786 RID: 1926
		internal bool _recoverable;

		// Token: 0x04000787 RID: 1927
		internal uint _version;

		// Token: 0x04000788 RID: 1928
		internal int _dataLength;

		// Token: 0x04000789 RID: 1929
		internal byte[] _data;
	}
}
