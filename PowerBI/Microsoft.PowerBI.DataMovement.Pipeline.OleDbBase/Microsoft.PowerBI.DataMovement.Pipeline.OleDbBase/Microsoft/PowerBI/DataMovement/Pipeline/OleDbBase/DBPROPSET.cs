using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200009B RID: 155
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROPSET
	{
		// Token: 0x040002D7 RID: 727
		public unsafe DBPROP* Properties;

		// Token: 0x040002D8 RID: 728
		public uint PropertyCount;

		// Token: 0x040002D9 RID: 729
		public Guid PropertySet;
	}
}
