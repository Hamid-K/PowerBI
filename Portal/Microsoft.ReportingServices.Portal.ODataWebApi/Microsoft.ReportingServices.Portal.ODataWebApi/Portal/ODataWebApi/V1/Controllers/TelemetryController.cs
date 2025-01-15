using System;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers
{
	// Token: 0x0200002B RID: 43
	public class TelemetryController : SingletonReflectionODataController<Telemetry>
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008788 File Offset: 0x00006988
		protected ITelemetryService TelemetryService
		{
			get
			{
				return this._telemetryService;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008790 File Offset: 0x00006990
		public TelemetryController(ILogger logger, ITelemetryService telemetryService)
			: base(logger)
		{
			if (telemetryService == null)
			{
				throw new ArgumentNullException("telemetryService");
			}
			this._telemetryService = telemetryService;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000087B0 File Offset: 0x000069B0
		protected override Telemetry GetSingleton()
		{
			if (base.User != null && base.User.Identity != null)
			{
				return new Telemetry
				{
					Properties = this._telemetryService.GetSystemProperties(base.User.Identity)
				};
			}
			throw new Exception("No Identity found");
		}

		// Token: 0x0400007C RID: 124
		private readonly ITelemetryService _telemetryService;
	}
}
