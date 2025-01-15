using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	public interface ITraceWriter
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600068B RID: 1675
		TraceLevel LevelFilter { get; }

		// Token: 0x0600068C RID: 1676
		void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex);
	}
}
