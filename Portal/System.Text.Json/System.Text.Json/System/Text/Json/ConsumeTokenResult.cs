using System;

namespace System.Text.Json
{
	// Token: 0x02000043 RID: 67
	internal enum ConsumeTokenResult : byte
	{
		// Token: 0x04000157 RID: 343
		Success,
		// Token: 0x04000158 RID: 344
		NotEnoughDataRollBackState,
		// Token: 0x04000159 RID: 345
		IncompleteNoRollBackNecessary
	}
}
