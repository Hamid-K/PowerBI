using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;
using Microsoft.ReportingServices.CatalogAccess.Streams;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Library.Soap2010;
using Microsoft.ReportingServices.Portal.Interfaces;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.SoapProxy;
using Microsoft.ReportingServices.Portal.Services;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;
using Microsoft.ReportingServices.Portal.Services.Util;
using Microsoft.SqlServer.ReportingServices2010;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.Repositories
{
	// Token: 0x02000015 RID: 21
	internal sealed class CatalogItemRepository : ICatalogRepository
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00003778 File Offset: 0x00001978
		internal CatalogItemRepository(ISoapRS2010Proxy soapProxy, ISystemService systemService, ILogger logger, IPortalConfigurationManager portalConfigurationManager)
			: this(soapProxy, systemService, logger, portalConfigurationManager, new CatalogDataModelRoleAccessor(), null, null, null)
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003798 File Offset: 0x00001998
		internal CatalogItemRepository(ISoapRS2010Proxy soapProxy, ISystemService systemService, ILogger logger, IPortalConfigurationManager portalConfigurationManager, ICatalogDataModelRoleAccessor dataModelRoleAccessor, ICatalogDataAccessor catalogDataAcessor = null, ICatalogDataModelDataSourceAccessor dataModelDataSourceAccessor = null, ICrypto crypto = null)
		{
			if (soapProxy == null)
			{
				throw new ArgumentNullException("soapProxy");
			}
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (portalConfigurationManager == null)
			{
				throw new ArgumentNullException("portalConfigurationManager");
			}
			if (portalConfigurationManager.Current == null)
			{
				throw new ArgumentNullException("portalConfigurationManager.Current");
			}
			if (dataModelRoleAccessor == null)
			{
				throw new ArgumentNullException("dataModelRoleAccessor");
			}
			this._soapRS2010Proxy = soapProxy;
			this._systemService = systemService;
			this._logger = logger;
			this._dataModelRoleAccessor = dataModelRoleAccessor;
			this._catalogAccessor = catalogDataAcessor ?? new CatalogDataAccessor();
			this._dataModelDataSourceAccessor = dataModelDataSourceAccessor ?? new CatalogDataModelDataSourceAccessor();
			this._subscriptionHistoryAccessor = new SubscriptionHistoryDataAccessor();
			this._fileSizeRestrictions = portalConfigurationManager.Current.FileSizeRestrictions;
			this._crypto = crypto ?? SymmetricKeyCrypto.Instance;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003898 File Offset: 0x00001A98
		public IQueryable<global::Model.CatalogItem> GetCatalogItems(IPrincipal userPrincipal)
		{
			Folder folder = this.GetCatalogItem(userPrincipal, "/") as Folder;
			if (folder == null)
			{
				return Enumerable.Empty<global::Model.CatalogItem>().AsQueryable<global::Model.CatalogItem>();
			}
			return new Folder[] { folder }.Concat(this.TraverseFolder(userPrincipal, "/", true)).AsQueryable<global::Model.CatalogItem>();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000038E6 File Offset: 0x00001AE6
		public IQueryable<T> GetCatalogItems<T>(IPrincipal userPrincipal) where T : global::Model.CatalogItem
		{
			return this.TraverseFolder<T>(userPrincipal, "/", true).AsQueryable<T>();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000038FA File Offset: 0x00001AFA
		public IQueryable<T> GetCatalogItems<T>(IPrincipal userPrincipal, string path) where T : global::Model.CatalogItem
		{
			return this.TraverseFolder<T>(userPrincipal, path, true).AsQueryable<T>();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000390C File Offset: 0x00001B0C
		private static string GetCatalogPathFromGuid(RSService rsService, Guid key)
		{
			CatalogItemPath catalogItemPath = null;
			rsService.ExecuteStorageAction(delegate
			{
				catalogItemPath = rsService.Storage.GetPathById(key);
			}, ConnectionTransactionType.AutoCommit);
			if (catalogItemPath == null)
			{
				throw new ItemNotFoundException(key.ToString());
			}
			string text = catalogItemPath.Value;
			if (string.IsNullOrEmpty(text))
			{
				text = "/";
			}
			return text;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003988 File Offset: 0x00001B88
		public global::Model.CatalogItem GetCatalogItem(IPrincipal userPrincipal, Guid key)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, key);
			return this.GetCatalogItem(rsservice, userPrincipal, catalogPathFromGuid);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000039B0 File Offset: 0x00001BB0
		public global::Model.CatalogItem GetCatalogItem(IPrincipal userPrincipal, string path)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			return this.GetCatalogItem(rsservice, userPrincipal, path);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000039D0 File Offset: 0x00001BD0
		private global::Model.CatalogItem GetCatalogItem(RSService rsService, IPrincipal userPrincipal, string path)
		{
			CatalogItemFactory catalogItemFactory = new CatalogItemFactory(userPrincipal, this, this._logger, this._catalogAccessor, this._systemService);
			FavoriteableCatalogItemDescriptor itemDescriptor = this.GetItemDescriptor(userPrincipal, rsService, path);
			this.ThrowIfRestrictedItemType(itemDescriptor.Type, path);
			switch (itemDescriptor.Type)
			{
			case ItemType.LinkedReport:
				return this.GetLinkedReport(userPrincipal, path);
			case ItemType.DataSource:
				return this.GetDataSource(userPrincipal, path);
			case ItemType.DataSet:
				return this.GetDataSet(userPrincipal, path);
			}
			return catalogItemFactory.Create(itemDescriptor);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003A58 File Offset: 0x00001C58
		public CatalogItemType GetCatalogItemTypeByGuid(IPrincipal userPrincipal, Guid id)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			return this.GetCatalogItem(rsservice, userPrincipal, CatalogItemRepository.GetCatalogPathFromGuid(rsservice, id)).Type;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003A80 File Offset: 0x00001C80
		public Guid? GetCatalogItemGuidByPath(IPrincipal userPrincipal, string path)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			Guid? catalogItemId = null;
			rsService.ExecuteStorageAction(delegate
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(rsService);
				if (!catalogItemContext.SetPath(path))
				{
					throw new InvalidItemPathException(path);
				}
				catalogItemId = rsService.CatalogItemFactory.GetCatalogItemGuidByPath(catalogItemContext);
			});
			return catalogItemId;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003AD0 File Offset: 0x00001CD0
		private FavoriteableCatalogItemDescriptor GetItemDescriptor(IPrincipal userPrincipal, RSService rsService, string path)
		{
			FavoriteableCatalogItemDescriptor itemDescriptor = null;
			rsService.ExecuteStorageAction(delegate
			{
				if (rsService.MyReportsEnabled && Localization.CatalogCultureCompare(path, rsService.PhysicalMyReportsPath) == 0)
				{
					rsService.EnsureMyReportsExists();
				}
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
				itemDescriptor = new FavoriteableCatalogItemDescriptor
				{
					ID = catalogItem.ItemID.ToString(),
					Description = (catalogItem.Description ?? properties.Description),
					Name = properties.Name,
					CreatedBy = properties.CreatedBy,
					CreationDate = Globals.ParsePublicDateTimeFormat(properties.CreationDate),
					MimeType = properties.MimeType,
					ModifiedBy = properties.ModifiedBy,
					ModifiedDate = Globals.ParsePublicDateTimeFormat(properties.ModifiedDate),
					Path = externalItemPath,
					Type = catalogItem.ThisItemType,
					Hidden = bool.Parse(properties.Hidden),
					Size = int.Parse(properties.Size, CultureInfo.InvariantCulture),
					IsFavorite = rsService.Storage.IsFavoriteItem(rsService.UserName, catalogItem.ItemID)
				};
				itemDescriptor.ItemMetadata.ParentID = new Guid(properties.ParentID);
				itemDescriptor.ItemMetadata.HasDataSources = catalogItem.ItemMetadata.HasDataSources;
				itemDescriptor.ItemMetadata.HasSharedDataSets = catalogItem.ItemMetadata.HasSharedDataSets;
				itemDescriptor.ItemMetadata.HasParameters = catalogItem.ItemMetadata.HasParameters;
			}, ConnectionTransactionType.AutoCommit);
			return itemDescriptor;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003B18 File Offset: 0x00001D18
		public global::Model.CatalogItem GetCatalogItemWithContent(IPrincipal userPrincipal, string path)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			return this.GetCatalogItemWithContentInternal(userPrincipal, rsservice, path, false);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B38 File Offset: 0x00001D38
		public global::Model.CatalogItem GetCatalogItemWithContentTrusted(IPrincipal userPrincipal, string path)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			return this.GetCatalogItemWithContentInternal(userPrincipal, rsservice, path, true);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003B58 File Offset: 0x00001D58
		private global::Model.CatalogItem GetCatalogItemWithContentInternal(IPrincipal userPrincipal, RSService rsService, string path, bool internalUse)
		{
			DateTime now = DateTime.Now;
			Microsoft.ReportingServices.Library.CatalogItem catalogItem = null;
			try
			{
				rsService.ExecuteStorageAction(delegate
				{
					CatalogItemContext catalogItemContext = new CatalogItemContext(rsService);
					if (!catalogItemContext.SetPath(path))
					{
						throw new InvalidItemPathException(path);
					}
					catalogItem = rsService.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
					if (internalUse)
					{
						catalogItem.InternalUsePermissionForExecution = true;
					}
					this.ThrowIfRestrictedItemType(catalogItem.ThisItemType, path);
					catalogItem.LoadDefinition();
				}, ConnectionTransactionType.AutoCommit);
			}
			catch (Exception ex)
			{
				if (ex is RSException)
				{
					ErrorCode code = (ex as RSException).Code;
				}
				throw ex;
			}
			global::Model.CatalogItem item = this.GetCatalogItem(userPrincipal, path);
			if (catalogItem.Content != null && catalogItem.Content.Length != 0)
			{
				item.Content = catalogItem.Content;
			}
			else
			{
				Stream stream = VarbinaryReadableStreamFactory.CreateExtendedContentReadableStream(item.Id, Microsoft.ReportingServices.CatalogAccess.ExtendedContentType.CatalogItem);
				if (stream != null)
				{
					item.SetContent(stream);
				}
			}
			rsService.ExecuteStorageAction(delegate
			{
				catalogItem.LoadProperties();
				ResourceCatalogItem resourceCatalogItem2 = catalogItem as ResourceCatalogItem;
				string text;
				if (resourceCatalogItem2 == null)
				{
					text = null;
				}
				else
				{
					CatalogItemContext itemContext = resourceCatalogItem2.ItemContext;
					text = ((itemContext != null) ? itemContext.ParentPath : null);
				}
				if (!string.IsNullOrEmpty(text))
				{
					RSService rsService2 = rsService;
					string parentPath = resourceCatalogItem2.ItemContext.ParentPath;
					ItemType itemType = ItemType.Resource;
					Microsoft.ReportingServices.Library.CatalogItem catalogItem2 = catalogItem;
					string text2;
					if (catalogItem2 == null)
					{
						text2 = null;
					}
					else
					{
						ItemProperties properties = catalogItem2.Properties;
						text2 = ((properties != null) ? properties.SubType : null);
					}
					if (rsService2.IsCommentAttachment(parentPath, itemType, text2))
					{
						CommentRestrictions.EnsureValidCommentAttachmentForDownload(resourceCatalogItem2.ItemContext.ItemName);
						if (!CommentRestrictions.IsValidCommentAttachmentMimeType(resourceCatalogItem2.MimeType))
						{
							item.ContentType = "application/octet-stream";
						}
					}
				}
			});
			if (!rsService.IsTrustedFileType(item.Name) || !rsService.IsTrustedContentType(item.ContentType))
			{
				item.ContentType = "application/octet-stream";
			}
			if (item.Type == CatalogItemType.Resource && string.IsNullOrEmpty(Path.GetExtension(item.Name)))
			{
				try
				{
					rsService.ExecuteStorageAction(delegate
					{
						catalogItem.LoadProperties();
					}, ConnectionTransactionType.AutoCommit);
					ResourceCatalogItem resourceCatalogItem = catalogItem as ResourceCatalogItem;
					if (item.Path.EndsWith("fbac82c8-9bad-4dba-929f-c04e7ca4111f/logo"))
					{
						item.ContentType = resourceCatalogItem.MimeType;
					}
					else if (catalogItem.Properties != null && catalogItem.Properties.SubType != null)
					{
						item.ContentType = "image/png";
					}
				}
				catch (Exception ex2)
				{
					if (ex2 is RSException)
					{
						ErrorCode code2 = (ex2 as RSException).Code;
					}
					throw ex2;
				}
			}
			return item;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003D6C File Offset: 0x00001F6C
		private string GetInstanceName(string machineName)
		{
			string text = machineName;
			string instanceName = Globals.Configuration.InstanceName;
			if (!string.IsNullOrEmpty(instanceName))
			{
				text = machineName + "\\" + instanceName;
			}
			return text;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003D9C File Offset: 0x00001F9C
		public global::Model.CatalogItem GetCatalogItemWithContent(IPrincipal userPrincipal, Guid key)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, key);
			return this.GetCatalogItemWithContentInternal(userPrincipal, rsservice, catalogPathFromGuid, false);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public global::Model.CatalogItem GetCatalogItemWithContentTrusted(IPrincipal userPrincipal, Guid key)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, key);
			return this.GetCatalogItemWithContentInternal(userPrincipal, rsservice, catalogPathFromGuid, true);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003DEC File Offset: 0x00001FEC
		public string GetCatalogItemDownloadFileName(global::Model.CatalogItem item)
		{
			if (item == null)
			{
				return "";
			}
			CatalogItemType type = item.Type;
			if (type == CatalogItemType.Report)
			{
				return CatalogItemRepository.EnsureExtension(item.Name, ".rdl", "report");
			}
			if (type == CatalogItemType.DataSet)
			{
				return CatalogItemRepository.EnsureExtension(item.Name, ".rsd", "dataset");
			}
			if (type != CatalogItemType.PowerBIReport)
			{
				return item.Name;
			}
			return CatalogItemRepository.EnsureExtension(item.Name, ".pbix", "report");
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003E60 File Offset: 0x00002060
		private static string EnsureExtension(string fileName, string extension, string defaultFileName)
		{
			if (string.IsNullOrWhiteSpace(fileName))
			{
				return defaultFileName + extension;
			}
			if (fileName.ToLower().EndsWith(extension))
			{
				return fileName;
			}
			fileName = fileName.TrimEnd(new char[] { '.' });
			return fileName + extension;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003E9C File Offset: 0x0000209C
		public IEnumerable<global::Model.CatalogItem> TraverseFolder(IPrincipal userPrincipal, string itemPath, bool recursive)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			CatalogItemFactory catalogItemFactory = new CatalogItemFactory(userPrincipal, this, this._logger, this._catalogAccessor, this._systemService);
			ListFavoriteableItemsAction listFavoriteableItemsAction = rsservice.ListFavoriteableItemsAction;
			listFavoriteableItemsAction.ActionParameters.Recursive = recursive;
			listFavoriteableItemsAction.ActionParameters.ItemPath = itemPath;
			listFavoriteableItemsAction.Execute();
			return (from c in listFavoriteableItemsAction.ActionParameters.Items.Where((FavoriteableCatalogItemDescriptor i) => !this.IsRestrictedItemType(i.Type)).Select(delegate(FavoriteableCatalogItemDescriptor i)
				{
					if (i.ID != null)
					{
						return this.GetCatalogItem(userPrincipal, new Guid(i.ID));
					}
					return catalogItemFactory.Create(i);
				})
				where c != null
				select c).AsQueryable<global::Model.CatalogItem>();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003F64 File Offset: 0x00002164
		public IEnumerable<T> TraverseFolder<T>(IPrincipal userPrincipal, string itemPath, bool recursive) where T : global::Model.CatalogItem
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			CatalogItemFactory catalogItemFactory = new CatalogItemFactory(userPrincipal, this, this._logger, this._catalogAccessor, this._systemService);
			ListFavoriteableItemsAction listFavoriteableItemsAction = rsservice.ListFavoriteableItemsAction;
			listFavoriteableItemsAction.ActionParameters.Recursive = recursive;
			listFavoriteableItemsAction.ActionParameters.ItemPath = itemPath;
			listFavoriteableItemsAction.Execute();
			return (from c in listFavoriteableItemsAction.ActionParameters.Items.Where((FavoriteableCatalogItemDescriptor i) => !this.IsRestrictedItemType(i.Type) && i.Type == (ItemType)Enum.Parse(typeof(ItemType), typeof(T).Name)).Select(delegate(FavoriteableCatalogItemDescriptor i)
				{
					if (i.ID != null)
					{
						return this.GetCatalogItem(userPrincipal, new Guid(i.ID));
					}
					return catalogItemFactory.Create(i);
				}).OfType<T>()
				where c != null
				select c).AsQueryable<T>();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004034 File Offset: 0x00002234
		public ItemPolicy GetItemPolicy(IPrincipal userPrincipal, string itemPath)
		{
			GetPoliciesAction getPoliciesAction = ServicesUtil.CreateRsService(userPrincipal).GetPoliciesAction;
			getPoliciesAction.ActionParameters.ItemPath = itemPath;
			getPoliciesAction.Execute();
			return new ItemPolicy
			{
				InheritParentPolicy = getPoliciesAction.ActionParameters.InheritParent,
				Policies = getPoliciesAction.ActionParameters.Policies.Select(new Func<Microsoft.ReportingServices.Library.Soap.Policy, global::Model.Policy>(Microsoft.ReportingServices.Portal.Services.ODataExtensions.PolicyExtensions.ToWebApiPolicy))
			};
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004098 File Offset: 0x00002298
		public void SetItemPolicy(IPrincipal userPrincipal, string itemPath, ItemPolicy itemPolicy)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			if (itemPolicy.InheritParentPolicy)
			{
				DeletePoliciesAction deletePoliciesAction = rsservice.DeletePoliciesAction;
				deletePoliciesAction.BatchID = Guid.Empty;
				deletePoliciesAction.ActionParameters.ItemPath = itemPath;
				deletePoliciesAction.Execute();
				return;
			}
			Microsoft.ReportingServices.Library.Soap.Policy[] array = itemPolicy.Policies.Select((global::Model.Policy x) => x.ToSoapPolicy()).ToArray<Microsoft.ReportingServices.Library.Soap.Policy>();
			SetPoliciesAction setPoliciesAction = rsservice.SetPoliciesAction;
			setPoliciesAction.BatchID = Guid.Empty;
			setPoliciesAction.ActionParameters.ItemPath = itemPath;
			setPoliciesAction.ActionParameters.Policies = Microsoft.ReportingServices.Library.Soap.Policy.PolicyArrayToXml(array);
			setPoliciesAction.Execute();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000413A File Offset: 0x0000233A
		public IQueryable<global::Model.Role> GetCatalogRoles(IPrincipal userPrincipal)
		{
			ListRolesAction listRolesAction = ServicesUtil.CreateListRolesAction(ServicesUtil.CreateRsService(userPrincipal), SecurityScopeEnum.Catalog);
			listRolesAction.Execute();
			return listRolesAction.ActionParameters.Roles.Select(new Func<Microsoft.ReportingServices.Library.Soap.Role, global::Model.Role>(Microsoft.ReportingServices.Portal.Services.ODataExtensions.RoleExtensions.ToWebApiRole)).AsQueryable<global::Model.Role>();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004170 File Offset: 0x00002370
		public global::Model.CatalogItem CreateFolder(IPrincipal userPrincipal, string parentPath, string folderName)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			CreateFolderAction createFolderAction = rsservice.CreateFolderAction;
			createFolderAction.ActionParameters.ParentPath = parentPath;
			createFolderAction.ActionParameters.ItemName = folderName;
			createFolderAction.Execute();
			return this.GetCatalogItem(rsservice, userPrincipal, this.PathCombine(parentPath, folderName));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000041B7 File Offset: 0x000023B7
		private string PathCombine(string path1, string path2)
		{
			if (path1.EndsWith("/"))
			{
				path1 += path2;
			}
			else
			{
				path1 = path1 + "/" + path2;
			}
			return path1;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000041E0 File Offset: 0x000023E0
		public IQueryable<global::Model.Property> GetItemProperties(IPrincipal userPrincipal, string path, IEnumerable<global::Model.Property> requestedPropertiesproperties)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			return this.GetItemProperties(rsservice, path, requestedPropertiesproperties);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004200 File Offset: 0x00002400
		public IQueryable<global::Model.Property> GetItemProperties(IPrincipal userPrincipal, Guid guid, IEnumerable<global::Model.Property> requestedPropertiesproperties)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, guid);
			return this.GetItemProperties(rsservice, catalogPathFromGuid, requestedPropertiesproperties);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004228 File Offset: 0x00002428
		private IQueryable<global::Model.Property> GetItemProperties(RSService rsService, string path, IEnumerable<global::Model.Property> requestedPropertiesproperties)
		{
			this.ThrowIfRestrictedItemType(rsService, path);
			GetPropertiesAction getPropertiesAction = rsService.GetPropertiesAction;
			getPropertiesAction.ActionParameters.ItemPath = path;
			if (requestedPropertiesproperties != null && requestedPropertiesproperties.Any<global::Model.Property>())
			{
				getPropertiesAction.ActionParameters.RequestedProperties = requestedPropertiesproperties.Select((global::Model.Property x) => x.ToSoapAPI()).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
			}
			getPropertiesAction.Execute();
			return getPropertiesAction.ActionParameters.PropertyValues.Select((Microsoft.ReportingServices.Library.Soap.Property x) => x.ToWebAPI()).AsQueryable<global::Model.Property>();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000042CA File Offset: 0x000024CA
		public void SetItemProperties(IPrincipal userPrincipal, string path, IEnumerable<global::Model.Property> properties)
		{
			this.SetItemPropertiesInternal(userPrincipal, path, properties, false);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000042D6 File Offset: 0x000024D6
		public void SetItemPropertiesTrusted(IPrincipal userPrincipal, string path, IEnumerable<global::Model.Property> properties)
		{
			this.SetItemPropertiesInternal(userPrincipal, path, properties, true);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000042E4 File Offset: 0x000024E4
		private void SetItemPropertiesInternal(IPrincipal userPrincipal, string path, IEnumerable<global::Model.Property> properties, bool ignoreSecurityCheck)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			this.ThrowIfRestrictedItemType(rsservice, path);
			SetPropertiesAction setPropertiesAction = rsservice.SetPropertiesAction;
			setPropertiesAction.ActionParameters.ItemPath = path;
			setPropertiesAction.ActionParameters.IgnoreSecCheck = ignoreSecurityCheck;
			if (properties != null && properties.Any<global::Model.Property>())
			{
				setPropertiesAction.ActionParameters.Properties = properties.Select((global::Model.Property x) => x.ToSoapAPI()).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
			}
			setPropertiesAction.Execute();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004368 File Offset: 0x00002568
		public global::Model.DataSet GetDataSet(IPrincipal userPrincipal, Guid key)
		{
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(ServicesUtil.CreateRsService(userPrincipal), key);
			return this.GetDataSet(userPrincipal, catalogPathFromGuid);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000438C File Offset: 0x0000258C
		public global::Model.DataSet GetDataSet(IPrincipal userPrincipal, string path)
		{
			DataSetRepository dataSetRepository = new DataSetRepository(userPrincipal, this);
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			FavoriteableCatalogItemDescriptor itemDescriptor = this.GetItemDescriptor(userPrincipal, rsservice, path);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, dataSetRepository);
			dataSetRepository.HasParameters = itemDescriptor.HasParameters;
			dataSetRepository.IsFavorite = itemDescriptor.IsFavorite;
			List<global::Model.Property> list = new List<global::Model.Property>();
			global::Model.Property property = new global::Model.Property
			{
				Name = "QueryExecutionTimeOut"
			};
			list.Add(property);
			IQueryable<global::Model.Property> itemProperties = this.GetItemProperties(userPrincipal, path, list);
			int num = 0;
			if (int.TryParse(itemProperties.First<global::Model.Property>().Value, out num))
			{
				dataSetRepository.QueryExecutionTimeOut = num;
			}
			return dataSetRepository;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000441C File Offset: 0x0000261C
		public void SetItemDataSets(IPrincipal userPrincipal, string itemPath, IEnumerable<global::Model.DataSet> dataSets)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			if (this.GetCatalogItem(rsservice, userPrincipal, itemPath).Type == CatalogItemType.Report)
			{
				SetReportItemReferencesAction setReportItemReferencesAction = rsservice.SetReportItemReferencesAction;
				setReportItemReferencesAction.ActionParameters.ItemPath = itemPath;
				setReportItemReferencesAction.ActionParameters.ItemReferences = dataSets.Select(new Func<global::Model.DataSet, Microsoft.ReportingServices.Library.Soap2010.ItemReference>(DataSetExtensions.ToItemReference2010)).ToArray<Microsoft.ReportingServices.Library.Soap2010.ItemReference>();
				setReportItemReferencesAction.Execute();
				return;
			}
			throw new CatalogItemInvalidException(Microsoft.ReportingServices.Portal.Services.SR.Error_CatalogItemInvalid);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004488 File Offset: 0x00002688
		public bool Create<T>(IPrincipal userPrincipal, T catalogItem, out T createdItem) where T : global::Model.CatalogItem
		{
			global::Model.CatalogItem catalogItem2 = catalogItem;
			global::Model.CatalogItem catalogItem3 = null;
			bool flag = this.CreateItem(userPrincipal, catalogItem2, out catalogItem3);
			createdItem = (T)((object)catalogItem3);
			return flag;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000044B4 File Offset: 0x000026B4
		public bool Create(IPrincipal userPrincipal, global::Model.CatalogItem catalogItem, out global::Model.CatalogItem createdItem)
		{
			return this.CreateItem(userPrincipal, catalogItem, out createdItem);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000044C0 File Offset: 0x000026C0
		private bool CreateItem(IPrincipal userPrincipal, global::Model.CatalogItem catalogItem, out global::Model.CatalogItem createdItem)
		{
			this.ThrowIfRestrictedItemType(catalogItem.Type, catalogItem.Path);
			this._fileSizeRestrictions.ThrowIfSizeIsOutOfLimits(catalogItem.Content);
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string text = catalogItem.ComputeParentPath();
			this.EnsureCatalogItemCanBeCreated(rsservice, text, catalogItem);
			bool flag = false;
			global::Model.Property[] array = null;
			if (catalogItem.Properties.Count > 0)
			{
				array = catalogItem.Properties.ToArray<global::Model.Property>();
			}
			switch (catalogItem.Type)
			{
			case CatalogItemType.Folder:
				createdItem = this.CreateFolder(userPrincipal, text, catalogItem.Name);
				this._logger.Trace(TraceLevel.Info, "Folder created");
				break;
			case CatalogItemType.Report:
				createdItem = this.CreateReport(rsservice, userPrincipal, catalogItem.Name, text, flag, catalogItem.Content, array);
				this._logger.Trace(TraceLevel.Info, "Report created");
				break;
			case CatalogItemType.DataSource:
				createdItem = this.CreateDataSource(rsservice, userPrincipal, text, (global::Model.DataSource)catalogItem);
				this._logger.Trace(TraceLevel.Info, "DataSource created");
				break;
			case CatalogItemType.DataSet:
				createdItem = this.CreateDataSet(rsservice, userPrincipal, catalogItem.Name, text, flag, catalogItem.Content, array);
				this._logger.Trace(TraceLevel.Info, "DataSet created");
				break;
			case CatalogItemType.Component:
				createdItem = this.CreateComponent(rsservice, userPrincipal, catalogItem.Name, text, flag, catalogItem.Content, array);
				this._logger.Trace(TraceLevel.Info, "Component created");
				break;
			case CatalogItemType.Resource:
				createdItem = this.CreateResource(rsservice, userPrincipal, catalogItem.Name, text, flag, catalogItem, array);
				this._logger.Trace(TraceLevel.Info, "Resource created");
				break;
			case CatalogItemType.Kpi:
				createdItem = this.CreateKpi(rsservice, userPrincipal, text, catalogItem as Kpi);
				this._logger.Trace(TraceLevel.Info, "Kpi created");
				break;
			case CatalogItemType.MobileReport:
				this._logger.Trace(TraceLevel.Info, "Mobile Report creation is not supported.");
				throw new NotSupportedException();
			case CatalogItemType.LinkedReport:
				createdItem = this.CreateLinkedReport(rsservice, userPrincipal, (LinkedReport)catalogItem);
				this._logger.Trace(TraceLevel.Info, "LinkedReport created");
				break;
			case CatalogItemType.ReportModel:
				this._logger.Trace(TraceLevel.Info, "ReportModel creation is not supported.");
				throw new NotSupportedException();
			case CatalogItemType.PowerBIReport:
				this.CreatePowerBIReport(rsservice, userPrincipal, text, flag, catalogItem as PowerBIReport, array);
				createdItem = catalogItem;
				this._logger.Trace(TraceLevel.Info, "PowerBI Report created");
				break;
			case CatalogItemType.ExcelWorkbook:
				createdItem = this.CreateExcel(rsservice, userPrincipal, text, flag, catalogItem as ExcelWorkbook, array);
				this._logger.Trace(TraceLevel.Info, "ExcelWorkbook created");
				break;
			default:
				createdItem = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000473C File Offset: 0x0000293C
		private void CreateModel(RSService rsService, string modelName, string parentFolder, bool overwrite, byte[] definition, global::Model.Property[] properties)
		{
			Global.CheckItemName(modelName, ItemType.Model, "Name");
			if (Path.GetExtension(modelName).Equals(".SMDL", StringComparison.OrdinalIgnoreCase))
			{
				modelName = Path.GetFileNameWithoutExtension(modelName);
			}
			CreateModelAction createModelAction = rsService.CreateModelAction;
			createModelAction.ActionParameters.ItemName = modelName;
			createModelAction.ActionParameters.ParentPath = parentFolder;
			createModelAction.ActionParameters.ModelDefinition = definition;
			createModelAction.ActionParameters.Overwrite = overwrite;
			if (properties != null && properties.Length != 0)
			{
				createModelAction.ActionParameters.Properties = properties.Select((global::Model.Property prop) => prop.ToSoapAPI()).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
			}
			createModelAction.Execute();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000047F0 File Offset: 0x000029F0
		internal global::Model.DataSource CreateDataSource(RSService rsService, IPrincipal userPrincipal, string parentPath, global::Model.DataSource dataSource)
		{
			global::Model.Extension[] array = this._systemService.ListExtensions(userPrincipal, ExtensionType.Data);
			dataSource.LoadFromContent();
			dataSource.Validate(array);
			Global.CheckItemName(dataSource.Name, ItemType.DataSource, "Name");
			CreateDataSourceAction createDataSourceAction = rsService.CreateDataSourceAction;
			createDataSourceAction.ActionParameters.ItemName = dataSource.Name;
			createDataSourceAction.ActionParameters.ParentPath = parentPath;
			createDataSourceAction.ActionParameters.Overwrite = false;
			createDataSourceAction.ActionParameters.DataSourceDefinition = dataSource.ToDataSourceDefinition();
			createDataSourceAction.ActionParameters.Properties = new Microsoft.ReportingServices.Library.Soap.Property[]
			{
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Hidden",
					Value = (dataSource.Hidden ? true.ToString() : false.ToString())
				},
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Description",
					Value = dataSource.Description
				}
			};
			createDataSourceAction.Execute();
			return this.GetCatalogItem(rsService, userPrincipal, this.PathCombine(parentPath, dataSource.Name)) as global::Model.DataSource;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000048F8 File Offset: 0x00002AF8
		private LinkedReport CreateLinkedReport(RSService rsService, IPrincipal userPrincipal, LinkedReport linkedReport)
		{
			CreateLinkedReportAction createLinkedReportAction = rsService.CreateLinkedReportAction;
			createLinkedReportAction.ActionParameters.ItemName = linkedReport.Name;
			createLinkedReportAction.ActionParameters.ParentPath = linkedReport.Path;
			createLinkedReportAction.ActionParameters.LinkPath = linkedReport.Link;
			createLinkedReportAction.ActionParameters.Properties = new Microsoft.ReportingServices.Library.Soap.Property[]
			{
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Hidden",
					Value = (linkedReport.Hidden ? true.ToString() : false.ToString())
				},
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Description",
					Value = linkedReport.Description
				}
			};
			createLinkedReportAction.Execute();
			return this.GetCatalogItem(rsService, userPrincipal, this.PathCombine(linkedReport.Path, linkedReport.Name)) as LinkedReport;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000049CC File Offset: 0x00002BCC
		public bool Update(IPrincipal userPrincipal, string origItemPath, global::Model.CatalogItem catalogItem, string[] delta)
		{
			this.ThrowIfRestrictedItemType(catalogItem.Type, catalogItem.Path);
			this._fileSizeRestrictions.ThrowIfSizeIsOutOfLimits(catalogItem.Content);
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string text = catalogItem.ComputeParentPath();
			global::Model.CatalogItem catalogItem2 = this.GetCatalogItem(userPrincipal, origItemPath);
			string text2 = catalogItem2.ComputeParentPath();
			if (catalogItem.Description == null)
			{
				catalogItem.Description = catalogItem2.Description;
			}
			bool flag = string.Compare(text2, text, StringComparison.OrdinalIgnoreCase) != 0;
			if (flag)
			{
				this.EnsureCatalogItemCanBeCreated(rsservice, text, catalogItem);
			}
			else
			{
				this.EnsureCatalogItemCanBeEdited(rsservice, text2, catalogItem2, text, catalogItem);
			}
			bool flag2 = flag || string.Compare(catalogItem2.Name, catalogItem.Name, StringComparison.Ordinal) != 0;
			switch (catalogItem.Type)
			{
			case CatalogItemType.Folder:
				this.UpdateFolder(rsservice, origItemPath, catalogItem, flag2, delta);
				break;
			case CatalogItemType.Report:
				this.UpdateReport(userPrincipal, origItemPath, (global::Model.Report)catalogItem, flag2, delta);
				break;
			case CatalogItemType.DataSource:
				this.UpdateDataSource(userPrincipal, origItemPath, (global::Model.DataSource)catalogItem, flag2, delta);
				break;
			case CatalogItemType.DataSet:
				this.UpdateDataSet(userPrincipal, origItemPath, (global::Model.DataSet)catalogItem, flag2, delta);
				break;
			case CatalogItemType.Component:
				this.UpdateComponent(userPrincipal, origItemPath, (Component)catalogItem, flag2, delta);
				break;
			case CatalogItemType.Resource:
				this.UpdateResource(rsservice, origItemPath, catalogItem, flag2, delta);
				break;
			case CatalogItemType.Kpi:
				this.UpdateKpi(rsservice, origItemPath, (Kpi)catalogItem, flag2, delta);
				break;
			case CatalogItemType.MobileReport:
				this._logger.Trace(TraceLevel.Info, "Mobile Report updating is not supported.");
				throw new NotSupportedException();
			case CatalogItemType.LinkedReport:
				this.UpdateLinkedReport(rsservice, origItemPath, (LinkedReport)catalogItem, flag2, delta);
				break;
			case CatalogItemType.ReportModel:
				this.UpdateReportModel(userPrincipal, origItemPath, (ReportModel)catalogItem, flag2, delta);
				break;
			case CatalogItemType.PowerBIReport:
				this.UpdatePowerBIReport(userPrincipal, origItemPath, catalogItem2.Id, (PowerBIReport)catalogItem, flag2, delta);
				break;
			case CatalogItemType.ExcelWorkbook:
				this.UpdateExcel(rsservice, origItemPath, (ExcelWorkbook)catalogItem, flag2, delta);
				break;
			default:
				throw new NotImplementedException("Update for type " + catalogItem.Type);
			}
			return true;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public bool Update(IPrincipal userPrincipal, Guid key, global::Model.CatalogItem catalogItem, string[] delta)
		{
			this.ThrowIfRestrictedItemType(catalogItem.Type, catalogItem.Path);
			string itemPathFromId = CatalogItemRepository.GetItemPathFromId(ServicesUtil.CreateRsService(userPrincipal), key);
			return this.Update(userPrincipal, itemPathFromId, catalogItem, delta);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004C08 File Offset: 0x00002E08
		public bool Delete(IPrincipal userPrincipal, Guid key)
		{
			string itemPathFromId = CatalogItemRepository.GetItemPathFromId(ServicesUtil.CreateRsService(userPrincipal), key);
			return this.Delete(userPrincipal, itemPathFromId);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004C2C File Offset: 0x00002E2C
		public bool Delete(IPrincipal userPrincipal, string path)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			this.DeleteItem(rsservice, path);
			this._logger.Trace(TraceLevel.Info, () => string.Format("Item deleted: {0}", path));
			return true;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004C74 File Offset: 0x00002E74
		public List<global::Model.Subscription> GetSubscriptions(IPrincipal userPrincipal, string itemPath)
		{
			List<global::Model.Subscription> list = this.ListSubscriptionByType(userPrincipal, itemPath, SubscriptionType.ReportSubscription).ActionParameters.Children.Select((SubscriptionImpl x) => x.ToWebApiModel(this._soapRS2010Proxy.GetParameterTypes(userPrincipal, x.ReportName), null)).ToList<global::Model.Subscription>();
			this._systemService.PopulateScheduleDescriptions(userPrincipal, list, 0);
			this._systemService.PopulateLocalizedExtensionNames(userPrincipal, list);
			return list;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004CEC File Offset: 0x00002EEC
		public List<global::Model.Subscription> GetSubscriptionsUsingDataSource(IPrincipal userPrincipal, string itemPath)
		{
			List<global::Model.Subscription> list = (from x in this._soapRS2010Proxy.GetSubscriptionsUsingDataSource(userPrincipal, itemPath)
				select x.ToWebAPIModel()).ToList<global::Model.Subscription>();
			this._systemService.PopulateScheduleDescriptions(userPrincipal, list, 0);
			this._systemService.PopulateLocalizedExtensionNames(userPrincipal, list);
			return list;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004D4C File Offset: 0x00002F4C
		public List<SubscriptionHistory> GetSubscriptionsHistory(Guid subscriptionId)
		{
			List<SubscriptionHistory> list = new List<SubscriptionHistory>();
			foreach (SubscriptionHistoryEntity subscriptionHistoryEntity in this._subscriptionHistoryAccessor.GetSubscriptionHistory(subscriptionId).Result)
			{
				list.Add(new SubscriptionHistory
				{
					Id = subscriptionHistoryEntity.SubscriptionHistoryID,
					SubscriptionID = subscriptionHistoryEntity.SubscriptionID,
					Type = (global::Model.SubscriptionExecutionType)Convert.ToInt32(subscriptionHistoryEntity.Type),
					StartTime = subscriptionHistoryEntity.StartTime,
					EndTime = subscriptionHistoryEntity.EndTime,
					Status = (global::Model.SubscriptionStatus)Convert.ToInt32(subscriptionHistoryEntity.Status),
					Message = subscriptionHistoryEntity.Message,
					Details = subscriptionHistoryEntity.Details
				});
			}
			return list;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004E2C File Offset: 0x0000302C
		public List<global::Model.CacheRefreshPlan> GetCacheRefreshPlans(IPrincipal userPrincipal, string itemPath)
		{
			return this.ListSubscriptionByType(userPrincipal, itemPath, SubscriptionType.CacheRefreshPlan).ActionParameters.Children.Select((SubscriptionImpl x) => x.ToCacheRefreshPlanModel(this._soapRS2010Proxy.GetParameterTypes(userPrincipal, x.ReportName))).ToList<global::Model.CacheRefreshPlan>();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004E7B File Offset: 0x0000307B
		public List<global::Model.CacheRefreshPlan> GetCacheRefreshPlansForPowerBIReport(IPrincipal userPrincipal, string itemPath)
		{
			return this.ListSubscriptionByType(userPrincipal, itemPath, SubscriptionType.CacheRefreshPlan).ActionParameters.Children.Select((SubscriptionImpl x) => x.ToCacheRefreshPlanModel(null)).ToList<global::Model.CacheRefreshPlan>();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004EBC File Offset: 0x000030BC
		public CacheOptions GetItemCacheOptions(IPrincipal userPrincipal, string path)
		{
			global::Model.CatalogItem catalogItem = this.GetCatalogItem(userPrincipal, path);
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			CacheOptions cacheOptions = new CacheOptions();
			if (CatalogItemRepository.SupportsSnapshots(catalogItem))
			{
				GetExecutionOptionsAction getExecutionOptionsAction = rsservice.GetExecutionOptionsAction;
				getExecutionOptionsAction.ActionParameters.ReportPath = path;
				getExecutionOptionsAction.Execute();
				if (getExecutionOptionsAction.ActionParameters.ExecutionSettings == ExecutionSettingEnum.Snapshot)
				{
					cacheOptions.ExecutionType = ItemExecutionType.Snapshot;
					if (!(getExecutionOptionsAction.ActionParameters.Schedule is Microsoft.ReportingServices.Library.Soap.NoSchedule))
					{
						cacheOptions.Expiration = new ExpirationReference();
						cacheOptions.Expiration.Schedule = getExecutionOptionsAction.ActionParameters.Schedule.ToWebApi();
					}
					return cacheOptions;
				}
			}
			GetCacheOptionsAction getCacheOptionsAction = rsservice.GetCacheOptionsAction;
			getCacheOptionsAction.ActionParameters.ReportPath = path;
			getCacheOptionsAction.Execute();
			if (getCacheOptionsAction.ActionParameters.CacheReport)
			{
				cacheOptions.ExecutionType = ItemExecutionType.Cache;
				cacheOptions.Expiration = new ExpirationReference();
				Microsoft.ReportingServices.Library.Soap.ExpirationDefinition expiration = getCacheOptionsAction.ActionParameters.Expiration;
				Microsoft.ReportingServices.Library.Soap.TimeExpiration timeExpiration = expiration as Microsoft.ReportingServices.Library.Soap.TimeExpiration;
				if (timeExpiration != null)
				{
					cacheOptions.Expiration.Minutes = timeExpiration.Minutes;
				}
				else
				{
					Microsoft.ReportingServices.Library.Soap.ScheduleExpiration scheduleExpiration = expiration as Microsoft.ReportingServices.Library.Soap.ScheduleExpiration;
					if (scheduleExpiration != null)
					{
						cacheOptions.Expiration.Schedule = scheduleExpiration.Schedule.ToWebApi();
					}
				}
			}
			else
			{
				cacheOptions.ExecutionType = ItemExecutionType.Live;
			}
			return cacheOptions;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004FE4 File Offset: 0x000031E4
		public void SetItemCacheOptions(IPrincipal userPrincipal, string path, CacheOptions cacheOptions)
		{
			global::Model.CatalogItem catalogItem = this.GetCatalogItem(userPrincipal, path);
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			if (CatalogItemRepository.SupportsSnapshots(catalogItem))
			{
				SetExecutionOptionsAction setExecutionOptionsAction = rsservice.SetExecutionOptionsAction;
				setExecutionOptionsAction.BatchID = Guid.Empty;
				setExecutionOptionsAction.ActionParameters.ReportPath = path;
				if (cacheOptions.ExecutionType != ItemExecutionType.Live)
				{
					this.EnsureCatalogItemDataSourcesValid(userPrincipal, catalogItem);
				}
				if (cacheOptions.ExecutionType == ItemExecutionType.Snapshot)
				{
					setExecutionOptionsAction.ActionParameters.ExecutionSettings = ExecutionSettingEnum.Snapshot;
					if (cacheOptions.Expiration != null && cacheOptions.Expiration.Schedule != null)
					{
						setExecutionOptionsAction.ActionParameters.Schedule = cacheOptions.Expiration.Schedule.ToSoapApi();
					}
				}
				else
				{
					setExecutionOptionsAction.ActionParameters.ExecutionSettings = ExecutionSettingEnum.Live;
				}
				setExecutionOptionsAction.Execute();
			}
			if (cacheOptions.ExecutionType == ItemExecutionType.Live || cacheOptions.ExecutionType == ItemExecutionType.Cache)
			{
				SetCacheOptionsAction setCacheOptionsAction = rsservice.SetCacheOptionsAction;
				setCacheOptionsAction.BatchID = Guid.Empty;
				setCacheOptionsAction.ActionParameters.ReportPath = path;
				if (cacheOptions.ExecutionType == ItemExecutionType.Cache)
				{
					setCacheOptionsAction.ActionParameters.CacheReport = true;
					if (cacheOptions.Expiration.Schedule != null)
					{
						Microsoft.ReportingServices.Library.Soap.ScheduleExpiration scheduleExpiration = new Microsoft.ReportingServices.Library.Soap.ScheduleExpiration();
						scheduleExpiration.Schedule = cacheOptions.Expiration.Schedule.ToSoapApi();
						setCacheOptionsAction.ActionParameters.Expiration = scheduleExpiration;
					}
					else
					{
						setCacheOptionsAction.ActionParameters.Expiration = Microsoft.ReportingServices.Library.Soap.TimeExpiration.IntToThis(cacheOptions.Expiration.Minutes);
					}
				}
				else
				{
					setCacheOptionsAction.ActionParameters.CacheReport = false;
				}
				setCacheOptionsAction.Execute();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005143 File Offset: 0x00003343
		private static bool SupportsSnapshots(global::Model.CatalogItem item)
		{
			return item.Type == CatalogItemType.Report || item.Type == CatalogItemType.LinkedReport;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000515C File Offset: 0x0000335C
		public List<global::Model.ReportHistorySnapshot> GetReportHistorySnapshots(IPrincipal userPrincipal, string path)
		{
			ListHistoryAction listHistoryAction = ServicesUtil.CreateRsService(userPrincipal).ListHistoryAction;
			listHistoryAction.ActionParameters.ReportPath = path;
			listHistoryAction.Execute();
			return listHistoryAction.ActionParameters.ReportHistory.Select((Microsoft.ReportingServices.Library.Soap.ReportHistorySnapshot x) => x.ToWebApi()).ToList<global::Model.ReportHistorySnapshot>();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000051BC File Offset: 0x000033BC
		public List<global::Model.HistorySnapshot> GetHistorySnapshots(IPrincipal userPrincipal, string path)
		{
			ListHistorySnapshotsAction listHistorySnapshotsAction = ServicesUtil.CreateRsService(userPrincipal).ListHistorySnapshotsAction;
			listHistorySnapshotsAction.ActionParameters.ReportPath = path;
			listHistorySnapshotsAction.Execute();
			return listHistorySnapshotsAction.ActionParameters.ReportHistory.Select((Microsoft.ReportingServices.Library.Soap.HistorySnapshot x) => x.ToWebApi()).ToList<global::Model.HistorySnapshot>();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005219 File Offset: 0x00003419
		public bool DeleteItemHistorySnapshot(IPrincipal userPrincipal, string path, string historyId)
		{
			DeleteSnapshotAction deleteSnapshotAction = ServicesUtil.CreateRsService(userPrincipal).DeleteSnapshotAction;
			deleteSnapshotAction.BatchID = Guid.Empty;
			deleteSnapshotAction.ActionParameters.ReportPath = path;
			deleteSnapshotAction.ActionParameters.SnapshotID = historyId;
			deleteSnapshotAction.Execute();
			return true;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000524F File Offset: 0x0000344F
		public bool DeleteItemHistorySnapshotByHistoryId(IPrincipal userPrincipal, string path, string historyId)
		{
			DeleteHistorySnapshotAction deleteHistorySnapshotAction = ServicesUtil.CreateRsService(userPrincipal).DeleteHistorySnapshotAction;
			deleteHistorySnapshotAction.BatchID = Guid.Empty;
			deleteHistorySnapshotAction.ActionParameters.ReportPath = path;
			deleteHistorySnapshotAction.ActionParameters.HistoryId = historyId;
			deleteHistorySnapshotAction.Execute();
			return true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005288 File Offset: 0x00003488
		public string CreateItemHistorySnapshot(IPrincipal userPrincipal, string path)
		{
			global::Model.CatalogItem catalogItem = this.GetCatalogItem(userPrincipal, path);
			this.EnsureCatalogItemDataSourcesValid(userPrincipal, catalogItem);
			return this._soapRS2010Proxy.CreateItemHistorySnapshot(userPrincipal, path);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000052B3 File Offset: 0x000034B3
		public void UpdateItemExecutionSnapshot(IPrincipal userPrincipal, string path)
		{
			this._soapRS2010Proxy.UpdateItemExecutionSnapshot(userPrincipal, path);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000052C4 File Offset: 0x000034C4
		public ItemHistoryOptions GetItemHistoryOptions(IPrincipal userPrincipal, string path)
		{
			ItemHistoryOptions itemHistoryOptions = new ItemHistoryOptions();
			GetReportHistoryOptionsAction getReportHistoryOptionsAction = ServicesUtil.CreateRsService(userPrincipal).GetReportHistoryOptionsAction;
			getReportHistoryOptionsAction.ActionParameters.ReportPath = path;
			getReportHistoryOptionsAction.Execute();
			itemHistoryOptions.EnableManualSnapshotCreation = getReportHistoryOptionsAction.ActionParameters.ManualCreationEnabled;
			itemHistoryOptions.KeepExecutionSnapshots = getReportHistoryOptionsAction.ActionParameters.KeepExecutionSnapshots;
			itemHistoryOptions.Schedule = getReportHistoryOptionsAction.ActionParameters.Schedule.ToWebApi();
			return itemHistoryOptions;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000532C File Offset: 0x0000352C
		public ReportHistorySnapshotsOptions GetReportHistorySnapshotsOptions(IPrincipal userPrincipal, string path)
		{
			ReportHistorySnapshotsOptions reportHistorySnapshotsOptions = new ReportHistorySnapshotsOptions();
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			GetReportHistoryOptionsAction getReportHistoryOptionsAction = rsservice.GetReportHistoryOptionsAction;
			getReportHistoryOptionsAction.ActionParameters.ReportPath = path;
			getReportHistoryOptionsAction.Execute();
			reportHistorySnapshotsOptions.ManualCreationEnabled = getReportHistoryOptionsAction.ActionParameters.ManualCreationEnabled;
			reportHistorySnapshotsOptions.KeepExecutionSnapshots = getReportHistoryOptionsAction.ActionParameters.KeepExecutionSnapshots;
			reportHistorySnapshotsOptions.Schedule = getReportHistoryOptionsAction.ActionParameters.Schedule.ToWebApi();
			GetSnapshotLimitAction getSnapshotLimitAction = rsservice.GetSnapshotLimitAction;
			getSnapshotLimitAction.ActionParameters.ReportPath = path;
			getSnapshotLimitAction.Execute();
			reportHistorySnapshotsOptions.ScopedLimit = getSnapshotLimitAction.ActionParameters.ScopedLimit;
			reportHistorySnapshotsOptions.UseDefaultSystemLimit = getSnapshotLimitAction.ActionParameters.UseSystem;
			reportHistorySnapshotsOptions.SystemLimit = getSnapshotLimitAction.ActionParameters.SystemLimit;
			return reportHistorySnapshotsOptions;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000053E4 File Offset: 0x000035E4
		public void SetReportHistorySnapshotOptions(IPrincipal userPrincipal, string path, ReportHistorySnapshotsOptions reportHistorySnapshotOptions)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			Guid guid = rsservice.CreateBatch();
			SetReportHistoryOptionsAction setReportHistoryOptionsAction = rsservice.SetReportHistoryOptionsAction;
			setReportHistoryOptionsAction.BatchID = guid;
			setReportHistoryOptionsAction.ActionParameters.ReportPath = path;
			setReportHistoryOptionsAction.ActionParameters.ManualCreationEnabled = reportHistorySnapshotOptions.ManualCreationEnabled;
			setReportHistoryOptionsAction.ActionParameters.KeepExecutionSnapshots = reportHistorySnapshotOptions.KeepExecutionSnapshots;
			setReportHistoryOptionsAction.ActionParameters.Schedule = reportHistorySnapshotOptions.Schedule.ToSoapApi();
			setReportHistoryOptionsAction.Execute();
			SetSnapshotLimitAction setSnapshotLimitAction = rsservice.SetSnapshotLimitAction;
			setSnapshotLimitAction.BatchID = guid;
			setSnapshotLimitAction.ActionParameters.ReportPath = path;
			setSnapshotLimitAction.ActionParameters.UseSystem = reportHistorySnapshotOptions.UseDefaultSystemLimit;
			setSnapshotLimitAction.ActionParameters.ScopedLimit = reportHistorySnapshotOptions.ScopedLimit;
			setSnapshotLimitAction.Execute();
			rsservice.ExecuteBatch(guid);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000549C File Offset: 0x0000369C
		public void SetLinkedReportLink(IPrincipal userPrincipal, LinkedReport item, string link)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			rsService.ExecuteStorageAction(delegate
			{
				SetReportLinkAction setReportLinkAction = rsService.SetReportLinkAction;
				Microsoft.ReportingServices.Library.Soap.Property[] array;
				this.ResolveCommonCatalogItemProperties(item, out array);
				setReportLinkAction.ActionParameters.ReportPath = item.Path;
				setReportLinkAction.ActionParameters.LinkPath = link;
				setReportLinkAction.PerformActionNow();
			});
			this._logger.Trace(TraceLevel.Info, () => string.Format("LinkedReport parent updated: {0}", item.Id));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005500 File Offset: 0x00003700
		private global::Model.Report CreateReport(RSService rsService, IPrincipal userPrincipal, string reportName, string parentFolder, bool overwrite, byte[] definition, global::Model.Property[] properties)
		{
			if (string.IsNullOrEmpty(reportName))
			{
				throw new ArgumentNullException("reportName");
			}
			if (Path.GetExtension(reportName).Equals(".RDL", StringComparison.OrdinalIgnoreCase))
			{
				reportName = Path.GetFileNameWithoutExtension(reportName);
			}
			Microsoft.SqlServer.ReportingServices2010.Property[] array = null;
			if (properties != null && properties.Length != 0)
			{
				array = properties.Select((global::Model.Property prop) => prop.ToProxyAPI()).ToArray<Microsoft.SqlServer.ReportingServices2010.Property>();
			}
			this._soapRS2010Proxy.CreateCatalogItem(userPrincipal, "Report", reportName, parentFolder, overwrite, definition, array);
			return this.GetCatalogItem(rsService, userPrincipal, this.PathCombine(parentFolder, reportName)) as global::Model.Report;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000055A4 File Offset: 0x000037A4
		private void UpdateReport(IPrincipal userPrincipal, string origItemPath, global::Model.Report item, bool renameOrMove, string[] delta)
		{
			string[] array = new string[] { "Hidden", "Description" };
			string[] array2 = new string[] { "HasDataSources", "HasParameters", "Subscriptions", "DataSources", "ReportHistorySnapshots" };
			bool updateProperties = true;
			bool flag = true;
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			if (delta != null)
			{
				updateProperties = delta.Intersect(array).Count<string>() > 0;
				flag = delta.Intersect(array2).Count<string>() > 0;
			}
			if (flag && item.Content.Length != 0)
			{
				Microsoft.SqlServer.ReportingServices2010.Property[] array3 = null;
				IList<global::Model.Property> properties = item.Properties;
				if (properties != null && properties.Count > 0)
				{
					array3 = properties.Select((global::Model.Property prop) => prop.ToProxyAPI()).ToArray<Microsoft.SqlServer.ReportingServices2010.Property>();
				}
				this._soapRS2010Proxy.SetItemDefinition(userPrincipal, origItemPath, item.Content, array3);
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (updateProperties)
				{
					this.UpdateItemProperties(rsService, item, origItemPath, null);
				}
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, item.Path);
				}
			});
			if (item.DataSources != null && item.DataSources.Count > 0)
			{
				if (!item.DataSources.Where((global::Model.DataSource ds) => ds.CredentialsInServer != null).Any<global::Model.DataSource>())
				{
					this.SetItemDataSources(userPrincipal, item.Path, item.DataSources);
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005760 File Offset: 0x00003960
		private static void GetReportDefinition(RSService rsService, string itemPath, out byte[] definition)
		{
			GetReportDefinitionAction getReportDefinitionAction = rsService.GetReportDefinitionAction;
			getReportDefinitionAction.ActionParameters.ItemPath = itemPath;
			getReportDefinitionAction.ActionParameters.ItemType = ItemType.Report;
			getReportDefinitionAction.Execute();
			definition = getReportDefinitionAction.ActionParameters.ReportDefinition;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000057A0 File Offset: 0x000039A0
		private Kpi CreateKpi(RSService rsService, IPrincipal userPrincipal, string parentPath, Kpi item)
		{
			CreateKpiAction createKpiAction = rsService.CreateKpiAction;
			CreateKpiActionParameters actionParameters = createKpiAction.ActionParameters;
			actionParameters.ParentPath = parentPath;
			actionParameters.ItemName = item.Name;
			Microsoft.ReportingServices.Library.Soap.Property[] array;
			DataSetInfoCollection dataSetInfoCollection;
			this.ResolveKpiProperties(item, new ResolveCatalogItem(this.ResolveCatalogItem), out array, out dataSetInfoCollection);
			actionParameters.Properties = array;
			actionParameters.SharedDataSets = dataSetInfoCollection;
			createKpiAction.Execute();
			return this.GetCatalogItem(rsService, userPrincipal, this.PathCombine(parentPath, item.Name)) as Kpi;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005812 File Offset: 0x00003A12
		public string GetPowerBiReportParentPath(PowerBIReport pbiReport)
		{
			if (!(pbiReport.Id == Guid.Empty))
			{
				return pbiReport.ComputeParentPath();
			}
			return pbiReport.Path;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005834 File Offset: 0x00003A34
		public void UploadPowerBIReport(IPrincipal userPrincipal, PowerBIReport pbiReport)
		{
			using (ScopeMeter.Use(new string[] { "SQL", "UploadPowerBIReport", pbiReport.Name }))
			{
				this.ThrowIfRestrictedItemType(pbiReport.Type, pbiReport.Path);
				this._fileSizeRestrictions.ThrowIfSizeIsOutOfLimits(pbiReport.Content);
				RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
				string powerBiReportParentPath = this.GetPowerBiReportParentPath(pbiReport);
				bool flag = true;
				global::Model.Property[] array = null;
				if (pbiReport.Properties.Count > 0)
				{
					array = pbiReport.Properties.ToArray<global::Model.Property>();
				}
				this.CreatePowerBIReport(rsservice, userPrincipal, powerBiReportParentPath, flag, pbiReport, array);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000058E4 File Offset: 0x00003AE4
		private void CreatePowerBIReport(RSService rsService, IPrincipal userPrincipal, string parentPath, bool overwrite, PowerBIReport item, global::Model.Property[] properties)
		{
			using (ScopeMeter.Use(new string[] { "SQL", "CreatePowerBIReport", item.Name }))
			{
				item.Name = item.Name.Trim();
				if (Path.GetExtension(item.Name).Equals(".PBIX", StringComparison.OrdinalIgnoreCase))
				{
					item.Name = Path.GetFileNameWithoutExtension(item.Name);
				}
				if (Path.GetExtension(item.Path).Equals(".PBIX", StringComparison.OrdinalIgnoreCase))
				{
					item.Path = (parentPath.Equals("/", StringComparison.OrdinalIgnoreCase) ? string.Format("/{0}", item.Name) : string.Format("{0}/{1}", parentPath, item.Name));
				}
				UploadPowerBIReportAction uploadPowerBiReportAction = rsService.UploadPowerBiReportAction;
				UploadPowerBIReportActionParameters actionParameters = uploadPowerBiReportAction.ActionParameters;
				actionParameters.ItemName = item.Name;
				actionParameters.ParentPath = parentPath;
				actionParameters.Overwrite = overwrite;
				string text = JsonConvert.SerializeObject(item.DataModelParameters);
				actionParameters.Original = item.GetOriginalPbiStream();
				actionParameters.Pbix = item.GetPbixStream();
				actionParameters.Model = item.GetModelStream();
				actionParameters.DataModelParameters = text;
				if (properties != null && properties.Any<global::Model.Property>())
				{
					actionParameters.Properties = properties.Select((global::Model.Property prop) => prop.ToSoapAPI()).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
				}
				uploadPowerBiReportAction.Execute();
				item.Id = actionParameters.ItemInfo.ItemID;
				this.SetDataModelDataSourcesTrusted(userPrincipal, actionParameters.ItemInfo.ItemID, item.DataSources, overwrite);
				this.SetDataModelRolesTrusted(actionParameters.ItemInfo.ItemID, item.DataModelRoles, overwrite);
				IEnumerable<global::Model.DataSource> enumerable = this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(item.Id).Result.Select((DataModelDataSourceEntity x) => x.ToDataSourceWithDecryptedSecret());
				bool flag = this.IsModelRefreshAllowed(enumerable);
				this.SetItemPropertiesInternal(userPrincipal, item.Path, new global::Model.Property[]
				{
					new global::Model.Property
					{
						Name = "ModelRefreshAllowed",
						Value = flag.ToString()
					}
				}, true);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005B3C File Offset: 0x00003D3C
		private void UpdatePowerBIReport(IPrincipal userPrincipal, string origItemPath, Guid origItemId, PowerBIReport item, bool renameOrMove, string[] delta)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			rsService.ExecuteStorageAction(delegate
			{
				if (item.HasContent())
				{
					using (ScopeMeter.Use(new string[] { "SQL", "StoreContent" }))
					{
						SetPowerBIReportContentsAction setPowerBIReportContentsAction = rsService.SetPowerBIReportContentsAction;
						setPowerBIReportContentsAction.ActionParameters.ItemPath = origItemPath;
						setPowerBIReportContentsAction.ActionParameters.CatalogItemContent = item.ToCatalogItemStreamContent();
						UpdatePowerBIReportActionParameters actionParameters = setPowerBIReportContentsAction.ActionParameters;
						actionParameters.Original = item.GetOriginalPbiStream();
						actionParameters.Pbix = item.GetPbixStream();
						actionParameters.Model = item.GetModelStream();
						string text = JsonConvert.SerializeObject(item.DataModelParameters);
						actionParameters.DataModelParameters = text;
						setPowerBIReportContentsAction.PerformActionNow();
					}
					this.SetDataModelDataSourcesTrusted(userPrincipal, origItemId, item.DataSources, true);
					this.SetDataModelRolesTrusted(origItemId, item.DataModelRoles, true);
				}
				IEnumerable<global::Model.DataSource> enumerable = this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(origItemId).Result.Select((DataModelDataSourceEntity x) => x.ToDataSourceWithDecryptedSecret());
				bool flag = this.IsModelRefreshAllowed(enumerable);
				this.UpdateModelRefreshPropertyIfRequired(item, flag);
				this.UpdateItemProperties(rsService, item, origItemPath, item.Properties.Select((global::Model.Property x) => x.ToSoapAPI()).ToArray<Microsoft.ReportingServices.Library.Soap.Property>());
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, item.Path);
				}
			});
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005BA4 File Offset: 0x00003DA4
		internal void UpdateModelRefreshPropertyIfRequired(PowerBIReport item, bool modelRefreshAllowed)
		{
			global::Model.Property property = item.Properties.FirstOrDefault((global::Model.Property x) => x.Name == "ModelRefreshAllowed");
			if (property == null)
			{
				item.Properties.Add(new global::Model.Property
				{
					Name = "ModelRefreshAllowed",
					Value = modelRefreshAllowed.ToString()
				});
				return;
			}
			if (property.Value != modelRefreshAllowed.ToString())
			{
				property.Value = modelRefreshAllowed.ToString();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005C29 File Offset: 0x00003E29
		internal bool IsModelRefreshAllowed(IEnumerable<global::Model.DataSource> updatedDataModelDatasources)
		{
			if (updatedDataModelDatasources.Count<global::Model.DataSource>() > 0)
			{
				return updatedDataModelDatasources.All((global::Model.DataSource x) => x.DataModelDataSource.Type == DataModelDataSourceType.Import && x.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Unknown && x.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Integrated && x.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Impersonate && (x.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Windows || !x.IsNullOrWhitespaceUsernameOrSecret()) && x.IsNotConnectionToLocalFile());
			}
			return false;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005C5C File Offset: 0x00003E5C
		internal ExcelWorkbook CreateExcel(RSService rsService, IPrincipal userPrincipal, string parentPath, bool overwrite, ExcelWorkbook item, global::Model.Property[] properties)
		{
			ExcelWorkbook excelWorkbook;
			using (ScopeMeter.Use(new string[] { "SQL", "CreateExcel" }))
			{
				item.Name = item.Name.Trim();
				string fileExtension = Path.GetExtension(item.Name);
				if (new string[] { ".xlsx", ".xlsb", ".xls", ".xlsm", ".csv" }.ToList<string>().All((string e) => string.Compare(fileExtension, e, true) != 0))
				{
					throw new ResourceFileFormatNotAllowedException(RepLibRes.DisallowedResourceExtensionError(fileExtension));
				}
				if (!item.HasContent())
				{
					throw new ExcelWorkbookContentInvalidException(Microsoft.ReportingServices.Portal.Services.SR.Error_CatalogItemContentInvalid);
				}
				if (string.IsNullOrWhiteSpace(item.ContentType))
				{
					item.ContentType = "application/octet-stream";
				}
				CreateExcelWorkbookAction createExcelAction = rsService.CreateExcelAction;
				CreateExcelWorkbookActionParameters actionParameters = createExcelAction.ActionParameters;
				actionParameters.ItemName = item.Name;
				actionParameters.ParentPath = parentPath;
				actionParameters.Overwrite = overwrite;
				actionParameters.MimeType = item.ContentType;
				actionParameters.CatalogItemContent = item.ToCatalogItemStreamContent();
				if (properties != null && properties.Any<global::Model.Property>())
				{
					actionParameters.Properties = properties.Select((global::Model.Property prop) => prop.ToSoapAPI()).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
				}
				createExcelAction.Execute();
				excelWorkbook = this.GetCatalogItem(rsService, userPrincipal, this.PathCombine(parentPath, item.Name)) as ExcelWorkbook;
			}
			return excelWorkbook;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005E00 File Offset: 0x00004000
		private void UpdateExcel(RSService rsService, string origItemPath, ExcelWorkbook item, bool renameOrMove, string[] delta)
		{
			string[] array = new string[] { "Hidden", "Description" };
			bool updateProperties = true;
			if (delta != null)
			{
				updateProperties = delta.Intersect(array).Count<string>() > 0;
			}
			if (string.IsNullOrWhiteSpace(item.ContentType))
			{
				item.ContentType = "application/octet-stream";
			}
			rsService.ExecuteStorageAction(delegate
			{
				using (ScopeMeter.Use(new string[] { "SQL", "UpdateExcel" }))
				{
					rsService.ThrowIfExcelFileExtensionChanged(item.Path, origItemPath);
					if (updateProperties)
					{
						this.UpdateItemProperties(rsService, item, origItemPath, null);
					}
					if (item.HasContent())
					{
						SetExcelWorkbookContentsAction setExcelWorkbookContentsAction = rsService.SetExcelWorkbookContentsAction;
						setExcelWorkbookContentsAction.ActionParameters.ItemPath = origItemPath;
						setExcelWorkbookContentsAction.ActionParameters.CatalogItemContent = item.ToCatalogItemStreamContent();
						setExcelWorkbookContentsAction.ActionParameters.MimeType = item.ContentType;
						setExcelWorkbookContentsAction.PerformActionNow();
					}
					if (renameOrMove)
					{
						this.RenameOrMoveItem(rsService, origItemPath, item.Path);
					}
				}
			});
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005EA8 File Offset: 0x000040A8
		private void EnsureCatalogItemCanBeCreated(RSService rsService, string parentPath, global::Model.CatalogItem item)
		{
			rsService.ExecuteStorageAction(delegate
			{
				rsService.EnsureCatalogItemCanBeCreated(parentPath, ItemType.Folder, item.Name, item.Type.ToLibraryItemType());
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005EE8 File Offset: 0x000040E8
		private void EnsureCatalogItemCanBeEdited(RSService rsService, string originalParentPath, global::Model.CatalogItem originalItem, string newParentPath, global::Model.CatalogItem newCatalogItem)
		{
			rsService.ExecuteStorageAction(delegate
			{
				rsService.EnsureCatalogItemCanBeEdited(originalParentPath, originalItem.Name, newCatalogItem.Type.ToLibraryItemType(), newParentPath, newCatalogItem.Name);
			});
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005F38 File Offset: 0x00004138
		private void RenameOrMoveItem(RSService rsService, string sourcePath, string destPath)
		{
			if (!sourcePath.StartsWith("/", StringComparison.Ordinal))
			{
				sourcePath = "/" + sourcePath;
			}
			if (!destPath.StartsWith("/", StringComparison.Ordinal))
			{
				destPath = "/" + destPath;
			}
			MoveItemAction moveItemAction = rsService.MoveItemAction;
			moveItemAction.ActionParameters.SourceItemPath = sourcePath;
			moveItemAction.ActionParameters.TargetItemPath = destPath;
			moveItemAction.PerformActionNow();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005FA0 File Offset: 0x000041A0
		private void UpdateItemProperties(RSService rsService, global::Model.CatalogItem item, string itemPath, Microsoft.ReportingServices.Library.Soap.Property[] additionalProperties = null)
		{
			Microsoft.ReportingServices.Library.Soap.Property[] array;
			this.ResolveCommonCatalogItemProperties(item, out array);
			if (additionalProperties != null)
			{
				array = array.Concat(additionalProperties).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
			}
			SetPropertiesAction setPropertiesAction = rsService.SetPropertiesAction;
			setPropertiesAction.ActionParameters.Properties = array;
			setPropertiesAction.ActionParameters.ItemPath = itemPath;
			setPropertiesAction.PerformActionNow();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005FEC File Offset: 0x000041EC
		private void UpdateFolder(RSService rsService, string origItemPath, global::Model.CatalogItem folderItem, bool renameOrMove, string[] delta)
		{
			string[] array = new string[] { "Hidden", "Description" };
			bool updateProperties = true;
			if (delta != null)
			{
				updateProperties = delta.Intersect(array).Any<string>();
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (updateProperties)
				{
					this.UpdateItemProperties(rsService, folderItem, origItemPath, null);
				}
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, folderItem.Path);
				}
			});
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006070 File Offset: 0x00004270
		private void UpdateKpi(RSService rsService, string origItemPath, Kpi item, bool renameOrMove, string[] delta)
		{
			string[] array = new string[] { "Hidden", "Description", "Values", "Data", "ValueFormat", "Visualization", "Currency", "DrillthroughTarget" };
			string oldName = CatalogItemRepository.GetNameFromFullPath(origItemPath);
			string parentPath = global::Model.CatalogItem.GetParentPathFromFullPath(origItemPath);
			bool updateProperties = true;
			if (delta != null)
			{
				updateProperties = delta.Intersect(array).Count<string>() > 0;
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (updateProperties)
				{
					CreateKpiAction createKpiAction = rsService.CreateKpiAction;
					CreateKpiActionParameters actionParameters = createKpiAction.ActionParameters;
					actionParameters.ItemName = oldName;
					actionParameters.ParentPath = parentPath;
					actionParameters.Overwrite = true;
					Microsoft.ReportingServices.Library.Soap.Property[] array2;
					DataSetInfoCollection dataSetInfoCollection;
					this.ResolveKpiProperties(item, new ResolveCatalogItem(this.ResolveCatalogItem), out array2, out dataSetInfoCollection);
					actionParameters.Properties = array2;
					actionParameters.SharedDataSets = dataSetInfoCollection;
					createKpiAction.PerformActionNow();
				}
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, item.Path);
				}
			});
			this._logger.Trace(TraceLevel.Info, () => string.Format("Kpi updated: {0}", item.Id));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006160 File Offset: 0x00004360
		private void UpdateLinkedReport(RSService rsService, string origItemPath, LinkedReport item, bool renameOrMove, string[] delta)
		{
			string[] array = new string[] { "Hidden", "Description", "Link" };
			bool updateProperties = true;
			if (delta != null)
			{
				updateProperties = delta.Intersect(array).Any<string>();
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (updateProperties)
				{
					SetPropertiesAction setPropertiesAction = rsService.SetPropertiesAction;
					Microsoft.ReportingServices.Library.Soap.Property[] array2;
					this.ResolveCommonCatalogItemProperties(item, out array2);
					setPropertiesAction.ActionParameters.Properties = array2;
					setPropertiesAction.ActionParameters.ItemPath = origItemPath;
					setPropertiesAction.PerformActionNow();
				}
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, item.Path);
				}
			});
			this._logger.Trace(TraceLevel.Info, () => string.Format("LinkedReport updated: {0}", item.Id));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006204 File Offset: 0x00004404
		internal void ResolveCommonCatalogItemProperties(global::Model.CatalogItem item, out Microsoft.ReportingServices.Library.Soap.Property[] soapProperties)
		{
			List<Microsoft.ReportingServices.Library.Soap.Property> list = new List<Microsoft.ReportingServices.Library.Soap.Property>
			{
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Description",
					Value = item.Description
				},
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Hidden",
					Value = (item.Hidden ? "true" : "false")
				}
			};
			soapProperties = list.ToArray();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006274 File Offset: 0x00004474
		internal void ResolveCatalogItem(Guid id, string path, ItemType itemType, bool throwIfNotExists, out Guid actualId, out string actualPath)
		{
			Guid outId = Guid.Empty;
			string outPath = null;
			RSService rsService = new RSService(false);
			rsService.ExecuteStorageAction(delegate
			{
				rsService.ResolveCatalogItem(id, path, itemType, throwIfNotExists, out outId, out outPath);
			}, ConnectionTransactionType.AutoCommit);
			actualId = outId;
			actualPath = outPath;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000062F0 File Offset: 0x000044F0
		internal void ResolveKpiProperties(Kpi item, ResolveCatalogItem resolveCatalogItem, out Microsoft.ReportingServices.Library.Soap.Property[] soapProperties, out DataSetInfoCollection sharedDataSets)
		{
			List<Microsoft.ReportingServices.Library.Soap.Property> list = new List<Microsoft.ReportingServices.Library.Soap.Property>
			{
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Description",
					Value = item.Description
				},
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Hidden",
					Value = (item.Hidden ? "true" : "false")
				},
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "ValueFormat",
					Value = ((int)item.ValueFormat).ToString()
				},
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Visualization",
					Value = ((int)item.Visualization).ToString()
				},
				new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "Currency",
					Value = item.Currency
				}
			};
			if (item.DrillthroughTarget == null)
			{
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.Type",
					Value = null
				});
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.Url",
					Value = null
				});
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.CatalogItemType",
					Value = null
				});
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.Path",
					Value = null
				});
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.Id",
					Value = null
				});
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.Parameters",
					Value = null
				});
			}
			else
			{
				if (item.DrillthroughTarget.Type != DrillthroughTargetType.Url)
				{
					this._logger.Trace(TraceLevel.Info, "KPI link catalog item unsuported.");
					throw new NotSupportedException();
				}
				UrlDrillthroughTarget urlDrillthroughTarget = item.DrillthroughTarget as UrlDrillthroughTarget;
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.Type",
					Value = 0.ToString()
				});
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.Url",
					Value = this.ValidateDrillthroughTargetUrl(urlDrillthroughTarget.Url)
				});
				list.Add(new Microsoft.ReportingServices.Library.Soap.Property
				{
					Name = "DrillthroughTarget.DirectNavigation",
					Value = urlDrillthroughTarget.DirectNavigation.ToString()
				});
			}
			DataSetInfoCollection dataSetInfoCollection = new DataSetInfoCollection();
			CatalogItemRepository.ResolveKpiDataItemProperties("Value", item.Data.Value, item.Values.Value, resolveCatalogItem, list, dataSetInfoCollection);
			CatalogItemRepository.ResolveKpiDataItemProperties("Goal", item.Data.Goal, item.Values.Goal, resolveCatalogItem, list, dataSetInfoCollection);
			CatalogItemRepository.ResolveKpiDataItemProperties("Status", item.Data.Status, item.Values.Status, resolveCatalogItem, list, dataSetInfoCollection);
			CatalogItemRepository.ResolveKpiDataItemProperties("TrendSet", item.Data.TrendSet, item.Values.TrendSet, resolveCatalogItem, list, dataSetInfoCollection);
			sharedDataSets = dataSetInfoCollection;
			soapProperties = list.ToArray();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000065E0 File Offset: 0x000047E0
		private string ValidateDrillthroughTargetUrl(string url)
		{
			if (!string.IsNullOrEmpty(url) && !this._allowedUrlPrefixes.Any((string x) => url.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
			{
				throw new InvalidElementException("Custom URL");
			}
			return url;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006634 File Offset: 0x00004834
		private static void ResolveKpiDataItemProperties(string dataItemName, KpiDataItem kpiDataItem, object value, ResolveCatalogItem resolveCatalogItem, IList<Microsoft.ReportingServices.Library.Soap.Property> properties, DataSetInfoCollection dataSets)
		{
			string text = string.Empty;
			Guid empty = Guid.Empty;
			string empty2 = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			if (value != null)
			{
				text = ((value is double?[]) ? JsonConvert.SerializeObject(value as double?[]) : Convert.ToString(value, CultureInfo.InvariantCulture));
			}
			if (kpiDataItem != null)
			{
				try
				{
					text3 = ((int)kpiDataItem.Type).ToString(CultureInfo.InvariantCulture);
					if (kpiDataItem.Type == KpiDataItemType.Shared)
					{
						KpiSharedDataItem kpiSharedDataItem = kpiDataItem as KpiSharedDataItem;
						resolveCatalogItem(kpiSharedDataItem.Id, kpiSharedDataItem.Path, ItemType.DataSet, true, out empty, out empty2);
						dataSets.Add(new DataSetInfo(dataItemName, empty2, empty));
						if (!string.IsNullOrWhiteSpace(kpiSharedDataItem.Column))
						{
							text2 = kpiSharedDataItem.Column.Trim();
						}
						if (dataItemName == "TrendSet")
						{
							text4 = 0.ToString();
						}
						else if (kpiSharedDataItem.Aggregation == global::Model.KpiSharedDataItemAggregation.None)
						{
							text4 = 1.ToString();
						}
						else
						{
							text4 = ((int)kpiSharedDataItem.Aggregation).ToString();
						}
						text5 = CatalogItemRepository.ResolveDataSetParameters(kpiSharedDataItem.Parameters);
					}
				}
				catch (Exception ex)
				{
					throw new CatalogItemPropertyInvalidException(Microsoft.ReportingServices.Portal.Services.SR.Error_CatalogItemPropertyInvalid, ex);
				}
			}
			properties.Add(new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = string.Format(CultureInfo.InvariantCulture, "{0}.Value", dataItemName),
				Value = ((text3 != null) ? text : string.Empty)
			});
			properties.Add(new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = string.Format(CultureInfo.InvariantCulture, "{0}.Type", dataItemName),
				Value = text3
			});
			properties.Add(new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = string.Format(CultureInfo.InvariantCulture, "{0}.Path", dataItemName),
				Value = empty2
			});
			properties.Add(new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = string.Format(CultureInfo.InvariantCulture, "{0}.Id", dataItemName),
				Value = (Guid.Empty.Equals(empty) ? string.Empty : empty.ToString())
			});
			properties.Add(new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = string.Format(CultureInfo.InvariantCulture, "{0}.Column", dataItemName),
				Value = text2
			});
			properties.Add(new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = string.Format(CultureInfo.InvariantCulture, "{0}.Aggregation", dataItemName),
				Value = text4
			});
			properties.Add(new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = string.Format(CultureInfo.InvariantCulture, "{0}.Parameters", dataItemName),
				Value = text5
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000068CC File Offset: 0x00004ACC
		private static string ResolveCatalogItemParameters(IEnumerable<CatalogItemParameter> parameters)
		{
			if (parameters == null || !parameters.Any<CatalogItemParameter>())
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (CatalogItemParameter catalogItemParameter in parameters)
			{
				stringBuilder.Append(Uri.EscapeDataString(catalogItemParameter.Name));
				stringBuilder.Append("=");
				stringBuilder.Append(Uri.EscapeDataString(catalogItemParameter.Value));
				stringBuilder.Append("&");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006978 File Offset: 0x00004B78
		private static string ResolveDataSetParameters(IEnumerable<DataSetParameter> parameters)
		{
			if (parameters == null || !parameters.Any<DataSetParameter>())
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DataSetParameter dataSetParameter in parameters)
			{
				stringBuilder.Append(Uri.EscapeDataString(dataSetParameter.Name));
				stringBuilder.Append("=");
				stringBuilder.Append(Uri.EscapeDataString(dataSetParameter.Value));
				stringBuilder.Append("&");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006A24 File Offset: 0x00004C24
		public void SetItemDataSources(IPrincipal userPrincipal, string itemPath, IEnumerable<global::Model.DataSource> dataSources)
		{
			SetItemDataSourcesAction setItemDataSourcesAction = ServicesUtil.CreateRsService(userPrincipal).SetItemDataSourcesAction;
			setItemDataSourcesAction.ActionParameters.ItemPath = itemPath;
			setItemDataSourcesAction.ActionParameters.ItemDataSources = dataSources.Select(new Func<global::Model.DataSource, Microsoft.ReportingServices.Library.Soap2005.DataSource>(DataSourceExtensions.ToSoapDataSource)).ToArray<Microsoft.ReportingServices.Library.Soap2005.DataSource>();
			setItemDataSourcesAction.ActionParameters.IgnoreSecCheck = false;
			setItemDataSourcesAction.Execute();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006A7C File Offset: 0x00004C7C
		public void SetItemDataSourcesTrusted(IPrincipal userPrincipal, string itemPath, IEnumerable<global::Model.DataSource> dataSources)
		{
			SetItemDataSourcesAction setItemDataSourcesAction = ServicesUtil.CreateRsService(userPrincipal).SetItemDataSourcesAction;
			setItemDataSourcesAction.ActionParameters.ItemPath = itemPath;
			setItemDataSourcesAction.ActionParameters.ItemDataSources = dataSources.Select(new Func<global::Model.DataSource, Microsoft.ReportingServices.Library.Soap2005.DataSource>(DataSourceExtensions.ToSoapDataSource)).ToArray<Microsoft.ReportingServices.Library.Soap2005.DataSource>();
			setItemDataSourcesAction.ActionParameters.IgnoreSecCheck = true;
			setItemDataSourcesAction.Execute();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006AD4 File Offset: 0x00004CD4
		private void UpdateDataSource(IPrincipal userPrincipal, string origItemPath, global::Model.DataSource dataSource, bool renameOrMove, string[] updatedFields)
		{
			bool flag = true;
			bool flag2 = true;
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			dataSource.LoadFromContent();
			if (updatedFields != null)
			{
				this.ValidateUpdateForFields(updatedFields, dataSource, out flag2, out flag);
			}
			Global.CheckItemName(dataSource.Name, ItemType.DataSource, "Name");
			if (flag2)
			{
				SetDataSourceContentsAction setDataSourceContentsAction = rsService.SetDataSourceContentsAction;
				setDataSourceContentsAction.ActionParameters.ItemPath = origItemPath;
				setDataSourceContentsAction.ActionParameters.DataSourceDefinition = dataSource.ToDataSourceDefinition();
				setDataSourceContentsAction.Execute();
			}
			if (flag)
			{
				SetPropertiesAction setPropertiesAction = rsService.SetPropertiesAction;
				setPropertiesAction.ActionParameters.ItemPath = origItemPath;
				setPropertiesAction.ActionParameters.Properties = new Microsoft.ReportingServices.Library.Soap.Property[]
				{
					new Microsoft.ReportingServices.Library.Soap.Property
					{
						Name = "Hidden",
						Value = (dataSource.Hidden ? true.ToString() : false.ToString())
					},
					new Microsoft.ReportingServices.Library.Soap.Property
					{
						Name = "Description",
						Value = dataSource.Description
					}
				};
				setPropertiesAction.Execute();
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, dataSource.Path);
				}
			});
			this._logger.Trace(TraceLevel.Info, () => string.Format("DataSource updated: {0}", dataSource.Id));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006C50 File Offset: 0x00004E50
		private void UpdateReportModel(IPrincipal userPrincipal, string origItemPath, ReportModel reportModel, bool renameOrMove, string[] updatedFields)
		{
			bool flag = true;
			bool flag2 = true;
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			if (updatedFields != null)
			{
				this.ValidateUpdateForFields(updatedFields, null, out flag2, out flag);
			}
			Global.CheckItemName(reportModel.Name, ItemType.Model, "Name");
			if (flag)
			{
				SetPropertiesAction setPropertiesAction = rsService.SetPropertiesAction;
				setPropertiesAction.ActionParameters.ItemPath = origItemPath;
				setPropertiesAction.ActionParameters.Properties = new Microsoft.ReportingServices.Library.Soap.Property[]
				{
					new Microsoft.ReportingServices.Library.Soap.Property
					{
						Name = "Hidden",
						Value = (reportModel.Hidden ? true.ToString() : false.ToString())
					},
					new Microsoft.ReportingServices.Library.Soap.Property
					{
						Name = "Description",
						Value = reportModel.Description
					}
				};
				setPropertiesAction.Execute();
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, reportModel.Path);
				}
			});
			this._logger.Trace(TraceLevel.Info, () => string.Format("ReportModel updated: {0}", reportModel.Id));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006D80 File Offset: 0x00004F80
		private void ValidateUpdateForFields(IEnumerable<string> updatedFields, global::Model.DataSource datasource, out bool containsPasswordSensitiveFields, out bool containsHiddenOrDescription)
		{
			bool flag = false;
			bool flag2 = false;
			containsPasswordSensitiveFields = false;
			containsHiddenOrDescription = false;
			foreach (string text in updatedFields)
			{
				if (string.Compare(text, "Name", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text, "Description", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text, "Hidden", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text, "Path", StringComparison.OrdinalIgnoreCase) != 0)
				{
					containsPasswordSensitiveFields = true;
				}
				if (string.Compare(text, "CredentialsInServer", StringComparison.OrdinalIgnoreCase) == 0 && datasource != null && datasource.CredentialsInServer != null && !string.IsNullOrWhiteSpace(datasource.CredentialsInServer.Password))
				{
					flag = true;
				}
				if (string.Compare(text, "ConnectionType", StringComparison.OrdinalIgnoreCase) == 0 && datasource != null && datasource.CredentialRetrieval == CredentialRetrievalType.store)
				{
					flag2 = true;
				}
				if (string.Compare(text, "Description", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text, "Hidden", StringComparison.OrdinalIgnoreCase) == 0)
				{
					containsHiddenOrDescription = true;
				}
			}
			if (containsPasswordSensitiveFields && !flag && flag2)
			{
				throw new ArgumentException("Did not update password.");
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006E90 File Offset: 0x00005090
		private bool IsRestrictedItemType(ItemType type)
		{
			return this.IsRestrictedItemType(type.ToCatalogItemType());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006E9E File Offset: 0x0000509E
		private bool IsRestrictedItemType(CatalogItemType type)
		{
			return this._systemService.GetRestrictedItemTypes().Contains(type);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006EB4 File Offset: 0x000050B4
		private void ThrowIfRestrictedItemType(RSService rsService, string path)
		{
			using (new RSServiceStorageAccess(rsService))
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(rsService);
				if (!catalogItemContext.SetPath(path))
				{
					throw new InvalidItemPathException(path);
				}
				ItemType itemType;
				rsService.Storage.ObjectExists(rsService.CatalogToExternal(catalogItemContext.CatalogItemPath), out itemType);
				this.ThrowIfRestrictedItemType(itemType, path);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006F20 File Offset: 0x00005120
		private void ThrowIfRestrictedItemType(ItemType type, string path)
		{
			this.ThrowIfRestrictedItemType(type.ToCatalogItemType(), path);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006F2F File Offset: 0x0000512F
		private void ThrowIfRestrictedItemType(CatalogItemType type, string path)
		{
			if (this.IsRestrictedItemType(type))
			{
				throw new RestrictedItemException(path ?? string.Empty);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006F4C File Offset: 0x0000514C
		public global::Model.DataSource GetDataSource(IPrincipal userPrincipal, Guid key)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			rsservice.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, ConnectionManager.DefaultIsolationLevel);
			string itemPathFromId = CatalogItemRepository.GetItemPathFromId(rsservice, key);
			return this.GetDataSource(userPrincipal, itemPathFromId);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006F7C File Offset: 0x0000517C
		public IList<global::Model.DataSource> GetDataSourcesForCatalogItem(IPrincipal userPrincipal, string itemPath)
		{
			GetItemDataSourcesAction getItemDataSourcesAction = ServicesUtil.CreateRsService(userPrincipal).GetItemDataSourcesAction;
			getItemDataSourcesAction.ActionParameters.ItemPath = itemPath;
			getItemDataSourcesAction.ActionParameters.FetchEncryptedCredentials = true;
			getItemDataSourcesAction.Execute();
			return getItemDataSourcesAction.ActionParameters.DataSources.Select(new Func<Microsoft.ReportingServices.Library.Soap2005.DataSource, global::Model.DataSource>(DataSourceExtensions.ToDataSource)).ToList<global::Model.DataSource>();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006FD4 File Offset: 0x000051D4
		public IList<global::Model.DataSet> GetDataSetsForCatalogItem(IPrincipal userPrincipal, string itemPath)
		{
			GetReportItemReferencesAction getReportItemReferencesAction = ServicesUtil.CreateRsService(userPrincipal).GetReportItemReferencesAction;
			getReportItemReferencesAction.ActionParameters.ItemPath = itemPath;
			getReportItemReferencesAction.Execute();
			List<global::Model.DataSet> list = new List<global::Model.DataSet>();
			foreach (Microsoft.ReportingServices.Library.Soap2010.ItemReferenceData itemReferenceData in getReportItemReferencesAction.ActionParameters.ItemReferences)
			{
				if (Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSet.ToString().Equals(itemReferenceData.ReferenceType, StringComparison.OrdinalIgnoreCase))
				{
					Guid guid = Guid.Empty;
					if (!string.IsNullOrEmpty(itemReferenceData.Reference))
					{
						guid = this.GetDataSet(userPrincipal, itemReferenceData.Reference).Id;
					}
					list.Add(new global::Model.DataSet
					{
						Id = guid,
						Name = itemReferenceData.Name,
						Path = itemReferenceData.Reference
					});
				}
			}
			return list;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00007094 File Offset: 0x00005294
		private LinkedReport GetLinkedReport(IPrincipal userPrincipal, string path)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			rsservice.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, ConnectionManager.DefaultIsolationLevel);
			GetReportLinkAction getReportLinkAction = rsservice.GetReportLinkAction;
			getReportLinkAction.ActionParameters.ReportPath = path;
			getReportLinkAction.Execute();
			LinkedReportRepository linkedReportRepository = new LinkedReportRepository(userPrincipal, this);
			linkedReportRepository.Link = getReportLinkAction.ActionParameters.LinkPath;
			FavoriteableCatalogItemDescriptor itemDescriptor = this.GetItemDescriptor(userPrincipal, rsservice, path);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, linkedReportRepository);
			linkedReportRepository.HasParameters = itemDescriptor.HasParameters;
			return linkedReportRepository;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00007104 File Offset: 0x00005304
		public global::Model.DataSource GetDataSource(IPrincipal userPrincipal, string path)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			FavoriteableCatalogItemDescriptor itemDescriptor = this.GetItemDescriptor(userPrincipal, rsservice, path);
			GetDataSourceContentsAction getDataSourceContentsAction = rsservice.GetDataSourceContentsAction;
			getDataSourceContentsAction.ActionParameters.DataSourcePath = path;
			getDataSourceContentsAction.Execute();
			DataSourceRepository dataSourceRepository = new DataSourceRepository(userPrincipal, this);
			global::Model.DataSource dataSource = getDataSourceContentsAction.ActionParameters.DataSourceDefinition.ToDataSource(dataSourceRepository);
			CatalogItemFactory.PopulateCommonFields(itemDescriptor, dataSource);
			dataSource.IsFavorite = itemDescriptor.IsFavorite;
			return dataSource;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007168 File Offset: 0x00005368
		private global::Model.DataSet CreateDataSet(RSService rsService, IPrincipal userPrincipal, string dataSetName, string parentFolder, bool overwrite, byte[] definition, global::Model.Property[] properties)
		{
			Global.CheckItemName(dataSetName, ItemType.DataSet, "Name");
			if (Path.GetExtension(dataSetName).Equals(".RSD", StringComparison.OrdinalIgnoreCase))
			{
				dataSetName = Path.GetFileNameWithoutExtension(dataSetName);
			}
			CreateDataSetAction createDataSetAction = rsService.CreateDataSetAction;
			createDataSetAction.ActionParameters.ItemName = dataSetName;
			createDataSetAction.ActionParameters.ParentPath = parentFolder;
			createDataSetAction.ActionParameters.DataSetDefinition = definition;
			createDataSetAction.ActionParameters.Overwrite = overwrite;
			if (properties != null && properties.Any<global::Model.Property>())
			{
				createDataSetAction.ActionParameters.Properties = properties.Select((global::Model.Property prop) => prop.ToSoapAPI()).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
			}
			createDataSetAction.Execute();
			return this.GetCatalogItem(rsService, userPrincipal, this.PathCombine(parentFolder, dataSetName)) as global::Model.DataSet;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007238 File Offset: 0x00005438
		private void UpdateDataSet(IPrincipal userPrincipal, string origItemPath, global::Model.DataSet item, bool renameOrMove, string[] delta)
		{
			string[] array = new string[] { "Hidden", "Description", "QueryExecutionTimeOut" };
			bool updateProperties = true;
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			if (delta != null)
			{
				updateProperties = delta.Intersect(array).Any<string>();
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (updateProperties)
				{
					Microsoft.ReportingServices.Library.Soap.Property[] array2;
					this.ResolveCommonCatalogItemProperties(item, out array2);
					Microsoft.ReportingServices.Library.Soap.Property[] array3 = null;
					if (delta != null)
					{
						string[] delta2 = delta;
						for (int i = 0; i < delta2.Length; i++)
						{
							if (delta2[i].Equals("QueryExecutionTimeOut", StringComparison.OrdinalIgnoreCase))
							{
								array3 = new Microsoft.ReportingServices.Library.Soap.Property[]
								{
									new Microsoft.ReportingServices.Library.Soap.Property
									{
										Name = "QueryExecutionTimeOut",
										Value = item.QueryExecutionTimeOut.ToString()
									}
								};
							}
						}
					}
					this.UpdateItemProperties(rsService, item, origItemPath, array3);
				}
				if (item.Content != null && item.Content.Length != 0)
				{
					SetDataSetDefinitionAction setDataSetDefinitionAction = rsService.SetDataSetDefinitionAction;
					setDataSetDefinitionAction.ActionParameters.ItemPath = origItemPath;
					setDataSetDefinitionAction.ActionParameters.DataSetDefinition = item.Content;
					setDataSetDefinitionAction.PerformActionNow();
				}
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, item.Path);
				}
			});
			if (item.DataSources != null && item.DataSources.Count > 0)
			{
				this.SetItemDataSources(userPrincipal, item.Path, item.DataSources);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007318 File Offset: 0x00005518
		private Resource CreateResource(RSService rsService, IPrincipal userPrincipal, string resourceName, string parentFolder, bool overwrite, global::Model.CatalogItem item, global::Model.Property[] properties)
		{
			if (string.IsNullOrWhiteSpace(item.ContentType))
			{
				item.ContentType = "application/octet-stream";
			}
			CreateResourceAction createResourceAction = rsService.CreateResourceAction;
			createResourceAction.ActionParameters.ItemName = resourceName;
			createResourceAction.ActionParameters.ParentPath = parentFolder;
			createResourceAction.ActionParameters.Overwrite = overwrite;
			createResourceAction.ActionParameters.Content = item.Content;
			createResourceAction.ActionParameters.MimeType = item.ContentType;
			if (properties != null && properties.Any<global::Model.Property>())
			{
				createResourceAction.ActionParameters.Properties = properties.Select((global::Model.Property prop) => prop.ToSoapAPI()).ToArray<Microsoft.ReportingServices.Library.Soap.Property>();
			}
			createResourceAction.Execute();
			return this.GetCatalogItem(rsService, userPrincipal, this.PathCombine(parentFolder, resourceName)) as Resource;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000073F0 File Offset: 0x000055F0
		private void UpdateResource(RSService rsService, string origItemPath, global::Model.CatalogItem item, bool renameOrMove, string[] delta)
		{
			string[] array = new string[] { "Hidden", "Description" };
			bool updateProperties = true;
			string mimeType = item.ContentType;
			global::Model.Property[] array2 = item.Properties.ToArray<global::Model.Property>();
			if (delta != null)
			{
				updateProperties = delta.Intersect(array).Count<string>() > 0;
			}
			if (string.IsNullOrWhiteSpace(mimeType))
			{
				mimeType = CatalogItemRepository.MimeTypeFromProperties(ref array2);
			}
			if (string.IsNullOrWhiteSpace(mimeType))
			{
				mimeType = "application/octet-stream";
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (updateProperties)
				{
					this.UpdateItemProperties(rsService, item, origItemPath, null);
				}
				if (item.Content.Length != 0)
				{
					SetResourceContentsAction setResourceContentsAction = rsService.SetResourceContentsAction;
					setResourceContentsAction.ActionParameters.ItemPath = origItemPath;
					setResourceContentsAction.ActionParameters.Definition = item.Content;
					setResourceContentsAction.ActionParameters.MimeType = mimeType;
					setResourceContentsAction.PerformActionNow();
				}
				rsService.ThrowIfInvalidFileFormat(item.Path);
				rsService.ThrowIfResctrictedMimeType(item.ContentType);
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, item.Path);
				}
			});
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000074CC File Offset: 0x000056CC
		private Component CreateComponent(RSService rsService, IPrincipal userPrincipal, string componentName, string parentFolder, bool overwrite, byte[] definition, global::Model.Property[] properties)
		{
			if (string.IsNullOrEmpty(componentName))
			{
				throw new ArgumentNullException("componentName");
			}
			if (Path.GetExtension(componentName).Equals(".RSC", StringComparison.OrdinalIgnoreCase))
			{
				componentName = Path.GetFileNameWithoutExtension(componentName);
			}
			Microsoft.SqlServer.ReportingServices2010.Property[] array = new Microsoft.SqlServer.ReportingServices2010.Property[0];
			if (properties != null && properties.Length != 0)
			{
				array = properties.Select((global::Model.Property prop) => prop.ToProxyAPI()).ToArray<Microsoft.SqlServer.ReportingServices2010.Property>();
			}
			else
			{
				array = null;
			}
			this._soapRS2010Proxy.CreateCatalogItem(userPrincipal, "Component", componentName, parentFolder, overwrite, definition, array);
			return this.GetCatalogItem(rsService, userPrincipal, this.PathCombine(parentFolder, componentName)) as Component;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007577 File Offset: 0x00005777
		private void DeleteItem(RSService rsService, string path)
		{
			this.ThrowIfRestrictedItemType(rsService, path);
			DeleteItemAction deleteItemAction = rsService.DeleteItemAction;
			deleteItemAction.ActionParameters.ItemPath = path;
			deleteItemAction.Execute();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007598 File Offset: 0x00005798
		private void UpdateComponent(IPrincipal userPrincipal, string origItemPath, Component item, bool renameOrMove, string[] delta)
		{
			string[] array = new string[] { "Hidden", "Description" };
			bool updateProperties = true;
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			if (delta != null)
			{
				updateProperties = delta.Intersect(array).Count<string>() > 0;
			}
			if (item.Content.Length != 0)
			{
				this.ReplaceComponentContent(userPrincipal, origItemPath, item);
				return;
			}
			rsService.ExecuteStorageAction(delegate
			{
				if (updateProperties)
				{
					this.UpdateItemProperties(rsService, item, origItemPath, null);
				}
				if (renameOrMove)
				{
					this.RenameOrMoveItem(rsService, origItemPath, item.Path);
				}
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007648 File Offset: 0x00005848
		private void ReplaceComponentContent(IPrincipal userPrincipal, string originalPath, Component item)
		{
			ServicesUtil.CreateRsService(userPrincipal);
			Microsoft.SqlServer.ReportingServices2010.Property[] array = new Microsoft.SqlServer.ReportingServices2010.Property[0];
			IList<global::Model.Property> properties = item.Properties;
			if (properties != null && properties.Any<global::Model.Property>())
			{
				array = properties.Select((global::Model.Property prop) => prop.ToProxyAPI()).ToArray<Microsoft.SqlServer.ReportingServices2010.Property>();
			}
			else
			{
				array = null;
			}
			this._soapRS2010Proxy.SetItemDefinition(userPrincipal, originalPath, item.Content, array);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000076B8 File Offset: 0x000058B8
		private static string MimeTypeFromProperties(ref global::Model.Property[] properties)
		{
			string text = null;
			List<global::Model.Property> list = new List<global::Model.Property>();
			if (properties != null && properties.Length != 0)
			{
				foreach (global::Model.Property property in properties)
				{
					if (text == null && property != null && string.Compare("MimeType", property.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = property.Value;
					}
					else
					{
						list.Add(property);
					}
				}
			}
			properties = list.ToArray();
			return text;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007724 File Offset: 0x00005924
		private static string GetItemPathFromId(RSService rsService, Guid key)
		{
			string value;
			using (new RSServiceStorageAccess(rsService))
			{
				CatalogItemPath pathById = rsService.Storage.GetPathById(key);
				if (pathById == null)
				{
					throw new ItemNotFoundException(key.ToString());
				}
				value = pathById.Value;
			}
			return value;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007780 File Offset: 0x00005980
		private static string GetNameFromFullPath(string itemPath)
		{
			string text = string.Empty;
			if (itemPath != "/")
			{
				string[] array = itemPath.Split(new char[] { '/' });
				int num = itemPath.Length - array[array.Length - 1].Length;
				text = itemPath.Substring(num, itemPath.Length - num);
			}
			return text;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000077D8 File Offset: 0x000059D8
		private ListSubscriptionsAction ListSubscriptionByType(IPrincipal userPrincipal, string itemPath, SubscriptionType type)
		{
			ListSubscriptionsAction listSubscriptionsAction = ServicesUtil.CreateRsService(userPrincipal).ListSubscriptionsAction;
			listSubscriptionsAction.ActionParameters.Path = itemPath;
			listSubscriptionsAction.ActionParameters.PathIsSiteOrFolder = false;
			listSubscriptionsAction.ActionParameters.SubscriptionType = type;
			listSubscriptionsAction.ActionParameters.IncludeExtensionSettings = true;
			listSubscriptionsAction.Execute();
			return listSubscriptionsAction;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007826 File Offset: 0x00005A26
		internal static void ExecuteStorageAction(RSService rsService, Action action)
		{
			CatalogItemRepository.ExecuteStorageAction(rsService, action, ConnectionManager.DefaultTransactionType, ConnectionManager.DefaultIsolationLevel);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007839 File Offset: 0x00005A39
		internal static void ExecuteStorageAction(RSService rsService, Action action, ConnectionTransactionType transactionType)
		{
			CatalogItemRepository.ExecuteStorageAction(rsService, action, transactionType, ConnectionManager.DefaultIsolationLevel);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007848 File Offset: 0x00005A48
		internal static void ExecuteStorageAction(RSService rsService, Action action, ConnectionTransactionType transactionType, IsolationLevel isolationLevel)
		{
			try
			{
				rsService.WillDisconnectStorage();
				rsService.SetDatabaseConnectionSettings(transactionType, isolationLevel);
				action();
			}
			catch (Exception ex)
			{
				rsService.AbortTransaction();
				if (ex is RSException)
				{
					if (ex is ReportServerStorageException && RSTrace.CatalogTrace.TraceError)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, ex.ToString());
					}
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				rsService.DisconnectStorage();
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000078CC File Offset: 0x00005ACC
		public IList<ReportParameterDefinition> GetSimpleParameterDefinitions(IPrincipal userPrincipal, string reportpath)
		{
			return (from itemParameter in this._soapRS2010Proxy.GetItemParameters(userPrincipal, reportpath, null, false, new Microsoft.SqlServer.ReportingServices2010.ParameterValue[0], null)
				select itemParameter.ToWebApiReportParameter()).ToList<ReportParameterDefinition>();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007918 File Offset: 0x00005B18
		public IList<ReportParameterDefinition> GetReportParameterDefinitionsWithQuery(IPrincipal userPrincipal, string reportpath)
		{
			return this.GetReportParameterDefinitionsWithQueryAndCurrentValues(userPrincipal, reportpath, new List<global::Model.ParameterValue>()).ToList<ReportParameterDefinition>();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000792C File Offset: 0x00005B2C
		public IEnumerable<ReportParameterDefinition> GetReportParameterDefinitionsWithQueryAndCurrentValues(IPrincipal userPrincipal, string reportpath, IEnumerable<global::Model.ParameterValue> parameterValues)
		{
			if (parameterValues == null)
			{
				throw new ArgumentNullException("parameterValues");
			}
			IEnumerable<Microsoft.SqlServer.ReportingServices2010.ParameterValue> enumerable = Enumerable.Empty<Microsoft.SqlServer.ReportingServices2010.ParameterValue>();
			Dictionary<string, ReportParameterType> parameterTypes = this._soapRS2010Proxy.GetParameterTypes(userPrincipal, reportpath);
			enumerable = parameterValues.Where((global::Model.ParameterValue parameterValue) => !parameterValue.IsValueFieldReference).Select(delegate(global::Model.ParameterValue parameterValue)
			{
				if (parameterTypes.ContainsKey(parameterValue.Name))
				{
					return parameterValue.ToSoapParameterValue(parameterTypes[parameterValue.Name]);
				}
				throw new ItemNotFoundException(parameterValue.Name);
			});
			return from itemParameter in this._soapRS2010Proxy.GetItemParameters(userPrincipal, reportpath, null, true, enumerable.ToArray<Microsoft.SqlServer.ReportingServices2010.ParameterValue>(), null)
				select itemParameter.ToWebApiReportParameter();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000079D8 File Offset: 0x00005BD8
		public void UpdateReportParameterDefinition(IPrincipal userPrincipal, string reportpath, IEnumerable<ReportParameterProperties> parameterProperties)
		{
			if (parameterProperties == null)
			{
				throw new ArgumentNullException("parameterProperties");
			}
			Dictionary<string, ReportParameterType> parameterTypes = this._soapRS2010Proxy.GetParameterTypes(userPrincipal, reportpath);
			IEnumerable<Microsoft.SqlServer.ReportingServices2010.ItemParameter> enumerable = parameterProperties.Select((ReportParameterProperties parameterPatch) => parameterPatch.ToSoapItemParameter(parameterTypes[parameterPatch.ParameterName]));
			this._soapRS2010Proxy.SetItemParamters(userPrincipal, reportpath, enumerable.ToArray<Microsoft.SqlServer.ReportingServices2010.ItemParameter>());
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007A34 File Offset: 0x00005C34
		public void UpdateReportParameterDefinition(IPrincipal userPrincipal, string reportpath, IEnumerable<ReportParameterDefinitionPatch> parameterProperties)
		{
			if (parameterProperties == null)
			{
				throw new ArgumentNullException("parameterProperties");
			}
			Dictionary<string, ReportParameterType> parameterTypes = this._soapRS2010Proxy.GetParameterTypes(userPrincipal, reportpath);
			Microsoft.SqlServer.ReportingServices2010.ItemParameter[] array = parameterProperties.Select((ReportParameterDefinitionPatch parameterPatch) => parameterPatch.ToSoapItemParameter(parameterTypes[parameterPatch.Name])).ToArray<Microsoft.SqlServer.ReportingServices2010.ItemParameter>();
			this._soapRS2010Proxy.SetItemParamters(userPrincipal, reportpath, array);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007A90 File Offset: 0x00005C90
		public bool AddToFavorites(IPrincipal userPrincipal, Guid id)
		{
			AddToFavoritesAction addToFavoritesAction = ServicesUtil.CreateRsService(userPrincipal).AddToFavoritesAction;
			addToFavoritesAction.ActionParameters.ItemId = id;
			addToFavoritesAction.Execute();
			this._logger.Trace(TraceLevel.Info, () => string.Format("Favorite added: {0}", id));
			return addToFavoritesAction.ActionParameters.Status;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public bool RemoveFromFavorites(IPrincipal userPrincipal, Guid id)
		{
			RemoveFromFavoritesAction removeFromFavoritesAction = ServicesUtil.CreateRsService(userPrincipal).RemoveFromFavoritesAction;
			removeFromFavoritesAction.ActionParameters.ItemId = id;
			removeFromFavoritesAction.Execute();
			this._logger.Trace(TraceLevel.Info, () => string.Format("Favorite removed: {0}", id));
			return removeFromFavoritesAction.ActionParameters.Status;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007B50 File Offset: 0x00005D50
		public IQueryable<global::Model.CatalogItem> GetFavoriteItems(IPrincipal userPrincipal)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			CatalogItemFactory catalogItemFactory = new CatalogItemFactory(userPrincipal, this, this._logger, this._catalogAccessor, this._systemService);
			IQueryable<global::Model.CatalogItem> queryable;
			using (new RSServiceStorageAccess(rsservice))
			{
				queryable = (from i in rsservice.Storage.GetAllFavoriteItems(rsservice.SecMgr, rsservice)
					where !this.IsRestrictedItemType(i.Type)
					select catalogItemFactory.Create(i) into c
					where c != null
					select c).AsQueryable<global::Model.CatalogItem>();
			}
			return queryable;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007C14 File Offset: 0x00005E14
		public string GetDataModelParameters(IPrincipal userPrincipal, Guid id)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsService, id);
			this.GetLibraryCatalogItem(rsService, catalogPathFromGuid).ThrowIfNoAccess(ReportOperation.ReadDatasource);
			string dataModelParameters = null;
			CatalogItemRepository.ExecuteStorageAction(rsService, delegate
			{
				dataModelParameters = rsService.Storage.GetDataModelParametersById(id);
			});
			return dataModelParameters;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007C84 File Offset: 0x00005E84
		public void SetDataModelParameters(IPrincipal userPrincipal, Guid id, string parameters)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsService, id);
			this.GetLibraryCatalogItem(rsService, catalogPathFromGuid).ThrowIfNoAccess(ReportOperation.UpdateParameters);
			CatalogItemRepository.ExecuteStorageAction(rsService, delegate
			{
				rsService.Storage.WriteDataModelParameters(id, parameters);
			});
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007CF0 File Offset: 0x00005EF0
		public DataSourceCheckResult TestDataSource(IPrincipal userPrincipal, global::Model.DataSource dataSource)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			Microsoft.SqlServer.ReportingServices2010.DataSourceDefinition dataSourceDefinition = dataSource.ToDataSourceDefinition2010();
			string text;
			bool flag = this._soapRS2010Proxy.TestConnectForDataSourceDefinition(userPrincipal, dataSourceDefinition, null, null, out text);
			return new DataSourceCheckResult
			{
				IsSuccessful = flag,
				ErrorMessage = text
			};
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007D38 File Offset: 0x00005F38
		private void EnsureCanCreateOrEdit(global::Model.DataSource dataSource, RSService rsService, string parent)
		{
			try
			{
				this.EnsureCatalogItemCanBeCreated(rsService, parent, dataSource);
			}
			catch (ItemAlreadyExistsException ex)
			{
				this._logger.Trace(TraceLevel.Info, string.Format("Item already exists, check if we can edit: {0}", ex.Message));
				this.EnsureCatalogItemCanBeEdited(rsService, parent, dataSource, parent, dataSource);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007D8C File Offset: 0x00005F8C
		public DataSourceCheckResult TestDataSource(IPrincipal userPrincipal, Guid key)
		{
			return this.TestDataSource(userPrincipal, key, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007D98 File Offset: 0x00005F98
		public DataSourceCheckResult TestDataSource(IPrincipal userPrincipal, string path, string dataSourceName)
		{
			ServicesUtil.CreateRsService(userPrincipal);
			string text;
			bool flag = this._soapRS2010Proxy.TestConnectForItemDataSource(userPrincipal, path, dataSourceName, null, null, out text);
			return new DataSourceCheckResult
			{
				IsSuccessful = flag,
				ErrorMessage = text
			};
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007DD4 File Offset: 0x00005FD4
		public DataSourceCheckResult TestDataSource(IPrincipal userPrincipal, Guid key, string dataSourceName)
		{
			string itemPathFromId = CatalogItemRepository.GetItemPathFromId(ServicesUtil.CreateRsService(userPrincipal), key);
			return this.TestDataSource(userPrincipal, itemPathFromId, dataSourceName);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007DF8 File Offset: 0x00005FF8
		public void EnsureCatalogItemDataSourcesValid(IPrincipal userPrincipal, global::Model.CatalogItem item)
		{
			CatalogItemType type = item.Type;
			string text;
			if (type <= CatalogItemType.DataSet)
			{
				if (type != CatalogItemType.Report && type != CatalogItemType.DataSet)
				{
					goto IL_003E;
				}
			}
			else
			{
				if (type == CatalogItemType.LinkedReport)
				{
					text = (item as LinkedReport).Link;
					goto IL_004A;
				}
				if (type - CatalogItemType.ReportModel > 1)
				{
					goto IL_003E;
				}
			}
			text = item.Path;
			goto IL_004A;
			IL_003E:
			throw new WrongItemTypeException(item.Path);
			IL_004A:
			if (!string.IsNullOrEmpty(text))
			{
				foreach (global::Model.DataSource dataSource in this.GetDataSourcesForCatalogItem(userPrincipal, text))
				{
					if ((dataSource.CredentialRetrieval == CredentialRetrievalType.store || dataSource.CredentialRetrieval == CredentialRetrievalType.prompt) && !this.TestDataSource(userPrincipal, text, dataSource.Name).IsSuccessful)
					{
						throw new InvalidDataSourceCredentialException();
					}
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007EC0 File Offset: 0x000060C0
		public IEnumerable<string> GetQueryFields(IPrincipal userPrincipal, global::Model.DataSource datasource, global::Model.Query query)
		{
			Microsoft.SqlServer.ReportingServices2010.DataSource dataSource = datasource.ToProxy2010DataSource();
			Microsoft.SqlServer.ReportingServices2010.DataSetDefinition dataSetDefinition = new Microsoft.SqlServer.ReportingServices2010.DataSetDefinition
			{
				Query = query.ToSoapQueryDefinition()
			};
			return this._soapRS2010Proxy.PrepareQuery(userPrincipal, dataSource, dataSetDefinition).Fields.Select((Microsoft.SqlServer.ReportingServices2010.Field x) => x.Name);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007F1D File Offset: 0x0000611D
		public byte[] GetDataSourcePasswordForSubscription(IPrincipal userPrincipal, Guid key)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			rsservice.WillDisconnectStorage();
			rsservice.SubscriptionManager.GetSubscription(key, false);
			return rsservice.Storage.GetDataSourcePasswordForSubscription(key);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007F44 File Offset: 0x00006144
		public global::Model.DataRetrievalPlan GetDataRetrievalPlanFromCatalog(IPrincipal userPrincipal, Guid key)
		{
			global::Model.DataRetrievalPlan dataRetrievalPlan = new global::Model.DataRetrievalPlan();
			DataDrivenSubscriptionProperties dataDrivenSubscriptionProperties = this._soapRS2010Proxy.GetDataDrivenSubscriptionProperties(userPrincipal, key.ToString());
			dataRetrievalPlan.DataSource = dataDrivenSubscriptionProperties.DataSettings.Item.ToDataSource();
			dataRetrievalPlan.Query = dataDrivenSubscriptionProperties.DataSettings.DataSet.ToQueryDefinition();
			return dataRetrievalPlan;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007F9C File Offset: 0x0000619C
		public IQueryable<global::Model.CatalogItem> GetDependentItems(IPrincipal userPrincipal, Guid key)
		{
			string itemPathFromId = CatalogItemRepository.GetItemPathFromId(ServicesUtil.CreateRsService(userPrincipal), key);
			return this.GetDependentItems(userPrincipal, itemPathFromId);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007FC0 File Offset: 0x000061C0
		public IQueryable<global::Model.CatalogItem> GetDependentItems(IPrincipal userPrincipal, string path)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			CatalogItemFactory catalogItemFactory = new CatalogItemFactory(userPrincipal, this, this._logger, this._catalogAccessor, this._systemService);
			ListDependentItemsAction listDependentItemsAction = rsService.ListDependentItemsAction;
			listDependentItemsAction.ActionParameters.ItemPath = path;
			listDependentItemsAction.ActionParameters.DirectDependentItemsOnly = true;
			listDependentItemsAction.Execute();
			return (from d in listDependentItemsAction.ActionParameters.DependentItems
				group d by d.ID into g
				where !this.IsRestrictedItemType(g.First<CatalogItemDescriptor>().Type)
				select catalogItemFactory.Create(g.First<CatalogItemDescriptor>()) into x
				select this.SetIsFavorite(rsService, x) into c
				where c != null
				select c).AsQueryable<global::Model.CatalogItem>();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000080B8 File Offset: 0x000062B8
		public IQueryable<global::Model.CatalogItem> SearchItems(IPrincipal userPrincipal, Guid key, string searchText)
		{
			string itemPathFromId = CatalogItemRepository.GetItemPathFromId(ServicesUtil.CreateRsService(userPrincipal), key);
			return this.SearchItems(userPrincipal, itemPathFromId, searchText);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000080DC File Offset: 0x000062DC
		public IQueryable<global::Model.CatalogItem> SearchItems(IPrincipal userPrincipal, string path, string searchText)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			CatalogItemFactory catalogItemFactory = new CatalogItemFactory(userPrincipal, this, this._logger, this._catalogAccessor, this._systemService);
			if (string.IsNullOrEmpty(path))
			{
				path = "/";
			}
			return (from x in rsService.FindItems(path, searchText)
				where !this.IsRestrictedItemType(x.Type)
				select this.SetIsFavorite(rsService, catalogItemFactory.Create(x)) into c
				where c != null
				select c).AsQueryable<global::Model.CatalogItem>();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008188 File Offset: 0x00006388
		public BulkOperationsResult DeleteItems(IPrincipal userPrincipal, IEnumerable<string> catalogItemPaths)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			return CatalogItemRepository.ExecuteBulkOperation(catalogItemPaths, delegate(string path)
			{
				DeleteItemAction deleteItemAction = rsService.DeleteItemAction;
				deleteItemAction.ActionParameters.ItemPath = CatalogItemPathUtils.EnsureSeparators(path);
				deleteItemAction.Execute();
			});
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000081BC File Offset: 0x000063BC
		public BulkOperationsResult MoveItems(IPrincipal userPrincipal, IEnumerable<string> catalogItemPaths, string targetPath)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			return CatalogItemRepository.ExecuteBulkOperation(catalogItemPaths, delegate(string sourcePath)
			{
				string text = CatalogItemPathUtils.EnsureSeparators(sourcePath);
				string nameFromFullPath = CatalogItemRepository.GetNameFromFullPath(text);
				string text2 = CatalogItemPathUtils.ConcatPathSegments(new string[] { targetPath, nameFromFullPath });
				MoveItemAction moveItemAction = rsService.MoveItemAction;
				moveItemAction.ActionParameters.SourceItemPath = text;
				moveItemAction.ActionParameters.TargetItemPath = text2;
				moveItemAction.Execute();
			});
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000081F4 File Offset: 0x000063F4
		public CatalogItemAccessToken CreateCatalogItemAccessToken(IEncryptionService encryptionService, IPrincipal userPrincipal, Guid key)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			Microsoft.ReportingServices.Library.CatalogItem libraryCatalogItem = this.GetLibraryCatalogItem(rsservice, CatalogItemRepository.GetCatalogPathFromGuid(rsservice, key));
			libraryCatalogItem.ThrowIfNoAccess(ReportOperation.ExecuteAndView);
			libraryCatalogItem.ThrowIfNoAccess(ReportOperation.ReadProperties);
			return CatalogItemRepository.CreateAccessToken(encryptionService, userPrincipal, key);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000822C File Offset: 0x0000642C
		private Microsoft.ReportingServices.Library.CatalogItem GetLibraryCatalogItem(RSService rsService, string itemPath)
		{
			Microsoft.ReportingServices.Library.CatalogItem catalogItem = null;
			rsService.ExecuteStorageAction(delegate
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(rsService);
				if (!catalogItemContext.SetPath(itemPath))
				{
					throw new InvalidItemPathException(itemPath);
				}
				catalogItem = rsService.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
				this.ThrowIfRestrictedItemType(catalogItem.ThisItemType, itemPath);
			}, ConnectionTransactionType.AutoCommit);
			return catalogItem;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000827C File Offset: 0x0000647C
		public static CatalogItemAccessToken CreateAccessToken(IEncryptionService encryptionService, IPrincipal userPrincipal, Guid key)
		{
			string text = JsonConvert.SerializeObject(new CatalogItemAccessTokenContent(userPrincipal.Identity.Name, userPrincipal.Identity.AuthenticationType, key));
			return new CatalogItemAccessToken(encryptionService.Encrypt(text));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000082B8 File Offset: 0x000064B8
		private static BulkOperationsResult ExecuteBulkOperation(IEnumerable<string> operations, Action<string> callback)
		{
			Queue<string> queue = new Queue<string>();
			foreach (string text in operations)
			{
				try
				{
					callback(text);
				}
				catch (RSException)
				{
					queue.Enqueue(text);
				}
			}
			if (queue.Any<string>())
			{
				return BulkOperationsResult.WithFailures(queue);
			}
			return BulkOperationsResult.Ok();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008334 File Offset: 0x00006534
		public List<string> GetAllowedActions(IPrincipal userPrincipal, string path)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			GetPermissionsAction getPermissionsAction = ServicesUtil.CreateGetItemPermissionsAction(rsservice, path);
			getPermissionsAction.Execute();
			IEnumerable<string> enumerable = getPermissionsAction.ActionParameters.Operations.Cast<string>();
			if (rsservice.MyReportsEnabled && Localization.CatalogCultureCompare(path, rsservice.PhysicalMyReportsPath) == 0)
			{
				enumerable = enumerable.Except(new List<string> { "Update Properties", "Delete", "Update Security Policies" });
			}
			return enumerable.ToList<string>();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000083B0 File Offset: 0x000065B0
		public string GetMyReportsPath(IPrincipal userPrincipal)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			return this.GetMyReportsPath(rsservice, userPrincipal);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000083CC File Offset: 0x000065CC
		public string GetMyReportsPath(RSService service, IPrincipal userPrincipal)
		{
			if (!service.MyReportsEnabled)
			{
				return null;
			}
			return service.PhysicalMyReportsPath;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000083E0 File Offset: 0x000065E0
		private global::Model.CatalogItem SetIsFavorite(RSService rsService, global::Model.CatalogItem catalogItem)
		{
			if (catalogItem != null)
			{
				rsService.ExecuteStorageAction(delegate
				{
					catalogItem.IsFavorite = rsService.Storage.IsFavoriteItem(rsService.UserName, catalogItem.Id);
				});
			}
			return catalogItem;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008428 File Offset: 0x00006628
		public global::Model.Comment GetComment(IPrincipal userPrincipal, long id)
		{
			CommentEntity result = this._catalogAccessor.GetCommentAsync(id).Result;
			if (result == null)
			{
				return null;
			}
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			this.CheckCommentPermissions(rsservice, result.ItemPath, false);
			return result.ToOdataModel();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008468 File Offset: 0x00006668
		public IList<global::Model.Comment> GetCommentsByItem(IPrincipal userPrincipal, Guid catalogItemId)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			this.CheckCommentPermissions(rsservice, catalogItemId, false);
			return this._catalogAccessor.GetCommentsByItemAsync(catalogItemId).Result.Select((CommentEntity c) => c.ToOdataModel()).ToList<global::Model.Comment>();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000084C0 File Offset: 0x000066C0
		public bool CreateComment(IPrincipal userPrincipal, global::Model.Comment entity, out global::Model.Comment createdEntity)
		{
			if (entity.ItemId == null)
			{
				this._logger.Trace(TraceLevel.Error, "ItemId was null when passed to CreateComment");
			}
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			rsservice.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, ConnectionManager.DefaultIsolationLevel);
			this.CheckCommentPermissions(rsservice, entity.ItemId.Value, false);
			global::Model.CatalogItem catalogItem = this.GetCatalogItem(userPrincipal, entity.ItemId.Value);
			if (!string.IsNullOrEmpty(entity.AttachmentPath) && !CatalogItemPathUtils.IsParentOf(entity.AttachmentPath, catalogItem.Path))
			{
				throw new CatalogItemContentInvalidException(string.Format("Comment {0} has an invalid attachment path.", entity.ItemId.Value));
			}
			if (entity.ThreadId != null)
			{
				global::Model.Comment comment = this.GetComment(userPrincipal, entity.ThreadId.Value);
				if (comment != null)
				{
					Guid? itemId = comment.ItemId;
					Guid value = entity.ItemId.Value;
					if (itemId != null && (itemId == null || !(itemId.GetValueOrDefault() != value)))
					{
						if (comment.ThreadId != null)
						{
							throw new CatalogItemContentInvalidException(string.Format("Parent of comment {0} is not top-level", entity.ItemId.Value));
						}
						goto IL_016C;
					}
				}
				throw new ItemNotFoundException(string.Format("Comment {0}", entity.ItemId.Value));
			}
			IL_016C:
			if (this._catalogAccessor.GetCommentsCountByItemAsync(entity.ItemId.Value).Result >= 100)
			{
				throw new MaxCountCommentsException();
			}
			Microsoft.ReportingServices.Library.Comment comment2 = new Microsoft.ReportingServices.Library.Comment
			{
				ItemId = entity.ItemId.Value,
				Text = entity.Text,
				ThreadId = entity.ThreadId,
				AttachmentPath = entity.AttachmentPath
			};
			using (RSServiceStorageAccess rsserviceStorageAccess = new RSServiceStorageAccess(rsservice))
			{
				Microsoft.ReportingServices.Library.Comment comment3 = rsservice.Storage.InsertComment(comment2);
				createdEntity = ((comment3 != null) ? comment3.ToOdataModel() : null);
				rsserviceStorageAccess.Commit();
			}
			if (createdEntity != null)
			{
				try
				{
					this._catalogAccessor.AddCommentEvent(createdEntity.Id);
				}
				catch (Exception ex)
				{
					this._logger.Trace(TraceLevel.Error, string.Format("Error in creating a CommentAddedAlert event: {0}", ex.ToString()));
				}
			}
			return createdEntity != null;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008738 File Offset: 0x00006938
		public bool UpdateComment(IPrincipal userPrincipal, global::Model.Comment entity, string[] delta)
		{
			Microsoft.ReportingServices.Library.Comment comment = new Microsoft.ReportingServices.Library.Comment
			{
				Text = entity.Text,
				Id = entity.Id
			};
			if (entity.ItemId == null)
			{
				this._logger.Trace(TraceLevel.Error, "entity.ItemId is null in UpdateComment");
			}
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			rsservice.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, ConnectionManager.DefaultIsolationLevel);
			this.CheckCommentPermissions(rsservice, entity.ItemId.Value, false);
			bool flag2;
			using (RSServiceStorageAccess rsserviceStorageAccess = new RSServiceStorageAccess(rsservice))
			{
				bool flag = rsservice.Storage.UpdateComment(comment);
				rsserviceStorageAccess.Commit();
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000087E8 File Offset: 0x000069E8
		public bool DeleteComment(IPrincipal userPrincipal, long id, bool checkManager)
		{
			global::Model.Comment comment = this.GetComment(userPrincipal, id);
			if (comment.ItemId == null)
			{
				this._logger.Trace(TraceLevel.Error, "ItemId was null on comment retrieved from the database");
			}
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			rsservice.SetDatabaseConnectionSettings(ConnectionTransactionType.AutoCommit, ConnectionManager.DefaultIsolationLevel);
			this.CheckCommentPermissions(rsservice, comment.ItemId.Value, checkManager);
			return this._catalogAccessor.DeleteCommentAsync(id).Result > 0;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000885C File Offset: 0x00006A5C
		public bool IsUserContextOwner(IPrincipal user, long id)
		{
			RSService rsservice = ServicesUtil.CreateRsService(user);
			bool flag;
			using (new RSServiceStorageAccess(rsservice))
			{
				flag = rsservice.Storage.IsUserContextOwner(id);
			}
			return flag;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000088A4 File Offset: 0x00006AA4
		private void CheckCommentPermissions(RSService rsService, Guid itemId, bool checkDelete)
		{
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsService, itemId);
			this.CheckCommentPermissions(rsService, catalogPathFromGuid, checkDelete);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000088C4 File Offset: 0x00006AC4
		private void CheckCommentPermissions(RSService rsService, string path, bool checkDelete)
		{
			Microsoft.ReportingServices.Library.CatalogItem libraryCatalogItem = this.GetLibraryCatalogItem(rsService, path);
			this.CheckCommentsAllowedOnType(libraryCatalogItem.ThisItemType);
			libraryCatalogItem.ThrowIfNoAccess(CommonOperation.Comment);
			if (checkDelete)
			{
				libraryCatalogItem.ThrowIfNoAccess(CommonOperation.ManageComments);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000088F7 File Offset: 0x00006AF7
		private void CheckCommentsAllowedOnType(ItemType itemType)
		{
			if (itemType != ItemType.Report && itemType != ItemType.LinkedReport && itemType - ItemType.PowerBIReport > 1)
			{
				throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008914 File Offset: 0x00006B14
		public bool AddExecutionLogInfo(IPrincipal userPrincipal, ExecutionLogInfo executionLogInfo)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			return this.LogExecutionLogInfo(rsservice, executionLogInfo);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008930 File Offset: 0x00006B30
		private bool LogExecutionLogInfo(RSService rsService, ExecutionLogInfo executionLogInfo)
		{
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				DBInterface dbinterface = new DBInterface(rsService.UserContext);
				dbinterface.ConnectionManager = connectionManager;
				string instanceName = this.GetInstanceName(Environment.MachineName);
				ExternalItemPath externalItemPath = new ExternalItemPath(executionLogInfo.ItemPath);
				CatalogItemPath catalogItemPath = rsService.ExternalToCatalogItemPath(externalItemPath);
				ReportExecutionInfo reportExecutionInfo = executionLogInfo.ToReportExecutionLog();
				if (executionLogInfo.Status == -1L)
				{
					reportExecutionInfo.Status = ErrorCode.pvInternalError;
				}
				dbinterface.AddExecutionLogEntry(instanceName, catalogItemPath, DBInterface.RequestType.Interactive, reportExecutionInfo.Format, reportExecutionInfo.Parameters, executionLogInfo.StartTime, executionLogInfo.EndTime, executionLogInfo.DataRetrievalTime, executionLogInfo.ProcessingTime, executionLogInfo.RenderingTime, reportExecutionInfo.Source, reportExecutionInfo.Status.ToString(), reportExecutionInfo.ByteCount, reportExecutionInfo.RowCount, reportExecutionInfo.ExecutionId, (byte)reportExecutionInfo.EventType, reportExecutionInfo.GetAdditionalInfoXml());
				dbinterface.Commit();
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
			return true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008A34 File Offset: 0x00006C34
		public Guid GetItemIdFromHistoryId(Guid historyId)
		{
			return this._catalogAccessor.GetItemIdFromHistoryId(historyId);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008A44 File Offset: 0x00006C44
		public Guid GetUserIdFromName(string userName, AuthenticationType authType)
		{
			if (AuthenticationType.Windows == authType)
			{
				SecurityIdentifier accountSID = LoginUtil.GetAccountSID(userName);
				byte[] array = new byte[accountSID.BinaryLength];
				accountSID.GetBinaryForm(array, 0);
				return this._catalogAccessor.GetUserIDWithNoCreate(array, null, (int)authType).Result;
			}
			return this._catalogAccessor.GetUserIDWithNoCreate(new byte[0], userName, (int)authType).Result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008A9C File Offset: 0x00006C9C
		public Guid GetUserId(string userName, AuthenticationType authType)
		{
			if (AuthenticationType.Windows == authType)
			{
				byte[] sidFromUserName = this.GetSidFromUserName(userName);
				return this._catalogAccessor.GetUserID(sidFromUserName, userName, (int)authType).Result;
			}
			if (!AuthenticationExtensionFactory.GetAuthenticationExtension(authType).IsValidPrincipalName(userName))
			{
				throw new UnknownUserNameException(userName);
			}
			return this._catalogAccessor.GetUserID(new byte[0], userName, (int)authType).Result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00008AF8 File Offset: 0x00006CF8
		public UserSettings GetUserSettings(Guid userId)
		{
			string result = this._catalogAccessor.GetDefaultEmailAsync(userId).Result;
			return new UserSettings
			{
				Id = userId,
				EmailAddress = result
			};
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008B2A File Offset: 0x00006D2A
		public bool UpdateUserSettings(UserSettings settings)
		{
			return this._catalogAccessor.SetDefaultEmailAsync(settings.Id, settings.EmailAddress).Result > 0;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008B4C File Offset: 0x00006D4C
		private byte[] GetSidFromUserName(string userName)
		{
			SecurityIdentifier accountSID;
			try
			{
				accountSID = LoginUtil.GetAccountSID(userName);
			}
			catch
			{
				throw new UnknownUserNameException(userName);
			}
			byte[] array = new byte[accountSID.BinaryLength];
			accountSID.GetBinaryForm(array, 0);
			return array;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008B90 File Offset: 0x00006D90
		private string GetPrincipalNameFromSid(byte[] sidBytes)
		{
			string text = null;
			IWindowsAuthenticationExtension2 windowsAuthenticationExtension = AuthenticationExtensionFactory.GetAuthenticationExtension(AuthenticationType.Windows) as IWindowsAuthenticationExtension2;
			if (windowsAuthenticationExtension != null)
			{
				text = windowsAuthenticationExtension.SidToPrincipalName(sidBytes);
				if (text != null && text.Length > 260)
				{
					throw new ServerConfigurationErrorException("Invalid name returned by authentication extension" + text);
				}
			}
			return text;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public long GetEmailAlertSubscriptionId(Guid userId, Guid itemId, string alertType)
		{
			return this._catalogAccessor.GetAlertSubscriptionId(userId, itemId, alertType).Result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public bool AddEmailAlertSubscription(Guid userId, Guid itemId, string alertType, out AlertSubscription createdAlertSubscription)
		{
			int result = this._catalogAccessor.AddAlertSubscription(userId, itemId, alertType).Result;
			createdAlertSubscription = new AlertSubscription
			{
				AlertType = alertType,
				ItemId = itemId,
				Id = this._catalogAccessor.GetAlertSubscriptionId(userId, itemId, alertType).Result
			};
			return result > 0;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008C42 File Offset: 0x00006E42
		public bool DeleteEmailAlertSubscription(long id)
		{
			return this._catalogAccessor.DeleteAlertSubscription(id).Result > 0;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008C58 File Offset: 0x00006E58
		public void SetDataModelDataSourcesTrusted(IPrincipal userPrincipal, Guid itemId, IEnumerable<global::Model.DataSource> dataSources, bool isOverwrite)
		{
			AuthenticationType authenticationType = userPrincipal.Identity.ToAuthenticationType();
			Guid userId = this.GetUserId(userPrincipal.Identity.Name, authenticationType);
			IEnumerable<global::Model.DataSource> enumerable = dataSources;
			List<global::Model.DataSource> list = new List<global::Model.DataSource>();
			if (dataSources != null)
			{
				if (isOverwrite)
				{
					IEnumerable<global::Model.DataSource> enumerable2 = this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(itemId).Result.Select((DataModelDataSourceEntity d) => d.ToDataSourceWithoutSecret());
					enumerable = dataSources.Except(enumerable2, new CatalogItemRepository.DataSourceModelComparer());
					list = enumerable2.Except(dataSources, new CatalogItemRepository.DataSourceModelComparer()).ToList<global::Model.DataSource>();
					Dictionary<global::Model.DataSource, global::Model.DataSource> dictionary = dataSources.ToDictionary((global::Model.DataSource p) => p, new CatalogItemRepository.DataSourceModelComparer());
					foreach (global::Model.DataSource dataSource in enumerable2)
					{
						if (dictionary.ContainsKey(dataSource))
						{
							global::Model.DataSource dataSource2 = dictionary[dataSource];
							DataModelDataSource dataModelDataSource = dataSource.DataModelDataSource;
							string text = ((dataModelDataSource != null) ? dataModelDataSource.ModelConnectionName : null);
							DataModelDataSource dataModelDataSource2 = dataSource2.DataModelDataSource;
							if (text != ((dataModelDataSource2 != null) ? dataModelDataSource2.ModelConnectionName : null))
							{
								ICatalogDataModelDataSourceAccessor dataModelDataSourceAccessor = this._dataModelDataSourceAccessor;
								Guid id = dataSource.Id;
								DataModelDataSource dataModelDataSource3 = dataSource2.DataModelDataSource;
								dataModelDataSourceAccessor.UpdateDataModelDataSourceConnectionNameAsync(id, (dataModelDataSource3 != null) ? dataModelDataSource3.ModelConnectionName : null, this._crypto.Encrypt(dataSource2.ConnectionString ?? string.Empty));
							}
						}
					}
					string text2;
					if (!enumerable.Any<global::Model.DataSource>())
					{
						text2 = "None";
					}
					else
					{
						text2 = string.Join(",", enumerable.Select((global::Model.DataSource p) => p.ConnectionString));
					}
					string text3 = text2;
					string text4;
					if (!list.Any<global::Model.DataSource>())
					{
						text4 = "None";
					}
					else
					{
						text4 = string.Join(",", list.Select((global::Model.DataSource p) => p.ConnectionString));
					}
					string text5 = text4;
					this._logger.Trace(TraceLevel.Info, string.Format("Overwritting for item {0} is removing connections {1} and adding connections {2}", itemId, text5, text3));
				}
				foreach (global::Model.DataSource dataSource3 in enumerable)
				{
					this._dataModelDataSourceAccessor.AddDataModelDataSourceAsync(itemId, EnumUtils.Parse<DataModelDataSourceEntity.DataModelDataSourceType>(dataSource3.DataModelDataSource.Type), EnumUtils.Parse<DataModelDataSourceEntity.DataModelDataSourceKind>(dataSource3.DataModelDataSource.Kind), EnumUtils.Parse<DataModelDataSourceEntity.DataModelDataSourceAuthType>(dataSource3.DataModelDataSource.AuthType), this._crypto.Encrypt(dataSource3.ConnectionString ?? string.Empty), this._crypto.Encrypt(dataSource3.DataModelDataSource.Username ?? string.Empty), this._crypto.Encrypt(dataSource3.DataModelDataSource.Secret ?? string.Empty), userId, dataSource3.DataModelDataSource.ModelConnectionName);
				}
				foreach (global::Model.DataSource dataSource4 in list)
				{
					this._dataModelDataSourceAccessor.DeleteDataModelDataSourceByIdAsync(dataSource4.Id);
				}
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008FC0 File Offset: 0x000071C0
		public async global::System.Threading.Tasks.Task UpdateDataModelDataSourcesAsync(IPrincipal userPrincipal, Guid itemId, List<global::Model.DataSource> dataSources)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string path = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, itemId);
			this.GetLibraryCatalogItem(rsservice, path).ThrowIfNoAccess(ReportOperation.UpdateDatasource);
			foreach (global::Model.DataSource dataSource in dataSources)
			{
				if (dataSource.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Unknown && !dataSource.DataModelDataSource.Kind.GetSupportedAuthKinds(dataSource.DataModelDataSource.Type).Contains(dataSource.DataModelDataSource.AuthType))
				{
					throw new CatalogItemContentInvalidException(string.Format("Unable to update the datasources for '{0}'. '{1}' is not a supported authentication type for '{2}' datasources.", path, dataSource.DataModelDataSource.AuthType.ToString(), dataSource.DataModelDataSource.Type.ToString()));
				}
			}
			IEnumerable<global::Model.DataSource> enumerable = (await this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(itemId)).Select((DataModelDataSourceEntity d) => d.ToDataSourceWithoutSecret());
			global::Model.Property property = this.GetCatalogItem(userPrincipal, itemId).Properties.ToList<global::Model.Property>().Find((global::Model.Property a) => a.Name == "ModelVersion");
			bool flag = ((property != null) ? property.Value : null) != "PowerBI_V3";
			IEnumerable<global::Model.DataSource> enumerable2 = this.FetchUpdatableDataSources(enumerable, dataSources, flag);
			AuthenticationType authenticationType = userPrincipal.Identity.ToAuthenticationType();
			Guid userId = this.GetUserId(userPrincipal.Identity.Name, authenticationType);
			foreach (global::Model.DataSource dataSource2 in enumerable2)
			{
				await this._dataModelDataSourceAccessor.UpdateDataModelDataSourceAsync(dataSource2.Id, EnumUtils.Parse<DataModelDataSourceEntity.DataModelDataSourceAuthType>(dataSource2.DataModelDataSource.AuthType), SymmetricKeyCrypto.Instance.Encrypt(dataSource2.ConnectionString), SymmetricKeyCrypto.Instance.Encrypt(dataSource2.DataModelDataSource.Username), SymmetricKeyCrypto.Instance.Encrypt(dataSource2.DataModelDataSource.Secret), userId);
			}
			IEnumerator<global::Model.DataSource> enumerator2 = null;
			IEnumerable<global::Model.DataSource> enumerable3 = (await this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(itemId)).Select((DataModelDataSourceEntity x) => x.ToDataSourceWithDecryptedSecret());
			bool flag2 = this.IsModelRefreshAllowed(enumerable3);
			this.UpdateModelRefreshProperty(userPrincipal, path, flag2);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00009020 File Offset: 0x00007220
		private void UpdateModelRefreshProperty(IPrincipal userPrincipal, string path, bool modelRefreshAllowed)
		{
			List<global::Model.Property> list = new List<global::Model.Property>();
			global::Model.Property property = new global::Model.Property
			{
				Name = "ModelRefreshAllowed",
				Value = modelRefreshAllowed.ToString()
			};
			list.Add(property);
			this.SetItemProperties(userPrincipal, path, list);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009064 File Offset: 0x00007264
		public async global::System.Threading.Tasks.Task DeleteDataModelDataSourcesTrustedAsync(List<global::Model.DataSource> dataSources)
		{
			foreach (global::Model.DataSource dataSource in dataSources)
			{
				await this._dataModelDataSourceAccessor.DeleteDataModelDataSourceByIdAsync(dataSource.Id);
			}
			List<global::Model.DataSource>.Enumerator enumerator = default(List<global::Model.DataSource>.Enumerator);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000090B4 File Offset: 0x000072B4
		internal IEnumerable<global::Model.DataSource> FetchUpdatableDataSources(IEnumerable<global::Model.DataSource> existingDataModelDataSources, IEnumerable<global::Model.DataSource> dataSources, bool isV1Model)
		{
			List<global::Model.DataSource> list = new List<global::Model.DataSource>();
			using (IEnumerator<global::Model.DataSource> enumerator = dataSources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					global::Model.DataSource ds = enumerator.Current;
					global::Model.DataSource dataSource = existingDataModelDataSources.Where((global::Model.DataSource x) => x.Id == ds.Id && x.DataModelDataSource.Type == ds.DataModelDataSource.Type && x.DataModelDataSource.Kind == ds.DataModelDataSource.Kind && string.Equals(x.DataModelDataSource.ModelConnectionName, ds.DataModelDataSource.ModelConnectionName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault<global::Model.DataSource>();
					if (dataSource == null)
					{
						throw new CatalogItemContentInvalidException("Only AuthType, UserName, Secret and ConnectionString properties can be updated for datasources of DataModel DataSourceSubType");
					}
					bool flag = string.Equals(dataSource.ConnectionString, ds.ConnectionString, StringComparison.OrdinalIgnoreCase);
					DataModelDataSourceType type = dataSource.DataModelDataSource.Type;
					if ((isV1Model && (flag || type == DataModelDataSourceType.Live || type == DataModelDataSourceType.DirectQuery)) || (!isV1Model && (flag || type == DataModelDataSourceType.Live)))
					{
						list.Add(ds);
					}
					else if (!flag)
					{
						if ((isV1Model && type != DataModelDataSourceType.Live && type != DataModelDataSourceType.DirectQuery) || (!isV1Model && type != DataModelDataSourceType.Live))
						{
							throw new CatalogItemContentInvalidException("Connection strings for reports created with October 2020 or newer with enhanced metadata can only update Live connections. Prior versions can update Live and Direct Query connections.");
						}
					}
					else
					{
						if (ds.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Integrated && ((ds.DataModelDataSource.AuthType != DataModelDataSourceAuthType.Windows && ds.DataModelDataSource.AuthType != DataModelDataSourceAuthType.UsernamePassword) || ds.DataModelDataSource.Username == null || ds.DataModelDataSource.Secret == null))
						{
							throw new CatalogItemContentInvalidException("ConnectionString modified without updating UserName and Secret.");
						}
						list.Add(ds);
					}
				}
			}
			return list;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00009238 File Offset: 0x00007438
		public global::Model.DataSource GetDataSourceForTestConnection(IPrincipal userPrincipal, Guid itemId, Guid dataSourceId)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, itemId);
			this.GetLibraryCatalogItem(rsservice, catalogPathFromGuid).ThrowIfNoAccess(ReportOperation.ReadDatasource);
			return this._dataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(itemId).Result.Single((DataModelDataSourceEntity ds) => ds.DataSourceId == dataSourceId).ToDataSourceWithDecryptedSecret();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00009298 File Offset: 0x00007498
		public void SetDataModelRolesTrusted(Guid itemId, IEnumerable<DataModelRole> dataModelRoles, bool isOverwrite)
		{
			IEnumerable<DataModelRole> enumerable = dataModelRoles;
			List<DataModelRole> list = new List<DataModelRole>();
			List<DataModelRole> list2 = new List<DataModelRole>();
			if (dataModelRoles != null)
			{
				Dictionary<Guid, long> result = this.GetRoleIdMapping(itemId).Result;
				if (isOverwrite)
				{
					IEnumerable<DataModelRole> enumerable2 = this._dataModelRoleAccessor.GetDataModelRolesByItemAsync(itemId).Result.Select((DataModelRoleEntity r) => r.ToDataModelRole());
					enumerable = dataModelRoles.Except(enumerable2, new CatalogItemRepository.DataModelRoleComparer());
					list = enumerable2.Except(dataModelRoles, new CatalogItemRepository.DataModelRoleComparer()).ToList<DataModelRole>();
					list2 = enumerable2.Intersect(dataModelRoles, new CatalogItemRepository.DataModelRoleComparer()).ToList<DataModelRole>();
				}
				foreach (DataModelRole dataModelRole in enumerable)
				{
					this._dataModelRoleAccessor.AddDataModelRoleAsync(itemId, dataModelRole.ModelRoleId, dataModelRole.ModelRoleName);
				}
				foreach (DataModelRole dataModelRole2 in list)
				{
					this._dataModelRoleAccessor.DeleteDataModelRoleByIdAsync(result[dataModelRole2.ModelRoleId]);
				}
				using (List<DataModelRole>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DataModelRole dataModelRoleToUpdate = enumerator2.Current;
						string modelRoleName = dataModelRoles.Single((DataModelRole r) => r.ModelRoleId == dataModelRoleToUpdate.ModelRoleId).ModelRoleName;
						this._dataModelRoleAccessor.UpdateDataModelRoleAsync(result[dataModelRoleToUpdate.ModelRoleId], modelRoleName);
					}
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00009458 File Offset: 0x00007658
		public async Task<IList<DataModelRole>> GetDataModelRolesAsync(IPrincipal userPrincipal, Guid itemId)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, itemId);
			this.GetLibraryCatalogItem(rsservice, catalogPathFromGuid).ThrowIfNoAccess(ReportOperation.ReadReportDefinition);
			return (await this._dataModelRoleAccessor.GetDataModelRolesByItemAsync(itemId)).Select((DataModelRoleEntity r) => r.ToDataModelRole()).ToList<DataModelRole>();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000094B0 File Offset: 0x000076B0
		public async Task<IList<DataModelRoleAssignment>> GetDataModelRoleAssignmentsAsync(IPrincipal userPrincipal, Guid itemId)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, itemId);
			this.GetLibraryCatalogItem(rsservice, catalogPathFromGuid).ThrowIfNoAccess(ReportOperation.ReadReportDefinition);
			List<DataModelRoleAssignment> list = (await this._dataModelRoleAccessor.GetDataModelRoleAssignmentsByItemAsync(itemId)).Select((DataModelRoleAssignmentEntity r) => r.ToDataModelRoleAssignment()).ToList<DataModelRoleAssignment>();
			this.ValidateAndNormalizeUsers(list, userPrincipal);
			return list;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009508 File Offset: 0x00007708
		public async global::System.Threading.Tasks.Task UpdateDataModelRoleAssignmentsAsync(IPrincipal userPrincipal, Guid itemId, List<DataModelRoleAssignment> dataModelRoleAssignments)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			string catalogPathFromGuid = CatalogItemRepository.GetCatalogPathFromGuid(rsservice, itemId);
			this.GetLibraryCatalogItem(rsservice, catalogPathFromGuid).ThrowIfNoAccess(ReportOperation.UpdateReportDefinition);
			if (dataModelRoleAssignments != null)
			{
				this.ValidateAndNormalizeUsers(dataModelRoleAssignments, userPrincipal);
				Dictionary<Guid, long> dictionary = await this.GetRoleIdMapping(itemId);
				Dictionary<Guid, long> roleIdMapping = dictionary;
				this.ValidateDataModelRoleAssignments(dataModelRoleAssignments, roleIdMapping);
				IList<DataModelRoleAssignmentEntity> existingDataModelRoleAssignmentEntities = await this._dataModelRoleAccessor.GetDataModelRoleAssignmentsByItemAsync(itemId);
				List<DataModelRoleAssignment> existingDataModelRoleAssignments = existingDataModelRoleAssignmentEntities.Select((DataModelRoleAssignmentEntity r) => r.ToDataModelRoleAssignment()).ToList<DataModelRoleAssignment>();
				this.ValidateAndNormalizeUsers(existingDataModelRoleAssignments, userPrincipal);
				using (List<DataModelRoleAssignment>.Enumerator enumerator = dataModelRoleAssignments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DataModelRoleAssignment dataRoleAssignment = enumerator.Current;
						DataModelRoleAssignment dataModelRoleAssignment = existingDataModelRoleAssignments.SingleOrDefault((DataModelRoleAssignment a) => a.GroupUserName.Equals(dataRoleAssignment.GroupUserName, StringComparison.CurrentCultureIgnoreCase));
						IList<Guid> list = ((dataModelRoleAssignment != null) ? dataModelRoleAssignment.DataModelRoles : new List<Guid>());
						IEnumerable<Guid> enumerable = dataRoleAssignment.DataModelRoles.Except(list);
						IEnumerable<Guid> rolesToDelete = list.Except(dataRoleAssignment.DataModelRoles);
						if (enumerable.Any<Guid>() || rolesToDelete.Any<Guid>())
						{
							Guid userId = this.GetOrCreateUserId(userPrincipal, dataRoleAssignment.GroupUserName);
							foreach (Guid guid in enumerable)
							{
								await this._dataModelRoleAccessor.AddDataModelRoleAssignmentAsync(userId, roleIdMapping[guid]);
							}
							IEnumerator<Guid> enumerator2 = null;
							foreach (Guid guid2 in rolesToDelete)
							{
								await this._dataModelRoleAccessor.DeleteDataModelRoleAssignmentAsync(userId, roleIdMapping[guid2]);
							}
							enumerator2 = null;
							userId = default(Guid);
						}
						rolesToDelete = null;
					}
				}
				List<DataModelRoleAssignment>.Enumerator enumerator = default(List<DataModelRoleAssignment>.Enumerator);
				IEnumerable<DataModelRoleAssignment> enumerable2 = existingDataModelRoleAssignments.Except(dataModelRoleAssignments, new CatalogItemRepository.DataModelRoleAssignmentUserComparer());
				if (enumerable2.Any<DataModelRoleAssignment>())
				{
					Dictionary<string, Guid> userNameToIdMapping = this.GetGroupUserNameToIdMapping(existingDataModelRoleAssignments, existingDataModelRoleAssignmentEntities);
					foreach (DataModelRoleAssignment dataModelRoleAssignment2 in enumerable2)
					{
						Guid userId = userNameToIdMapping[dataModelRoleAssignment2.GroupUserName];
						foreach (Guid guid3 in dataModelRoleAssignment2.DataModelRoles)
						{
							await this._dataModelRoleAccessor.DeleteDataModelRoleAssignmentAsync(userId, roleIdMapping[guid3]);
						}
						IEnumerator<Guid> enumerator2 = null;
						userId = default(Guid);
					}
					IEnumerator<DataModelRoleAssignment> enumerator3 = null;
					userNameToIdMapping = null;
				}
				roleIdMapping = null;
				existingDataModelRoleAssignmentEntities = null;
				existingDataModelRoleAssignments = null;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009568 File Offset: 0x00007768
		private void ValidateAndNormalizeUsers(IEnumerable<DataModelRoleAssignment> dataModelRoleAssignments, IPrincipal userPrincipal)
		{
			AuthenticationType authenticationType = userPrincipal.Identity.ToAuthenticationType();
			if (AuthenticationType.Windows == authenticationType)
			{
				foreach (DataModelRoleAssignment dataModelRoleAssignment in dataModelRoleAssignments)
				{
					try
					{
						byte[] sidFromUserName = this.GetSidFromUserName(dataModelRoleAssignment.GroupUserName);
						string principalNameFromSid = this.GetPrincipalNameFromSid(sidFromUserName);
						if (principalNameFromSid != null)
						{
							dataModelRoleAssignment.GroupUserName = principalNameFromSid;
						}
					}
					catch (UnknownUserNameException)
					{
						this._logger.Trace(TraceLevel.Info, string.Format("UnknownUserNameException when normalizing user names: {0}", dataModelRoleAssignment.GroupUserName));
					}
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009608 File Offset: 0x00007808
		private Dictionary<string, Guid> GetGroupUserNameToIdMapping(IEnumerable<DataModelRoleAssignment> dataModelRoleAssignments, IList<DataModelRoleAssignmentEntity> dataModelRoleAssignmentEntities)
		{
			Dictionary<string, Guid> dictionary = new Dictionary<string, Guid>();
			int num = 0;
			foreach (DataModelRoleAssignment dataModelRoleAssignment in dataModelRoleAssignments)
			{
				dictionary.Add(dataModelRoleAssignment.GroupUserName, dataModelRoleAssignmentEntities[num].UserId);
				num++;
			}
			return dictionary;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00009670 File Offset: 0x00007870
		private void ValidateDataModelRoleAssignments(List<DataModelRoleAssignment> dataModelRoleAssignments, Dictionary<Guid, long> roleIdMapping)
		{
			using (List<DataModelRoleAssignment>.Enumerator enumerator = dataModelRoleAssignments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DataModelRoleAssignment dataRoleAssignment = enumerator.Current;
					if (dataModelRoleAssignments.Count((DataModelRoleAssignment a) => a.GroupUserName.Equals(dataRoleAssignment.GroupUserName, StringComparison.OrdinalIgnoreCase)) > 1)
					{
						throw new InvalidPolicyDefinitionException(dataRoleAssignment.GroupUserName);
					}
					if (dataRoleAssignment.DataModelRoles == null || dataRoleAssignment.DataModelRoles.Count == 0)
					{
						throw new InvalidPolicyDefinitionException(dataRoleAssignment.GroupUserName);
					}
					using (IEnumerator<Guid> enumerator2 = dataRoleAssignment.DataModelRoles.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Guid roleId = enumerator2.Current;
							if (!roleIdMapping.ContainsKey(roleId))
							{
								throw new RoleNotFoundException(roleId.ToString());
							}
							if (dataRoleAssignment.DataModelRoles.Count((Guid r) => r.Equals(roleId)) > 1)
							{
								throw new InvalidPolicyDefinitionException(dataRoleAssignment.GroupUserName);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000097D0 File Offset: 0x000079D0
		private Guid GetOrCreateUserId(IPrincipal userPrincipal, string groupUserName)
		{
			AuthenticationType authenticationType = userPrincipal.Identity.ToAuthenticationType();
			return this.GetUserId(groupUserName, authenticationType);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000097F4 File Offset: 0x000079F4
		private async Task<Dictionary<Guid, long>> GetRoleIdMapping(Guid itemId)
		{
			return (await this._dataModelRoleAccessor.GetDataModelRolesByItemAsync(itemId)).ToDictionary((DataModelRoleEntity k) => k.ModelRoleId, (DataModelRoleEntity v) => v.DataModelRoleId);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00009841 File Offset: 0x00007A41
		public void DeleteFromExtendedContent(Guid id)
		{
			CatalogItemAccessor catalogItemAccessor = new CatalogItemAccessor();
			((ICatalogItemAccessor)catalogItemAccessor).DeleteExtendedContent(id, Microsoft.ReportingServices.CatalogAccess.ExtendedContentType.DataModel);
			((ICatalogItemAccessor)catalogItemAccessor).DeleteExtendedContent(id, Microsoft.ReportingServices.CatalogAccess.ExtendedContentType.PowerBIReportDefinition);
		}

		// Token: 0x0400005B RID: 91
		private const string BrandingLogoPath = "fbac82c8-9bad-4dba-929f-c04e7ca4111f/logo";

		// Token: 0x0400005C RID: 92
		private readonly ISoapRS2010Proxy _soapRS2010Proxy;

		// Token: 0x0400005D RID: 93
		private readonly ISystemService _systemService;

		// Token: 0x0400005E RID: 94
		private readonly ILogger _logger;

		// Token: 0x0400005F RID: 95
		private readonly ICatalogDataAccessor _catalogAccessor;

		// Token: 0x04000060 RID: 96
		private readonly ICatalogDataModelDataSourceAccessor _dataModelDataSourceAccessor;

		// Token: 0x04000061 RID: 97
		private readonly ICatalogDataModelRoleAccessor _dataModelRoleAccessor;

		// Token: 0x04000062 RID: 98
		private readonly ISubscriptionHistoryDataAccessor _subscriptionHistoryAccessor;

		// Token: 0x04000063 RID: 99
		private readonly IFileSizeRestrictions _fileSizeRestrictions;

		// Token: 0x04000064 RID: 100
		private readonly ICrypto _crypto;

		// Token: 0x04000065 RID: 101
		private readonly string[] _allowedUrlPrefixes = new string[] { "HTTP://", "HTTPS://", "MAILTO:" };

		// Token: 0x020000E4 RID: 228
		internal sealed class DataSourceModelComparer : EqualityComparer<global::Model.DataSource>
		{
			// Token: 0x0600074D RID: 1869 RVA: 0x0001CDCC File Offset: 0x0001AFCC
			public override bool Equals(global::Model.DataSource x, global::Model.DataSource y)
			{
				return y != null && x != null && string.Equals(this.ConvertConnectionStringToPath(x), this.ConvertConnectionStringToPath(y), StringComparison.InvariantCultureIgnoreCase) && x.DataModelDataSource != null && y.DataModelDataSource != null && x.DataModelDataSource.Kind == y.DataModelDataSource.Kind && x.DataModelDataSource.Type == y.DataModelDataSource.Type;
			}

			// Token: 0x0600074E RID: 1870 RVA: 0x0001CE37 File Offset: 0x0001B037
			public override int GetHashCode(global::Model.DataSource obj)
			{
				if (obj != null && obj.ConnectionString != null)
				{
					return this.ConvertConnectionStringToPath(obj).ToLower().GetHashCode();
				}
				return 0;
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x0001CE58 File Offset: 0x0001B058
			private string ConvertConnectionStringToPath(global::Model.DataSource x)
			{
				if (x != null)
				{
					DataModelDataSource dataModelDataSource = x.DataModelDataSource;
					if (((dataModelDataSource != null) ? new DataModelDataSourceType?(dataModelDataSource.Type) : null) == DataModelDataSourceType.DirectQuery && !string.IsNullOrEmpty((x != null) ? x.ConnectionString : null))
					{
						try
						{
							DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
							dbConnectionStringBuilder.ConnectionString = x.ConnectionString;
							List<string> list = new List<string>();
							if (dbConnectionStringBuilder.ContainsKey("Data Source"))
							{
								list.Add(dbConnectionStringBuilder["Data Source"].ToString());
								if (dbConnectionStringBuilder.ContainsKey("Initial Catalog"))
								{
									list.Add(dbConnectionStringBuilder["Initial Catalog"].ToString());
								}
							}
							return string.Join(";", list).ToLower();
						}
						catch
						{
						}
					}
				}
				if (x == null)
				{
					return null;
				}
				return x.ConnectionString;
			}
		}

		// Token: 0x020000E5 RID: 229
		internal sealed class DataModelRoleComparer : EqualityComparer<DataModelRole>
		{
			// Token: 0x06000751 RID: 1873 RVA: 0x0001CF58 File Offset: 0x0001B158
			public override bool Equals(DataModelRole x, DataModelRole y)
			{
				return x.ModelRoleId == y.ModelRoleId;
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x0001CF6C File Offset: 0x0001B16C
			public override int GetHashCode(DataModelRole obj)
			{
				return obj.ModelRoleId.GetHashCode();
			}
		}

		// Token: 0x020000E6 RID: 230
		internal sealed class DataModelRoleAssignmentUserComparer : EqualityComparer<DataModelRoleAssignment>
		{
			// Token: 0x06000754 RID: 1876 RVA: 0x0001CF95 File Offset: 0x0001B195
			public override bool Equals(DataModelRoleAssignment x, DataModelRoleAssignment y)
			{
				return x.GroupUserName.Equals(y.GroupUserName, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x0001CFA9 File Offset: 0x0001B1A9
			public override int GetHashCode(DataModelRoleAssignment obj)
			{
				return obj.GroupUserName.ToUpper().GetHashCode();
			}
		}
	}
}
