using System;
using System.Diagnostics;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x0200012F RID: 303
	public interface ITracingService
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000539 RID: 1337
		string TracePath { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600053A RID: 1338
		SourceLevels Levels { get; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600053B RID: 1339
		ITracingOptions Options { get; }

		// Token: 0x0600053C RID: 1340
		IHostTrace CreateTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel, bool forPerformance, IResource resource = null);

		// Token: 0x0600053D RID: 1341
		IHostTrace CreateUserTrace(Guid? activityId, string correlationId, string entryName, TraceEventType severityLevel);

		// Token: 0x0600053E RID: 1342
		void Flush();

		// Token: 0x0600053F RID: 1343
		void Close();
	}
}
