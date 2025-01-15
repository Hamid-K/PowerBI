using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A4 RID: 164
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBBINDEXT
	{
		// Token: 0x04000306 RID: 774
		public unsafe void* Extension;

		// Token: 0x04000307 RID: 775
		public DBCOUNTITEM ExtensionCount;
	}
}
