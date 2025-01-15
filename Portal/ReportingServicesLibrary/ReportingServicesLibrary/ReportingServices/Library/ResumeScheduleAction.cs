using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001DF RID: 479
	internal sealed class ResumeScheduleAction : RSSoapAction<ResumeScheduleActionParameters>
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x0003A0EC File Offset: 0x000382EC
		public ResumeScheduleAction(RSService service)
			: base("ResumeScheduleAction", service)
		{
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0003A0FC File Offset: 0x000382FC
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.ResumeSchedule, base.ActionParameters.ScheduleID, "ScheduleID", null, null, null, null, false, null, null);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0003A144 File Offset: 0x00038344
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ScheduleID = Globals.ParseGuidParameter(parameters.Item, "ScheduleID").ToString();
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0003A186 File Offset: 0x00038386
		internal override void PerformActionNow()
		{
			base.Service.SchedCoordinator.ResumeTask(Globals.ParseGuidParameter(base.ActionParameters.ScheduleID, "ScheduleID"));
		}
	}
}
