using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200013B RID: 315
	[Obsolete("Telemetry is sent automatically by MSAL.NET. See https://aka.ms/msal-net-telemetry.", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum TelemetryAudienceType
	{
		// Token: 0x040004D6 RID: 1238
		PreProduction,
		// Token: 0x040004D7 RID: 1239
		Production
	}
}
