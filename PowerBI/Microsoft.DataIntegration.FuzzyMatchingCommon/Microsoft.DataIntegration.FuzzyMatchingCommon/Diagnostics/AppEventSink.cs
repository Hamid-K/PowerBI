using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	internal class AppEventSink : IEventSink
	{
		// Token: 0x06000231 RID: 561 RVA: 0x000130F0 File Offset: 0x000112F0
		public void Assert(bool condition)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000130F2 File Offset: 0x000112F2
		public void Assert(bool condition, string message)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000130F4 File Offset: 0x000112F4
		public void Trace(string message)
		{
			Console.WriteLine(message);
		}
	}
}
