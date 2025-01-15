using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x0200005F RID: 95
	internal interface IQueryExecutionStrategy
	{
		// Token: 0x06000248 RID: 584
		Task RunQueriesAsync(IReadOnlyList<QueryExecutor<CommandExecutor>> queryExecutors, IConnectionFactory connectionFactory, CancellationToken token);
	}
}
