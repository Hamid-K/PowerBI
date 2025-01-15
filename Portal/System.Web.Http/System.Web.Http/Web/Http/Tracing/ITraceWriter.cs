using System;
using System.Net.Http;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000119 RID: 281
	public interface ITraceWriter
	{
		// Token: 0x06000769 RID: 1897
		void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction);
	}
}
