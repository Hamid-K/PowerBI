using System;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client.TelemetryCore.TelemetryClient
{
	// Token: 0x020001E4 RID: 484
	internal static class TelemetryClientExtensions
	{
		// Token: 0x060014AA RID: 5290 RVA: 0x00045C74 File Offset: 0x00043E74
		internal static bool HasEnabledClients(this ITelemetryClient[] clients, string eventName)
		{
			for (int i = 0; i < clients.Length; i++)
			{
				if (clients[i].IsEnabled(eventName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00045CA0 File Offset: 0x00043EA0
		internal static void TrackEvent(this ITelemetryClient[] clients, TelemetryEventDetails eventDetails)
		{
			foreach (ITelemetryClient telemetryClient in clients)
			{
				if (telemetryClient.IsEnabled(eventDetails.Name))
				{
					telemetryClient.TrackEvent(eventDetails);
				}
			}
		}
	}
}
