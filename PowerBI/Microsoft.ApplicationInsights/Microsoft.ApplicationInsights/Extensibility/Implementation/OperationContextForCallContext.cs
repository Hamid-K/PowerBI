using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000074 RID: 116
	internal class OperationContextForCallContext
	{
		// Token: 0x04000174 RID: 372
		public string ParentOperationId;

		// Token: 0x04000175 RID: 373
		public string RootOperationId;

		// Token: 0x04000176 RID: 374
		public string RootOperationName;

		// Token: 0x04000177 RID: 375
		public IDictionary<string, string> CorrelationContext;
	}
}
