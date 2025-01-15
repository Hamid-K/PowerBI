using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DsqGeneration;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecutionCommon
{
	// Token: 0x0200006A RID: 106
	internal abstract class CommandExecutorBase<TDataReader> : ICommandExecutor, IDisposable where TDataReader : class
	{
		// Token: 0x06000272 RID: 626 RVA: 0x000072A6 File Offset: 0x000054A6
		protected CommandExecutorBase(QueryCommandOptions commandOptions, ITelemetryService telemetryService, ITracer tracer)
		{
			this._commandOptions = commandOptions;
			this._telemetryService = telemetryService;
			this._tracer = tracer;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000072C4 File Offset: 0x000054C4
		protected void SetCommandProperties(IDbCommand command, out int? appContextLength)
		{
			command.SetMemoryLimit(this._commandOptions.MemoryLimit);
			command.SetTimeout(this._commandOptions.Timeout);
			command.SetRequestPriority(this._commandOptions.RequestPriority);
			command.SetRequestExecutionMetrics(this._commandOptions.RequestExecutionMetrics, this._commandOptions.MaxExecutionEventsPerQuery);
			string text = ApplicationContextSerializer.Serialize(this._commandOptions.ApplicationContextObject);
			appContextLength = ((text != null) ? new int?(text.Length) : null);
			command.SetApplicationContext(text);
			string text2;
			string text3;
			string text4;
			if (this._telemetryService.TryGetTelemetryIDs(out text2, out text3, out text4))
			{
				command.SetTelemetryIds(text2, text3, text4);
			}
		}

		// Token: 0x06000274 RID: 628
		protected abstract void ProcessQueryExecutionMetrics();

		// Token: 0x06000275 RID: 629
		protected abstract bool ReaderHasMoreData();

		// Token: 0x06000276 RID: 630
		protected abstract bool ShouldCallCancel();

		// Token: 0x06000277 RID: 631
		protected abstract void CloseDataReader();

		// Token: 0x06000278 RID: 632
		public abstract bool IsClosed();

		// Token: 0x06000279 RID: 633
		public abstract Task ExecuteAsync(IDbConnection connection, CancellationToken cancellationToken, CancellationToken internalCancelToken);

		// Token: 0x0600027A RID: 634 RVA: 0x00007374 File Offset: 0x00005574
		public async Task CloseAsync(bool shouldNotThrow)
		{
			if (!this.IsClosed())
			{
				await this.CloseImpl(shouldNotThrow);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000073C0 File Offset: 0x000055C0
		private async Task CloseImpl(bool shouldNotThrow)
		{
			if (this._dataReader != null)
			{
				bool needsCancel = false;
				QueryExecutionUtils.ExecuteInTryCatch(this._tracer, delegate
				{
					needsCancel = this.ReaderHasMoreData();
				}, shouldNotThrow, "An error occurred verifying for more rows during cleanup");
				if (needsCancel && this.ShouldCallCancel())
				{
					await QueryExecutionUtils.ExecuteInTryCatch(this._tracer, async delegate
					{
						await this._command.CancelAsync();
					}, true, "An error occurred canceling the command during cleanup");
				}
				this.ProcessQueryExecutionMetrics();
				QueryExecutionUtils.ExecuteInTryCatch(this._tracer, delegate
				{
					this.CloseDataReader();
				}, shouldNotThrow, "An error occurred closing the data reader");
				this._dataReader = default(TDataReader);
			}
			if (this._command != null)
			{
				this.ProcessQueryExecutionMetrics();
				QueryExecutionUtils.ExecuteInTryCatch(this._tracer, delegate
				{
					this._command.Close();
					this._command.Dispose();
				}, shouldNotThrow, "An error occurred closing the command");
				this._command = null;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000740B File Offset: 0x0000560B
		public void Dispose()
		{
			this.CloseAsync(true).WaitAndUnwrap();
		}

		// Token: 0x04000181 RID: 385
		protected readonly ITracer _tracer;

		// Token: 0x04000182 RID: 386
		protected readonly ITelemetryService _telemetryService;

		// Token: 0x04000183 RID: 387
		protected readonly QueryCommandOptions _commandOptions;

		// Token: 0x04000184 RID: 388
		protected IDbCommand _command;

		// Token: 0x04000185 RID: 389
		protected TDataReader _dataReader;
	}
}
