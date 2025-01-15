using System;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x0200021D RID: 541
	internal struct RefreshTimeLine
	{
		// Token: 0x06001E78 RID: 7800 RVA: 0x000CBCDC File Offset: 0x000C9EDC
		public RefreshTimeLine(DateTime effectiveTime, RefreshPolicy refreshPolicy, DateTime existingIncrementalHead)
		{
			this.IncrementalHead = RefreshTimeLine.CalculateIncrementalHead(effectiveTime, refreshPolicy);
			if (DateTime.Compare(existingIncrementalHead, DateTime.MinValue) > 0 && DateTime.Compare(existingIncrementalHead, this.IncrementalHead) < 0)
			{
				this.IncrementalTail = RefreshTimeLine.CalculateIncrementalTail(existingIncrementalHead, refreshPolicy);
			}
			else
			{
				this.IncrementalTail = RefreshTimeLine.CalculateIncrementalTail(this.IncrementalHead, refreshPolicy);
			}
			this.RollingWindowTail = RefreshTimeLine.CalculateRollingWindowTail(this.IncrementalHead, refreshPolicy);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000CBD48 File Offset: 0x000C9F48
		private static DateTime CalculateIncrementalHead(DateTime effectiveTime, RefreshPolicy refreshPolicy)
		{
			switch (refreshPolicy.IncrementalGranularity)
			{
			case RefreshGranularityType.Day:
				return PropertyHelper.StartOfDay(effectiveTime.AddDays((double)refreshPolicy.IncrementalPeriodsOffset));
			case RefreshGranularityType.Month:
				return PropertyHelper.StartOfMonth(effectiveTime.AddMonths(refreshPolicy.IncrementalPeriodsOffset));
			case RefreshGranularityType.Quarter:
				return PropertyHelper.StartOfQuarter(effectiveTime.AddMonths(refreshPolicy.IncrementalPeriodsOffset * 3));
			case RefreshGranularityType.Year:
				return PropertyHelper.StartOfYear(effectiveTime.AddYears(refreshPolicy.IncrementalPeriodsOffset));
			default:
				throw TomInternalException.Create("Invalid RefreshGranularityType {0} provided.", new object[] { refreshPolicy.IncrementalGranularity });
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000CBDE4 File Offset: 0x000C9FE4
		private static DateTime CalculateIncrementalTail(DateTime incrementalHead, RefreshPolicy refreshPolicy)
		{
			switch (refreshPolicy.IncrementalGranularity)
			{
			case RefreshGranularityType.Day:
				return incrementalHead.AddDays((double)(1 - refreshPolicy.IncrementalPeriods));
			case RefreshGranularityType.Month:
				return incrementalHead.AddMonths(1 - refreshPolicy.IncrementalPeriods);
			case RefreshGranularityType.Quarter:
				return incrementalHead.AddMonths((1 - refreshPolicy.IncrementalPeriods) * 3);
			case RefreshGranularityType.Year:
				return incrementalHead.AddYears(1 - refreshPolicy.IncrementalPeriods);
			default:
				throw TomInternalException.Create("Invalid RefreshGranularityType {0} provided.", new object[] { refreshPolicy.IncrementalGranularity });
			}
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000CBE74 File Offset: 0x000CA074
		private static DateTime CalculateRollingWindowTail(DateTime incrementalHead, RefreshPolicy refreshPolicy)
		{
			switch (refreshPolicy.RollingWindowGranularity)
			{
			case RefreshGranularityType.Day:
				return PropertyHelper.StartOfDay(incrementalHead.AddDays((double)(-(double)refreshPolicy.RollingWindowPeriods)));
			case RefreshGranularityType.Month:
				return PropertyHelper.StartOfMonth(incrementalHead.AddMonths(-refreshPolicy.RollingWindowPeriods));
			case RefreshGranularityType.Quarter:
				return PropertyHelper.StartOfQuarter(incrementalHead.AddMonths(-refreshPolicy.RollingWindowPeriods * 3));
			case RefreshGranularityType.Year:
				return PropertyHelper.StartOfYear(incrementalHead.AddYears(-refreshPolicy.RollingWindowPeriods));
			default:
				throw TomInternalException.Create("Invalid RefreshGranularityType {0} provided.", new object[] { refreshPolicy.RollingWindowGranularity });
			}
		}

		// Token: 0x040006FF RID: 1791
		public readonly DateTime IncrementalHead;

		// Token: 0x04000700 RID: 1792
		public readonly DateTime IncrementalTail;

		// Token: 0x04000701 RID: 1793
		public readonly DateTime RollingWindowTail;
	}
}
