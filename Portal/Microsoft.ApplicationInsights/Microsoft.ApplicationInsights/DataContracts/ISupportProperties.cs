using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D3 RID: 211
	public interface ISupportProperties
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000744 RID: 1860
		IDictionary<string, string> Properties { get; }
	}
}
