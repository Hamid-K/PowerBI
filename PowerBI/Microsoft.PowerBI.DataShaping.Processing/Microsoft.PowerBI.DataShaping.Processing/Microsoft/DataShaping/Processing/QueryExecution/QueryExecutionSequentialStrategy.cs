using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000066 RID: 102
	internal sealed class QueryExecutionSequentialStrategy : IQueryExecutionStrategy
	{
		// Token: 0x06000268 RID: 616 RVA: 0x000070FC File Offset: 0x000052FC
		public async Task RunQueriesAsync(IReadOnlyList<QueryExecutor<CommandExecutor>> queryExecutors, IConnectionFactory connectionFactory, CancellationToken cancelToken)
		{
			for (int index = 0; index < queryExecutors.Count; index++)
			{
				await queryExecutors[index].RunQueryAsync(connectionFactory, cancelToken, CancellationToken.None);
			}
		}
	}
}
