using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000166 RID: 358
	[DomName("ApplicationCache")]
	public enum CacheStatus : byte
	{
		// Token: 0x0400097C RID: 2428
		[DomName("UNCACHED")]
		Uncached,
		// Token: 0x0400097D RID: 2429
		[DomName("IDLE")]
		Idle,
		// Token: 0x0400097E RID: 2430
		[DomName("CHECKING")]
		Checking,
		// Token: 0x0400097F RID: 2431
		[DomName("DOWNLOADING")]
		Downloading,
		// Token: 0x04000980 RID: 2432
		[DomName("UPDATEREADY")]
		UpdateReady,
		// Token: 0x04000981 RID: 2433
		[DomName("OBSOLETE")]
		Obsolete
	}
}
