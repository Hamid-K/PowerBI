using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EEE RID: 7918
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBTIME2
	{
		// Token: 0x04006443 RID: 25667
		public ushort hour;

		// Token: 0x04006444 RID: 25668
		public ushort minute;

		// Token: 0x04006445 RID: 25669
		public ushort second;

		// Token: 0x04006446 RID: 25670
		public uint fraction;
	}
}
