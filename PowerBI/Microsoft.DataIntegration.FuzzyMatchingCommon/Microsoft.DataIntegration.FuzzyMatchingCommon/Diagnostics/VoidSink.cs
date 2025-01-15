using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	internal class VoidSink : IEventSink
	{
		// Token: 0x0600022D RID: 557 RVA: 0x000130E2 File Offset: 0x000112E2
		public void Assert(bool condition)
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000130E4 File Offset: 0x000112E4
		public void Assert(bool condition, string message)
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000130E6 File Offset: 0x000112E6
		public void Trace(string message)
		{
		}
	}
}
