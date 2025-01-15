using System;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000060 RID: 96
	public enum DomainModelManagerStatus : long
	{
		// Token: 0x04000128 RID: 296
		Uninitialized,
		// Token: 0x04000129 RID: 297
		FaultedOnVerifyRuntime,
		// Token: 0x0400012A RID: 298
		FaultedDueToUnsupportedLanguage,
		// Token: 0x0400012B RID: 299
		UpdatingDatabaseContext,
		// Token: 0x0400012C RID: 300
		UpdatingDataIndex,
		// Token: 0x0400012D RID: 301
		FailedToUpdateDatabaseContext,
		// Token: 0x0400012E RID: 302
		FailedToUpdateDataIndex,
		// Token: 0x0400012F RID: 303
		StaleDataIndex,
		// Token: 0x04000130 RID: 304
		Ready
	}
}
