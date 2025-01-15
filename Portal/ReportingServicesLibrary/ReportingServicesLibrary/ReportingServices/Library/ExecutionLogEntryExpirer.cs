using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003E RID: 62
	internal sealed class ExecutionLogEntryExpirer : TimerActionBase
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		internal static int SecondsToNextEvent
		{
			get
			{
				DateTime dateTime = DateTime.Today.AddMinutes((double)Globals.Configuration.DailyCleanupMinuteOfDay);
				DateTime dateTime2;
				if (dateTime > DateTime.Now)
				{
					dateTime2 = dateTime;
				}
				else if (dateTime.AddHours(1.0) > DateTime.Now)
				{
					dateTime2 = DateTime.Now.AddSeconds(10.0);
				}
				else
				{
					dateTime2 = dateTime.AddDays(1.0);
				}
				return (int)dateTime2.Subtract(DateTime.Now).TotalSeconds;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public override void DoTimerAction()
		{
			if (!DatabaseSessionStorage.Current.TryAcquireCleanupLock())
			{
				if (RSTrace.CleanupTracer.TraceInfo)
				{
					RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Skipping cleanup: other instance has cleaned recently");
				}
				return;
			}
			this.ExpireOldExecutionLogEntries();
			if (!base.ContinueExecuting)
			{
				return;
			}
			this.CleanBrokenSnapshots();
			if (!base.ContinueExecuting)
			{
				return;
			}
			this.CleanExpiredParameters();
			if (!base.ContinueExecuting)
			{
				return;
			}
			this.CleanupUnusedSegmentMappings();
			if (!base.ContinueExecuting)
			{
				return;
			}
			this.CleanupUnusedSegments();
			if (!base.ContinueExecuting)
			{
				return;
			}
			ExecutionLogEntryExpirer.CleanOrphanedTempFiles();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000EE10 File Offset: 0x0000D010
		private void CleanupUnusedSegments()
		{
			int num = 0;
			while (base.ContinueExecuting)
			{
				int num2 = DatabaseSessionStorage.Current.CleanupSegments();
				num += num2;
				Thread.Sleep(0);
				if (num2 <= 0)
				{
					break;
				}
			}
			if (RSTrace.CleanupTracer.TraceInfo)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Cleaned {0} unused segments.", new object[] { num });
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000EE70 File Offset: 0x0000D070
		private void CleanupUnusedSegmentMappings()
		{
			int num = 0;
			while (base.ContinueExecuting)
			{
				int num2 = DatabaseSessionStorage.Current.CleanSegmentMappings();
				num += num2;
				Thread.Sleep(0);
				if (num2 <= 0)
				{
					break;
				}
			}
			if (RSTrace.CleanupTracer.TraceInfo)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Cleaned {0} unused segment mappings.", new object[] { num });
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		private void CleanExpiredParameters()
		{
			int num = 0;
			while (base.ContinueExecuting)
			{
				int num2 = DatabaseSessionStorage.Current.CleanExpiredParametersValues();
				num += num2;
				Thread.Sleep(0);
				if (num2 <= 0)
				{
					break;
				}
			}
			if (RSTrace.CleanupTracer.TraceInfo)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Cleaned {0} parameters values", new object[] { num });
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000EF30 File Offset: 0x0000D130
		private void CleanBrokenSnapshots()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			while (base.ContinueExecuting)
			{
				bool flag = DatabaseSessionStorage.Current.CleanBrokenSnapshots(out num, out num2);
				num4 += num2;
				num3 += num;
				Thread.Sleep(0);
				if (!flag)
				{
					num5++;
				}
				if (num2 <= 0 && num <= 0 && (flag || num5 >= 10))
				{
					break;
				}
			}
			if (RSTrace.CleanupTracer.TraceInfo)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Cleaned {0} broken snapshots, {1} chunks, there were {2} unsuccessful attempts", new object[] { num4, num3, num5 });
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		private void ExpireOldExecutionLogEntries()
		{
			if (RSTrace.CleanupTracer.TraceInfo)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Expiring old execution log entries");
			}
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				int num = new DBInterface
				{
					ConnectionManager = connectionManager
				}.ExpireExecutionLogEntries();
				if (RSTrace.CleanupTracer.TraceInfo)
				{
					if (num < 0)
					{
						RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Expiration of old execution log entries is complete.  Expiration is disabled.");
					}
					else
					{
						RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Expiration of old execution log entries is complete.  Removed {0} entries.", new object[] { num });
					}
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.CleanupTracer.TraceError)
				{
					RSTrace.CleanupTracer.Trace(TraceLevel.Error, "Expiration of old execution log entries failed: " + ex.ToString());
				}
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		private static void CleanOrphanedTempFiles()
		{
			TimeSpan timeSpan = new TimeSpan(1, 0, 0, 0);
			int num = Global.PartitionManager.PerformTimeBasedCleanup(timeSpan);
			RSTrace.CleanupTracer.Trace(TraceLevel.Info, "Completed cleaning {0} files", new object[] { num });
			IEnumerable<Guid> fileChunkCleanupCandidates = Global.PartitionManager.GetFileChunkCleanupCandidates(timeSpan);
			DatabaseSessionStorage.Current.CleanupOrphanedFileChunks(fileChunkCleanupCandidates);
		}

		// Token: 0x04000136 RID: 310
		internal const string EventName = "Execution Log Entry Expiration";
	}
}
