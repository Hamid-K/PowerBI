using System;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.TelemetryCore.TelemetryClient
{
	// Token: 0x020001E3 RID: 483
	internal class MsalTelemetryEventDetails : TelemetryEventDetails
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x00045C63 File Offset: 0x00043E63
		public MsalTelemetryEventDetails(string eventName)
		{
			this.Name = eventName;
		}
	}
}
