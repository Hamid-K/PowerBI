using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x0200001E RID: 30
	public class FavoriteItemsController : EntitySetReflectionODataController<FavoriteItem>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006B7C File Offset: 0x00004D7C
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006B84 File Offset: 0x00004D84
		public FavoriteItemsController(ICatalogRepository catalogRepository, ILogger logger)
			: base(logger)
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

		// Token: 0x06000184 RID: 388 RVA: 0x00006BB7 File Offset: 0x00004DB7
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<FavoriteItem>("FavoriteItems");
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006BC8 File Offset: 0x00004DC8
		protected override IQueryable<FavoriteItem> GetEntitySet(string castName)
		{
			IQueryable<FavoriteItem> queryable;
			try
			{
				List<FavoriteItem> list = new List<FavoriteItem>();
				foreach (CatalogItem catalogItem in this._catalogRepository.GetFavoriteItems(base.User))
				{
					list.Add(new FavoriteItem
					{
						Id = catalogItem.Id,
						Item = catalogItem
					});
				}
				queryable = list.AsQueryable<FavoriteItem>();
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return queryable;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override FavoriteItem GetEntity(string key, string castName)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006C64 File Offset: 0x00004E64
		protected override bool AddEntity(FavoriteItem entity)
		{
			bool flag = false;
			try
			{
				flag = this._catalogRepository.AddToFavorites(base.User, entity.Id);
			}
			catch (AccessDeniedException)
			{
				this._logger.Trace(TraceLevel.Info, string.Format("User {0} doesn't have permission to add to favorite on an item they don't have permissions to view ({1}).", base.User.Identity.Name, entity.Id));
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return flag;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override bool PutEntity(string key, FavoriteItem entity)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override bool PatchEntity(string key, FavoriteItem entity, string[] delta)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006CDC File Offset: 0x00004EDC
		protected override bool DeleteEntity(string Id)
		{
			Guid guid = Guid.Parse(Id);
			bool flag;
			try
			{
				flag = this._catalogRepository.RemoveFromFavorites(base.User, guid);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return flag;
		}

		// Token: 0x04000061 RID: 97
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000062 RID: 98
		private readonly ILogger _logger;
	}
}
