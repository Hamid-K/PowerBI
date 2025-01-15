using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000059 RID: 89
	[CLSCompliant(false)]
	public enum DBPARAMFLAGS : uint
	{
		// Token: 0x040001F8 RID: 504
		DBPARAMFLAGS_ISINPUT = 1U,
		// Token: 0x040001F9 RID: 505
		DBPARAMFLAGS_ISOUTPUT,
		// Token: 0x040001FA RID: 506
		DBPARAMFLAGS_ISSIGNED = 16U,
		// Token: 0x040001FB RID: 507
		DBPARAMFLAGS_ISNULLABLE = 64U,
		// Token: 0x040001FC RID: 508
		DBPARAMFLAGS_ISLONG = 128U,
		// Token: 0x040001FD RID: 509
		DBPARAMFLAGS_SCALEISNEGATIVE = 256U
	}
}
