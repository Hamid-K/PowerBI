using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x020003A1 RID: 929
	public class EventFilesDownloadResult
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0006C330 File Offset: 0x0006A530
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x0006C338 File Offset: 0x0006A538
		public IEnumerable<string> Paths { get; private set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0006C341 File Offset: 0x0006A541
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x0006C349 File Offset: 0x0006A549
		public IEventingRepositoryContinuation Continuation { get; private set; }

		// Token: 0x06001C82 RID: 7298 RVA: 0x0006C352 File Offset: 0x0006A552
		public EventFilesDownloadResult(IEnumerable<string> paths, IEventingRepositoryContinuation continuation)
		{
			this.Paths = paths;
			this.Continuation = continuation;
		}
	}
}
