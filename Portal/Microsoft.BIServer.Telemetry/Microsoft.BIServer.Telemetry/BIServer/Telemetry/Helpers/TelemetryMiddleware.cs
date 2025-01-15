using System;
using System.Threading.Tasks;
using Microsoft.BIServer.Telemetry.Interfaces;
using Microsoft.Owin;

namespace Microsoft.BIServer.Telemetry.Helpers
{
	// Token: 0x02000008 RID: 8
	public sealed class TelemetryMiddleware : OwinMiddleware
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000022C8 File Offset: 0x000004C8
		public TelemetryMiddleware(OwinMiddleware next, ITelemetryService telemetryService)
			: base(next)
		{
			this._telemetryService = telemetryService;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022D8 File Offset: 0x000004D8
		public override async Task Invoke(IOwinContext context)
		{
			if (context.Request.Path.Value.Contains("/api/") && this._telemetryService.IsEnabled())
			{
				await this._telemetryService.TrackOwinRequestAsync(base.Next, context);
			}
			else
			{
				await base.Next.Invoke(context);
			}
		}

		// Token: 0x0400002D RID: 45
		private readonly ITelemetryService _telemetryService;
	}
}
