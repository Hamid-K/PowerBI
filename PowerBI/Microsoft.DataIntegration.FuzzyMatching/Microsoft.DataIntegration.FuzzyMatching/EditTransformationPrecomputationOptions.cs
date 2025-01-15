using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200003A RID: 58
	public enum EditTransformationPrecomputationOptions
	{
		// Token: 0x04000093 RID: 147
		DoNotUsePrecomputedEditTransformations,
		// Token: 0x04000094 RID: 148
		PrecomputeButDoNotPersistToSql,
		// Token: 0x04000095 RID: 149
		LoadFromSqlIfPresentOtherwisePrecomputeAndWriteToSql,
		// Token: 0x04000096 RID: 150
		LoadFromSqlIfPresentOtherwiseFail,
		// Token: 0x04000097 RID: 151
		PrecomputeAndOverwiteAnyExistingPrecomputationTableInSql
	}
}
