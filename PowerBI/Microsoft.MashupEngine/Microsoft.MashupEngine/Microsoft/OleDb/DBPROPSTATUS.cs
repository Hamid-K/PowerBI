using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001EC2 RID: 7874
	public enum DBPROPSTATUS : uint
	{
		// Token: 0x0400632F RID: 25391
		OK,
		// Token: 0x04006330 RID: 25392
		NOTSUPPORTED,
		// Token: 0x04006331 RID: 25393
		BADVALUE,
		// Token: 0x04006332 RID: 25394
		BADOPTION,
		// Token: 0x04006333 RID: 25395
		BADCOLUMN,
		// Token: 0x04006334 RID: 25396
		NOTALLSETTABLE,
		// Token: 0x04006335 RID: 25397
		NOTSETTABLE,
		// Token: 0x04006336 RID: 25398
		NOTSET,
		// Token: 0x04006337 RID: 25399
		CONFLICTING
	}
}
