using System;
using System.Diagnostics;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003CC RID: 972
	internal interface IEventSink
	{
		// Token: 0x06002238 RID: 8760
		void WriteEntry(string src, TraceEventType msgType, string msgText);

		// Token: 0x06002239 RID: 8761
		bool Load(string id);
	}
}
