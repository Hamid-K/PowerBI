using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E1 RID: 481
	internal sealed class ListScheduledReportsAction : RSSoapAction<ListScheduledReportsActionParameters>
	{
		// Token: 0x06001097 RID: 4247 RVA: 0x0003A247 File Offset: 0x00038447
		public ListScheduledReportsAction(RSService service)
			: base("ListScheduledReportsAction", service)
		{
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003A255 File Offset: 0x00038455
		internal override void PerformActionNow()
		{
			base.ActionParameters.Children = base.Service.SchedCoordinator.ListScheduledReports(Globals.ParseGuidParameter(base.ActionParameters.ScheduleID, "ScheduleID"));
		}
	}
}
