using System;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200001E RID: 30
	[ImmutableObject(true)]
	internal sealed class ServiceRuntimeContext
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003666 File Offset: 0x00001866
		internal ServiceRuntimeContext(ITracer tracer, ITelemetryService telemetryService)
		{
			this.Tracer = tracer;
			this.TelemetryService = telemetryService;
		}

		// Token: 0x0400008E RID: 142
		internal readonly ITracer Tracer;

		// Token: 0x0400008F RID: 143
		internal readonly ITelemetryService TelemetryService;
	}
}
