using System;

namespace NLog.Targets
{
	// Token: 0x0200005D RID: 93
	[Flags]
	public enum Win32FileAttributes
	{
		// Token: 0x040001BA RID: 442
		ReadOnly = 1,
		// Token: 0x040001BB RID: 443
		Hidden = 2,
		// Token: 0x040001BC RID: 444
		System = 4,
		// Token: 0x040001BD RID: 445
		Archive = 32,
		// Token: 0x040001BE RID: 446
		Device = 64,
		// Token: 0x040001BF RID: 447
		Normal = 128,
		// Token: 0x040001C0 RID: 448
		Temporary = 256,
		// Token: 0x040001C1 RID: 449
		SparseFile = 512,
		// Token: 0x040001C2 RID: 450
		ReparsePoint = 1024,
		// Token: 0x040001C3 RID: 451
		Compressed = 2048,
		// Token: 0x040001C4 RID: 452
		NotContentIndexed = 8192,
		// Token: 0x040001C5 RID: 453
		Encrypted = 16384,
		// Token: 0x040001C6 RID: 454
		WriteThrough = -2147483648,
		// Token: 0x040001C7 RID: 455
		NoBuffering = 536870912,
		// Token: 0x040001C8 RID: 456
		DeleteOnClose = 67108864,
		// Token: 0x040001C9 RID: 457
		PosixSemantics = 16777216
	}
}
