using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200014F RID: 335
	internal class ListModelPerspectivesAction : RSSoapAction<ListModelPerspectivesActionParameters>
	{
		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002F979 File Offset: 0x0002DB79
		internal ListModelPerspectivesAction(RSService service)
			: base("ListModelPerspectivesAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ReportBuilder);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002F998 File Offset: 0x0002DB98
		internal override void PerformActionNow()
		{
			ModelCatalogItem.ModelStorage modelStorage = new ModelCatalogItem.ModelStorage(base.Service);
			if (base.ActionParameters.ItemPath == null)
			{
				base.ActionParameters.ModelsWithPerspectives = modelStorage.ListAllModelsAndPerspectives(null, null, base.Service);
				return;
			}
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Model");
			ModelCatalogItem modelCatalogItem = modelStorage.ListModelPerspectives(catalogItemContext, base.Service);
			base.ActionParameters.ModelsWithPerspectives = new ModelCatalogItem[1];
			base.ActionParameters.ModelsWithPerspectives[0] = modelCatalogItem;
		}
	}
}
