using System;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001C3 RID: 451
	internal static class ProcessorCounter
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x000446C8 File Offset: 0x000428C8
		internal static int ProcessorCount
		{
			get
			{
				int tickCount = Environment.TickCount;
				if (ProcessorCounter._processorCount == 0 || tickCount - ProcessorCounter._lastProcessorCountRefreshTicks >= 30000)
				{
					ProcessorCounter._processorCount = Environment.ProcessorCount;
					ProcessorCounter._lastProcessorCountRefreshTicks = tickCount;
				}
				return ProcessorCounter._processorCount;
			}
		}

		// Token: 0x04000841 RID: 2113
		private const int ProcessorCountRefreshIntervalMs = 30000;

		// Token: 0x04000842 RID: 2114
		private static volatile int _processorCount;

		// Token: 0x04000843 RID: 2115
		private static volatile int _lastProcessorCountRefreshTicks;
	}
}
