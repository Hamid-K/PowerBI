using System;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C1F RID: 7199
	public static class SplitTraceExtensions
	{
		// Token: 0x0600B3B4 RID: 46004 RVA: 0x00247F40 File Offset: 0x00246140
		public static SplitsTrace CreateSplits(this IHostTrace trace)
		{
			return new SplitsTrace(trace);
		}
	}
}
