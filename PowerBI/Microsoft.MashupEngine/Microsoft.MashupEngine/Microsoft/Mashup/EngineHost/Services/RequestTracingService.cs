using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.RequestTracing;
using Microsoft.Mashup.Evaluator;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B2D RID: 6957
	public class RequestTracingService : IRequestTracingService
	{
		// Token: 0x0600AE41 RID: 44609 RVA: 0x0023AF8F File Offset: 0x0023918F
		public RequestTracingService(Func<IEnumerable<IResource>> getTracedResources, Action<RequestTraceData> traceHandler)
		{
			this.getTracedResources = getTracedResources;
			this.traceHandler = traceHandler;
			this.nextTraceId = 0;
		}

		// Token: 0x0600AE42 RID: 44610 RVA: 0x0023AFAC File Offset: 0x002391AC
		public bool IsTraceEnabled(IResource resource)
		{
			return this.getTracedResources().Any((IResource c) => ResourcePath.StartsWith(c, resource));
		}

		// Token: 0x0600AE43 RID: 44611 RVA: 0x0023AFE4 File Offset: 0x002391E4
		public IRequestTrace CreateTrace(Guid? activityId, IResource resource, Guid sessionId, string type)
		{
			int num = Interlocked.Increment(ref this.nextTraceId);
			RequestTrace trace = new RequestTrace(num, activityId, resource, sessionId, type);
			GlobalizedEvaluatorThreadPool.Start(delegate
			{
				using (RequestTraceData data = trace.GetData())
				{
					this.traceHandler(data);
				}
			});
			return trace;
		}

		// Token: 0x040059DA RID: 23002
		private readonly Func<IEnumerable<IResource>> getTracedResources;

		// Token: 0x040059DB RID: 23003
		private readonly Action<RequestTraceData> traceHandler;

		// Token: 0x040059DC RID: 23004
		private int nextTraceId;
	}
}
