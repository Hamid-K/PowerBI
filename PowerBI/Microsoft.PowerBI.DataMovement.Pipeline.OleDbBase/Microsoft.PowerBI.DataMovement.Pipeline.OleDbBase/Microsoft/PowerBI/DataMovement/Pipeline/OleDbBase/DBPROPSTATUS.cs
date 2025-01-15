using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000058 RID: 88
	[CLSCompliant(false)]
	public enum DBPROPSTATUS : uint
	{
		// Token: 0x040001EE RID: 494
		OK,
		// Token: 0x040001EF RID: 495
		NOTSUPPORTED,
		// Token: 0x040001F0 RID: 496
		BADVALUE,
		// Token: 0x040001F1 RID: 497
		BADOPTION,
		// Token: 0x040001F2 RID: 498
		BADCOLUMN,
		// Token: 0x040001F3 RID: 499
		NOTALLSETTABLE,
		// Token: 0x040001F4 RID: 500
		NOTSETTABLE,
		// Token: 0x040001F5 RID: 501
		NOTSET,
		// Token: 0x040001F6 RID: 502
		CONFLICTING
	}
}
