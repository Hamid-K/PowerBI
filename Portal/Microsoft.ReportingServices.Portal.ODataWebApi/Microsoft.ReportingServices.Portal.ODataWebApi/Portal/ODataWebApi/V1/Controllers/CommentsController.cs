using System;
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

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x0200002A RID: 42
	public class CommentsController : EntitySetReflectionODataController<Comment>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008566 File Offset: 0x00006766
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000856E File Offset: 0x0000676E
		public CommentsController(ICatalogRepository catalogRepository, ILogger logger)
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

		// Token: 0x06000208 RID: 520 RVA: 0x000085A1 File Offset: 0x000067A1
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<Comment>("Comments");
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override IQueryable<Comment> GetEntitySet(string castName)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000085B0 File Offset: 0x000067B0
		protected override Comment GetEntity(string key, string castName)
		{
			Comment comment;
			try
			{
				long num = long.Parse(key);
				comment = this._catalogRepository.GetComment(base.User, num);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return comment;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000085F8 File Offset: 0x000067F8
		protected override bool AddEntity(Comment entity)
		{
			Comment comment;
			return this.AddEntity(entity, out comment);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008610 File Offset: 0x00006810
		protected override bool AddEntity(Comment entity, out Comment createdEntity)
		{
			bool flag;
			try
			{
				flag = this._catalogRepository.CreateComment(base.User, entity, out createdEntity);
			}
			catch (AccessDeniedException)
			{
				this._logger.Trace(TraceLevel.Info, string.Format("User {0} doesn't have permission to write comments on an item they don't have permissions to view ({1}).", base.User.Identity.Name, entity.ItemId));
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			catch (ItemNotFoundException)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			catch (CatalogItemContentInvalidException)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
			return flag;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000086B4 File Offset: 0x000068B4
		protected override bool PutEntity(string key, Comment entity)
		{
			entity.Id = long.Parse(key);
			if (!this._catalogRepository.IsUserContextOwner(base.User, entity.Id))
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			bool flag;
			try
			{
				flag = this._catalogRepository.UpdateComment(base.User, entity, null);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return flag;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override bool PatchEntity(string key, Comment entity, string[] delta)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008728 File Offset: 0x00006928
		protected override bool DeleteEntity(string key)
		{
			long num = long.Parse(key);
			bool flag = this._catalogRepository.IsUserContextOwner(base.User, num);
			bool flag2;
			try
			{
				flag2 = this._catalogRepository.DeleteComment(base.User, num, !flag);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return flag2;
		}

		// Token: 0x0400007A RID: 122
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x0400007B RID: 123
		private readonly ILogger _logger;
	}
}
