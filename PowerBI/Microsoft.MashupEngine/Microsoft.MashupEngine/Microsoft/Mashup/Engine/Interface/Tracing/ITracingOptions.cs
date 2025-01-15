using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x02000130 RID: 304
	public interface ITracingOptions
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000540 RID: 1344
		IEnumerable<string> Keys { get; }

		// Token: 0x06000541 RID: 1345
		bool IsEnabled(string key);
	}
}
