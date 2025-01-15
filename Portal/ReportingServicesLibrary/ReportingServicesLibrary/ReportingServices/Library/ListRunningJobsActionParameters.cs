using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000135 RID: 309
	internal sealed class ListRunningJobsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0002E328 File Offset: 0x0002C528
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0002E330 File Offset: 0x0002C530
		public ICollection<RunningJobContext> Jobs
		{
			get
			{
				return this.m_Jobs;
			}
			set
			{
				this.m_Jobs = value;
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x04000505 RID: 1285
		private ICollection<RunningJobContext> m_Jobs;
	}
}
