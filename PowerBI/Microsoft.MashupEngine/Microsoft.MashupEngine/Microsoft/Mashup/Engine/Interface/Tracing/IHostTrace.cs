using System;
using System.Diagnostics;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x0200012A RID: 298
	public interface IHostTrace : IDisposable
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600052B RID: 1323
		SourceLevels Levels { get; }

		// Token: 0x0600052C RID: 1324
		void Suspend();

		// Token: 0x0600052D RID: 1325
		void Resume();

		// Token: 0x0600052E RID: 1326
		IHostTraceValue Begin(string name, bool isPii);

		// Token: 0x0600052F RID: 1327
		void Add(string name, object value, bool isPii);

		// Token: 0x06000530 RID: 1328
		void Add(Exception e, bool hasPii = true);

		// Token: 0x06000531 RID: 1329
		void Add(Exception e, TraceEventType type, bool hasPii = true);
	}
}
