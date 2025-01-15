using System;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x0200030C RID: 780
	public static class SortDirection
	{
		// Token: 0x06001B1E RID: 6942 RVA: 0x0006E300 File Offset: 0x0006C500
		internal static SortOptions SoapSortDirectionToSortOptions(SortDirectionEnum direction)
		{
			if (direction == SortDirectionEnum.Ascending)
			{
				return SortOptions.Ascending;
			}
			if (direction != SortDirectionEnum.Descending)
			{
				return SortOptions.None;
			}
			return SortOptions.Descending;
		}
	}
}
