using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200008E RID: 142
	public sealed class RunningJobContext
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0000D300 File Offset: 0x0000B500
		public RunningJobContext(string jobId, ExternalItemPath requestPath, JobActionEnum action, JobType type, RunningJobList parentList, UserContext userContext, RequestContext optionalContext)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(null);
			catalogItemContext.SetPath(requestPath, ItemPathOptions.AllowEditSessionSyntax);
			this.m_jobId = jobId;
			this.m_reqPath = catalogItemContext.OriginalItemPath;
			this.m_reqName = catalogItemContext.ItemName;
			this.m_computerName = Environment.MachineName;
			this.m_userContext = userContext;
			this.m_runningJobs = parentList;
			this.m_action = action;
			this.m_type = type;
			this.m_startDate = DateTime.Now;
			this.m_status = JobStatusEnum.New;
			this.m_optionalContext = optionalContext;
			this.InitExecutionInfos();
			this.AddWorkThread();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public RunningJobContext(IDataRecord record)
		{
			this.m_jobId = record.GetString(0);
			this.m_startDate = record.GetDateTime(1);
			this.m_computerName = record.GetString(2);
			this.m_reqPath = new ExternalItemPath(record.GetString(4));
			this.m_reqName = record.GetString(3);
			string userNameBySid = UserUtil.GetUserNameBySid(record, 5, 6);
			AuthenticationType @int = (AuthenticationType)record.GetInt32(12);
			this.m_userContext = new UserContext(userNameBySid, null, @int);
			if (!record.IsDBNull(7))
			{
				this.m_description = record.GetString(7);
			}
			this.m_timeout = record.GetInt32(8);
			this.m_action = (JobActionEnum)record.GetInt16(9);
			JobTypeEnum int2 = (JobTypeEnum)record.GetInt16(10);
			this.m_type = new JobType(int2);
			this.m_status = (JobStatusEnum)record.GetInt16(11);
			this.InitExecutionInfos();
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000D4AD File Offset: 0x0000B6AD
		public ThreadJobContext InitialAndOnlyThreadCtx
		{
			get
			{
				if (this.m_threadsCtx.Count != 1)
				{
					throw new InternalCatalogException("Expected only one thread");
				}
				return (ThreadJobContext)this.m_threadsCtx[0];
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		public ThreadJobContext AddWorkThread()
		{
			ThreadJobContext threadJobContext = new ThreadJobContext(this);
			this.m_threadsCtx.Add(threadJobContext);
			return threadJobContext;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000D4FE File Offset: 0x0000B6FE
		public void RemoveWorkThread(ThreadJobContext threadCtx)
		{
			if (this.m_threadsCtx.Contains(threadCtx))
			{
				this.m_threadsCtx.Remove(threadCtx);
				threadCtx.Dispose();
			}
			if (this.m_threadsCtx.Count == 0)
			{
				this.m_runningJobs.RemoveJob(this.JobId);
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000D53E File Offset: 0x0000B73E
		public bool IsClientConnected()
		{
			if (this.m_optionalContext == null)
			{
				return true;
			}
			bool isClientConnected = this.m_optionalContext.IsClientConnected;
			if (!isClientConnected)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "RunningJobContext.IsClientConnected; found orphaned request " + this.JobId);
			}
			return isClientConnected;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000D574 File Offset: 0x0000B774
		public bool IsExpired()
		{
			if (this.m_timeout == -1 || this.m_timeout == 0)
			{
				return false;
			}
			if (this.m_startDate.AddSeconds((double)this.m_timeout) < DateTime.Now)
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Info, "RunningJobContext.IsExpired; found expired request");
				return true;
			}
			return false;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000D5C5 File Offset: 0x0000B7C5
		public void SetTimeout(int timeout)
		{
			if (timeout < 1 && timeout != -1)
			{
				throw new InternalCatalogException("Expected positive timeout value");
			}
			this.m_timeout = timeout;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		public int Cancel(ReportServerAbortInfo.AbortReason reason)
		{
			int num = 0;
			object statusSyncRoot = this.StatusSyncRoot;
			lock (statusSyncRoot)
			{
				if (this.m_status == JobStatusEnum.CancelRequested)
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "RunningJobContext.Cancel; Skipping -- Cancel has been requested already");
					return num;
				}
				this.m_status = JobStatusEnum.CancelRequested;
			}
			foreach (object obj in new ArrayList(this.m_threadsCtx))
			{
				ThreadJobContext threadJobContext = (ThreadJobContext)obj;
				if (threadJobContext.Cancel(reason))
				{
					num++;
				}
				else
				{
					ProcessingContext.JobsTracer.Trace(TraceLevel.Warning, "ThreadJobContext.Cancel({0}) failed for job {1} and thread {2}", new object[]
					{
						reason.ToString(),
						this.JobId,
						threadJobContext.ThreadId.ToString(CultureInfo.InvariantCulture)
					});
				}
			}
			return num;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000D6F0 File Offset: 0x0000B8F0
		public string JobId
		{
			get
			{
				return this.m_jobId;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public object StatusSyncRoot
		{
			get
			{
				return this.m_statusSyncRoot;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000D700 File Offset: 0x0000B900
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000D708 File Offset: 0x0000B908
		public JobStatusEnum Status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000D711 File Offset: 0x0000B911
		public JobType JobType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000D719 File Offset: 0x0000B919
		public JobTypeEnum Type
		{
			get
			{
				return this.m_type.Type;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000D726 File Offset: 0x0000B926
		public DateTime StartDate
		{
			get
			{
				return this.m_startDate;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000D72E File Offset: 0x0000B92E
		public JobSubTypeEnum SubType
		{
			get
			{
				return this.m_type.SubType;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000D73B File Offset: 0x0000B93B
		public JobActionEnum Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000D743 File Offset: 0x0000B943
		public ExternalItemPath Path
		{
			get
			{
				return this.m_reqPath;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000D74B File Offset: 0x0000B94B
		public string Name
		{
			get
			{
				return this.m_reqName;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000D753 File Offset: 0x0000B953
		public string Machine
		{
			get
			{
				return this.m_computerName;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000D75B File Offset: 0x0000B95B
		public UserContext UserContext
		{
			get
			{
				return this.m_userContext;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000D763 File Offset: 0x0000B963
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000D76B File Offset: 0x0000B96B
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000D774 File Offset: 0x0000B974
		public int Timeout
		{
			get
			{
				return this.m_timeout;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000D77C File Offset: 0x0000B97C
		public ReportExecutionInfo ExecutionInfo
		{
			get
			{
				return this.m_primaryExecutionInfo;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000D784 File Offset: 0x0000B984
		public List<ReportExecutionInfo> ExecutionInfos
		{
			get
			{
				return this.m_executionInfos;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000D78C File Offset: 0x0000B98C
		public ReportExecutionInfo GetSecondaryExecutionInfo()
		{
			ReportExecutionInfo reportExecutionInfo = new ReportExecutionInfo();
			object statusSyncRoot = this.m_statusSyncRoot;
			lock (statusSyncRoot)
			{
				reportExecutionInfo.ExecutionId = this.m_executionInfos[0].ExecutionId;
				this.m_executionInfos.Add(reportExecutionInfo);
			}
			return reportExecutionInfo;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		private void InitExecutionInfos()
		{
			this.m_executionInfos = new List<ReportExecutionInfo>();
			this.m_executionInfos.Add(new ReportExecutionInfo());
			this.m_primaryExecutionInfo = this.m_executionInfos[0];
		}

		// Token: 0x04000290 RID: 656
		public const int ProcessingTimeoutNoTimeout = -1;

		// Token: 0x04000291 RID: 657
		private DateTime m_startDate;

		// Token: 0x04000292 RID: 658
		private string m_jobId;

		// Token: 0x04000293 RID: 659
		private string m_computerName;

		// Token: 0x04000294 RID: 660
		private UserContext m_userContext;

		// Token: 0x04000295 RID: 661
		private ExternalItemPath m_reqPath;

		// Token: 0x04000296 RID: 662
		private string m_reqName;

		// Token: 0x04000297 RID: 663
		private string m_description;

		// Token: 0x04000298 RID: 664
		private int m_timeout = -1;

		// Token: 0x04000299 RID: 665
		private JobActionEnum m_action;

		// Token: 0x0400029A RID: 666
		private JobType m_type;

		// Token: 0x0400029B RID: 667
		private JobStatusEnum m_status;

		// Token: 0x0400029C RID: 668
		private object m_statusSyncRoot = new object();

		// Token: 0x0400029D RID: 669
		private ArrayList m_threadsCtx = ArrayList.Synchronized(new ArrayList());

		// Token: 0x0400029E RID: 670
		private RequestContext m_optionalContext;

		// Token: 0x0400029F RID: 671
		private RunningJobList m_runningJobs;

		// Token: 0x040002A0 RID: 672
		private List<ReportExecutionInfo> m_executionInfos;

		// Token: 0x040002A1 RID: 673
		private ReportExecutionInfo m_primaryExecutionInfo;

		// Token: 0x040002A2 RID: 674
		private const int TimeoutNotSet = -1;

		// Token: 0x020000F6 RID: 246
		private enum RunningJobsProjection
		{
			// Token: 0x040004C8 RID: 1224
			JobID,
			// Token: 0x040004C9 RID: 1225
			StartDate,
			// Token: 0x040004CA RID: 1226
			ComputerName,
			// Token: 0x040004CB RID: 1227
			RequestName,
			// Token: 0x040004CC RID: 1228
			RequestPath,
			// Token: 0x040004CD RID: 1229
			UserNameFromSid,
			// Token: 0x040004CE RID: 1230
			UserNameBackup,
			// Token: 0x040004CF RID: 1231
			Description,
			// Token: 0x040004D0 RID: 1232
			Timeout,
			// Token: 0x040004D1 RID: 1233
			JobAction,
			// Token: 0x040004D2 RID: 1234
			JobType,
			// Token: 0x040004D3 RID: 1235
			JobStatus,
			// Token: 0x040004D4 RID: 1236
			AuthType
		}
	}
}
