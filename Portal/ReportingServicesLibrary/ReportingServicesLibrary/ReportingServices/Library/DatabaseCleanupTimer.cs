using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000058 RID: 88
	internal sealed class DatabaseCleanupTimer : TimerActionBase
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		public override void DoTimerAction()
		{
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Starting database cleanup.");
			}
			this.CleanBatch();
			if (RSTrace.CleanupTracer.TraceVerbose)
			{
				RSTrace.CleanupTracer.Trace(TraceLevel.Verbose, "Database cleanup executed.");
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		public void CleanBatch()
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				DBInterface dbinterface = new DBInterface();
				SQLPersistedStreamDB sqlpersistedStreamDB = new SQLPersistedStreamDB(connectionManager, null);
				dbinterface.ConnectionManager = connectionManager;
				this.Tracer.Trace(TraceLevel.Info, "Call to CleanBatch()");
				int num = dbinterface.CleanBatchRecords();
				int num2 = dbinterface.CleanOrphanedPolicies();
				int num3 = 0;
				while (base.ContinueExecuting)
				{
					int num4 = DatabaseSessionStorage.Current.CleanExpiredSessions();
					if (num4 < 1)
					{
						break;
					}
					num3 += num4;
					Thread.Sleep(10);
				}
				int num5 = 0;
				while (base.ContinueExecuting)
				{
					int num6 = sqlpersistedStreamDB.DeleteExpiredStreams();
					if (num6 < 1)
					{
						break;
					}
					num5 += num6;
					Thread.Sleep(10);
				}
				int num7 = DatabaseSessionStorage.Current.CleanExpiredCache();
				int num8 = dbinterface.CleanExpiredContentCache();
				int num9 = 0;
				int num10 = 0;
				int num11 = 0;
				int num12 = 0;
				int num16;
				do
				{
					int num13 = 0;
					int num14 = 0;
					int num15 = 0;
					if (!base.ContinueExecuting)
					{
						break;
					}
					num16 = DatabaseSessionStorage.Current.CleanOrphanedSnapshots(out num13, out num15, out num14);
					if (num16 > 0)
					{
						num9 += num16;
						num10 += num13;
						num11 += num15;
						num12 += num14;
					}
					Thread.Sleep(0);
				}
				while (num16 > 0);
				int num17 = new RunningJobsDb().CleanExpiredJobs();
				int num18 = 0;
				while (base.ContinueExecuting)
				{
					int num19 = dbinterface.CleanExpiredEditSessions();
					num18 += num19;
					if (num19 <= 0)
					{
						break;
					}
				}
				this.Tracer.Trace(TraceLevel.Info, "Cleaned {0} batch records, {1} policies, {2} sessions, {3} cache entries, {4} snapshots, {5} chunks, {6} running jobs, {7} persisted streams, {8} segments, {9} segment mappings, {10} edit sessions, {11} extended content cache.", new object[]
				{
					num, num2, num3, num7, num9, num10, num17, num5, num12, num11,
					num18, num8
				});
				this.Tracer.Trace(TraceLevel.Info, "Call to CleanBatch() ends");
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000110FC File Offset: 0x0000F2FC
		private RSTrace Tracer
		{
			get
			{
				return Global.m_Tracer;
			}
		}
	}
}
