using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap2006
{
	// Token: 0x020002F8 RID: 760
	public class Job
	{
		// Token: 0x06001AEF RID: 6895 RVA: 0x000025F4 File Offset: 0x000007F4
		public Job()
		{
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0006CF06 File Offset: 0x0006B106
		private static JobActionEnum MapJobAction(JobActionEnum jobAction)
		{
			switch (jobAction)
			{
			case JobActionEnum.Render:
				return JobActionEnum.Render;
			case JobActionEnum.SnapshotCreation:
				return JobActionEnum.SnapshotCreation;
			case JobActionEnum.ReportHistoryCreation:
				return JobActionEnum.ReportHistoryCreation;
			case JobActionEnum.ExecuteQuery:
				return JobActionEnum.ExecuteQuery;
			case JobActionEnum.GetUserModel:
				return JobActionEnum.GetUserModel;
			default:
				return JobActionEnum.Render;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0006CF30 File Offset: 0x0006B130
		internal Job(RunningJobContext ctx)
		{
			this.JobID = ctx.JobId;
			this.Name = ctx.Name;
			this.Path = ctx.Path.Value;
			this.Description = ctx.Description;
			this.Machine = ctx.Machine;
			this.User = ctx.UserContext.UserName;
			this.StartDateTime = ctx.StartDate;
			this.Action = Job.MapJobAction(ctx.Action);
			this.Type = ctx.Type;
			this.Status = ctx.Status;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0006CFCC File Offset: 0x0006B1CC
		internal static Job[] ToJobArray(ICollection<RunningJobContext> list)
		{
			if (list == null)
			{
				return null;
			}
			Job[] array = new Job[list.Count];
			int num = 0;
			foreach (RunningJobContext runningJobContext in list)
			{
				Job job = new Job(runningJobContext);
				array[num++] = job;
			}
			return array;
		}

		// Token: 0x04000A02 RID: 2562
		public string JobID;

		// Token: 0x04000A03 RID: 2563
		public string Name;

		// Token: 0x04000A04 RID: 2564
		public string Path;

		// Token: 0x04000A05 RID: 2565
		public string Description;

		// Token: 0x04000A06 RID: 2566
		public string Machine;

		// Token: 0x04000A07 RID: 2567
		public string User;

		// Token: 0x04000A08 RID: 2568
		public DateTime StartDateTime;

		// Token: 0x04000A09 RID: 2569
		public JobActionEnum Action;

		// Token: 0x04000A0A RID: 2570
		public JobTypeEnum Type;

		// Token: 0x04000A0B RID: 2571
		public JobStatusEnum Status;
	}
}
