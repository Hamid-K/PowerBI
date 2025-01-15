using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE5 RID: 7909
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROPINFOSET
	{
		// Token: 0x04006414 RID: 25620
		public unsafe DBPROPINFO* propertyInfos;

		// Token: 0x04006415 RID: 25621
		public uint countPropertyInfos;

		// Token: 0x04006416 RID: 25622
		public Guid guidPropertySet;
	}
}
