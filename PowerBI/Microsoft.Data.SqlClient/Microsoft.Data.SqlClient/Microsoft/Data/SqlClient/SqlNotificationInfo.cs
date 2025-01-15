using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200008A RID: 138
	public enum SqlNotificationInfo
	{
		// Token: 0x040002DA RID: 730
		Truncate,
		// Token: 0x040002DB RID: 731
		Insert,
		// Token: 0x040002DC RID: 732
		Update,
		// Token: 0x040002DD RID: 733
		Delete,
		// Token: 0x040002DE RID: 734
		Drop,
		// Token: 0x040002DF RID: 735
		Alter,
		// Token: 0x040002E0 RID: 736
		Restart,
		// Token: 0x040002E1 RID: 737
		Error,
		// Token: 0x040002E2 RID: 738
		Query,
		// Token: 0x040002E3 RID: 739
		Invalid,
		// Token: 0x040002E4 RID: 740
		Options,
		// Token: 0x040002E5 RID: 741
		Isolation,
		// Token: 0x040002E6 RID: 742
		Expired,
		// Token: 0x040002E7 RID: 743
		Resource,
		// Token: 0x040002E8 RID: 744
		PreviousFire,
		// Token: 0x040002E9 RID: 745
		TemplateLimit,
		// Token: 0x040002EA RID: 746
		Merge,
		// Token: 0x040002EB RID: 747
		Unknown = -1,
		// Token: 0x040002EC RID: 748
		AlreadyChanged = -2
	}
}
