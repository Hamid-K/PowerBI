using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000061 RID: 97
	[CLSCompliant(false)]
	public enum DBACCESSORFLAGS : uint
	{
		// Token: 0x04000261 RID: 609
		INVALID,
		// Token: 0x04000262 RID: 610
		PASSBYREF,
		// Token: 0x04000263 RID: 611
		ROWDATA,
		// Token: 0x04000264 RID: 612
		PARAMETERDATA = 4U,
		// Token: 0x04000265 RID: 613
		OPTIMIZED = 8U,
		// Token: 0x04000266 RID: 614
		INHERITED = 16U
	}
}
