using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002F1 RID: 753
	public class Job
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x000025F4 File Offset: 0x000007F4
		public Job()
		{
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0006CC2C File Offset: 0x0006AE2C
		internal Job(RunningJobContext ctx)
		{
			this.JobID = ctx.JobId;
			this.Name = ctx.Name;
			this.Path = ctx.Path.Value;
			this.Description = ctx.Description;
			this.Machine = ctx.Machine;
			this.User = ctx.UserContext.UserName;
			this.StartDateTime = ctx.StartDate;
			this.JobActionName = ctx.Action.ToString();
			this.JobTypeName = ctx.Type.ToString();
			this.JobStatusName = ctx.Status.ToString();
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0006CCEC File Offset: 0x0006AEEC
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

		// Token: 0x040009D5 RID: 2517
		public string JobID;

		// Token: 0x040009D6 RID: 2518
		public string Name;

		// Token: 0x040009D7 RID: 2519
		public string Path;

		// Token: 0x040009D8 RID: 2520
		public string Description;

		// Token: 0x040009D9 RID: 2521
		public string Machine;

		// Token: 0x040009DA RID: 2522
		public string User;

		// Token: 0x040009DB RID: 2523
		public DateTime StartDateTime;

		// Token: 0x040009DC RID: 2524
		public string JobActionName;

		// Token: 0x040009DD RID: 2525
		public string JobTypeName;

		// Token: 0x040009DE RID: 2526
		public string JobStatusName;
	}
}
