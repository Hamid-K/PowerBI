using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EEA RID: 7914
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBBINDING
	{
		// Token: 0x04006429 RID: 25641
		public DBORDINAL iOrdinal;

		// Token: 0x0400642A RID: 25642
		public DBBYTEOFFSET obValue;

		// Token: 0x0400642B RID: 25643
		public DBBYTEOFFSET obLength;

		// Token: 0x0400642C RID: 25644
		public DBBYTEOFFSET obStatus;

		// Token: 0x0400642D RID: 25645
		public unsafe void* pTypeInfo;

		// Token: 0x0400642E RID: 25646
		public unsafe void* pObject;

		// Token: 0x0400642F RID: 25647
		public unsafe void* pBindExt;

		// Token: 0x04006430 RID: 25648
		public DBPART dwPart;

		// Token: 0x04006431 RID: 25649
		public DBMEMOWNER dwMemOwner;

		// Token: 0x04006432 RID: 25650
		public DBPARAMIO eParamIO;

		// Token: 0x04006433 RID: 25651
		public DBLENGTH cbMaxLen;

		// Token: 0x04006434 RID: 25652
		public uint dwFlags;

		// Token: 0x04006435 RID: 25653
		public DBTYPE wType;

		// Token: 0x04006436 RID: 25654
		public byte bPrecision;

		// Token: 0x04006437 RID: 25655
		public byte bScale;
	}
}
