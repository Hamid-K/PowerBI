using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;
using Newtonsoft.Json;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000022 RID: 34
	public class SystemController : SingletonReflectionODataController<global::Model.System>
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000735C File Offset: 0x0000555C
		public SystemController(ILogger logger, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager)
			: base(logger)
		{
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			if (portalConfigurationManager == null)
			{
				throw new ArgumentNullException("portalConfigurationManager");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._systemService = systemService;
			this._portalConfigurationManager = portalConfigurationManager;
			this._logger = logger;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000073B0 File Offset: 0x000055B0
		protected override global::Model.System GetSingleton()
		{
			return new global::Model.System
			{
				ReportServerAbsoluteUrl = this._portalConfigurationManager.Current.ReportServerUrl,
				ReportServerRelativeUrl = this._portalConfigurationManager.Current.ReportServerVirtualDirectory,
				WebPortalRelativeUrl = this._portalConfigurationManager.Current.ReportServerWebAppVirtualDirectory,
				ProductName = this._systemService.GetServerProductName(),
				ProductVersion = this._systemService.GetServerProductVersion(),
				TimeZone = this._systemService.GetTimeZoneFullName(),
				ProductType = (this._systemService.IsBiServer() ? ProductType.PowerBiReportServer : ProductType.SqlServerReportingServices)
			};
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007450 File Offset: 0x00005650
		[HttpGet]
		[ODataRoute("System/AllowedActions")]
		public IHttpActionResult GetAllowedActions()
		{
			List<AllowedAction> list = (from action in this._systemService.GetAllowedSystemActions(base.User)
				select new AllowedAction
				{
					Action = action
				}).ToList<AllowedAction>();
			return base.CreateOk(list);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000749F File Offset: 0x0000569F
		[HttpPost]
		[HttpDelete]
		[HttpPatch]
		[HttpPut]
		[ODataRoute("System/AllowedActions")]
		public IHttpActionResult SetAllowedActions()
		{
			return base.NotAllowed();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000074A8 File Offset: 0x000056A8
		[HttpGet]
		[ODataRoute("System/Policies")]
		public IHttpActionResult GetSystemPolicies()
		{
			Policy[] array = this._systemService.GetSystemPolicies(base.User).ToArray<Policy>();
			return base.CreateOk(array);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000074D3 File Offset: 0x000056D3
		[HttpPut]
		[ODataRoute("System/Policies")]
		public IHttpActionResult PutSystemPolicies([FromBody] SystemPolicy policy)
		{
			this._systemService.SetSystemPolicies(base.RequestContext.Principal, policy.Policies);
			return base.Ok();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000749F File Offset: 0x0000569F
		[HttpPatch]
		[ODataRoute("System/Policies")]
		public IHttpActionResult PatchSystemPolicies()
		{
			return base.NotAllowed();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000749F File Offset: 0x0000569F
		[HttpPost]
		[ODataRoute("System/Policies")]
		public IHttpActionResult PostSystemPolicies()
		{
			return base.NotAllowed();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000749F File Offset: 0x0000569F
		[HttpDelete]
		[ODataRoute("System/Policies")]
		public IHttpActionResult DeleteSystemPolicies()
		{
			return base.NotAllowed();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000074F8 File Offset: 0x000056F8
		[HttpGet]
		[ODataRoute("System/Roles")]
		public IHttpActionResult Roles()
		{
			Role[] array = this._systemService.GetSystemRoles(base.User).ToArray<Role>();
			return base.CreateOk(array);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000749F File Offset: 0x0000569F
		[HttpPost]
		[HttpPut]
		[HttpDelete]
		[HttpPatch]
		[ODataRoute("System/Roles")]
		public IHttpActionResult PostRoles()
		{
			return base.NotAllowed();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000749F File Offset: 0x0000569F
		[HttpPut]
		[ODataRoute("System/Properties")]
		public IHttpActionResult PutSystemProperties()
		{
			return base.NotAllowed();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000749F File Offset: 0x0000569F
		[HttpPost]
		[ODataRoute("System/Properties")]
		public IHttpActionResult PostSystemProperties()
		{
			return base.NotAllowed();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007524 File Offset: 0x00005724
		[HttpGet]
		[ODataRoute("System/Properties")]
		public IHttpActionResult GetSystemProperties([FromUri] string properties = null)
		{
			if (properties == null)
			{
				return base.BadRequest();
			}
			string[] array = properties.Split(new char[] { ',' });
			List<Property> list = this._systemService.GetSystemProperties(base.User, array).ToList<Property>();
			Property property = list.FirstOrDefault((Property p) => string.Equals(p.Name, "SiteName", StringComparison.InvariantCultureIgnoreCase));
			if (property != null)
			{
				string value = property.Value;
				if (value != null && string.Equals(value, "Default", StringComparison.InvariantCultureIgnoreCase))
				{
					property.Value = this._systemService.GetDefaultServerProductName();
				}
			}
			return base.CreateOk(list);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000075C0 File Offset: 0x000057C0
		[HttpPatch]
		[ODataRoute("System/Properties")]
		public async Task<IHttpActionResult> PatchSystemProperties()
		{
			string result = base.Request.Content.ReadAsStringAsync().Result;
			List<Property> properties = new List<Property>();
			try
			{
				JsonConvert.PopulateObject(result, properties);
			}
			catch (Exception ex)
			{
				base.Logger.Trace(TraceLevel.Verbose, string.Format("Invalid payload: {0}", ex.Message));
				return base.BadRequest();
			}
			IEnumerable<Property> enumerable = await this._systemService.ValidateWopiUrlProperty(properties, "OfficeOnlineDiscoveryUrl", "ExcelWopiClientUrl");
			this._systemService.ValidatePowerBIMigrateUrl(properties, "PowerBIMigrateUrl");
			this._systemService.UpdateSystemProperties(base.User, enumerable);
			return base.Ok();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007605 File Offset: 0x00005805
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.Singleton<global::Model.System>("System");
			builder.EntityType<SystemPolicy>();
		}

		// Token: 0x04000067 RID: 103
		private readonly ISystemService _systemService;

		// Token: 0x04000068 RID: 104
		private readonly IPortalConfigurationManager _portalConfigurationManager;

		// Token: 0x04000069 RID: 105
		private readonly ILogger _logger;
	}
}
