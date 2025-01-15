using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CE4 RID: 3300
	[ComConversionLoss]
	[StructLayout(0, Pack = 4)]
	public struct _HTML_PAINT_DRAW_INFO
	{
		// Token: 0x04000428 RID: 1064
		public tagRECT rcViewport;

		// Token: 0x04000429 RID: 1065
		[ComConversionLoss]
		public IntPtr hrgnUpdate;

		// Token: 0x0400042A RID: 1066
		public _HTML_PAINT_XFORM xform;
	}
}
