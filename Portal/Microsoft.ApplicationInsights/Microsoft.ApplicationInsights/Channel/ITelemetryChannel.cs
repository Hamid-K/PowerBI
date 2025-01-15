using System;

namespace Microsoft.ApplicationInsights.Channel
{
	// Token: 0x020000E4 RID: 228
	public interface ITelemetryChannel : IDisposable
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600085B RID: 2139
		// (set) Token: 0x0600085C RID: 2140
		bool? DeveloperMode { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600085D RID: 2141
		// (set) Token: 0x0600085E RID: 2142
		string EndpointAddress { get; set; }

		// Token: 0x0600085F RID: 2143
		void Send(ITelemetry item);

		// Token: 0x06000860 RID: 2144
		void Flush();
	}
}
