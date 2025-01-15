using System;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003BB RID: 955
	[CLSCompliant(false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
	public struct PromotedParameterDescriptor
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0007047D File Offset: 0x0006E67D
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x00070485 File Offset: 0x0006E685
		public ushort StringKey1Offset { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0007048E File Offset: 0x0006E68E
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x00070496 File Offset: 0x0006E696
		public ushort ParentActivityOffset { get; set; }
	}
}
