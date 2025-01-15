using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE1 RID: 7905
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROPIDSET
	{
		// Token: 0x04006404 RID: 25604
		public unsafe DBPROPID* rgPropertyIDs;

		// Token: 0x04006405 RID: 25605
		public uint cPropertyIDs;

		// Token: 0x04006406 RID: 25606
		public Guid guidPropertySet;
	}
}
