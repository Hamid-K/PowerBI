using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001D3 RID: 467
	internal sealed class CreateScheduleAction : RSSoapAction<CreateScheduleActionParameters>
	{
		// Token: 0x0600104A RID: 4170 RVA: 0x000398A8 File Offset: 0x00037AA8
		public CreateScheduleAction(RSService service)
			: base("CreateScheduleAction", service)
		{
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000398B8 File Offset: 0x00037AB8
		protected override void AddActionToBatch()
		{
			base.ActionParameters.ScheduleID = Guid.NewGuid().ToString();
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateSchedule, null, null, base.ActionParameters.ScheduleID, null, base.ActionParameters.Name, "Name", false, null, ScheduleDefinition.DefinitionToXml(base.ActionParameters.ScheduleDefinition));
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00039938 File Offset: 0x00037B38
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ScheduleID = parameters.Parent;
			base.ActionParameters.Name = parameters.Param;
			base.ActionParameters.ScheduleDefinition = ScheduleDefinition.XmlToDefinition(parameters.Properties);
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003998C File Offset: 0x00037B8C
		internal override void PerformActionNow()
		{
			base.ActionParameters.ScheduleID = base.Service.SchedCoordinator.CreateTask(new Guid(base.ActionParameters.ScheduleID), base.ActionParameters.Name, base.ActionParameters.ScheduleDefinition, new ExternalItemPath(base.ActionParameters.Site));
		}
	}
}
