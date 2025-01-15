using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000028 RID: 40
	public interface IDiagnosticsService
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A1 RID: 161
		HashSet<string> EnabledChannels { get; }

		// Token: 0x060000A2 RID: 162
		void Emit(string channelName, string eventName, DateTime eventTime, IResource resource, Dictionary<string, object> properties);
	}
}
