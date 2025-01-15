using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A1 RID: 161
	internal interface IDiagnosticsSender
	{
		// Token: 0x060004F4 RID: 1268
		void Send(TraceEvent eventData);
	}
}
