using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000CF RID: 207
	public interface IRSTraceInternalWithDynamicLevel : IRSTraceInternal
	{
		// Token: 0x0600048D RID: 1165
		void SetTraceLevel(TraceLevel traceLevel);
	}
}
