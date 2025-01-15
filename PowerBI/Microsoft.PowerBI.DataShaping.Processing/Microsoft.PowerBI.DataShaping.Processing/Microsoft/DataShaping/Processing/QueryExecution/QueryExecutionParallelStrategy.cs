using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x02000065 RID: 101
	internal sealed class QueryExecutionParallelStrategy : IQueryExecutionStrategy
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00007034 File Offset: 0x00005234
		public async Task RunQueriesAsync(IReadOnlyList<QueryExecutor<CommandExecutor>> queryExecutors, IConnectionFactory connectionFactory, CancellationToken externalCancellationToken)
		{
			QueryExecutionParallelStrategy.<>c__DisplayClass1_0 CS$<>8__locals1 = new QueryExecutionParallelStrategy.<>c__DisplayClass1_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.connectionFactory = connectionFactory;
			if (queryExecutors.Count == 1)
			{
				await queryExecutors[0].RunQueryAsync(CS$<>8__locals1.connectionFactory, externalCancellationToken, CancellationToken.None);
			}
			else
			{
				QueryExecutionParallelStrategy.<>c__DisplayClass1_1 CS$<>8__locals2 = new QueryExecutionParallelStrategy.<>c__DisplayClass1_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.internalCancellationTokenSource = new CancellationTokenSource();
				try
				{
					CS$<>8__locals2.compositeTokenSource = CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken, CS$<>8__locals2.internalCancellationTokenSource.Token);
					try
					{
						Task[] array = new Task[queryExecutors.Count];
						for (int i = 1; i < queryExecutors.Count; i++)
						{
							QueryExecutor<CommandExecutor> executor = queryExecutors[i];
							array[i] = Task.Run(() => CS$<>8__locals2.CS$<>8__locals1.<>4__this.RunQueryWithCancellationFallback(executor, CS$<>8__locals2.CS$<>8__locals1.connectionFactory, CS$<>8__locals2.compositeTokenSource.Token, CS$<>8__locals2.internalCancellationTokenSource));
						}
						array[0] = this.RunQueryWithCancellationFallback(queryExecutors[0], CS$<>8__locals2.CS$<>8__locals1.connectionFactory, CS$<>8__locals2.compositeTokenSource.Token, CS$<>8__locals2.internalCancellationTokenSource);
						Task aggregateTask = Task.WhenAll(array);
						await aggregateTask.ContinueWith(delegate(Task t)
						{
							if (t.Exception != null && CS$<>8__locals2.CS$<>8__locals1.<>4__this._firstException != null)
							{
								using (IEnumerator<Exception> enumerator = t.Exception.Flatten().InnerExceptions.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (enumerator.Current == CS$<>8__locals2.CS$<>8__locals1.<>4__this._firstException.SourceException)
										{
											CS$<>8__locals2.CS$<>8__locals1.<>4__this._firstException.Throw();
										}
									}
								}
							}
						}, TaskContinuationOptions.ExecuteSynchronously);
						await aggregateTask;
						aggregateTask = null;
					}
					finally
					{
						if (CS$<>8__locals2.compositeTokenSource != null)
						{
							((IDisposable)CS$<>8__locals2.compositeTokenSource).Dispose();
						}
					}
				}
				finally
				{
					if (CS$<>8__locals2.internalCancellationTokenSource != null)
					{
						((IDisposable)CS$<>8__locals2.internalCancellationTokenSource).Dispose();
					}
				}
				CS$<>8__locals2 = null;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00007090 File Offset: 0x00005290
		private async Task RunQueryWithCancellationFallback(QueryExecutor<CommandExecutor> executor, IConnectionFactory connectionFactory, CancellationToken compositeToken, CancellationTokenSource internalCancellationTokenSource)
		{
			try
			{
				await executor.RunQueryAsync(connectionFactory, compositeToken, internalCancellationTokenSource.Token);
			}
			catch (Exception ex) when (!ErrorUtils.IsStoppingException(ex))
			{
				if (this._firstException == null)
				{
					this._firstException = ExceptionDispatchInfo.Capture(ex);
				}
				if (!compositeToken.IsCancellationRequested)
				{
					internalCancellationTokenSource.Cancel();
				}
				throw;
			}
		}

		// Token: 0x0400017F RID: 383
		private volatile ExceptionDispatchInfo _firstException;
	}
}
