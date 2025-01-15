using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D5 RID: 469
	internal sealed class DeleteScheduleAction : RSSoapAction<DeleteScheduleActionParameters>
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x00039A68 File Offset: 0x00037C68
		public DeleteScheduleAction(RSService service)
			: base("DeleteScheduleAction", service)
		{
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00039A78 File Offset: 0x00037C78
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.DeleteSchedule, base.ActionParameters.ScheduleID, "scheduleID", null, null, null, null, false, null, null);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00039AC0 File Offset: 0x00037CC0
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ScheduleID = Globals.ParseGuidParameter(parameters.Item, "ScheduleID").ToString();
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00039B02 File Offset: 0x00037D02
		internal override void PerformActionNow()
		{
			base.Service.SchedCoordinator.DeleteTask(Globals.ParseGuidParameter(base.ActionParameters.ScheduleID, "ScheduleID"));
		}
	}
}
