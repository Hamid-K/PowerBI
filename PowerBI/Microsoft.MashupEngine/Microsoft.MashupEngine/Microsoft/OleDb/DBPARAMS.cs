using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EEC RID: 7916
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPARAMS
	{
		// Token: 0x0400643E RID: 25662
		public unsafe void* pData;

		// Token: 0x0400643F RID: 25663
		public ulong cParamSets;

		// Token: 0x04006440 RID: 25664
		public HACCESSOR hAccessor;
	}
}
