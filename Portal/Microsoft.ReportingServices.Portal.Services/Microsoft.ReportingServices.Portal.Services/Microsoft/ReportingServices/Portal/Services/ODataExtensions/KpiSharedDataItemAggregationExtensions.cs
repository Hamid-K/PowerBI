using System;
using Microsoft.ReportingServices.Library;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200003F RID: 63
	internal static class KpiSharedDataItemAggregationExtensions
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0001068E File Offset: 0x0000E88E
		public static Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation ToServiceAggregationType(this global::Model.KpiSharedDataItemAggregation aggregation)
		{
			switch (aggregation)
			{
			case global::Model.KpiSharedDataItemAggregation.None:
				return Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation.None;
			case global::Model.KpiSharedDataItemAggregation.First:
				return Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation.First;
			case global::Model.KpiSharedDataItemAggregation.Last:
				return Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation.Last;
			case global::Model.KpiSharedDataItemAggregation.Min:
				return Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation.Min;
			case global::Model.KpiSharedDataItemAggregation.Max:
				return Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation.Max;
			case global::Model.KpiSharedDataItemAggregation.Average:
				return Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation.Average;
			case global::Model.KpiSharedDataItemAggregation.Sum:
				return Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation.Sum;
			default:
				return Microsoft.ReportingServices.Library.KpiSharedDataItemAggregation.None;
			}
		}
	}
}
