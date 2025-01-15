using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CE2 RID: 3298
	[StructLayout(0, Pack = 4)]
	public struct _HTML_PAINTER_INFO
	{
		// Token: 0x0400041E RID: 1054
		public int lFlags;

		// Token: 0x0400041F RID: 1055
		public int lZOrder;

		// Token: 0x04000420 RID: 1056
		public Guid iidDrawObject;

		// Token: 0x04000421 RID: 1057
		public tagRECT rcExpand;
	}
}
