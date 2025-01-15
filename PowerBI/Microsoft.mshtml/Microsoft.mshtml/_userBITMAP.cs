using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CA9 RID: 3241
	[ComConversionLoss]
	[StructLayout(0, Pack = 4)]
	public struct _userBITMAP
	{
		// Token: 0x040003DF RID: 991
		public int bmType;

		// Token: 0x040003E0 RID: 992
		public int bmWidth;

		// Token: 0x040003E1 RID: 993
		public int bmHeight;

		// Token: 0x040003E2 RID: 994
		public int bmWidthBytes;

		// Token: 0x040003E3 RID: 995
		public ushort bmPlanes;

		// Token: 0x040003E4 RID: 996
		public ushort bmBitsPixel;

		// Token: 0x040003E5 RID: 997
		public uint cbSize;

		// Token: 0x040003E6 RID: 998
		[ComConversionLoss]
		public IntPtr pBuffer;
	}
}
