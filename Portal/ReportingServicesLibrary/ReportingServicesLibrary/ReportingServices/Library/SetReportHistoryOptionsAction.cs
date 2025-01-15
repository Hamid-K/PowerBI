using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B3 RID: 435
	internal sealed class SetReportHistoryOptionsAction : RSSoapAction<ReportHistoryOptionsActionParameters>
	{
		// Token: 0x06000F99 RID: 3993 RVA: 0x00037C10 File Offset: 0x00035E10
		public SetReportHistoryOptionsAction(RSService service)
			: base("SetReportHistoryOptionsAction", service)
		{
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00037C20 File Offset: 0x00035E20
		protected override void AddActionToBatch()
		{
			string text;
			string text2;
			base.Service.GetBatchSettingsForScheduleDefinition(base.ActionParameters.Schedule, out text, out text2);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetReportHistoryOptions, base.ActionParameters.ReportPath, "Report", text, null, base.ActionParameters.KeepExecutionSnapshots.ToString(), null, base.ActionParameters.ManualCreationEnabled, null, text2);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00037CA0 File Offset: 0x00035EA0
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.ManualCreationEnabled = parameters.BoolParam;
			base.ActionParameters.KeepExecutionSnapshots = bool.Parse(parameters.Param);
			base.ActionParameters.Schedule = base.Service.RetriveScheduleFromBatchStrings(parameters.Parent, parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00037D10 File Offset: 0x00035F10
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.LoadProperties();
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.UpdatePolicy);
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.CreateSnapshot);
			int num = baseReportCatalogItem.ExecutionOptions;
			if (base.ActionParameters.Schedule == null || base.ActionParameters.Schedule is NoSchedule)
			{
				num &= ~ExecutionOptions.HistoryOnSchedule;
			}
			else
			{
				num |= ExecutionOptions.HistoryOnSchedule;
				baseReportCatalogItem.ThrowIfNotUsableByProperties();
				baseReportCatalogItem.LoadParameters();
				baseReportCatalogItem.ThrowIfNotGoodForUnattended(true);
			}
			if (base.ActionParameters.ManualCreationEnabled)
			{
				num &= ~ExecutionOptions.DisableManualHistory;
			}
			else
			{
				num |= ExecutionOptions.DisableManualHistory;
			}
			if (base.ActionParameters.KeepExecutionSnapshots)
			{
				num |= ExecutionOptions.KeepExecutionSnapshots;
			}
			else
			{
				num &= ~ExecutionOptions.KeepExecutionSnapshots;
			}
			base.Service.ExecCacheDb.SetExecutionOptionsIfChanged(catalogItemContext.CatalogItemPath, num, baseReportCatalogItem.ExecutionOptions);
			base.Service.SchedCoordinator.SetReportSchedule(baseReportCatalogItem.ItemID, base.ActionParameters.Schedule, ReportScheduleActions.CreateReportHistorySnapshot);
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00037632 File Offset: 0x00035832
		protected override IsolationLevel IsolationLevel
		{
			get
			{
				return IsolationLevel.RepeatableRead;
			}
		}
	}
}
