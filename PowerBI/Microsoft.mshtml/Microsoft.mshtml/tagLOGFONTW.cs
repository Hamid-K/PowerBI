using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DBC RID: 3516
	[StructLayout(0, Pack = 4)]
	public struct tagLOGFONTW
	{
		// Token: 0x040004AC RID: 1196
		public int lfHeight;

		// Token: 0x040004AD RID: 1197
		public int lfWidth;

		// Token: 0x040004AE RID: 1198
		public int lfEscapement;

		// Token: 0x040004AF RID: 1199
		public int lfOrientation;

		// Token: 0x040004B0 RID: 1200
		public int lfWeight;

		// Token: 0x040004B1 RID: 1201
		public byte lfItalic;

		// Token: 0x040004B2 RID: 1202
		public byte lfUnderline;

		// Token: 0x040004B3 RID: 1203
		public byte lfStrikeOut;

		// Token: 0x040004B4 RID: 1204
		public byte lfCharSet;

		// Token: 0x040004B5 RID: 1205
		public byte lfOutPrecision;

		// Token: 0x040004B6 RID: 1206
		public byte lfClipPrecision;

		// Token: 0x040004B7 RID: 1207
		public byte lfQuality;

		// Token: 0x040004B8 RID: 1208
		public byte lfPitchAndFamily;

		// Token: 0x040004B9 RID: 1209
		[MarshalAs(30, SizeConst = 32)]
		public ushort[] lfFaceName;
	}
}
