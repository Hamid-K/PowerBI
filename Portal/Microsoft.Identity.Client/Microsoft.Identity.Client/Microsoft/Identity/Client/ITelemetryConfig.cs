using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200013C RID: 316
	[Obsolete("Telemetry is sent automatically by MSAL.NET. See https://aka.ms/msal-net-telemetry.", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface ITelemetryConfig
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000FE8 RID: 4072
		TelemetryAudienceType AudienceType { get; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000FE9 RID: 4073
		string SessionId { get; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000FEA RID: 4074
		Action<ITelemetryEventPayload> DispatchAction { get; }
	}
}
