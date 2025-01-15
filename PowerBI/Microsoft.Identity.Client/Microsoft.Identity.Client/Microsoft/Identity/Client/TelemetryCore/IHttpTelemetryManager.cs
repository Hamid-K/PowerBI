using System;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.TelemetryCore
{
	// Token: 0x020001DF RID: 479
	internal interface IHttpTelemetryManager
	{
		// Token: 0x060014A1 RID: 5281
		void RecordStoppedEvent(ApiEvent apiEvent);

		// Token: 0x060014A2 RID: 5282
		string GetCurrentRequestHeader(ApiEvent currentApiEvent);

		// Token: 0x060014A3 RID: 5283
		string GetLastRequestHeader();

		// Token: 0x060014A4 RID: 5284
		void ResetPreviousUnsentData();
	}
}
