using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000198 RID: 408
	public class AsyncWatchdogResult<T>
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00024060 File Offset: 0x00022260
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00024068 File Offset: 0x00022268
		public bool IsTimedOut { get; private set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00024071 File Offset: 0x00022271
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00024079 File Offset: 0x00022279
		public T Result { get; private set; }

		// Token: 0x06000A7C RID: 2684 RVA: 0x00024082 File Offset: 0x00022282
		public AsyncWatchdogResult(bool isTimedOut, T result)
		{
			this.IsTimedOut = isTimedOut;
			this.Result = result;
		}
	}
}
