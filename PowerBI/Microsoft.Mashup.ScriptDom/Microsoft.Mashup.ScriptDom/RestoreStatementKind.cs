using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000125 RID: 293
	[Serializable]
	internal enum RestoreStatementKind
	{
		// Token: 0x04001136 RID: 4406
		None,
		// Token: 0x04001137 RID: 4407
		Database,
		// Token: 0x04001138 RID: 4408
		TransactionLog,
		// Token: 0x04001139 RID: 4409
		FileListOnly,
		// Token: 0x0400113A RID: 4410
		VerifyOnly,
		// Token: 0x0400113B RID: 4411
		LabelOnly,
		// Token: 0x0400113C RID: 4412
		RewindOnly,
		// Token: 0x0400113D RID: 4413
		HeaderOnly
	}
}
