using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CBF RID: 3263
	[ComConversionLoss]
	[StructLayout(0, Pack = 4)]
	public struct _FLAGGED_BYTE_BLOB
	{
		// Token: 0x040003F2 RID: 1010
		public uint fFlags;

		// Token: 0x040003F3 RID: 1011
		public uint clSize;

		// Token: 0x040003F4 RID: 1012
		[ComConversionLoss]
		public IntPtr abData;
	}
}
