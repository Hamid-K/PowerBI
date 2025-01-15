using System;
using System.Diagnostics;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C8 RID: 456
	internal class TraceUtility : ITraceProvider
	{
		// Token: 0x06000EFB RID: 3835 RVA: 0x00032FC8 File Offset: 0x000311C8
		public void WriteEntry(string src, TraceEventType eventType, string message)
		{
			if (message == null)
			{
				return;
			}
			DiagnosticsTraceUtility.WriteEntry(src, eventType, message);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool Load(string id)
		{
			return true;
		}
	}
}
