using System;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Portal.Interfaces.Services;
using Microsoft.ReportingServices.Portal.ODataWebApi.V1.Controllers;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000026 RID: 38
	public class TelemetryController : TelemetryController
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000077C1 File Offset: 0x000059C1
		public TelemetryController(ILogger logger, ITelemetryService telemetryService)
			: base(logger, telemetryService)
		{
			this._telemetryService = base.TelemetryService;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000077D7 File Offset: 0x000059D7
		protected override Telemetry GetSingleton()
		{
			return base.GetSingleton();
		}

		// Token: 0x0400006F RID: 111
		private readonly ITelemetryService _telemetryService;
	}
}
