using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000EA RID: 234
	internal sealed class GetItemDataSourcePromptsAction : RSSoapAction<GetItemDataSourcePromptsActionParameters>
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x000264E1 File Offset: 0x000246E1
		public GetItemDataSourcePromptsAction(RSService service)
			: base("GetItemDataSourcePromptsAction", service)
		{
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000264F0 File Offset: 0x000246F0
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Item");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.Model,
				ItemType.RdlxReport
			});
			catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
			DataSourcePromptCollection dataSourcePromptCollection = null;
			ServerDataSourceSettings serverDataSourceSettings = new ServerDataSourceSettings(Globals.Configuration.IsSurrogatePresent, Global.EnableIntegratedSecurity);
			if (catalogItem.ThisItemType == ItemType.Report || catalogItem.ThisItemType == ItemType.RdlxReport)
			{
				FullReportCatalogItem fullReportCatalogItem = catalogItem as FullReportCatalogItem;
				RSTrace.CatalogTrace.Assert(fullReportCatalogItem != null, "reportItem != null");
				if (fullReportCatalogItem.RuntimeDataSources != null)
				{
					dataSourcePromptCollection = fullReportCatalogItem.RuntimeDataSources.GetPromptRepresentatives(serverDataSourceSettings);
				}
			}
			else if (catalogItem.ThisItemType == ItemType.Model)
			{
				DataSourceInfoCollection dataSources = base.Service.Storage.GetDataSources(catalogItem.ItemID);
				dataSourcePromptCollection = new DataSourcePromptCollection();
				DataSourceInfo theOnlyDataSource = dataSources.GetTheOnlyDataSource();
				dataSourcePromptCollection.AddSingleIfPrompt(theOnlyDataSource, serverDataSourceSettings);
			}
			else
			{
				RSTrace.CatalogTrace.Assert(false, "Only Reports and Models are allowed at this point!");
			}
			base.ActionParameters.DataSourcePrompts = DataSourcePrompt.CollectionToPromptArray(dataSourcePromptCollection);
		}
	}
}
