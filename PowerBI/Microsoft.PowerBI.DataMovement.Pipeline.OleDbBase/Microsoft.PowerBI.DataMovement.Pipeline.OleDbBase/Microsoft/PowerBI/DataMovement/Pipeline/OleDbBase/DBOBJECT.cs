using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A3 RID: 163
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBOBJECT
	{
		// Token: 0x04000304 RID: 772
		public uint Flags;

		// Token: 0x04000305 RID: 773
		public Guid IId;
	}
}
