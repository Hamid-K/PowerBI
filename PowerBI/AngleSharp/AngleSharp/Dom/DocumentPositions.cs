using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000169 RID: 361
	[Flags]
	[DomName("Document")]
	public enum DocumentPositions : byte
	{
		// Token: 0x0400098A RID: 2442
		Same = 0,
		// Token: 0x0400098B RID: 2443
		[DomName("DOCUMENT_POSITION_DISCONNECTED")]
		Disconnected = 1,
		// Token: 0x0400098C RID: 2444
		[DomName("DOCUMENT_POSITION_PRECEDING")]
		Preceding = 2,
		// Token: 0x0400098D RID: 2445
		[DomName("DOCUMENT_POSITION_FOLLOWING")]
		Following = 4,
		// Token: 0x0400098E RID: 2446
		[DomName("DOCUMENT_POSITION_CONTAINS")]
		Contains = 8,
		// Token: 0x0400098F RID: 2447
		[DomName("DOCUMENT_POSITION_CONTAINED_BY")]
		ContainedBy = 16,
		// Token: 0x04000990 RID: 2448
		[DomName("DOCUMENT_POSITION_IMPLEMENTATION_SPECIFIC")]
		ImplementationSpecific = 32
	}
}
