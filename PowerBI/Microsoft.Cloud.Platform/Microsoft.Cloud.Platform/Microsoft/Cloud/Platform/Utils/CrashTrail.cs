using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001F5 RID: 501
	public class CrashTrail
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x0002E1E8 File Offset: 0x0002C3E8
		public string Id { get; internal set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0002E1F1 File Offset: 0x0002C3F1
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x0002E1F9 File Offset: 0x0002C3F9
		public CrashScenario CrashScenario { get; internal set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0002E202 File Offset: 0x0002C402
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x0002E20A File Offset: 0x0002C40A
		public string CrashStackTrace { get; internal set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0002E213 File Offset: 0x0002C413
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x0002E21B File Offset: 0x0002C41B
		public object Sender { get; internal set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0002E224 File Offset: 0x0002C424
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0002E22C File Offset: 0x0002C42C
		public CrashEventArgs EventArgs { get; internal set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0002E235 File Offset: 0x0002C435
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0002E23D File Offset: 0x0002C43D
		public TraceDump Dump { get; internal set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0002E246 File Offset: 0x0002C446
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x0002E24E File Offset: 0x0002C44E
		public Exception Exception { get; internal set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0002E257 File Offset: 0x0002C457
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x0002E25F File Offset: 0x0002C45F
		public string ExceptionStackTrace { get; internal set; }
	}
}
