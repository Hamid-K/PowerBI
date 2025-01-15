using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200004F RID: 79
	internal abstract class CreateSystemResourceActionBase<TParameterType, TCreatedType> : CreateItemAction<TParameterType, TCreatedType> where TParameterType : CreateItemActionParameters, new() where TCreatedType : CatalogItem
	{
		// Token: 0x06000394 RID: 916 RVA: 0x0000FFB1 File Offset: 0x0000E1B1
		internal CreateSystemResourceActionBase(string actionName, RSService service)
			: base(actionName, service)
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000FFBC File Offset: 0x0000E1BC
		internal override void PerformActionNow()
		{
			this.InitAndCheckParams();
			base.ActionParameters.ItemName = CatalogItemNameUtility.ValidateAndTrimItemName(base.ActionParameters.ItemName, "name");
			string parentPath = base.ActionParameters.ParentPath;
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, parentPath, "parent");
			if (!SystemResourceManager.IsSystemResourcePath(catalogItemContext.ItemPathAsString))
			{
				throw new InvalidItemPathException(parentPath);
			}
			string text = CatalogItemNameUtility.BuildChildPath(catalogItemContext.ItemPath.Value, base.ActionParameters.ItemName);
			CatalogItemContext catalogItemContext2 = new CatalogItemContext(base.Service, text, "ItemPath");
			if (base.Service.Storage.ObjectExists(catalogItemContext2.ItemPath))
			{
				throw new ItemAlreadyExistsException(text);
			}
			base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext).ThrowIfWrongItemType(new ItemType[] { ItemType.Folder });
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.UpdateSystemProperties, null))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			TCreatedType tcreatedType = CatalogItemFactory.CreateCatalogItem(base.ItemType, base.Service) as TCreatedType;
			RSTrace.CatalogTrace.Assert(tcreatedType != null, "item is not TCreatedType");
			this.PerformCreateActions(catalogItemContext, catalogItemContext2, tcreatedType);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010119 File Offset: 0x0000E319
		private void PerformCreateActions(CatalogItemContext parentContext, CatalogItemContext itemContext, TCreatedType item)
		{
			this.InitializeNewItem(item, itemContext);
			this.CreateNewItem(item, parentContext);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001012C File Offset: 0x0000E32C
		private void InitializeNewItem(TCreatedType item, CatalogItemContext itemContext)
		{
			item.Initialize(itemContext, Guid.Empty, null);
			string userName = base.Service.UserName;
			DateTime now = DateTime.Now;
			item.ModifiedBy = userName;
			item.ModificationDate = now;
			item.CreatedBy = userName;
			item.CreationDate = now;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00010190 File Offset: 0x0000E390
		private void CreateNewItem(TCreatedType item, CatalogItemContext parentContext)
		{
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(parentContext);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Folder,
				ItemType.Site
			});
			item.Parent = catalogItem;
			ItemProperties itemProperties = new ItemProperties(base.ActionParameters.Properties, item.ThisItemType);
			itemProperties.EnsurePropertiesWritable();
			item.CombineProperties(itemProperties);
			this.PerformVirtualItemSecurityCheck(item);
			this.PrepareForNewItem(item);
			this.CreateItem(item);
			this.FinalizeNewItem(item);
			this.CollectAndReturnItemInfo(item);
		}
	}
}
