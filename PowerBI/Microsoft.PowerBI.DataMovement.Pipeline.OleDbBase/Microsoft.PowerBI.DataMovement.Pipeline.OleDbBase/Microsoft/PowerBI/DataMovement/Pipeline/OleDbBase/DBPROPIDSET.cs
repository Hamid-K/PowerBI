using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000099 RID: 153
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROPIDSET
	{
		// Token: 0x040002CF RID: 719
		public unsafe DBPROPID* PropertyIDs;

		// Token: 0x040002D0 RID: 720
		public uint PropertyIDCount;

		// Token: 0x040002D1 RID: 721
		public Guid PropertySet;
	}
}
