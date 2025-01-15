using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200083B RID: 2107
	public interface ITelemetryEvent
	{
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06004302 RID: 17154
		string EventName { get; }

		// Token: 0x06004303 RID: 17155
		void SetEventProperties(IDictionary<string, string> properties);
	}
}
