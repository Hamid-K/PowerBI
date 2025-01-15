using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x02000083 RID: 131
	[NullableContext(1)]
	internal interface ITraceWriter
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600068C RID: 1676
		TraceLevel LevelFilter { get; }

		// Token: 0x0600068D RID: 1677
		void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex);
	}
}
