using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000020 RID: 32
	public interface ITelemetryIdsProvider
	{
		// Token: 0x0600008E RID: 142
		bool TryGetTelemetryIds(out string clientActivityId, out string currentActivityId, out string rootActivityId, out string applicationContext);
	}
}
