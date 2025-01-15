using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200014D RID: 333
	internal sealed class ListModelItemChildrenAction : RSSoapAction<ListModelItemChildrenActionParameters>
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x0002F7B0 File Offset: 0x0002D9B0
		internal ListModelItemChildrenAction(RSService service)
			: base("ListModelItemChildrenAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ReportBuilder);
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002F7D0 File Offset: 0x0002D9D0
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Model");
			ModelCatalogItem modelCatalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true) as ModelCatalogItem;
			Microsoft.ReportingServices.Modeling.ModelItem modelItem = modelCatalogItem.LoadUserModelAndGetModelItem(base.ActionParameters.ModelItemID);
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.ReadProperties);
			base.ActionParameters.ModelItemChildren = this.GetModelItemChildren(modelItem);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002F83C File Offset: 0x0002DA3C
		private Microsoft.ReportingServices.Library.Soap2005.ModelItem[] GetModelItemChildren(Microsoft.ReportingServices.Modeling.ModelItem modelItem)
		{
			List<Microsoft.ReportingServices.Library.Soap2005.ModelItem> list = new List<Microsoft.ReportingServices.Library.Soap2005.ModelItem>();
			foreach (Microsoft.ReportingServices.Modeling.ModelItem modelItem2 in modelItem.GetOwnedItems())
			{
				Microsoft.ReportingServices.Library.Soap2005.ModelItem modelItem3 = new Microsoft.ReportingServices.Library.Soap2005.ModelItem();
				modelItem3.ID = Microsoft.ReportingServices.Modeling.ModelItem.IDToString(modelItem2.ID);
				modelItem3.Name = modelItem2.Name;
				modelItem3.Description = modelItem2.Description;
				if (!(modelItem2 is Perspective))
				{
					modelItem3.Type = this.GetModelItemType(modelItem2);
					if (base.ActionParameters.Recursive)
					{
						modelItem3.ModelItems = this.GetModelItemChildren(modelItem2);
					}
					else
					{
						modelItem3.ModelItems = null;
					}
					list.Add(modelItem3);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002F8FC File Offset: 0x0002DAFC
		private ModelItemTypeEnum GetModelItemType(Microsoft.ReportingServices.Modeling.ModelItem modelItem)
		{
			if (modelItem is SemanticModel)
			{
				return ModelItemTypeEnum.Model;
			}
			if (modelItem is ModelEntityFolder)
			{
				return ModelItemTypeEnum.EntityFolder;
			}
			if (modelItem is ModelFieldFolder)
			{
				return ModelItemTypeEnum.FieldFolder;
			}
			if (modelItem is ModelEntity)
			{
				return ModelItemTypeEnum.Entity;
			}
			if (modelItem is ModelAttribute)
			{
				return ModelItemTypeEnum.Attribute;
			}
			if (modelItem is ModelRole)
			{
				return ModelItemTypeEnum.Role;
			}
			throw new InternalCatalogException("Model Item Type is not supported!");
		}
	}
}
