using System;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200007E RID: 126
	[Obsolete("This class will be removed in the next major version. Application Insights base library wouldn't provide this functionality any longer.")]
	public class TaskTimer : IDisposable
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00011D48 File Offset: 0x0000FF48
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00011D55 File Offset: 0x0000FF55
		public TimeSpan Delay
		{
			get
			{
				return this.internalTimer.Delay;
			}
			set
			{
				this.internalTimer.Delay = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00011D63 File Offset: 0x0000FF63
		public bool IsStarted
		{
			get
			{
				return this.internalTimer.IsStarted;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00011D70 File Offset: 0x0000FF70
		public void Start(Func<Task> elapsed)
		{
			this.internalTimer.Start(elapsed);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00011D7E File Offset: 0x0000FF7E
		public void Cancel()
		{
			this.internalTimer.Cancel();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00011D8B File Offset: 0x0000FF8B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00011D9A File Offset: 0x0000FF9A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.internalTimer.Dispose();
			}
		}

		// Token: 0x04000199 RID: 409
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x0400019A RID: 410
		private TaskTimerInternal internalTimer = new TaskTimerInternal();
	}
}
