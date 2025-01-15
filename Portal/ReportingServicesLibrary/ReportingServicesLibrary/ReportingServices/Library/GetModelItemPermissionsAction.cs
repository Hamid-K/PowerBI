using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000147 RID: 327
	internal sealed class GetModelItemPermissionsAction : RSSoapAction<GetModelItemPermissionsActionParameters>
	{
		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002F296 File Offset: 0x0002D496
		internal GetModelItemPermissionsAction(RSService service)
			: base("GetModelItemPermissionsAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ModelItemSecurity);
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002F2B8 File Offset: 0x0002D4B8
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Model");
			ModelCatalogItem modelCatalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true) as ModelCatalogItem;
			ModelItem modelItem = modelCatalogItem.LoadModelAndGetModelItem(base.ActionParameters.ModelItemID);
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.ReadProperties);
			ModelItemPolicy policy = modelCatalogItem.ModelItemPolicies.GetPolicy(modelItem);
			if (policy.Rights.BinaryDescriptor == null)
			{
				base.ActionParameters.Permissions = null;
				return;
			}
			StringCollection modelItemPermissions = base.Service.SecMgr.GetModelItemPermissions(policy.Rights.BinaryDescriptor, new ExternalItemPath(base.ActionParameters.ItemPath));
			if (modelItemPermissions != null)
			{
				base.ActionParameters.Permissions = new string[modelItemPermissions.Count];
				for (int i = 0; i < modelItemPermissions.Count; i++)
				{
					base.ActionParameters.Permissions[i] = modelItemPermissions[i];
				}
			}
		}
	}
}
