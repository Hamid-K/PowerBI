using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000067 RID: 103
	[CLSCompliant(false)]
	public enum DBCONVERTFLAGS : uint
	{
		// Token: 0x04000296 RID: 662
		DBCONVERTFLAGS_COLUMN,
		// Token: 0x04000297 RID: 663
		DBCONVERTFLAGS_PARAMETER,
		// Token: 0x04000298 RID: 664
		DBCONVERTFLAGS_ISLONG,
		// Token: 0x04000299 RID: 665
		DBCONVERTFLAGS_ISFIXEDLENGTH = 4U,
		// Token: 0x0400029A RID: 666
		DBCONVERTFLAGS_FROMVARIANT = 8U
	}
}
