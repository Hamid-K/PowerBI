using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client.TelemetryCore
{
	// Token: 0x020001E0 RID: 480
	internal interface ITelemetryReceiver
	{
		// Token: 0x060014A5 RID: 5285
		void HandleTelemetryEvents(List<Dictionary<string, string>> events);
	}
}
