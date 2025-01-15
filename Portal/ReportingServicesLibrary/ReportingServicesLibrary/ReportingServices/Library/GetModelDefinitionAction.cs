using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000145 RID: 325
	internal sealed class GetModelDefinitionAction : RSSoapAction<GetModelDefinitionActionParameters>
	{
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002F1A8 File Offset: 0x0002D3A8
		internal GetModelDefinitionAction(RSService service)
			: base("GetModelDefinitionAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ReportBuilder);
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002F1C8 File Offset: 0x0002D3C8
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Model");
			ModelCatalogItem modelCatalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true) as ModelCatalogItem;
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.ReadContent);
			if (modelCatalogItem.Content == null)
			{
				modelCatalogItem.LoadModel(true);
			}
			base.ActionParameters.ModelDefinition = modelCatalogItem.Content;
		}
	}
}
