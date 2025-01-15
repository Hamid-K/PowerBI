using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000065 RID: 101
	[CLSCompliant(false)]
	public enum DBDATACONVERT : uint
	{
		// Token: 0x0400028E RID: 654
		DEFAULT,
		// Token: 0x0400028F RID: 655
		SETDATABEHAVIOR,
		// Token: 0x04000290 RID: 656
		LENGTHFROMNTS,
		// Token: 0x04000291 RID: 657
		DSTISFIXEDLENGTH = 4U,
		// Token: 0x04000292 RID: 658
		DECIMALSCALE = 8U
	}
}
