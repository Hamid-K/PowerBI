using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x02000700 RID: 1792
	internal enum XaSwitchType
	{
		// Token: 0x040020FF RID: 8447
		Open,
		// Token: 0x04002100 RID: 8448
		Close,
		// Token: 0x04002101 RID: 8449
		Start,
		// Token: 0x04002102 RID: 8450
		End,
		// Token: 0x04002103 RID: 8451
		Prepare,
		// Token: 0x04002104 RID: 8452
		Commit,
		// Token: 0x04002105 RID: 8453
		Rollback,
		// Token: 0x04002106 RID: 8454
		Recover,
		// Token: 0x04002107 RID: 8455
		Forget,
		// Token: 0x04002108 RID: 8456
		Complete,
		// Token: 0x04002109 RID: 8457
		Destroy
	}
}
