using System;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000067 RID: 103
	internal static class QueryExecutionStrategyFactory
	{
		// Token: 0x0600026A RID: 618 RVA: 0x00007157 File Offset: 0x00005357
		internal static IQueryExecutionStrategy CreateStrategy(bool useParallelExecutionStrategy = true)
		{
			if (useParallelExecutionStrategy)
			{
				return new QueryExecutionParallelStrategy();
			}
			return new QueryExecutionSequentialStrategy();
		}
	}
}
