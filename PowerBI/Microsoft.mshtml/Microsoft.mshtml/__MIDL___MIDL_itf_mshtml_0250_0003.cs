using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CA1 RID: 3233
	[StructLayout(0, Pack = 4)]
	public struct __MIDL___MIDL_itf_mshtml_0250_0003
	{
		// Token: 0x040003B1 RID: 945
		public int lfHeight;

		// Token: 0x040003B2 RID: 946
		public int lfWidth;

		// Token: 0x040003B3 RID: 947
		public int lfEscapement;

		// Token: 0x040003B4 RID: 948
		public int lfOrientation;

		// Token: 0x040003B5 RID: 949
		public int lfWeight;

		// Token: 0x040003B6 RID: 950
		public byte lfItalic;

		// Token: 0x040003B7 RID: 951
		public byte lfUnderline;

		// Token: 0x040003B8 RID: 952
		public byte lfStrikeOut;

		// Token: 0x040003B9 RID: 953
		public byte lfCharSet;

		// Token: 0x040003BA RID: 954
		public byte lfOutPrecision;

		// Token: 0x040003BB RID: 955
		public byte lfClipPrecision;

		// Token: 0x040003BC RID: 956
		public byte lfQuality;

		// Token: 0x040003BD RID: 957
		public byte lfPitchAndFamily;

		// Token: 0x040003BE RID: 958
		[MarshalAs(30, SizeConst = 32)]
		public byte[] lfFaceName;
	}
}
