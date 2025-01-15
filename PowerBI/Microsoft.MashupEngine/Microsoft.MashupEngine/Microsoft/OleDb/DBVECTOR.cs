using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EDC RID: 7900
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBVECTOR
	{
		// Token: 0x040063E2 RID: 25570
		public DBLENGTH size;

		// Token: 0x040063E3 RID: 25571
		public unsafe void* ptr;
	}
}
