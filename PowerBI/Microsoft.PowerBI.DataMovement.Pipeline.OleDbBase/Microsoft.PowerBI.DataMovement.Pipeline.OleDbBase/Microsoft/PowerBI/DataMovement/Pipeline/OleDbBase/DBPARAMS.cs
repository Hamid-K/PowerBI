using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A7 RID: 167
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPARAMS
	{
		// Token: 0x0400031D RID: 797
		public unsafe void* Data;

		// Token: 0x0400031E RID: 798
		public ulong ParamSets;

		// Token: 0x0400031F RID: 799
		public HACCESSOR Accessor;
	}
}
