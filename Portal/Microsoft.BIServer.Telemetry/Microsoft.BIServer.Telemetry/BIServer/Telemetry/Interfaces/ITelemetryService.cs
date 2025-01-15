using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Microsoft.BIServer.Telemetry.Interfaces
{
	// Token: 0x02000005 RID: 5
	public interface ITelemetryService
	{
		// Token: 0x06000013 RID: 19
		bool IsEnabled();

		// Token: 0x06000014 RID: 20
		void FlushRequests();

		// Token: 0x06000015 RID: 21
		Task TrackOwinRequestAsync(OwinMiddleware next, IOwinContext context);

		// Token: 0x06000016 RID: 22
		void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

		// Token: 0x06000017 RID: 23
		void TrackTrace(string trace);
	}
}
