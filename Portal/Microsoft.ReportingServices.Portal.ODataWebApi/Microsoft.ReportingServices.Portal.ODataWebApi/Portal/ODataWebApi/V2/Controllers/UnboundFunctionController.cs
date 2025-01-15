using System;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000027 RID: 39
	public class UnboundFunctionController : UnboundFunctionController
	{
		// Token: 0x060001EC RID: 492 RVA: 0x000077DF File Offset: 0x000059DF
		public UnboundFunctionController(ICatalogRepository catalogRepository, ISystemService systemService, ISystemResourceService systemResourceService, ILogger logger, IPortalConfigurationManager portalConfigurationManager)
			: base(catalogRepository, systemService, systemResourceService)
		{
			this._catalogRepository = base.CatalogRepository;
			this._systemService = base.SystemService;
			this._systemResourceService = base.SystemResourceService;
			this._logger = logger;
			this._portalConfigurationManager = portalConfigurationManager;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007820 File Offset: 0x00005A20
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.Function("ServiceState").Returns<ServiceState>();
			builder.Function("AllowedActions").ReturnsCollection<string>().Parameter<string>("path");
			FunctionConfiguration functionConfiguration = builder.Function("SafeGetSystemResourceContent").Returns<byte[]>();
			functionConfiguration.Parameter<string>("type");
			functionConfiguration.Parameter<string>("key");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007880 File Offset: 0x00005A80
		[HttpGet]
		[EnableQuery]
		public override IHttpActionResult AllowedActions(string path)
		{
			return base.AllowedActions(path);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000788C File Offset: 0x00005A8C
		[HttpGet]
		public override IHttpActionResult ServiceState()
		{
			ServiceState serviceState = new ServiceState
			{
				IsAvailable = true,
				RestrictedFeatures = this._systemService.GetRestrictedFeatures(),
				AllowedSystemActions = this._systemService.GetAllowedSystemActions(base.User),
				TimeZone = UnboundFunctionController.GetTimeZoneFullName(),
				UserHasFavorites = this._catalogRepository.GetFavoriteItems(base.User).Any<CatalogItem>(),
				AcceptLanguage = base.GetAcceptLanguage(),
				RequireIntune = this._systemService.GetSystemProperties(base.User, new string[] { "RequireIntune" }).Any((Property prop) => prop.Name == "RequireIntune" && bool.Parse(prop.Value)),
				ProductType = (this._systemService.IsBiServer() ? ProductType.PowerBiReportServer : ProductType.SqlServerReportingServices)
			};
			return this.Ok<ServiceState>(serviceState);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000079D5 File Offset: 0x00005BD5
		[HttpGet]
		public override IHttpActionResult SafeGetSystemResourceContent(string type, string key)
		{
			return base.SafeGetSystemResourceContent(type, key);
		}

		// Token: 0x04000070 RID: 112
		private readonly ICatalogRepository _catalogRepository;

		// Token: 0x04000071 RID: 113
		private readonly ISystemService _systemService;

		// Token: 0x04000072 RID: 114
		private readonly ISystemResourceService _systemResourceService;

		// Token: 0x04000073 RID: 115
		private readonly ILogger _logger;

		// Token: 0x04000074 RID: 116
		private readonly IPortalConfigurationManager _portalConfigurationManager;
	}
}
