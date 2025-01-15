using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002D3 RID: 723
	public sealed class PageBreakHelper
	{
		// Token: 0x06001B1D RID: 6941 RVA: 0x0006C397 File Offset: 0x0006A597
		internal static PageBreakLocation GetPageBreakLocation(bool pageBreakAtStart, bool pageBreakAtEnd)
		{
			if (pageBreakAtStart && pageBreakAtEnd)
			{
				return PageBreakLocation.StartAndEnd;
			}
			if (pageBreakAtStart)
			{
				return PageBreakLocation.Start;
			}
			if (pageBreakAtEnd)
			{
				return PageBreakLocation.End;
			}
			return PageBreakLocation.None;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0006C3AB File Offset: 0x0006A5AB
		internal static PageBreakLocation MergePageBreakLocations(PageBreakLocation outer, PageBreakLocation inner)
		{
			if (outer == inner)
			{
				return outer;
			}
			if (PageBreakLocation.StartAndEnd == outer || inner == PageBreakLocation.None)
			{
				return outer;
			}
			if (PageBreakLocation.StartAndEnd == inner || outer == PageBreakLocation.None)
			{
				return inner;
			}
			if (PageBreakLocation.End == outer && PageBreakLocation.Start == inner)
			{
				return PageBreakLocation.StartAndEnd;
			}
			if (PageBreakLocation.Start == outer && PageBreakLocation.End == inner)
			{
				return PageBreakLocation.StartAndEnd;
			}
			return PageBreakLocation.None;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0006C3DA File Offset: 0x0006A5DA
		internal static bool HasPageBreakAtStart(PageBreakLocation pageBreakLoc)
		{
			return pageBreakLoc == PageBreakLocation.Start || pageBreakLoc == PageBreakLocation.StartAndEnd;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0006C3E6 File Offset: 0x0006A5E6
		internal static bool HasPageBreakAtEnd(PageBreakLocation pageBreakLoc)
		{
			return pageBreakLoc == PageBreakLocation.End || pageBreakLoc == PageBreakLocation.StartAndEnd;
		}
	}
}
