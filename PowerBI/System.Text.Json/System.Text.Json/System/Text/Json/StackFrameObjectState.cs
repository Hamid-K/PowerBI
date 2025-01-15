using System;

namespace System.Text.Json
{
	// Token: 0x02000053 RID: 83
	internal enum StackFrameObjectState : byte
	{
		// Token: 0x040001F3 RID: 499
		None,
		// Token: 0x040001F4 RID: 500
		StartToken,
		// Token: 0x040001F5 RID: 501
		ReadMetadata,
		// Token: 0x040001F6 RID: 502
		ConstructorArguments,
		// Token: 0x040001F7 RID: 503
		CreatedObject,
		// Token: 0x040001F8 RID: 504
		ReadElements,
		// Token: 0x040001F9 RID: 505
		EndToken,
		// Token: 0x040001FA RID: 506
		EndTokenValidation
	}
}
