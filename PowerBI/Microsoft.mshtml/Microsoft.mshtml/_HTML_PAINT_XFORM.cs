using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CE3 RID: 3299
	[StructLayout(0, Pack = 4)]
	public struct _HTML_PAINT_XFORM
	{
		// Token: 0x04000422 RID: 1058
		public float eM11;

		// Token: 0x04000423 RID: 1059
		public float eM12;

		// Token: 0x04000424 RID: 1060
		public float eM21;

		// Token: 0x04000425 RID: 1061
		public float eM22;

		// Token: 0x04000426 RID: 1062
		public float eDx;

		// Token: 0x04000427 RID: 1063
		public float eDy;
	}
}
