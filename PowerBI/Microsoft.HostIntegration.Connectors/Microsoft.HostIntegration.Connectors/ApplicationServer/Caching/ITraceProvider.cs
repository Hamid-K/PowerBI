using System;
using System.Diagnostics;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C2 RID: 450
	internal interface ITraceProvider
	{
		// Token: 0x06000EDB RID: 3803
		bool Load(string id);

		// Token: 0x06000EDC RID: 3804
		void WriteEntry(string source, TraceEventType msgType, string msgText);
	}
}
