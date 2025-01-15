using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B7 RID: 439
	internal sealed class SetSnapshotLimitAction : RSSoapAction<SetSnapshotLimitActionParameters>
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x00037F99 File Offset: 0x00036199
		public SetSnapshotLimitAction(RSService service)
			: base("SetSnapshotLimitAction", service)
		{
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00037FA8 File Offset: 0x000361A8
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetSnapshotLimit, base.ActionParameters.ReportPath, "report", null, null, base.ActionParameters.ScopedLimit.ToString(CultureInfo.InvariantCulture), "historyLimit", base.ActionParameters.UseSystem, null, null);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00038014 File Offset: 0x00036214
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.UseSystem = parameters.BoolParam;
			base.ActionParameters.ScopedLimit = int.Parse(parameters.Param, CultureInfo.InvariantCulture);
			this.PerformActionNow();
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00038064 File Offset: 0x00036264
		internal override void PerformActionNow()
		{
			if (base.ActionParameters.UseSystem)
			{
				base.ActionParameters.ScopedLimit = -2;
			}
			else if (base.ActionParameters.ScopedLimit <= 0 && base.ActionParameters.ScopedLimit != -1)
			{
				throw new InvalidParameterException("HistoryLimit");
			}
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.UpdatePolicy);
			int systemSnapshotLimit = base.Service.SystemSnapshotLimit;
			int num = baseReportCatalogItem.HistorySnapshotLimit;
			int num2 = base.ActionParameters.ScopedLimit;
			if (num == -2)
			{
				num = systemSnapshotLimit;
			}
			if (num2 == -2)
			{
				num2 = systemSnapshotLimit;
			}
			base.Service.Storage.SetHistoryLimit(catalogItemContext.CatalogItemPath, base.ActionParameters.ScopedLimit);
			if ((num2 < num && num2 >= 0 && num >= 0) || (num == -1 && num2 >= 0) || (num == -2 && num2 >= 0))
			{
				if (num2 == 0)
				{
					base.Service.Storage.DeleteAllHistoryForReport(baseReportCatalogItem.ItemID);
					return;
				}
				base.Service.Storage.CleanHistoryForReport(baseReportCatalogItem.ItemID, num2);
			}
		}
	}
}
