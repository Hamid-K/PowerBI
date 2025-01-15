using System;

namespace System.Text.Json
{
	// Token: 0x0200004C RID: 76
	internal enum PolymorphicSerializationState : byte
	{
		// Token: 0x0400019F RID: 415
		None,
		// Token: 0x040001A0 RID: 416
		PolymorphicReEntryStarted,
		// Token: 0x040001A1 RID: 417
		PolymorphicReEntrySuspended,
		// Token: 0x040001A2 RID: 418
		PolymorphicReEntryNotFound
	}
}
