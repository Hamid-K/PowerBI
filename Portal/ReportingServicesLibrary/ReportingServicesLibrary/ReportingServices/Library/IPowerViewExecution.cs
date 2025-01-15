using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200017F RID: 383
	internal interface IPowerViewExecution
	{
		// Token: 0x06000E02 RID: 3586
		void ProcessInput(bool foundSession, ProgressiveCacheEntry entry);

		// Token: 0x06000E03 RID: 3587
		void RenderItem();

		// Token: 0x06000E04 RID: 3588
		void WriteMessage(string code, string message);

		// Token: 0x06000E05 RID: 3589
		void WriteSessionId(string sessionId);
	}
}
