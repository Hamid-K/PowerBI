using System;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000052 RID: 82
	internal interface IDebugOutput
	{
		// Token: 0x0600028D RID: 653
		void WriteLine(string message);

		// Token: 0x0600028E RID: 654
		bool IsLogging();

		// Token: 0x0600028F RID: 655
		bool IsAttached();
	}
}
