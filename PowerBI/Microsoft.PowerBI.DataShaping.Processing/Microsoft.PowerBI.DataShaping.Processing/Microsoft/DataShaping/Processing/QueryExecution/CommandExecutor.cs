using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.ExecutionMetadata;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecution
{
	// Token: 0x0200005C RID: 92
	internal sealed class CommandExecutor : CommandExecutorBase<ProcessingDataReader>
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00006598 File Offset: 0x00004798
		internal CommandExecutor(CommandExecutionContext commandContext, ITelemetryService telemetryService, ITracer tracer, IConnectionUserImpersonator connectionUserImpersonator, IDataShapingExecutionMetricsService executionMetricsService)
			: base((commandContext != null) ? commandContext.CommandOptions : null, telemetryService, tracer)
		{
			this._context = commandContext;
			this._connectionUserImpersonator = connectionUserImpersonator;
			this._executionMetricsService = executionMetricsService;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000065C5 File Offset: 0x000047C5
		public override bool IsClosed()
		{
			return this._dataReader == null && this._command == null;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000065DC File Offset: 0x000047DC
		public override async Task ExecuteAsync(IDbConnection connection, CancellationToken cancelToken, CancellationToken internalCancelToken)
		{
			CommandExecutor.<>c__DisplayClass8_0 CS$<>8__locals1 = new CommandExecutor.<>c__DisplayClass8_0();
			CS$<>8__locals1.cancelToken = cancelToken;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.internalCancelToken = internalCancelToken;
			this._command = connection.CreateCommand(this._context.DataSet.Query);
			base.SetCommandProperties(this._command, out CS$<>8__locals1.appContextLength);
			await this._telemetryService.RunInAsyncActivity(ActivityKind.ExecuteReader, delegate
			{
				CommandExecutor.<>c__DisplayClass8_0.<<ExecuteAsync>b__0>d <<ExecuteAsync>b__0>d;
				<<ExecuteAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<ExecuteAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<ExecuteAsync>b__0>d.<>1__state = -1;
				<<ExecuteAsync>b__0>d.<>t__builder.Start<CommandExecutor.<>c__DisplayClass8_0.<<ExecuteAsync>b__0>d>(ref <<ExecuteAsync>b__0>d);
				return <<ExecuteAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006637 File Offset: 0x00004837
		protected override bool ReaderHasMoreData()
		{
			return this._dataReader.ReadRow() != null || this._dataReader.NextResultSet();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006653 File Offset: 0x00004853
		protected override void CloseDataReader()
		{
			this._dataReader.Close();
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006660 File Offset: 0x00004860
		internal ProcessingDataReader DataReader
		{
			get
			{
				return this._dataReader;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006668 File Offset: 0x00004868
		private void TriggerCancel(object internalCancelToken)
		{
			if (!this.ShouldCallCancel())
			{
				return;
			}
			if (((CancellationToken)internalCancelToken).IsCancellationRequested)
			{
				this.CancelCommandAsync().WaitAndUnwrap();
				return;
			}
			this._tracer.SanitizedTrace(TraceLevel.Info, "Cancel called");
			Task.Run(new Func<Task>(this.CancelCommandAsync));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000066C0 File Offset: 0x000048C0
		private async Task CancelCommandAsync()
		{
			await this._telemetryService.RunInAsyncActivity(ActivityKind.CancelCommand, async delegate
			{
				try
				{
					IDbCommand command = this._command;
					if (command == null || !command.IsOpen)
					{
						this._tracer.SanitizedTrace(TraceLevel.Info, "Command not open. Skipping cancel.");
					}
					else
					{
						this._cancelDuringQueryExecution = true;
						Func<Task> func = new Func<Task>(command.CancelAsync);
						if (this._connectionUserImpersonator != null)
						{
							await this._connectionUserImpersonator.ExecuteInContextAsync(func);
						}
						else
						{
							await func();
						}
					}
				}
				catch (Exception ex) when (!ErrorUtils.IsStoppingException(ex))
				{
					this._tracer.TraceSanitizedError(ex, "Unexpected exception calling Command.Cancel.  This is treated as non-fatal.");
				}
			});
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006703 File Offset: 0x00004903
		protected override bool ShouldCallCancel()
		{
			if (Interlocked.Increment(ref this._cancelAttemptsCount) > 1)
			{
				this._tracer.SanitizedTrace(TraceLevel.Info, "Not calling Cancel because it has already been called.");
				return false;
			}
			return true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006728 File Offset: 0x00004928
		protected override void ProcessQueryExecutionMetrics()
		{
			if (this._queryExecutionEvent == null)
			{
				return;
			}
			ITimedEventTracker queryExecutionEvent = this._queryExecutionEvent;
			this._queryExecutionEvent = null;
			if (this._dataReader != null)
			{
				queryExecutionEvent.SetMetric("RowCount", this._dataReader.RowCount);
			}
			if (this._cancelDuringQueryExecution)
			{
				queryExecutionEvent.SetMetric("Canceled", true);
			}
			if (this._context.CommandOptions.RequestExecutionMetrics == RequestExecutionMetricsKind.None)
			{
				return;
			}
			string id = queryExecutionEvent.Id;
			DataExtensionExecutionMetricsVisitor visitor = new DataExtensionExecutionMetricsVisitor(this._executionMetricsService, "Metrics Truncated", "DSE", id);
			QueryExecutionUtils.ExecuteInTryCatch(this._tracer, delegate
			{
				this._command.ReadExecutionMetrics(visitor);
			}, true, "An error occurred consuming execution metrics.  This will be ignored.");
		}

		// Token: 0x04000159 RID: 345
		private readonly CommandExecutionContext _context;

		// Token: 0x0400015A RID: 346
		private readonly IConnectionUserImpersonator _connectionUserImpersonator;

		// Token: 0x0400015B RID: 347
		private readonly IDataShapingExecutionMetricsService _executionMetricsService;

		// Token: 0x0400015C RID: 348
		private ITimedEventTracker _queryExecutionEvent;

		// Token: 0x0400015D RID: 349
		private int _cancelAttemptsCount;

		// Token: 0x0400015E RID: 350
		private volatile bool _cancelDuringQueryExecution;
	}
}
