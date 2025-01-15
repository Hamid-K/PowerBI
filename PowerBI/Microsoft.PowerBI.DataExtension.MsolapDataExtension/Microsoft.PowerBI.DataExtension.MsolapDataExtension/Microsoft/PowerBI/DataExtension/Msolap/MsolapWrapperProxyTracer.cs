using System;
using System.Diagnostics;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using MsolapWrapper;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x02000008 RID: 8
	internal sealed class MsolapWrapperProxyTracer : MsolapTracerBase
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000240A File Offset: 0x0000060A
		public MsolapWrapperProxyTracer(ITracer tracer)
		{
			this.tracerInstance = tracer;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002419 File Offset: 0x00000619
		public override void Trace(TraceLevel traceLevel, string message)
		{
			this.tracerInstance.Trace(traceLevel, message);
		}

		// Token: 0x0400003B RID: 59
		private readonly ITracer tracerInstance;
	}
}
