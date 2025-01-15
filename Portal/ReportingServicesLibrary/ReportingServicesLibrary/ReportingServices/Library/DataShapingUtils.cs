using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200002C RID: 44
	internal static class DataShapingUtils
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00006518 File Offset: 0x00004718
		internal static string DetermineConnectionCategory(bool isInternalDataSource)
		{
			if (!isInternalDataSource)
			{
				return "External";
			}
			return "Internal";
		}
	}
}
