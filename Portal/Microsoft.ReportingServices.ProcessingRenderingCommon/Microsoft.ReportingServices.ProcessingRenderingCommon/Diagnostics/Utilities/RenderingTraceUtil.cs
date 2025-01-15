using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B2 RID: 178
	internal static class RenderingTraceUtil
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x00011104 File Offset: 0x0000F304
		internal static void TracePageNumber(RSTrace tracer, string component, int pageNumber)
		{
			int pageNumberTraceGranularity = RenderingTraceUtil.GetPageNumberTraceGranularity(pageNumber);
			if (pageNumber % pageNumberTraceGranularity == 0)
			{
				tracer.Trace(string.Format("[{0}] Done rendering page number {1}", component, pageNumber));
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00011134 File Offset: 0x0000F334
		internal static void TraceRenderingDone(RSTrace tracer, string component, int totalPages)
		{
			tracer.Trace(string.Format("[{0}] Done rendering all pages. The total number of pages is {1}.", component, totalPages));
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00011150 File Offset: 0x0000F350
		private static int GetPageNumberTraceGranularity(int pageNumber)
		{
			int num = 1;
			foreach (KeyValuePair<int, int> keyValuePair in RenderingTraceUtil.PageNumberTraceGranularity)
			{
				if (pageNumber < keyValuePair.Key)
				{
					return num;
				}
				num = keyValuePair.Value;
			}
			return num;
		}

		// Token: 0x0400032B RID: 811
		private static readonly Dictionary<int, int> PageNumberTraceGranularity = new Dictionary<int, int>
		{
			{ 20, 10 },
			{ 100, 100 }
		};
	}
}
