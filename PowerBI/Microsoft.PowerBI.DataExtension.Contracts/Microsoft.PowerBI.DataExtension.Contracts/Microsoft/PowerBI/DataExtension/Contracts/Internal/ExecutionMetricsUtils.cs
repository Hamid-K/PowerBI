using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000013 RID: 19
	public static class ExecutionMetricsUtils
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002E70 File Offset: 0x00001070
		public static string ToIdString(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is Guid)
			{
				return ((Guid)value).ToString("D");
			}
			return (string)value;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EA4 File Offset: 0x000010A4
		public static void ReadExecutionMetrics(IExecutionMetricsVisitor visitor, ExecutionMetricsDataReader reader, ExecutionMetricsCache cache)
		{
			if (cache == null)
			{
				return;
			}
			if (!cache.HasCachedResultSet && reader != null)
			{
				reader.DiscardRowsAndConsumeMetrics();
			}
			if (cache.HasCachedResultSet)
			{
				cache.ReadExecutionMetrics(visitor);
			}
		}
	}
}
