using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A7 RID: 423
	internal sealed class SetReportParametersAction : RSSoapAction<SetReportParametersActionParameters>
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x00036F85 File Offset: 0x00035185
		internal SetReportParametersAction(RSService service)
			: base("SetReportParametersAction", service)
		{
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00036F94 File Offset: 0x00035194
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetReportParameters, base.ActionParameters.ItemPath, "Report", null, null, null, null, false, null, base.ActionParameters.Parameters.ToXmlWithTransientState());
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00036FE9 File Offset: 0x000351E9
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.Parameters = ParameterInfoCollection.DecodeFromXml(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00037018 File Offset: 0x00035218
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.LoadParameters();
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.UpdateParameters);
			LinkedReportCatalogItem linkedReportCatalogItem = baseReportCatalogItem as LinkedReportCatalogItem;
			if (linkedReportCatalogItem != null)
			{
				linkedReportCatalogItem.EnsureLinkID();
			}
			baseReportCatalogItem.CombineParameters(base.ActionParameters.Parameters);
			baseReportCatalogItem.SaveParameters();
		}
	}
}
