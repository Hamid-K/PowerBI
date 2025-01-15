using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EED RID: 7917
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DCINFO
	{
		// Token: 0x04006441 RID: 25665
		public DCINFOTYPE eInfoType;

		// Token: 0x04006442 RID: 25666
		public VARIANT vData;
	}
}
