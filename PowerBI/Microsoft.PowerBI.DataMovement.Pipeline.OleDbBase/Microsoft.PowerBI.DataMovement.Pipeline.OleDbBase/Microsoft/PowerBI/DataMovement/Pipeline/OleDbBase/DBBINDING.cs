using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A5 RID: 165
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBBINDING
	{
		// Token: 0x04000308 RID: 776
		public DBORDINAL Ordinal;

		// Token: 0x04000309 RID: 777
		public DBBYTEOFFSET Value;

		// Token: 0x0400030A RID: 778
		public DBBYTEOFFSET Length;

		// Token: 0x0400030B RID: 779
		public DBBYTEOFFSET Status;

		// Token: 0x0400030C RID: 780
		public unsafe void* TypeInfo;

		// Token: 0x0400030D RID: 781
		public unsafe void* Object;

		// Token: 0x0400030E RID: 782
		public unsafe void* BindExt;

		// Token: 0x0400030F RID: 783
		public DBPART Part;

		// Token: 0x04000310 RID: 784
		public DBMEMOWNER MemOwner;

		// Token: 0x04000311 RID: 785
		public DBPARAMIO ParamIO;

		// Token: 0x04000312 RID: 786
		public DBLENGTH MaxLen;

		// Token: 0x04000313 RID: 787
		public uint Flags;

		// Token: 0x04000314 RID: 788
		public DBTYPE Type;

		// Token: 0x04000315 RID: 789
		public byte Precision;

		// Token: 0x04000316 RID: 790
		public byte Scale;
	}
}
