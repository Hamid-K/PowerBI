using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200015C RID: 348
	public class ScheduledTaskInformation
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x0001F5A1 File Offset: 0x0001D7A1
		public ScheduledTaskInformation()
		{
			this.SuccessfulExecutionsCount = 0;
			this.FailedExecutionsCount = 0;
			this.m_totalExecutionDuration = default(TimeSpan);
			this.LastExecutionResult = ScheduledTaskResult.Skipped;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001F5CA File Offset: 0x0001D7CA
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0001F5D2 File Offset: 0x0001D7D2
		public ScheduledTaskResult LastExecutionResult { get; private set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001F5DB File Offset: 0x0001D7DB
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x0001F5E3 File Offset: 0x0001D7E3
		public DateTime LastExecutionStartTime { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0001F5EC File Offset: 0x0001D7EC
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
		public TimeSpan LastExecutionDuration { get; private set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001F600 File Offset: 0x0001D800
		public TimeSpan AverageExecutionDuration
		{
			get
			{
				int num = this.SuccessfulExecutionsCount + this.FailedExecutionsCount;
				return new TimeSpan(this.m_totalExecutionDuration.Ticks / (long)num);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001F62E File Offset: 0x0001D82E
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0001F636 File Offset: 0x0001D836
		public int SuccessfulExecutionsCount { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0001F63F File Offset: 0x0001D83F
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x0001F647 File Offset: 0x0001D847
		public int FailedExecutionsCount { get; private set; }

		// Token: 0x06000904 RID: 2308 RVA: 0x0001F650 File Offset: 0x0001D850
		internal void AppendLastExecutionInfo(DateTime start, TimeSpan duration, ScheduledTaskResult result)
		{
			if (result != ScheduledTaskResult.Skipped)
			{
				this.LastExecutionStartTime = start;
				this.LastExecutionDuration = duration;
				this.m_totalExecutionDuration = this.m_totalExecutionDuration.Add(duration);
				int num;
				if (result == ScheduledTaskResult.Succeeded)
				{
					num = this.SuccessfulExecutionsCount + 1;
					this.SuccessfulExecutionsCount = num;
					return;
				}
				num = this.FailedExecutionsCount + 1;
				this.FailedExecutionsCount = num;
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "<Last result: {0}, Last run time: {1}, Last duration: {2}, Average duration: {3},  Successes: {4}, Failures: {5}>", new object[] { this.LastExecutionResult, this.LastExecutionStartTime, this.LastExecutionDuration, this.AverageExecutionDuration, this.SuccessfulExecutionsCount, this.FailedExecutionsCount });
		}

		// Token: 0x04000371 RID: 881
		private TimeSpan m_totalExecutionDuration;
	}
}
