using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE9 RID: 7913
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBBINDEXT
	{
		// Token: 0x04006427 RID: 25639
		public unsafe void* pExtension;

		// Token: 0x04006428 RID: 25640
		public DBCOUNTITEM ulExtension;
	}
}
