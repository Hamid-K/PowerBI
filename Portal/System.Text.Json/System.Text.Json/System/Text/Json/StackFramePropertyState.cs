using System;

namespace System.Text.Json
{
	// Token: 0x02000054 RID: 84
	internal enum StackFramePropertyState : byte
	{
		// Token: 0x040001FC RID: 508
		None,
		// Token: 0x040001FD RID: 509
		ReadName,
		// Token: 0x040001FE RID: 510
		Name,
		// Token: 0x040001FF RID: 511
		ReadValue,
		// Token: 0x04000200 RID: 512
		ReadValueIsEnd,
		// Token: 0x04000201 RID: 513
		TryRead
	}
}
