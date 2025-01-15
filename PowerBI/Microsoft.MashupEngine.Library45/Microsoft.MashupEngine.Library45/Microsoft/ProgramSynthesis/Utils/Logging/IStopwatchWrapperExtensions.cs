using System;

namespace Microsoft.ProgramSynthesis.Utils.Logging
{
	// Token: 0x02000540 RID: 1344
	public static class IStopwatchWrapperExtensions
	{
		// Token: 0x06001E63 RID: 7779 RVA: 0x000592C6 File Offset: 0x000574C6
		public static void Stop(this IStopwatchWrapper stopwatchWrapper)
		{
			Logger.StopwatchWrapper stopwatchWrapper2 = stopwatchWrapper as Logger.StopwatchWrapper;
			if (stopwatchWrapper2 == null)
			{
				return;
			}
			stopwatchWrapper2.Stop();
		}
	}
}
