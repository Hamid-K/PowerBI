using System;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon
{
	// Token: 0x020000CA RID: 202
	public static class NumericExtensions
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x00012DCB File Offset: 0x00010FCB
		public static bool IsMultipleOf(this int source, int multipleOf)
		{
			return source % multipleOf == 0;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00012DD3 File Offset: 0x00010FD3
		public static bool IsMultipleOf(this long source, int multipleOf)
		{
			return source % (long)multipleOf == 0L;
		}
	}
}
