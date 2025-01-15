using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200019E RID: 414
	internal sealed class GetExecutionOptionsAction : RSSoapAction<ExecutionOptionsActionParameters>
	{
		// Token: 0x06000F1E RID: 3870 RVA: 0x00036AA4 File Offset: 0x00034CA4
		public GetExecutionOptionsAction(RSService service)
			: base("RSGetExecutionOptionsAction", service)
		{
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00036AB4 File Offset: 0x00034CB4
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadPolicy);
			ExecutionSettingEnum executionSettingEnum;
			ScheduleDefinitionOrReference scheduleDefinitionOrReference;
			base.Service.ExecCacheDb.GetExecutionOptions(catalogItemContext.CatalogItemPath, baseReportCatalogItem.ItemID, out executionSettingEnum, out scheduleDefinitionOrReference);
			base.ActionParameters.ExecutionSettings = executionSettingEnum;
			base.ActionParameters.Schedule = scheduleDefinitionOrReference;
		}
	}
}
