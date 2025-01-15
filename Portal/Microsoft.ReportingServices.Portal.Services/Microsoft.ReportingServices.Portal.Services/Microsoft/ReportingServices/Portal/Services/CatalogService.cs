using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Portal.Interfaces.Models;
using Microsoft.ReportingServices.Portal.Interfaces.Models.Impl;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Microsoft.ReportingServices.Portal.Services.Interfaces;
using Microsoft.ReportingServices.Portal.Services.Models;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x02000018 RID: 24
	internal sealed class CatalogService : ICatalogService
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000AD6C File Offset: 0x00008F6C
		internal CatalogService(ICatalogItemFactory catalogItemFactory)
		{
			this.catalogItemFactory = catalogItemFactory;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public IEnumerable<ICatalogItem> GetFolderContents(IPrincipal userPrincipal, string path)
		{
			ListChildrenAction listChildrenAction = ServicesUtil.CreateRsService(userPrincipal).ListChildrenAction;
			listChildrenAction.ActionParameters.Recursive = false;
			listChildrenAction.ActionParameters.ItemPath = path;
			listChildrenAction.Execute();
			return listChildrenAction.ActionParameters.Children.Select((CatalogItemDescriptor x) => this.catalogItemFactory.Create(userPrincipal, x));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		public ICatalogItem GetCatalogItem(IPrincipal userPrincipal, string path)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			CatalogItemDescriptor itemDescriptor = null;
			rsService.ExecuteStorageAction(delegate
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(rsService);
				if (!catalogItemContext.SetPath(path))
				{
					throw new InvalidItemPathException(path);
				}
				Microsoft.ReportingServices.Library.CatalogItem catalogItem = rsService.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
				catalogItem.ThrowIfNoAccess(CommonOperation.ReadProperties);
				catalogItem.LoadStoredAndDerivedProperties();
				ItemProperties properties = catalogItem.Properties;
				ExternalItemPath externalItemPath = catalogItemContext.ItemPath;
				if (catalogItemContext.IsRoot)
				{
					properties.Name = CatalogItemNameUtility.PathSeparatorString;
					externalItemPath = ExternalItemPath.CreateWithoutChecks(CatalogItemNameUtility.PathSeparatorString, null);
				}
				itemDescriptor = new CatalogItemDescriptor
				{
					ID = catalogItem.ItemID.ToString(),
					Description = (catalogItem.Description ?? properties.Description),
					Name = properties.Name,
					CreatedBy = properties.CreatedBy,
					CreationDate = Globals.ParsePublicDateTimeFormat(properties.CreationDate),
					ModifiedBy = properties.ModifiedBy,
					ModifiedDate = Globals.ParsePublicDateTimeFormat(properties.ModifiedDate),
					Path = externalItemPath,
					Type = catalogItem.ThisItemType,
					Hidden = bool.Parse(properties.Hidden),
					Size = int.Parse(properties.Size, CultureInfo.InvariantCulture)
				};
			}, ConnectionTransactionType.AutoCommit);
			return this.catalogItemFactory.Create(userPrincipal, itemDescriptor);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AE40 File Offset: 0x00009040
		public ItemPolicy GetItemPolicy(IPrincipal userPrincipal, string path)
		{
			GetPoliciesAction getPoliciesAction = ServicesUtil.CreateRsService(userPrincipal).GetPoliciesAction;
			getPoliciesAction.ActionParameters.ItemPath = path;
			getPoliciesAction.Execute();
			ItemPolicy itemPolicy = new ItemPolicy();
			itemPolicy.InheritParentPolicy = getPoliciesAction.ActionParameters.InheritParent;
			itemPolicy.Policies = getPoliciesAction.ActionParameters.Policies.Select((Microsoft.ReportingServices.Library.Soap.Policy x) => x.ToWebApiPolicy());
			return itemPolicy;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public void SetItemPolicy(IPrincipal userPrincipal, string path, ItemPolicy itemPolicy)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			if (itemPolicy.InheritParentPolicy)
			{
				DeletePoliciesAction deletePoliciesAction = rsservice.DeletePoliciesAction;
				deletePoliciesAction.BatchID = Guid.Empty;
				deletePoliciesAction.ActionParameters.ItemPath = path;
				deletePoliciesAction.Execute();
				return;
			}
			Microsoft.ReportingServices.Library.Soap.Policy[] array = itemPolicy.Policies.Select((Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Policy x) => x.ToSoapPolicy()).ToArray<Microsoft.ReportingServices.Library.Soap.Policy>();
			SetPoliciesAction setPoliciesAction = rsservice.SetPoliciesAction;
			setPoliciesAction.BatchID = Guid.Empty;
			setPoliciesAction.ActionParameters.ItemPath = path;
			setPoliciesAction.ActionParameters.Policies = Microsoft.ReportingServices.Library.Soap.Policy.PolicyArrayToXml(array);
			setPoliciesAction.Execute();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000AF5C File Offset: 0x0000915C
		public IEnumerable<Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Role> GetCatalogRoles(IPrincipal userPrincipal)
		{
			ListRolesAction listRolesAction = ServicesUtil.CreateListRolesAction(ServicesUtil.CreateRsService(userPrincipal), SecurityScopeEnum.Catalog);
			listRolesAction.Execute();
			return listRolesAction.ActionParameters.Roles.Select((Microsoft.ReportingServices.Library.Soap.Role x) => x.ToWebApiRole());
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000AFAC File Offset: 0x000091AC
		public IEnumerable<Microsoft.ReportingServices.Portal.Interfaces.Models.Impl.Subscription> GetItemSubscriptions(IPrincipal userPrincipal, string path)
		{
			ListSubscriptionsAction listSubscriptionsAction = ServicesUtil.CreateRsService(userPrincipal).ListSubscriptionsAction;
			listSubscriptionsAction.ActionParameters.Path = path;
			listSubscriptionsAction.ActionParameters.PathIsSiteOrFolder = false;
			listSubscriptionsAction.Execute();
			return listSubscriptionsAction.ActionParameters.Children.Select((SubscriptionImpl x) => x.ToWebApiSubscription());
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000B010 File Offset: 0x00009210
		public void DeleteSubscription(IPrincipal userPrincipal, string id)
		{
			DeleteSubscriptionAction deleteSubscriptionAction = ServicesUtil.CreateRsService(userPrincipal).DeleteSubscriptionAction;
			deleteSubscriptionAction.ActionParameters.SubscriptionID = id;
			deleteSubscriptionAction.Execute();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000B02E File Offset: 0x0000922E
		public void EnableSubscription(IPrincipal userPrincipal, string id)
		{
			EnableSubscriptionAction enableSubscriptionAction = ServicesUtil.CreateRsService(userPrincipal).EnableSubscriptionAction;
			enableSubscriptionAction.ActionParameters.SubscriptionID = id;
			enableSubscriptionAction.Execute();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000B04C File Offset: 0x0000924C
		public void DisableSubscription(IPrincipal userPrincipal, string id)
		{
			DisableSubscriptionAction disableSubscriptionAction = ServicesUtil.CreateRsService(userPrincipal).DisableSubscriptionAction;
			disableSubscriptionAction.ActionParameters.SubscriptionID = id;
			disableSubscriptionAction.Execute();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000B06A File Offset: 0x0000926A
		public void CreateFolder(IPrincipal userPrincipal, string path, string name)
		{
			CreateFolderAction createFolderAction = ServicesUtil.CreateRsService(userPrincipal).CreateFolderAction;
			createFolderAction.ActionParameters.ParentPath = path;
			createFolderAction.ActionParameters.ItemName = name;
			createFolderAction.Execute();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000B094 File Offset: 0x00009294
		public void DeleteItem(IPrincipal userPrincipal, string path)
		{
			DeleteItemAction deleteItemAction = ServicesUtil.CreateRsService(userPrincipal).DeleteItemAction;
			deleteItemAction.ActionParameters.ItemPath = path;
			deleteItemAction.Execute();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public void UpdateFolder(IPrincipal userPrincipal, string path, string oldName, IFolderCatalogItem newItem)
		{
			if (!path.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				path += "/";
			}
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			Guid guid = rsservice.CreateBatch();
			SetPropertiesAction setPropertiesAction = rsservice.SetPropertiesAction;
			setPropertiesAction.BatchID = guid;
			setPropertiesAction.ActionParameters.ItemPath = path + oldName;
			setPropertiesAction.ActionParameters.Properties = new Property[2];
			setPropertiesAction.ActionParameters.Properties[0] = new Property
			{
				Name = "Description",
				Value = newItem.Description
			};
			setPropertiesAction.ActionParameters.Properties[1] = new Property
			{
				Name = "Hidden",
				Value = (newItem.Hidden ? "true" : "false")
			};
			setPropertiesAction.Execute();
			if (!oldName.Equals(newItem.Name, StringComparison.Ordinal))
			{
				MoveItemAction moveItemAction = rsservice.MoveItemAction;
				moveItemAction.BatchID = guid;
				moveItemAction.ActionParameters.SourceItemPath = path + oldName;
				moveItemAction.ActionParameters.TargetItemPath = path + newItem.Name;
				moveItemAction.Execute();
			}
			rsservice.ExecuteBatch(guid);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000B1D4 File Offset: 0x000093D4
		public void MoveItem(IPrincipal userPrincipal, string sourcePath, string targetPath)
		{
			MoveItemAction moveItemAction = ServicesUtil.CreateRsService(userPrincipal).MoveItemAction;
			moveItemAction.ActionParameters.SourceItemPath = sourcePath;
			moveItemAction.ActionParameters.TargetItemPath = targetPath;
			moveItemAction.Execute();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000B200 File Offset: 0x00009400
		internal void ResolveCatalogItem(Guid id, string path, ItemType itemType, out Guid actualId, out string actualPath)
		{
			Guid outId = Guid.Empty;
			string outPath = null;
			RSService rsService = new RSService(false);
			rsService.ExecuteStorageAction(delegate
			{
				rsService.ResolveCatalogItem(id, path, itemType, true, out outId, out outPath);
			}, ConnectionTransactionType.AutoCommit);
			actualId = outId;
			actualPath = outPath;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000B274 File Offset: 0x00009474
		public IDictionary<string, string> GetItemProperties(IPrincipal userPrincipal, string path)
		{
			GetPropertiesAction getPropertiesAction = ServicesUtil.CreateRsService(userPrincipal).GetPropertiesAction;
			getPropertiesAction.ActionParameters.ItemPath = path;
			getPropertiesAction.Execute();
			return getPropertiesAction.ActionParameters.PropertyValues.ToDictionary((Property x) => x.Name, (Property x) => x.Value);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000B2EC File Offset: 0x000094EC
		public IResourceCatalogItem GetResource(IPrincipal userPrincipal, string path)
		{
			GetResourceContentsAction getResourceContentsAction = ServicesUtil.CreateRsService(userPrincipal).GetResourceContentsAction;
			GetResourceContentsActionParameters actionParameters = getResourceContentsAction.ActionParameters;
			actionParameters.ItemPath = path;
			getResourceContentsAction.Execute();
			return new Microsoft.ReportingServices.Portal.Services.Models.ResourceCatalogItem
			{
				Content = actionParameters.Content,
				ContentType = actionParameters.MimeType
			};
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000B334 File Offset: 0x00009534
		public string GetCatalogItemDownloadFileName(Microsoft.ReportingServices.Portal.Services.Models.CatalogItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000070 RID: 112
		private readonly ICatalogItemFactory catalogItemFactory;
	}
}
