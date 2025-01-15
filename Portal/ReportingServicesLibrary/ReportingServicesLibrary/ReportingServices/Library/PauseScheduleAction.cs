using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001DD RID: 477
	internal sealed class PauseScheduleAction : RSSoapAction<PauseScheduleActionParameters>
	{
		// Token: 0x06001081 RID: 4225 RVA: 0x00039FAC File Offset: 0x000381AC
		public PauseScheduleAction(RSService service)
			: base("PauseScheduleAction", service)
		{
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00039FBC File Offset: 0x000381BC
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.PauseSchedule, base.ActionParameters.ScheduleID, "ScheduleID", null, null, null, null, false, null, null);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003A004 File Offset: 0x00038204
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ScheduleID = Globals.ParseGuidParameter(parameters.Item, "ScheduleID").ToString();
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003A046 File Offset: 0x00038246
		internal override void PerformActionNow()
		{
			base.Service.SchedCoordinator.PauseTask(Globals.ParseGuidParameter(base.ActionParameters.ScheduleID, "ScheduleID"));
		}
	}
}
