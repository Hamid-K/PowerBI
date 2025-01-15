using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000052 RID: 82
	[Flags]
	public enum PersistentCacheMode
	{
		// Token: 0x04000137 RID: 311
		Disk = 1,
		// Token: 0x04000138 RID: 312
		Memory = 2,
		// Token: 0x04000139 RID: 313
		Hybrid = 3,
		// Token: 0x0400013A RID: 314
		Remote = 4
	}
}
