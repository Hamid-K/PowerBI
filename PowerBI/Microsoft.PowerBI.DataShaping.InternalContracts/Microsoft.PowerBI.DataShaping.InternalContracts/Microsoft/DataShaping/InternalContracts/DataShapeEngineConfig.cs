using System;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000008 RID: 8
	internal static class DataShapeEngineConfig
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002189 File Offset: 0x00000389
		internal static int MaximumDurationForDataShapeQueryTranslationInMs
		{
			get
			{
				return 60000;
			}
		}

		// Token: 0x04000029 RID: 41
		internal const int MaxNumberOfInFilterValues = 30000;

		// Token: 0x0400002A RID: 42
		internal const int MaxNumberOfInFilterValuesForTreeRewrite = 500;

		// Token: 0x0400002B RID: 43
		internal const int MaxNumberOfFilterDisjunctionsForSubqueryRewrite = 100;

		// Token: 0x0400002C RID: 44
		internal const int MaxNumberOfAllowedExtensionProperties = 100;

		// Token: 0x0400002D RID: 45
		internal const string DefaultDataSourceId = "EntityDataSource";

		// Token: 0x0400002E RID: 46
		internal const int MaxPointsForSparklineQuery = 760000;

		// Token: 0x0400002F RID: 47
		internal static int MaxNumberOfInFilterTupleValuesWithDefaults = (int)Math.Log(30000.0, 2.0) - 1;
	}
}
