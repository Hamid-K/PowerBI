using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000AB RID: 171
	internal interface IHeartbeatDefaultPayloadProvider
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000533 RID: 1331
		string Name { get; }

		// Token: 0x06000534 RID: 1332
		bool IsKeyword(string keyword);

		// Token: 0x06000535 RID: 1333
		Task<bool> SetDefaultPayload(IEnumerable<string> disabledFields, IHeartbeatProvider provider);
	}
}
