using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001DB RID: 475
	internal sealed class SetSchedulePropertiesAction : RSSoapAction<SetSchedulePropertiesActionParameters>
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x00039E14 File Offset: 0x00038014
		public SetSchedulePropertiesAction(RSService service)
			: base("SetSchedulePropertiesAction", service)
		{
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00039E24 File Offset: 0x00038024
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetScheduleProperties, base.ActionParameters.ScheduleID, "ScheduleID", null, null, base.ActionParameters.Name, "Name", false, null, ScheduleDefinition.DefinitionToXml(base.ActionParameters.ScheduleDefinition));
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00039E88 File Offset: 0x00038088
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.Name = parameters.Param;
			base.ActionParameters.ScheduleID = Globals.ParseGuidParameter(parameters.Item, "ScheduleID").ToString();
			base.ActionParameters.ScheduleDefinition = ScheduleDefinition.XmlToDefinition(parameters.Properties);
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00039EF1 File Offset: 0x000380F1
		internal override void PerformActionNow()
		{
			base.Service.SchedCoordinator.SetTaskProperties(Globals.ParseGuidParameter(base.ActionParameters.ScheduleID, "ScheduleID"), base.ActionParameters.Name, base.ActionParameters.ScheduleDefinition);
		}
	}
}
