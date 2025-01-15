using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D7 RID: 471
	internal sealed class ListSchedulesAction : RSSoapAction<ListScheduleActionParameters>
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x00039B86 File Offset: 0x00037D86
		public ListSchedulesAction(RSService service)
			: base("ListSchedulesAction", service)
		{
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00039B94 File Offset: 0x00037D94
		internal override void PerformActionNow()
		{
			base.ActionParameters.Children = base.Service.SchedCoordinator.ListTasksAsArray(new ExternalItemPath(base.ActionParameters.Site));
		}
	}
}
