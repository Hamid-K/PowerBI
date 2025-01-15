using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000233 RID: 563
	public enum RetentionActionType
	{
		// Token: 0x04000F58 RID: 3928
		None,
		// Token: 0x04000F59 RID: 3929
		MoveToDeletedItems,
		// Token: 0x04000F5A RID: 3930
		MoveToFolder,
		// Token: 0x04000F5B RID: 3931
		DeleteAndAllowRecovery,
		// Token: 0x04000F5C RID: 3932
		PermanentlyDelete,
		// Token: 0x04000F5D RID: 3933
		MarkAsPastRetentionLimit,
		// Token: 0x04000F5E RID: 3934
		MoveToArchive
	}
}
