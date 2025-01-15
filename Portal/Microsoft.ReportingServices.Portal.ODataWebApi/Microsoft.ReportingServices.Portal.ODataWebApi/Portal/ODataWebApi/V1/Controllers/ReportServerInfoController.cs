using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x02000032 RID: 50
	public class ReportServerInfoController : SingletonReflectionODataController<ReportServerInfo>
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A370 File Offset: 0x00008570
		protected ISystemService SystemService
		{
			get
			{
				return this._systemService;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A378 File Offset: 0x00008578
		protected IPortalConfigurationManager PortalConfigurationManager
		{
			get
			{
				return this._portalConfigurationManager;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A380 File Offset: 0x00008580
		public ReportServerInfoController(ILogger logger, ISystemService systemService, IPortalConfigurationManager portalConfigurationManager)
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

		// Token: 0x06000277 RID: 631 RVA: 0x0000A3D3 File Offset: 0x000085D3
		protected override ReportServerInfo GetSingleton()
		{
			return new ReportServerInfo
			{
				ReportServerUrl = this._portalConfigurationManager.Current.ReportServerUrl
			};
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A3F0 File Offset: 0x000085F0
		[HttpGet]
		public virtual IHttpActionResult DeliveryExtensions()
		{
			Extension[] array = this.ListExtensions(ExtensionType.Delivery);
			return base.CreateOk(array);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A40C File Offset: 0x0000860C
		[HttpGet]
		public virtual IHttpActionResult DeliveryUIExtensions()
		{
			Extension[] array = this.ListExtensions(ExtensionType.DeliveryUI);
			return base.CreateOk(array);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A428 File Offset: 0x00008628
		[HttpGet]
		public virtual IHttpActionResult RenderingExtensions()
		{
			Extension[] array = this.ListExtensions(ExtensionType.Render);
			return base.CreateOk(array);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A444 File Offset: 0x00008644
		[HttpGet]
		public virtual IHttpActionResult DataExtensions()
		{
			Extension[] array = this.ListExtensions(ExtensionType.Data);
			return base.CreateOk(array);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A460 File Offset: 0x00008660
		[HttpGet]
		public virtual IHttpActionResult ExtensionParameters(string extensionName)
		{
			ExtensionParameter[] array = this._systemService.ListExtensionParameters(base.User, extensionName);
			return base.CreateOk(array);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A488 File Offset: 0x00008688
		[HttpPost]
		public virtual IHttpActionResult ValidateExtensionSettings(ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			if (actionParameters == null)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			if (!actionParameters.ContainsKey("ParameterValues") || !actionParameters.ContainsKey("ExtensionName"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			string text = (string)actionParameters["ExtensionName"];
			IEnumerable<ParameterValue> enumerable = (IEnumerable<ParameterValue>)actionParameters["ParameterValues"];
			ExtensionParameter[] array = this._systemService.ValidateExtensionSettings(base.User, text, enumerable);
			return base.CreateOk(array);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A524 File Offset: 0x00008724
		[HttpGet]
		public virtual IHttpActionResult Policies()
		{
			Policy[] array = this._systemService.GetSystemPolicies(base.User).ToArray<Policy>();
			return base.CreateOk(array);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A550 File Offset: 0x00008750
		[HttpPost]
		public virtual IHttpActionResult SetSystemPolicies(ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("Policy"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			ItemPolicy itemPolicy = (ItemPolicy)actionParameters["Policy"];
			this._systemService.SetSystemPolicies(base.RequestContext.Principal, itemPolicy.Policies);
			return base.Ok();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A5B8 File Offset: 0x000087B8
		[HttpGet]
		public virtual IHttpActionResult Roles()
		{
			Role[] array = this._systemService.GetSystemRoles(base.User).ToArray<Role>();
			return base.CreateOk(array);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A5E4 File Offset: 0x000087E4
		[HttpGet]
		public virtual IHttpActionResult RestrictedSettings()
		{
			string[] array = new string[] { "SiteName", "SystemReportTimeout", "SystemSnapshotLimit", "OfficeOnlineDiscoveryUrl", "ExcelWopiClientUrl" };
			List<Property> list = this._systemService.GetSystemProperties(base.User, array).ToList<Property>();
			Property property = list.FirstOrDefault((Property s) => s.Name == "SiteName");
			if (property != null && string.Equals(property.Value, "Default", StringComparison.InvariantCultureIgnoreCase))
			{
				property.Value = this._systemService.GetDefaultServerProductName();
			}
			return base.CreateOk(list);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A68C File Offset: 0x0000888C
		[HttpGet]
		public virtual IHttpActionResult Settings()
		{
			string[] array = new string[] { "MaxFileSizeMb" };
			IQueryable<Property> systemProperties = this._systemService.GetSystemProperties(base.User, array);
			return base.CreateOk(systemProperties);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A6C4 File Offset: 0x000088C4
		[HttpGet]
		public virtual IHttpActionResult ServerProductInfo()
		{
			Property[] array = new Property[]
			{
				new Property
				{
					Name = "ServerProductName",
					Value = this._systemService.GetServerProductName()
				},
				new Property
				{
					Name = "ServerProductVersion",
					Value = this._systemService.GetServerProductVersion()
				}
			};
			return base.CreateOk(array);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A728 File Offset: 0x00008928
		[HttpGet]
		public virtual IHttpActionResult SiteName()
		{
			string[] array = new string[] { "SiteName" };
			string text = this._systemService.GetSystemProperties(base.User, array).First<Property>().Value;
			if (text != null && string.Equals(text, "Default", StringComparison.InvariantCultureIgnoreCase))
			{
				text = this._systemService.GetDefaultServerProductName();
			}
			return base.CreateOk(text);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A788 File Offset: 0x00008988
		[HttpPost]
		public virtual async Task<IHttpActionResult> UpdateSettings(ODataActionParameters actionParameters)
		{
			IHttpActionResult httpActionResult;
			if (!base.ModelState.IsValid || !actionParameters.ContainsKey("PropertyValues"))
			{
				httpActionResult = base.BadRequest(base.GetModelStateValidationErrors());
			}
			else
			{
				IEnumerable<Property> enumerable = (IEnumerable<Property>)actionParameters["PropertyValues"];
				enumerable = await this._systemService.ValidateWopiUrlProperty(enumerable, "OfficeOnlineDiscoveryUrl", "ExcelWopiClientUrl");
				bool flag = this._systemService.UpdateSystemProperties(base.User, enumerable);
				httpActionResult = base.CreateOk(flag);
			}
			return httpActionResult;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A7D5 File Offset: 0x000089D5
		[HttpGet]
		public virtual IHttpActionResult GetWebAppUrl()
		{
			return this.Ok<string>(this._portalConfigurationManager.Current.ReportServerWebAppVirtualDirectory);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A7ED File Offset: 0x000089ED
		[HttpGet]
		public virtual IHttpActionResult GetVirtualDirectory()
		{
			return this.Ok<string>(this._portalConfigurationManager.Current.ReportServerVirtualDirectory);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A805 File Offset: 0x00008A05
		private Extension[] ListExtensions(ExtensionType extensionType)
		{
			return this._systemService.ListExtensions(base.User, extensionType);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A81C File Offset: 0x00008A1C
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.Singleton<ReportServerInfo>("ReportServerInfo");
			builder.EntityType<ReportServerInfo>().Function("DeliveryExtensions").ReturnsCollection<Extension>();
			builder.EntityType<ReportServerInfo>().Function("DeliveryUIExtensions").ReturnsCollection<Extension>();
			builder.EntityType<ReportServerInfo>().Function("DataExtensions").ReturnsCollection<Extension>();
			builder.EntityType<ReportServerInfo>().Function("RenderingExtensions").ReturnsCollection<Extension>();
			builder.EntityType<ReportServerInfo>().Function("ExtensionParameters").ReturnsCollection<ExtensionParameter>()
				.Parameter<string>("ExtensionName");
			ActionConfiguration actionConfiguration = builder.EntityType<ReportServerInfo>().Action("ValidateExtensionSettings").ReturnsCollection<ExtensionParameter>();
			actionConfiguration.CollectionParameter<ParameterValue>("ParameterValues");
			actionConfiguration.Parameter<string>("ExtensionName");
			builder.EntityType<ReportServerInfo>().Function("Policies").ReturnsCollection<Policy>();
			builder.EntityType<ReportServerInfo>().Function("Roles").ReturnsCollection<Role>();
			builder.EntityType<ReportServerInfo>().Function("RestrictedSettings").ReturnsCollection<Property>();
			builder.EntityType<ReportServerInfo>().Function("Settings").ReturnsCollection<Property>();
			builder.EntityType<ReportServerInfo>().Function("ServerProductInfo").ReturnsCollection<Property>();
			builder.EntityType<ReportServerInfo>().Function("SiteName").Returns<string>();
			builder.EntityType<ReportServerInfo>().Action("UpdateSettings").Returns<bool>()
				.CollectionParameter<Property>("PropertyValues");
			builder.EntityType<ReportServerInfo>().Action("SetSystemPolicies").Returns<bool>()
				.Parameter<ItemPolicy>("Policy");
			builder.EntityType<ReportServerInfo>().Function("GetWebAppUrl").Returns<string>();
			builder.EntityType<ReportServerInfo>().Function("GetVirtualDirectory").Returns<string>();
		}

		// Token: 0x0400008D RID: 141
		private readonly ISystemService _systemService;

		// Token: 0x0400008E RID: 142
		private readonly IPortalConfigurationManager _portalConfigurationManager;

		// Token: 0x0400008F RID: 143
		private readonly ILogger _logger;
	}
}
