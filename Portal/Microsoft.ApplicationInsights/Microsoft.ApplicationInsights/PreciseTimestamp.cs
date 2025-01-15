using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x02000023 RID: 35
	internal class PreciseTimestamp
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00006F34 File Offset: 0x00005134
		public static DateTimeOffset GetUtcNow()
		{
			PreciseTimestamp.TimeSync timeSync = PreciseTimestamp.timeSync;
			long num = (long)((double)(Stopwatch.GetTimestamp() - timeSync.SyncStopwatchTicks) * PreciseTimestamp.StopwatchTicksToTimeSpanTicks);
			return timeSync.SyncUtcNow.AddTicks(num);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006F6B File Offset: 0x0000516B
		private static void Sync()
		{
			Thread.Sleep(1);
			PreciseTimestamp.timeSync = new PreciseTimestamp.TimeSync();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006F80 File Offset: 0x00005180
		private static Timer InitializeSyncTimer()
		{
			bool flag = false;
			Timer timer;
			try
			{
				if (!ExecutionContext.IsFlowSuppressed())
				{
					ExecutionContext.SuppressFlow();
					flag = true;
				}
				timer = new Timer(delegate(object s)
				{
					PreciseTimestamp.Sync();
				}, null, 0, 7200000);
			}
			finally
			{
				if (flag)
				{
					ExecutionContext.RestoreFlow();
				}
			}
			return timer;
		}

		// Token: 0x04000093 RID: 147
		internal static readonly double StopwatchTicksToTimeSpanTicks = 10000000.0 / (double)Stopwatch.Frequency;

		// Token: 0x04000094 RID: 148
		private static readonly Timer SyncTimeUpdater = PreciseTimestamp.InitializeSyncTimer();

		// Token: 0x04000095 RID: 149
		private static PreciseTimestamp.TimeSync timeSync = new PreciseTimestamp.TimeSync();

		// Token: 0x020000EA RID: 234
		private class TimeSync
		{
			// Token: 0x04000347 RID: 839
			public readonly DateTimeOffset SyncUtcNow = DateTimeOffset.UtcNow;

			// Token: 0x04000348 RID: 840
			public readonly long SyncStopwatchTicks = Stopwatch.GetTimestamp();
		}
	}
}
