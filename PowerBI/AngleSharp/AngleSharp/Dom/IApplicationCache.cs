using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200017A RID: 378
	[DomName("ApplicationCache")]
	public interface IApplicationCache : IEventTarget
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D93 RID: 3475
		[DomName("status")]
		CacheStatus Status { get; }

		// Token: 0x06000D94 RID: 3476
		[DomName("update")]
		void Update();

		// Token: 0x06000D95 RID: 3477
		[DomName("abort")]
		void Abort();

		// Token: 0x06000D96 RID: 3478
		[DomName("swapCache")]
		void Swap();

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06000D97 RID: 3479
		// (remove) Token: 0x06000D98 RID: 3480
		[DomName("onchecking")]
		event DomEventHandler Checking;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06000D99 RID: 3481
		// (remove) Token: 0x06000D9A RID: 3482
		[DomName("onerror")]
		event DomEventHandler Error;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06000D9B RID: 3483
		// (remove) Token: 0x06000D9C RID: 3484
		[DomName("onnoupdate")]
		event DomEventHandler NoUpdate;

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06000D9D RID: 3485
		// (remove) Token: 0x06000D9E RID: 3486
		[DomName("ondownloading")]
		event DomEventHandler Downloading;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06000D9F RID: 3487
		// (remove) Token: 0x06000DA0 RID: 3488
		[DomName("onprogress")]
		event DomEventHandler Progress;

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06000DA1 RID: 3489
		// (remove) Token: 0x06000DA2 RID: 3490
		[DomName("onupdateready")]
		event DomEventHandler UpdateReady;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06000DA3 RID: 3491
		// (remove) Token: 0x06000DA4 RID: 3492
		[DomName("oncached")]
		event DomEventHandler Cached;

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06000DA5 RID: 3493
		// (remove) Token: 0x06000DA6 RID: 3494
		[DomName("onobsolete")]
		event DomEventHandler Obsolete;
	}
}
