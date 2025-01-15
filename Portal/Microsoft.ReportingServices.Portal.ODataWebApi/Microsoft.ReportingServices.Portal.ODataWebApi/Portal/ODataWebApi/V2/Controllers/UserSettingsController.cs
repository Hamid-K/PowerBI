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
	// Token: 0x02000028 RID: 40
	public class UserSettingsController : EntitySetReflectionODataController<UserSettings>
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000079DF File Offset: 0x00005BDF
		protected ICatalogRepository CatalogRepository
		{
			get
			{
				return this._catalogRepository;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000079E7 File Offset: 0x00005BE7
		public UserSettingsController(ICatalogRepository catalogRepository, ILogger logger)
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

		// Token: 0x060001F3 RID: 499 RVA: 0x00007A1A File Offset: 0x00005C1A
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<UserSettings>("UserSettings");
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override IQueryable<UserSettings> GetEntitySet(string castName)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007A28 File Offset: 0x00005C28
		protected override UserSettings GetEntity(string key, string castName)
		{
			if (HostingState.Current.IsConfigSwitchEnabled(ConfigSwitches.EnableCommentAlerts, false))
			{
				try
				{
					return this._catalogRepository.GetUserSettings(new Guid(key));
				}
				catch (AccessDeniedException)
				{
					this._logger.Trace(TraceLevel.Info, string.Format("User {0} doesn't have permission to access user settings of requested user ({1})", base.User.Identity.Name, key));
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}
			}
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override bool AddEntity(UserSettings entity)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007AA8 File Offset: 0x00005CA8
		protected override bool PutEntity(string key, UserSettings entity)
		{
			if (HostingState.Current.IsConfigSwitchEnabled(ConfigSwitches.EnableCommentAlerts, false))
			{
				bool flag = false;
				try
				{
					if (entity.Id == Guid.Empty)
					{
						AuthenticationType authenticationType = base.User.Identity.ToAuthenticationType();
						entity.Id = this._catalogRepository.GetUserId(base.User.Identity.Name, authenticationType);
					}
					flag = this._catalogRepository.UpdateUserSettings(entity);
				}
				catch (AccessDeniedException)
				{
					this._logger.Trace(TraceLevel.Info, string.Format("User {0} doesn't have permission to edit user settings of requested user ({1})", base.User.Identity.Name, entity.Id));
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}
				return flag;
			}
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override bool PatchEntity(string key, UserSettings entity, string[] delta)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000038EE File Offset: 0x00001AEE
		protected override bool DeleteEntity(string key)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x04000075 RID: 117
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000076 RID: 118
		private readonly ILogger _logger;
	}
}
