using System;

namespace Microsoft.Mashup.Engine.Interface.RequestTracing
{
	// Token: 0x0200013D RID: 317
	public interface IRequestTracingService
	{
		// Token: 0x0600058F RID: 1423
		bool IsTraceEnabled(IResource resource);

		// Token: 0x06000590 RID: 1424
		IRequestTrace CreateTrace(Guid? activityId, IResource resource, Guid sessionId, string type);
	}
}
