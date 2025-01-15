using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200009D RID: 157
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPARAMINFO
	{
		// Token: 0x040002DF RID: 735
		public DBPARAMFLAGS Flags;

		// Token: 0x040002E0 RID: 736
		public DBORDINAL Ordinal;

		// Token: 0x040002E1 RID: 737
		public unsafe char* Name;

		// Token: 0x040002E2 RID: 738
		public unsafe void* TypeInfo;

		// Token: 0x040002E3 RID: 739
		public DBLENGTH ParamSize;

		// Token: 0x040002E4 RID: 740
		public DBTYPE Type;

		// Token: 0x040002E5 RID: 741
		public byte Precision;

		// Token: 0x040002E6 RID: 742
		public byte Scale;
	}
}
