using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D6 RID: 2262
	internal static class FlagUtils
	{
		// Token: 0x06007BB0 RID: 31664 RVA: 0x001FC6A6 File Offset: 0x001FA8A6
		public static bool HasFlag(DataActions value, DataActions flagToTest)
		{
			return (value & flagToTest) > DataActions.None;
		}

		// Token: 0x06007BB1 RID: 31665 RVA: 0x001FC6AE File Offset: 0x001FA8AE
		public static bool HasFlag(AggregateUpdateFlags value, AggregateUpdateFlags flagToTest)
		{
			return (value & flagToTest) > AggregateUpdateFlags.None;
		}
	}
}
