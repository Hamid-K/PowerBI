using System;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000031 RID: 49
	internal sealed class DatabaseMonitoringContext
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00004794 File Offset: 0x00002994
		public DatabaseMonitoringContext(IDatabaseEventsKit eventsKit, string command)
		{
			this.m_eventsKit = eventsKit;
			this.m_command = command;
			this.m_server = "N/A";
			this.m_database = "N/A";
			this.m_durations = new long[4];
			this.m_stage = DatabaseMonitoringContext.Stage.None;
			this.m_clock = Stopwatch.StartNew();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000047E9 File Offset: 0x000029E9
		public void NotifySpecification(IDatabaseSpecification specification)
		{
			this.m_server = specification.ConnectionProperties.DataSource;
			this.m_database = specification.ConnectionProperties.InitialCatalog;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000480D File Offset: 0x00002A0D
		public void NotifyThrottleBegin()
		{
			this.OnEvent(DatabaseMonitoringContext.Stage.Throttle, DatabaseMonitoringContext.EventOptions.None);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004818 File Offset: 0x00002A18
		public void NotifyThrottleCompleted()
		{
			this.OnEvent(DatabaseMonitoringContext.Stage.Throttle, DatabaseMonitoringContext.EventOptions.UpdateDuration);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004823 File Offset: 0x00002A23
		public void NotifyConnectionBegin()
		{
			this.OnEvent(DatabaseMonitoringContext.Stage.Connect, DatabaseMonitoringContext.EventOptions.None);
			this.m_connectionTries++;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000483C File Offset: 0x00002A3C
		public void NotifyConnectionRetry(Exception ex)
		{
			long num = this.OnEvent(DatabaseMonitoringContext.Stage.Connect, DatabaseMonitoringContext.EventOptions.UpdateDuration);
			this.m_eventsKit.NotifyCommandConnectionOpenRetry(this.m_server, this.m_database, this.m_command, ex.Message, this.m_connectionTries, num);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000487C File Offset: 0x00002A7C
		public void NotifyConnectionComplete()
		{
			this.OnEvent(DatabaseMonitoringContext.Stage.Connect, DatabaseMonitoringContext.EventOptions.UpdateDuration);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004887 File Offset: 0x00002A87
		public void NotifyRequestBegin()
		{
			this.OnEvent(DatabaseMonitoringContext.Stage.Request, DatabaseMonitoringContext.EventOptions.None);
			this.m_requestTries++;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000048A0 File Offset: 0x00002AA0
		public void NotifyRequestRetry(Exception ex)
		{
			long num = this.OnEvent(DatabaseMonitoringContext.Stage.Request, DatabaseMonitoringContext.EventOptions.UpdateDuration);
			this.m_eventsKit.NotifyQueryRetry(this.m_server, this.m_database, this.m_command, ex.Message, this.m_connectionTries, num);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000048E0 File Offset: 0x00002AE0
		public void NotifyRequestComplete()
		{
			this.OnEvent(DatabaseMonitoringContext.Stage.Request, DatabaseMonitoringContext.EventOptions.UpdateDuration);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000048EB File Offset: 0x00002AEB
		public void NotifyResponseBegin()
		{
			this.OnEvent(DatabaseMonitoringContext.Stage.Response, DatabaseMonitoringContext.EventOptions.None);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000048F6 File Offset: 0x00002AF6
		public void NotifyResponseComplete(long rowCount)
		{
			this.m_responseRowCount = rowCount;
			this.OnEvent(DatabaseMonitoringContext.Stage.Done, DatabaseMonitoringContext.EventOptions.UpdateDuration);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004908 File Offset: 0x00002B08
		public void NotifyError(MonitoredException mex)
		{
			this.m_eventsKit.NotifyCommandExecuteError(this.m_server, this.m_database, this.m_command, mex);
			this.OnEvent(DatabaseMonitoringContext.Stage.Fail, (this.m_stage != DatabaseMonitoringContext.Stage.None) ? DatabaseMonitoringContext.EventOptions.UpdateDuration : DatabaseMonitoringContext.EventOptions.None);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000493D File Offset: 0x00002B3D
		public void NotifyTimeout(long timeoutMs, MonitoredException mex)
		{
			this.m_eventsKit.NotifyCommandTimeout(this.m_server, this.m_database, this.m_command, timeoutMs, mex);
			this.OnEvent(DatabaseMonitoringContext.Stage.Timeout, (this.m_stage != DatabaseMonitoringContext.Stage.None) ? DatabaseMonitoringContext.EventOptions.UpdateDuration : DatabaseMonitoringContext.EventOptions.None);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004974 File Offset: 0x00002B74
		public MonitoredException NotifySqlError(Exception ex)
		{
			SqlException ex2 = ex as SqlException;
			int num = ((ex2 != null) ? ex2.ErrorCode : 0);
			MonitoredException ex3 = ((this.m_stage > DatabaseMonitoringContext.Stage.Connect) ? new DatabaseUnreadableException("Database unreadable Server: {0}, Database: {1}, Command: {2}".FormatWithInvariantCulture(new object[] { this.m_server, this.m_database, this.m_command }), ex) : new DatabaseUnreachableException("Database unreachable Server: {0}, Database: {1}, Command: {2}".FormatWithInvariantCulture(new object[] { this.m_server, this.m_database, this.m_command }), ex));
			this.m_eventsKit.NotifyCommandExecuteCriticalError(this.m_server, this.m_database, this.m_command, num, ex3);
			this.OnEvent(DatabaseMonitoringContext.Stage.Fail, DatabaseMonitoringContext.EventOptions.UpdateDuration);
			return ex3;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004A2C File Offset: 0x00002C2C
		public long ElapsedSinceLastNotification
		{
			get
			{
				return this.m_clock.ElapsedMilliseconds - this.m_lastEventTick;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004A40 File Offset: 0x00002C40
		public string Scope
		{
			get
			{
				return "{0}-{1}-{2}".FormatWithInvariantCulture(new object[] { this.m_server, this.m_database, this.m_command });
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004A70 File Offset: 0x00002C70
		private long OnEvent(DatabaseMonitoringContext.Stage newStage, DatabaseMonitoringContext.EventOptions options)
		{
			long lastEventTick = this.m_lastEventTick;
			this.m_lastEventTick = this.m_clock.ElapsedMilliseconds;
			long num = this.m_lastEventTick - lastEventTick;
			if (options.HasFlag(DatabaseMonitoringContext.EventOptions.UpdateDuration))
			{
				int num2 = (int)this.m_stage;
				if (num2 < 0 || num2 >= this.m_durations.Length)
				{
					num2 = 3;
				}
				this.m_durations[num2] += num;
			}
			this.m_stage = newStage;
			if (this.m_stage >= DatabaseMonitoringContext.Stage.Done)
			{
				this.m_eventsKit.NotifyDatabaseAccess(this.m_server, this.m_database, this.m_command, this.m_durations[0], this.m_durations[1], this.m_durations[2], this.m_durations[3], this.m_connectionTries, (long)this.m_requestTries, this.m_responseRowCount, this.m_lastEventTick, this.m_stage.ToString());
			}
			return num;
		}

		// Token: 0x04000089 RID: 137
		private DatabaseMonitoringContext.Stage m_stage;

		// Token: 0x0400008A RID: 138
		private string m_server;

		// Token: 0x0400008B RID: 139
		private string m_database;

		// Token: 0x0400008C RID: 140
		private int m_connectionTries;

		// Token: 0x0400008D RID: 141
		private int m_requestTries;

		// Token: 0x0400008E RID: 142
		private long m_responseRowCount;

		// Token: 0x0400008F RID: 143
		private long m_lastEventTick;

		// Token: 0x04000090 RID: 144
		private readonly long[] m_durations;

		// Token: 0x04000091 RID: 145
		private readonly Stopwatch m_clock;

		// Token: 0x04000092 RID: 146
		private readonly IDatabaseEventsKit m_eventsKit;

		// Token: 0x04000093 RID: 147
		private readonly string m_command;

		// Token: 0x02000584 RID: 1412
		private enum Stage
		{
			// Token: 0x04000EFF RID: 3839
			Throttle,
			// Token: 0x04000F00 RID: 3840
			Connect,
			// Token: 0x04000F01 RID: 3841
			Request,
			// Token: 0x04000F02 RID: 3842
			Response,
			// Token: 0x04000F03 RID: 3843
			None,
			// Token: 0x04000F04 RID: 3844
			Done,
			// Token: 0x04000F05 RID: 3845
			Fail,
			// Token: 0x04000F06 RID: 3846
			Timeout
		}

		// Token: 0x02000585 RID: 1413
		[Flags]
		private enum EventOptions
		{
			// Token: 0x04000F08 RID: 3848
			None = 0,
			// Token: 0x04000F09 RID: 3849
			UpdateDuration = 1
		}
	}
}
