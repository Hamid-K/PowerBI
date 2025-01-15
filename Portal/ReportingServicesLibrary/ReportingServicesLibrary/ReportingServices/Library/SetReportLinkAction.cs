using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A5 RID: 421
	internal sealed class SetReportLinkAction : RSSoapAction<SetReportLinkActionParameters>
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x00036E15 File Offset: 0x00035015
		public SetReportLinkAction(RSService service)
			: base("SetReportLinkAction", service)
		{
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00036E24 File Offset: 0x00035024
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetReportLink, base.ActionParameters.ReportPath, "Report", null, null, base.ActionParameters.LinkPath, "Link", false, null, null);
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00036E79 File Offset: 0x00035079
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ReportPath = parameters.Item;
			base.ActionParameters.LinkPath = parameters.Param;
			this.PerformActionNow();
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00036EA4 File Offset: 0x000350A4
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ReportPath, "Report");
			CatalogItemContext catalogItemContext2 = new CatalogItemContext(base.Service, base.ActionParameters.LinkPath, "Link");
			LinkedReportCatalogItem linkedReportCatalogItem = (LinkedReportCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.LinkedReport);
			ProfessionalReportCatalogItem professionalReportCatalogItem = (ProfessionalReportCatalogItem)base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext2, ItemType.Report);
			linkedReportCatalogItem.LoadParameters();
			linkedReportCatalogItem.ThrowIfNoAccess(ReportOperation.UpdateProperties);
			professionalReportCatalogItem.LoadParameters();
			professionalReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadProperties);
			professionalReportCatalogItem.ThrowIfNoAccess(ReportOperation.CreateLink);
			linkedReportCatalogItem.SourceReport = professionalReportCatalogItem;
			linkedReportCatalogItem.SaveParametersAndLink();
		}
	}
}
