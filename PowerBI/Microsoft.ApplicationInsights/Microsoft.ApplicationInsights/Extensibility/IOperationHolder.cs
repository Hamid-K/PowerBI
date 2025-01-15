using System;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000054 RID: 84
	public interface IOperationHolder<T> : IDisposable
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000291 RID: 657
		T Telemetry { get; }
	}
}
