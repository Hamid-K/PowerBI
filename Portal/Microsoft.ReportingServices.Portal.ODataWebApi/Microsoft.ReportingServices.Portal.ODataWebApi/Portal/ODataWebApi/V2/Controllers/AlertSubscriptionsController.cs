using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x0200000E RID: 14
	public class AlertSubscriptionsController : EntitySetReflectionODataController<AlertSubscription>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000038A5 File Offset: 0x00001AA5
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000038AD File Offset: 0x00001AAD
		public AlertSubscriptionsController(ICatalogRepository catalogRepository, ILogger logger)
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

		// Token: 0x06000034 RID: 52 RVA: 0x000038E0 File Offset: 0x00001AE0
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<AlertSubscription>("AlertSubscriptions");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override IQueryable<AlertSubscription> GetEntitySet(string castName)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override AlertSubscription GetEntity(string key, string castName)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000038FC File Offset: 0x00001AFC
		[HttpPost]
		protected override bool AddEntity(AlertSubscription entity)
		{
			AlertSubscription alertSubscription;
			return this.AddEntity(entity, out alertSubscription);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003914 File Offset: 0x00001B14
		protected override bool AddEntity(AlertSubscription entity, out AlertSubscription createdEntity)
		{
			if (HostingState.Current.IsConfigSwitchEnabled(ConfigSwitches.EnableCommentAlerts, false))
			{
				bool flag = false;
				try
				{
					CatalogItemType catalogItemTypeByGuid = this._catalogRepository.GetCatalogItemTypeByGuid(base.User, entity.ItemId);
					if (catalogItemTypeByGuid == CatalogItemType.Unknown || catalogItemTypeByGuid == CatalogItemType.Folder || catalogItemTypeByGuid == CatalogItemType.DataSource || catalogItemTypeByGuid == CatalogItemType.DataSet || catalogItemTypeByGuid == CatalogItemType.Component || catalogItemTypeByGuid == CatalogItemType.Resource || catalogItemTypeByGuid == CatalogItemType.Kpi)
					{
						throw new HttpResponseException(HttpStatusCode.BadRequest);
					}
					AuthenticationType authenticationType = base.User.Identity.ToAuthenticationType();
					Guid userId = this._catalogRepository.GetUserId(base.User.Identity.Name, authenticationType);
					this._catalogRepository.AddEmailAlertSubscription(userId, entity.ItemId, entity.AlertType, out createdEntity);
					flag = true;
				}
				catch (AccessDeniedException)
				{
					this._logger.Trace(TraceLevel.Info, string.Format("User {0} doesn't have permission to add an alert subscription", base.User.Identity.Name));
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}
				return flag;
			}
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override bool PutEntity(string key, AlertSubscription entity)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override bool PatchEntity(string key, AlertSubscription entity, string[] delta)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003A0C File Offset: 0x00001C0C
		protected override bool DeleteEntity(string Id)
		{
			if (HostingState.Current.IsConfigSwitchEnabled(ConfigSwitches.EnableCommentAlerts, false))
			{
				try
				{
					long num = long.Parse(Id);
					return this._catalogRepository.DeleteEmailAlertSubscription(num);
				}
				catch (AccessDeniedException)
				{
					this._logger.Trace(TraceLevel.Info, string.Format("User {0} doesn't have permission to change alert subscriptions", base.User.Identity.Name));
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}
			}
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x0400003E RID: 62
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x0400003F RID: 63
		private readonly ILogger _logger;
	}
}
