using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE3 RID: 7907
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROPSET
	{
		// Token: 0x0400640C RID: 25612
		public unsafe DBPROP* rgProperties;

		// Token: 0x0400640D RID: 25613
		public uint cProperties;

		// Token: 0x0400640E RID: 25614
		public Guid guidPropertySet;
	}
}
