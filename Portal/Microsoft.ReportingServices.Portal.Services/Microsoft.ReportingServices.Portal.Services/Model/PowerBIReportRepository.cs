using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web.Http;
using Microsoft.ReportingServices.CatalogAccess;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Services;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;

namespace Model
{
	// Token: 0x0200000C RID: 12
	internal class PowerBIReportRepository : PowerBIReport
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000266B File Offset: 0x0000086B
		public PowerBIReportRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository, ISystemService systemService)
			: this(userPrincipal, catalogRepository, new CatalogDataAccessor(), new CatalogDataModelDataSourceAccessor(), systemService)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002680 File Offset: 0x00000880
		public PowerBIReportRepository(IPrincipal userPrincipal, ICatalogRepository catalogRepository, ICatalogDataAccessor dataAccessor, ICatalogDataModelDataSourceAccessor dataModelDataSourceAccessor, ISystemService systemService)
		{
			if (userPrincipal == null)
			{
				throw new ArgumentNullException("userPrincipal");
			}
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			this._userPrincipal = userPrincipal;
			this._catalogRepository = catalogRepository;
			this._catalogAccessor = dataAccessor;
			this._catalogDataModelDataSourceAccessor = dataModelDataSourceAccessor;
			this._systemService = systemService;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026E4 File Offset: 0x000008E4
		protected override IList<Comment> LoadComments()
		{
			if (!this._systemService.IsBiServer())
			{
				throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
			}
			IList<Comment> commentsByItem;
			try
			{
				commentsByItem = this._catalogRepository.GetCommentsByItem(this._userPrincipal, base.Id);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return commentsByItem;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002744 File Offset: 0x00000944
		protected override IList<DataSource> LoadDataSources()
		{
			if (!this._systemService.IsBiServer())
			{
				throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
			}
			this.ThrowIfNoAccess(CommonOperation.ReadDatasource);
			return this._catalogDataModelDataSourceAccessor.GetDataModelDataSourcesByItemAsync(base.Id).Result.Select((DataModelDataSourceEntity d) => d.ToDataSourceWithoutSecret()).ToList<DataSource>();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027B0 File Offset: 0x000009B0
		protected override IList<Property> LoadProperties()
		{
			return this._catalogRepository.GetItemProperties(this._userPrincipal, base.Id, new Property[]
			{
				new Property
				{
					Name = "IsMobileOptimized"
				},
				new Property
				{
					Name = "HasEmbeddedModels"
				},
				new Property
				{
					Name = "PbixShredderVersion"
				},
				new Property
				{
					Name = "ModelRefreshAllowed"
				},
				new Property
				{
					Name = "HasDirectQuery"
				},
				new Property
				{
					Name = "ModelVersion"
				}
			}).ToList<Property>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002851 File Offset: 0x00000A51
		protected override IList<CacheRefreshPlan> LoadCacheRefreshPlans()
		{
			if (!this._systemService.IsBiServer())
			{
				throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
			}
			return this._catalogRepository.GetCacheRefreshPlansForPowerBIReport(this._userPrincipal, base.Path);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002884 File Offset: 0x00000A84
		internal virtual void ThrowIfNoAccess(CommonOperation operation)
		{
			RSService rsService = ServicesUtil.CreateRsService(this._userPrincipal);
			CatalogItem catalogItem = null;
			rsService.ExecuteStorageAction(delegate
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(rsService);
				if (!catalogItemContext.SetPath(this.Path))
				{
					throw new InvalidItemPathException(this.Path);
				}
				catalogItem = rsService.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			}, ConnectionTransactionType.AutoCommit);
			catalogItem.ThrowIfNoAccess(CommonOperation.ReadDatasource);
		}

		// Token: 0x0400003C RID: 60
		private readonly IPrincipal _userPrincipal;

		// Token: 0x0400003D RID: 61
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x0400003E RID: 62
		private readonly ICatalogDataAccessor _catalogAccessor;

		// Token: 0x0400003F RID: 63
		private readonly ICatalogDataModelDataSourceAccessor _catalogDataModelDataSourceAccessor;

		// Token: 0x04000040 RID: 64
		private readonly ISystemService _systemService;
	}
}
