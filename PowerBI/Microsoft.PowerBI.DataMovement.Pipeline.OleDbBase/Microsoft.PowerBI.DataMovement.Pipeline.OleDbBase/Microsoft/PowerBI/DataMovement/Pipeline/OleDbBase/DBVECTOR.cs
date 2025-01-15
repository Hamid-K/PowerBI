using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000094 RID: 148
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBVECTOR
	{
		// Token: 0x040002AD RID: 685
		public DBLENGTH Size;

		// Token: 0x040002AE RID: 686
		public unsafe void* Pointer;
	}
}
