using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CA2 RID: 3234
	[StructLayout(0, Pack = 4)]
	public struct __MIDL___MIDL_itf_mshtml_0250_0004
	{
		// Token: 0x040003BF RID: 959
		public int lfHeight;

		// Token: 0x040003C0 RID: 960
		public int lfWidth;

		// Token: 0x040003C1 RID: 961
		public int lfEscapement;

		// Token: 0x040003C2 RID: 962
		public int lfOrientation;

		// Token: 0x040003C3 RID: 963
		public int lfWeight;

		// Token: 0x040003C4 RID: 964
		public byte lfItalic;

		// Token: 0x040003C5 RID: 965
		public byte lfUnderline;

		// Token: 0x040003C6 RID: 966
		public byte lfStrikeOut;

		// Token: 0x040003C7 RID: 967
		public byte lfCharSet;

		// Token: 0x040003C8 RID: 968
		public byte lfOutPrecision;

		// Token: 0x040003C9 RID: 969
		public byte lfClipPrecision;

		// Token: 0x040003CA RID: 970
		public byte lfQuality;

		// Token: 0x040003CB RID: 971
		public byte lfPitchAndFamily;

		// Token: 0x040003CC RID: 972
		[MarshalAs(30, SizeConst = 32)]
		public ushort[] lfFaceName;
	}
}
