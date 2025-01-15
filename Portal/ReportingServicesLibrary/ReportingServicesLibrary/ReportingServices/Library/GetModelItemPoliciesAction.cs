using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000149 RID: 329
	internal sealed class GetModelItemPoliciesAction : RSSoapAction<GetModelItemPoliciesActionParameters>
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002F440 File Offset: 0x0002D640
		internal GetModelItemPoliciesAction(RSService service)
			: base("GetModelItemPoliciesAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ModelItemSecurity);
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002F460 File Offset: 0x0002D660
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Model");
			ModelCatalogItem modelCatalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true) as ModelCatalogItem;
			ModelItem modelItem = modelCatalogItem.LoadModelAndGetModelItem(base.ActionParameters.ModelItemID);
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.ReadModelItemAuthorizationPolicies);
			ModelItemPolicy policy = modelCatalogItem.ModelItemPolicies.GetPolicy(modelItem);
			base.ActionParameters.InheritParent = policy.Inherited;
			base.ActionParameters.Policy = policy.Rights.XmlDescriptor;
		}
	}
}
