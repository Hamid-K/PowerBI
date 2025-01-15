using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000155 RID: 341
	internal sealed class SetModelItemPoliciesAction : RSSoapAction<SetModelItemPoliciesActionParameters>
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x0002FF81 File Offset: 0x0002E181
		internal SetModelItemPoliciesAction(RSService service)
			: base("SetModelItemPoliciesAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ModelItemSecurity);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002FFA0 File Offset: 0x0002E1A0
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetModelItemPolicies, base.ActionParameters.ItemPath, "Model", null, null, base.ActionParameters.ModelItemID, "ModelItemID", base.ActionParameters.InheritParent, null, base.ActionParameters.Policy);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003000C File Offset: 0x0002E20C
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Model");
			ModelCatalogItem modelCatalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true) as ModelCatalogItem;
			ModelItem modelItem = modelCatalogItem.LoadModelAndGetModelItem(base.ActionParameters.ModelItemID);
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.UpdateModelItemAuthorizationPolicies);
			if (base.ActionParameters.InheritParent)
			{
				if (modelItem.ParentItem == null)
				{
					throw new CannotDeleteRootPolicyException();
				}
				base.Service.SecMgr.DeleteModelItemPolicy(modelCatalogItem.CatalogItemID, ModelItem.IDToString(modelItem.ID), modelItem.Name);
			}
			else
			{
				if (!modelCatalogItem.CanSetModelItemPolicy(modelItem))
				{
					throw new ModelRootPolicyRequiredException();
				}
				base.Service.SecMgr.SetModelItemPolicies(modelCatalogItem.CatalogItemID, ModelItem.IDToString(modelItem.ID), base.ActionParameters.Policy, new ExternalItemPath(base.ActionParameters.ItemPath));
			}
			CatalogItemList catalogItemList;
			base.Service.Storage.FindItemsByDataSource(modelCatalogItem.CatalogItemID, out catalogItemList, base.Service.SecMgr, base.Service, true);
			foreach (CatalogItemDescriptor catalogItemDescriptor in catalogItemList)
			{
				if (catalogItemDescriptor.Type == ItemType.Report || catalogItemDescriptor.Type == ItemType.DataSet)
				{
					CatalogItemPath catalogItemPath = base.Service.ExternalToCatalogItemPath(catalogItemDescriptor.Path);
					bool flag;
					ExpirationDefinition expirationDefinition;
					base.Service.ExecCacheDb.GetCacheOptions(catalogItemPath, out flag, out expirationDefinition);
					if (flag)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Invalidating cache of item '{0}'", new object[] { catalogItemDescriptor.Path });
						base.Service.ExecCacheDb.FlushCache(catalogItemPath);
					}
				}
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x000301D8 File Offset: 0x0002E3D8
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.ModelItemID = parameters.Param;
			base.ActionParameters.Policy = parameters.Properties;
			base.ActionParameters.InheritParent = parameters.BoolParam;
			this.PerformActionNow();
		}
	}
}
