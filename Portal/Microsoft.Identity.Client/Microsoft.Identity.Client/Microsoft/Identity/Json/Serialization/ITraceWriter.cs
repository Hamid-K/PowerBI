using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x02000082 RID: 130
	internal interface ITraceWriter
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000682 RID: 1666
		TraceLevel LevelFilter { get; }

		// Token: 0x06000683 RID: 1667
		void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex);
	}
}
