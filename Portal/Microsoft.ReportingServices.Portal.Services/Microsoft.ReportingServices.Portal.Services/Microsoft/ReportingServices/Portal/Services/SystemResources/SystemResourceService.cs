using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x02000036 RID: 54
	internal sealed class SystemResourceService : ISystemResourceService
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000F359 File Offset: 0x0000D559
		internal SystemResourceService(ICatalogRepository catalogRepository, ILogger logger)
		{
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._catalogRepository = catalogRepository;
			this._logger = logger;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F38C File Offset: 0x0000D58C
		public bool DeleteSystemResource(IPrincipal userPrincipal, Guid key)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			ItemProperties itemProperties;
			using (new RSServiceStorageAccess(rsservice))
			{
				Guid guid;
				string text;
				rsservice.ResolveCatalogItem(key, null, out guid, out text);
				rsservice.Storage.CatalogGetAllProperties(new ExternalItemPath(text), out itemProperties);
			}
			return rsservice.SystemResourceManager.TryDelete(itemProperties.Name);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		public global::Model.SystemResource GetSystemResourceByTypeName(IPrincipal userPrincipal, string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException("typeName");
			}
			Microsoft.ReportingServices.Library.SystemResource systemResource = this.InternalGetSystemResourceByTypeName(userPrincipal, typeName);
			if (systemResource != null)
			{
				return SystemResourceService.ConvertToSystemResourceModel(systemResource);
			}
			return null;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000F42C File Offset: 0x0000D62C
		public IQueryable<global::Model.SystemResource> GetSystemResources(IPrincipal userPrincipal)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			IEnumerable<Microsoft.ReportingServices.Library.SystemResource> enumerable = rsservice.SystemResourceManager.LoadAll().Union(SystemResourceService._embeddedResources.LoadAll(), new SystemResourceTypeNameComparer());
			ISystemResourceManager[] array = new ISystemResourceManager[]
			{
				rsservice.SystemResourceManager,
				SystemResourceService._embeddedResources
			};
			foreach (Microsoft.ReportingServices.Library.SystemResource systemResource in enumerable)
			{
				SystemResourceService._processingManager.ProcessResource(systemResource, array);
			}
			return enumerable.Select((Microsoft.ReportingServices.Library.SystemResource x) => SystemResourceService.ConvertToSystemResourceModel(x)).AsQueryable<global::Model.SystemResource>();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000F4E4 File Offset: 0x0000D6E4
		public global::Model.SystemResource InstallSystemResourcePackage(IPrincipal userPrincipal, byte[] packageBytes, string packageFileName, string typeName)
		{
			this.CheckAccess(userPrincipal);
			if (packageBytes == null)
			{
				throw new ArgumentNullException("packageBytes");
			}
			if (packageFileName == null)
			{
				throw new ArgumentNullException("packageFileName");
			}
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (SystemResourceService.GetSystemResourceType(typeName) == SystemResourceType.MobileReportRuntime)
			{
				throw new Microsoft.ReportingServices.Portal.Interfaces.Exceptions.SystemResourcePackageException(SR.Error_SystemResourcePackageException);
			}
			Microsoft.ReportingServices.Library.SystemResource systemResource = null;
			Func<string, ISystemResourcePackageContentValidator> func = (string t) => SystemResourceService.GetSystemResourcePackageContentValidator(SystemResourceService.GetSystemResourceType(t));
			using (MemoryStream memoryStream = new MemoryStream(packageBytes))
			{
				try
				{
					using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read, false))
					{
						Func<Stream> func2 = delegate
						{
							ZipArchiveEntry entry = archive.GetEntry("metadata.xml");
							if (entry == null)
							{
								throw new SystemResourcePackageMetadataNotFoundException();
							}
							return entry.Open();
						};
						Dictionary<string, Func<Stream>> dictionary = (from x in archive.Entries
							where !x.Name.EndsWith("/")
							where x.FullName != "metadata.xml"
							select x).ToDictionary((ZipArchiveEntry x) => x.FullName, (ZipArchiveEntry x) => () => x.Open());
						RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
						Func<string, string> func3 = (string fn) => MimeMapping.GetMimeMapping(fn);
						systemResource = rsservice.SystemResourceManager.Install(func2, dictionary, packageBytes, packageFileName, typeName, func, func3);
					}
				}
				catch (InvalidDataException)
				{
					throw new Microsoft.ReportingServices.Portal.Interfaces.Exceptions.SystemResourcePackageException(SR.Error_SystemResourcePackageException);
				}
			}
			return SystemResourceService.ConvertToSystemResourceModel(systemResource);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000F6D8 File Offset: 0x0000D8D8
		public bool TryGetPayload(IPrincipal userPrincipal, string typeName, string itemName, out string contentType, out string filename, out byte[] bytes)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			bytes = null;
			contentType = null;
			filename = null;
			Microsoft.ReportingServices.Library.SystemResource systemResource = this.InternalGetSystemResourceByTypeName(userPrincipal, typeName);
			if (systemResource == null)
			{
				return false;
			}
			bool flag = string.IsNullOrEmpty(itemName);
			if (!flag)
			{
				if (!systemResource.Items.ContainsKey(itemName))
				{
					SystemResourceManager systemResourceManager = ServicesUtil.CreateRsService(userPrincipal).SystemResourceManager;
					ISystemResourceManager[] array = new ISystemResourceManager[]
					{
						systemResourceManager,
						SystemResourceService._embeddedResources
					};
					try
					{
						SystemResourceService._processingManager.ProcessResource(systemResource, array);
					}
					catch (SystemResourceProcessingException ex)
					{
						this._logger.Trace(TraceLevel.Error, ex.Message);
					}
				}
				if (SystemResourceService._processingManager.TryLoadItem(systemResource, itemName, out bytes, out contentType, out filename))
				{
					return true;
				}
			}
			if (systemResource is EmbeddedSystemResource)
			{
				if (!SystemResourceService._embeddedResources.TryLoadContentItem(typeName, itemName, out bytes))
				{
					return false;
				}
				if (flag)
				{
					contentType = "application/octet-stream";
					filename = (systemResource as EmbeddedSystemResource).PackageName;
				}
				else
				{
					contentType = (systemResource as EmbeddedSystemResource).Contents[itemName].ContentType;
					filename = itemName;
				}
			}
			else
			{
				Guid guid;
				if (flag)
				{
					guid = systemResource.PackageId;
				}
				else
				{
					if (!systemResource.Items.Keys.Contains(itemName.ToLowerInvariant()))
					{
						return false;
					}
					guid = Guid.Parse(systemResource.Items[itemName.ToLowerInvariant()]);
				}
				global::Model.CatalogItem catalogItemWithContent = this._catalogRepository.GetCatalogItemWithContent(userPrincipal, guid);
				if (catalogItemWithContent != null && catalogItemWithContent.Content == null)
				{
					return false;
				}
				bytes = catalogItemWithContent.Content;
				contentType = catalogItemWithContent.ContentType;
				filename = catalogItemWithContent.Name;
			}
			return true;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000F868 File Offset: 0x0000DA68
		private void CheckAccess(IPrincipal userPrincipal)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			rsService.ExecuteStorageAction(delegate
			{
				if (!rsService.SecMgr.CheckAccess(rsService.SecMgr.SystemSecDesc, CatalogOperation.UpdateSystemProperties, null))
				{
					throw new AccessDeniedException(userPrincipal.Identity.Name, ErrorCode.rsAccessDenied);
				}
			});
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000F8AC File Offset: 0x0000DAAC
		private Microsoft.ReportingServices.Library.SystemResource InternalGetSystemResourceByTypeName(IPrincipal userPrincipal, string typeName)
		{
			Microsoft.ReportingServices.Library.SystemResource systemResource;
			if (ServicesUtil.CreateRsService(userPrincipal).SystemResourceManager.TryLoadByTypeName(typeName, out systemResource) || SystemResourceService._embeddedResources.TryLoadByTypeName(typeName, out systemResource))
			{
				return systemResource;
			}
			return null;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		internal static global::Model.SystemResource ConvertToSystemResourceModel(Microsoft.ReportingServices.Library.SystemResource systemResource)
		{
			global::Model.SystemResource resource = new global::Model.SystemResource();
			resource.Name = systemResource.Name;
			resource.TypeName = systemResource.TypeName;
			resource.Type = SystemResourceService.GetSystemResourceType(systemResource.TypeName);
			resource.Id = systemResource.Id;
			resource.Version = systemResource.Version;
			resource.PackageId = systemResource.PackageId;
			resource.IsEmbedded = systemResource is EmbeddedSystemResource;
			resource.Items = systemResource.Items.Select((KeyValuePair<string, string> i) => new SystemResourceItem
			{
				TypeName = systemResource.TypeName,
				Key = i.Key,
				Id = Guid.Parse(i.Value),
				IsEmbedded = resource.IsEmbedded
			}).ToArray<SystemResourceItem>();
			return resource;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000F9D9 File Offset: 0x0000DBD9
		internal static ISystemResourcePackageContentValidator GetSystemResourcePackageContentValidator(SystemResourceType resourceType)
		{
			if (resourceType == SystemResourceType.Brand)
			{
				return new BrandContentValidator();
			}
			if (resourceType != SystemResourceType.UniversalBrand)
			{
				return null;
			}
			return new UniversalBrandContentValidator();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000F9F2 File Offset: 0x0000DBF2
		internal static SystemResourceType GetSystemResourceType(string typeName)
		{
			if (string.Equals(typeName, "Brand", StringComparison.OrdinalIgnoreCase))
			{
				return SystemResourceType.Brand;
			}
			if (string.Equals(typeName, "UniversalBrand", StringComparison.OrdinalIgnoreCase))
			{
				return SystemResourceType.UniversalBrand;
			}
			return SystemResourceType.Unknown;
		}

		// Token: 0x040000B1 RID: 177
		internal const string BrandSystemResourceName = "Brand";

		// Token: 0x040000B2 RID: 178
		internal const string UniveralBrandResourceName = "UniversalBrand";

		// Token: 0x040000B3 RID: 179
		private static readonly EmbeddedSystemResourceManager _embeddedResources = EmbeddedSystemResourceManager.Create();

		// Token: 0x040000B4 RID: 180
		private static readonly SystemResourceProcessingManager _processingManager = SystemResourceProcessingManager.GetInstance();

		// Token: 0x040000B5 RID: 181
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x040000B6 RID: 182
		private readonly ILogger _logger;
	}
}
