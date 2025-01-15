using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200001F RID: 31
	internal static class ManualResetValueTaskSourceCoreShared
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000026D6 File Offset: 0x000008D6
		private static void CompletionSentinel(object _)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000025 RID: 37
		internal static readonly Action<object> s_sentinel = new Action<object>(ManualResetValueTaskSourceCoreShared.CompletionSentinel);
	}
}
