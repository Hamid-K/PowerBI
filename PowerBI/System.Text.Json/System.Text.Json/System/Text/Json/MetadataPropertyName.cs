using System;

namespace System.Text.Json
{
	// Token: 0x02000050 RID: 80
	[Flags]
	internal enum MetadataPropertyName : byte
	{
		// Token: 0x040001CD RID: 461
		None = 0,
		// Token: 0x040001CE RID: 462
		Values = 1,
		// Token: 0x040001CF RID: 463
		Id = 2,
		// Token: 0x040001D0 RID: 464
		Ref = 4,
		// Token: 0x040001D1 RID: 465
		Type = 8
	}
}
