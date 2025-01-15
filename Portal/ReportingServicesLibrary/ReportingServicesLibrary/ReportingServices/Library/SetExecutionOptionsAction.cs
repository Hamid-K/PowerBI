using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A9 RID: 425
	internal sealed class SetExecutionOptionsAction : RSSoapAction<ExecutionOptionsActionParameters>
	{
		// Token: 0x06000F5A RID: 3930 RVA: 0x00037134 File Offset: 0x00035334
		public SetExecutionOptionsAction(RSService service)
			: base("SetExecutionOptionsAction", service)
		{
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00037144 File Offset: 0x00035344
		protected override void AddActionToBatch()
		{
			string text = null;
			string text2 = null;
			base.Service.GetBatchSettingsForScheduleDefinition(base.ActionParameters.Schedule, out text, out text2);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetExecutionOptions, base.ActionParameters.ReportPath, "report", text, null, base.ActionParameters.ExecutionSettings.ToString(), null, false, null, text2);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x000371C4 File Offset: 0x000353C4
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.Schedule = base.Service.RetriveScheduleFromBatchStrings(parameters.Parent, parameters.Properties);
			base.ActionParameters.ExecutionSettings = (ExecutionSettingEnum)Enum.Parse(typeof(ExecutionSettingEnum), parameters.Param);
			this.PerformActionNow();
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00037230 File Offset: 0x00035430
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.LinkedReport,
				ItemType.Report
			});
			catalogItem.LoadProperties();
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.UpdatePolicy);
			ScheduleDefinitionOrReference schedule = base.ActionParameters.Schedule;
			int num = baseReportCatalogItem.ExecutionOptions & ~ExecutionOptions.LiveOrSnapshotMask;
			num |= ExecutionOptions.ExecutionSettingEnumToInt(base.ActionParameters.ExecutionSettings);
			if (ExecutionOptions.IsLiveExecution(num) && schedule != null && !(schedule is NoSchedule))
			{
				throw new InvalidParameterCombinationException();
			}
			if (ExecutionOptions.IsSnapshotExecution(num))
			{
				baseReportCatalogItem.ThrowIfNotUsableByProperties();
				baseReportCatalogItem.LoadParameters();
				baseReportCatalogItem.ThrowIfNotGoodForUnattended(true);
			}
			base.Service.ExecCacheDb.SetExecutionOptionsIfChanged(catalogItemContext.CatalogItemPath, num, baseReportCatalogItem.ExecutionOptions);
			base.Service.ExecCacheDb.SetReportSchedule(baseReportCatalogItem.ItemID, schedule, ReportScheduleActions.UpdateReportExecutionSnapshot);
		}
	}
}
