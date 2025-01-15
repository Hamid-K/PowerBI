using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000143 RID: 323
	internal sealed class GetDrillthroughReportsAction : RSSoapAction<GetDrillthroughReportsActionParameters>
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002F0BC File Offset: 0x0002D2BC
		public GetDrillthroughReportsAction(RSService service)
			: base("GetDrillthroughReportsAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DynamicDrillthrough);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002F0DC File Offset: 0x0002D2DC
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ModelPath, "Model");
			ModelCatalogItem modelCatalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true) as ModelCatalogItem;
			ModelItem modelItem = modelCatalogItem.LoadUserModelAndGetEntity(base.ActionParameters.ModelItemID);
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.ReadContent);
			string text = ModelItem.IDToString(modelItem.ID);
			base.ActionParameters.Reports = base.Service.Storage.GetDrillthroughReports(modelCatalogItem.CatalogItemID, text, base.Service);
		}
	}
}
