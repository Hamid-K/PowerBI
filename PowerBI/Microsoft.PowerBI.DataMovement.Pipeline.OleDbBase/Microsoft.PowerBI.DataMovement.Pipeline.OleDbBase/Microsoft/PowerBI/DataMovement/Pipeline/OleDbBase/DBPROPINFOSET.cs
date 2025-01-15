using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200009F RID: 159
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROPINFOSET
	{
		// Token: 0x040002ED RID: 749
		public unsafe DBPROPINFO* PropertyInfos;

		// Token: 0x040002EE RID: 750
		public uint CountPropertyInfos;

		// Token: 0x040002EF RID: 751
		public Guid GuidPropertySet;
	}
}
