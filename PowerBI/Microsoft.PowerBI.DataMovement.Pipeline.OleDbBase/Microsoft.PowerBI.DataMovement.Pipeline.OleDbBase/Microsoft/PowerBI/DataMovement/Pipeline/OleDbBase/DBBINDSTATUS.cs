using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000060 RID: 96
	[CLSCompliant(false)]
	public enum DBBINDSTATUS : uint
	{
		// Token: 0x04000259 RID: 601
		OK,
		// Token: 0x0400025A RID: 602
		BADORDINAL,
		// Token: 0x0400025B RID: 603
		UNSUPPORTEDCONVERSION,
		// Token: 0x0400025C RID: 604
		BADBINDINFO,
		// Token: 0x0400025D RID: 605
		BADSTORAGEFLAGS,
		// Token: 0x0400025E RID: 606
		NOINTERFACE,
		// Token: 0x0400025F RID: 607
		MULTIPLESTORAGE
	}
}
