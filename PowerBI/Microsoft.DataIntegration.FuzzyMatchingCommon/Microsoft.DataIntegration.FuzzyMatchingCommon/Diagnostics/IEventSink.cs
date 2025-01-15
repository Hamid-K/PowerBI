using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000041 RID: 65
	internal interface IEventSink
	{
		// Token: 0x0600022A RID: 554
		void Assert(bool condition);

		// Token: 0x0600022B RID: 555
		void Assert(bool condition, string message);

		// Token: 0x0600022C RID: 556
		void Trace(string message);
	}
}
