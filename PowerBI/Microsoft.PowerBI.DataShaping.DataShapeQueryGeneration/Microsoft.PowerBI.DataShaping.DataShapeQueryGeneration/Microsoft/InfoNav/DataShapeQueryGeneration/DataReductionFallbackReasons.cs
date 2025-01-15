using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000026 RID: 38
	internal static class DataReductionFallbackReasons
	{
		// Token: 0x040000BA RID: 186
		internal const string ModelSupport = "ModelSupport";

		// Token: 0x040000BB RID: 187
		internal const string SortByMeasure = "SortByMeasure";

		// Token: 0x040000BC RID: 188
		internal const string MultipleGroupings = "MultiGroup";

		// Token: 0x040000BD RID: 189
		internal const string NonBinKeys = "NonBinKeys";

		// Token: 0x040000BE RID: 190
		internal const string NonNumericAxis = "NonNumericAxis";

		// Token: 0x040000BF RID: 191
		internal const string MultipleAxisKeys = "MultipleAxisKeys";

		// Token: 0x040000C0 RID: 192
		internal const string MultipleAxisKeysWithBinnableOrderBy = "MultipleAxisKeysWithBinnableOrderBy";

		// Token: 0x040000C1 RID: 193
		internal const string ShowItemsWithNoData = "ShowAll";

		// Token: 0x040000C2 RID: 194
		internal const string NoMeasures = "NoMeasures";

		// Token: 0x040000C3 RID: 195
		internal const string PlotAxisBindingsMissing = "PlotAxisBindingsMissing";

		// Token: 0x040000C4 RID: 196
		internal const string TotalsPresent = "TotalsPresent";
	}
}
