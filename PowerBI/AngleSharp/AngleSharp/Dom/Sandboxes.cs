using System;

namespace AngleSharp.Dom
{
	// Token: 0x02000173 RID: 371
	[Flags]
	public enum Sandboxes : ushort
	{
		// Token: 0x040009E1 RID: 2529
		None = 0,
		// Token: 0x040009E2 RID: 2530
		Navigation = 1,
		// Token: 0x040009E3 RID: 2531
		AuxiliaryNavigation = 2,
		// Token: 0x040009E4 RID: 2532
		TopLevelNavigation = 4,
		// Token: 0x040009E5 RID: 2533
		Plugins = 8,
		// Token: 0x040009E6 RID: 2534
		Origin = 16,
		// Token: 0x040009E7 RID: 2535
		Forms = 32,
		// Token: 0x040009E8 RID: 2536
		PointerLock = 64,
		// Token: 0x040009E9 RID: 2537
		Scripts = 128,
		// Token: 0x040009EA RID: 2538
		AutomaticFeatures = 256,
		// Token: 0x040009EB RID: 2539
		Fullscreen = 512,
		// Token: 0x040009EC RID: 2540
		DocumentDomain = 1024
	}
}
