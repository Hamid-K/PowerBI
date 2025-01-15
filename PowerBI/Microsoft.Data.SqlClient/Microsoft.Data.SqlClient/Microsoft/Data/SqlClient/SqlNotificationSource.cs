using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200008B RID: 139
	public enum SqlNotificationSource
	{
		// Token: 0x040002EE RID: 750
		Data,
		// Token: 0x040002EF RID: 751
		Timeout,
		// Token: 0x040002F0 RID: 752
		Object,
		// Token: 0x040002F1 RID: 753
		Database,
		// Token: 0x040002F2 RID: 754
		System,
		// Token: 0x040002F3 RID: 755
		Statement,
		// Token: 0x040002F4 RID: 756
		Environment,
		// Token: 0x040002F5 RID: 757
		Execution,
		// Token: 0x040002F6 RID: 758
		Owner,
		// Token: 0x040002F7 RID: 759
		Unknown = -1,
		// Token: 0x040002F8 RID: 760
		Client = -2
	}
}
