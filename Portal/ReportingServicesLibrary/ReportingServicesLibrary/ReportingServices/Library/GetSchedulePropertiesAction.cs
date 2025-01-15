using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D9 RID: 473
	internal sealed class GetSchedulePropertiesAction : RSSoapAction<GetSchedulePropertiesActionParameters>
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x00039C68 File Offset: 0x00037E68
		public GetSchedulePropertiesAction(RSService service)
			: base("GetSchedulePropertiessAction", service)
		{
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00039C76 File Offset: 0x00037E76
		internal override void PerformActionNow()
		{
			base.ActionParameters.Schedule = Schedule.TaskToThis(base.Service.SchedCoordinator.GetTaskProperties(Globals.ParseGuidParameter(base.ActionParameters.ScheduleID, "ScheduleID")));
		}
	}
}
