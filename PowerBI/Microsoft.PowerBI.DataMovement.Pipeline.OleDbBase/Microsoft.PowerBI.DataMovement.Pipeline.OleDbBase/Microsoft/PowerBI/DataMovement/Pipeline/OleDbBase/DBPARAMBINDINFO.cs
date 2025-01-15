using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200009E RID: 158
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPARAMBINDINFO
	{
		// Token: 0x040002E7 RID: 743
		public unsafe char* DataSourceType;

		// Token: 0x040002E8 RID: 744
		public unsafe char* Name;

		// Token: 0x040002E9 RID: 745
		public DBLENGTH ParamSize;

		// Token: 0x040002EA RID: 746
		public DBPARAMFLAGS Flags;

		// Token: 0x040002EB RID: 747
		public byte Precision;

		// Token: 0x040002EC RID: 748
		public byte Scale;
	}
}
