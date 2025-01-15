using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x020002FE RID: 766
	public class Job
	{
		// Token: 0x06001AFD RID: 6909 RVA: 0x000025F4 File Offset: 0x000007F4
		public Job()
		{
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0006D3D3 File Offset: 0x0006B5D3
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
			default:
				return JobActionEnum.Render;
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0006D3F8 File Offset: 0x0006B5F8
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

		// Token: 0x06001B00 RID: 6912 RVA: 0x0006D494 File Offset: 0x0006B694
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

		// Token: 0x04000A30 RID: 2608
		public string JobID;

		// Token: 0x04000A31 RID: 2609
		public string Name;

		// Token: 0x04000A32 RID: 2610
		public string Path;

		// Token: 0x04000A33 RID: 2611
		public string Description;

		// Token: 0x04000A34 RID: 2612
		public string Machine;

		// Token: 0x04000A35 RID: 2613
		public string User;

		// Token: 0x04000A36 RID: 2614
		public DateTime StartDateTime;

		// Token: 0x04000A37 RID: 2615
		public JobActionEnum Action;

		// Token: 0x04000A38 RID: 2616
		public JobTypeEnum Type;

		// Token: 0x04000A39 RID: 2617
		public JobStatusEnum Status;
	}
}
