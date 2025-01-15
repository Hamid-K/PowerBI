using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using MsolapWrapper;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x02000006 RID: 6
	internal sealed class Command : Microsoft.PowerBI.DataExtension.Contracts.Internal.IDbCommand, IDisposable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		internal Command(Command command, IPrivateInformationService piiService)
		{
			this._command = command;
			this._piiService = piiService;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207D File Offset: 0x0000027D
		public Task<Microsoft.PowerBI.DataExtension.Contracts.Internal.IDataReader> ExecuteReaderAsync()
		{
			return Utilities.RunSynchronously<Microsoft.PowerBI.DataExtension.Contracts.Internal.IDataReader>(new Func<Microsoft.PowerBI.DataExtension.Contracts.Internal.IDataReader>(this.ExecuteReader));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002094 File Offset: 0x00000294
		public Microsoft.PowerBI.DataExtension.Contracts.Internal.IDataReader ExecuteReader()
		{
			Microsoft.PowerBI.DataExtension.Contracts.Internal.IDataReader dataReader2;
			try
			{
				this._command.AddProperty(CommandProperties.ForwardOnly, true);
				DataReader dataReader = new DataReader(this._command.ExecuteReader(), this._piiService);
				if (this._executionMetricsKind == RequestExecutionMetricsKind.None)
				{
					dataReader2 = dataReader;
				}
				else
				{
					dataReader2 = this.PrepareExecutionMetricsReader(dataReader);
				}
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to execute the DAX query.", new object[0]);
			}
			return dataReader2;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002108 File Offset: 0x00000308
		private Microsoft.PowerBI.DataExtension.Contracts.Internal.IDataReader PrepareExecutionMetricsReader(DataReader dataReader)
		{
			this._executionMetricsCache = new ExecutionMetricsCache();
			this._executionMetricsDataReader = new ExecutionMetricsDataReader(dataReader, Command.ExecutionMetricsColumnNames, this._executionMetricsMaxEventCount, this._executionMetricsCache);
			try
			{
				this._executionMetricsDataReader.ConsumeMetrics();
			}
			catch (DataExtensionException)
			{
				this._executionMetricsDataReader.Dispose();
				this._executionMetricsDataReader = null;
				throw;
			}
			return this._executionMetricsDataReader;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002178 File Offset: 0x00000378
		internal Task ExecuteNonQueryAsync()
		{
			return Utilities.RunSynchronously(new Action(this.ExecuteNonQueryImpl));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000218C File Offset: 0x0000038C
		private void ExecuteNonQueryImpl()
		{
			try
			{
				this._command.ExecuteNonQuery();
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to execute the command.", new object[0]);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021D0 File Offset: 0x000003D0
		public bool IsOpen
		{
			get
			{
				return this._command.IsOpen();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021DD File Offset: 0x000003DD
		public void SetTimeout(int timeout)
		{
			this._command.AddProperty(CommandProperties.CommandTimeout, timeout);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F1 File Offset: 0x000003F1
		public void SetMemoryLimit(int memoryLimitKB)
		{
			this._command.AddProperty(CommandProperties.MemoryLimit, memoryLimitKB);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002205 File Offset: 0x00000405
		public void SetTelemetryIds(string clientActivityId, string currentActivityId, string rootActivityId)
		{
			this._command.AddProperty(CommandProperties.ActivityId, clientActivityId);
			this._command.AddProperty(CommandProperties.CurrentActivityId, currentActivityId);
			this._command.AddProperty(CommandProperties.RequestId, rootActivityId);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000222E File Offset: 0x0000042E
		public void SetRequestPriority(RequestPriorityKind requestPriority)
		{
			if (requestPriority == RequestPriorityKind.Normal)
			{
				return;
			}
			if (requestPriority == RequestPriorityKind.Low)
			{
				this._command.AddProperty(CommandProperties.RequestPriority, CommandRequestPriorityKind.PF_REQUEST_PRIORITY_LOW);
				return;
			}
			throw new NotSupportedException(Utilities.FormatInvariant("Invalid QueryRequestPriority specified: {0}", new object[] { requestPriority }));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002269 File Offset: 0x00000469
		public void SetRequestExecutionMetrics(RequestExecutionMetricsKind metricsKind, int? maxEventCount)
		{
			if (metricsKind == RequestExecutionMetricsKind.None)
			{
				return;
			}
			this._executionMetricsKind = metricsKind;
			this._executionMetricsMaxEventCount = maxEventCount;
			this._command.AddProperty(CommandProperties.ExecutionMetrics, metricsKind.ToMsolapMetricsKind());
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002294 File Offset: 0x00000494
		public void ReadExecutionMetrics(IExecutionMetricsVisitor visitor)
		{
			ExecutionMetricsUtils.ReadExecutionMetrics(visitor, this._executionMetricsDataReader, this._executionMetricsCache);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022A8 File Offset: 0x000004A8
		public void SetApplicationContext(string applicationContext)
		{
			if (applicationContext == null)
			{
				return;
			}
			this._command.AddProperty(CommandProperties.ApplicationContext, applicationContext);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022BB File Offset: 0x000004BB
		public Task CancelAsync()
		{
			return Utilities.RunSynchronously(new Action(this.CancelImpl));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022D0 File Offset: 0x000004D0
		private void CancelImpl()
		{
			try
			{
				this._command.Cancel();
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failure during Cancel.", new object[0]);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002314 File Offset: 0x00000514
		public void Close()
		{
			if (this._executionMetricsDataReader != null)
			{
				this._executionMetricsDataReader.Close();
			}
			this._command.Close();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002334 File Offset: 0x00000534
		public void Dispose()
		{
			if (this._executionMetricsDataReader != null)
			{
				this._executionMetricsDataReader.Dispose();
			}
			this._command.Dispose();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002354 File Offset: 0x00000554
		public void ExecuteNonQuery(CommandType commandType, string commandText, string operationName)
		{
			throw new NotImplementedException("ExecuteNonQuery not implemented");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002360 File Offset: 0x00000560
		public void AddParameter(string parameterName, object parameterValue)
		{
			throw new NotImplementedException("AddParameter not implemented");
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000236C File Offset: 0x0000056C
		private static ExecutionMetricsColumnNames ExecutionMetricsColumnNames
		{
			get
			{
				if (Command._executionMetricsColumnNames == null)
				{
					Command._executionMetricsColumnNames = new ExecutionMetricsColumnNames("Id", "ParentId", "Name", "Component", "Start", "End", "Metrics");
				}
				return Command._executionMetricsColumnNames;
			}
		}

		// Token: 0x04000034 RID: 52
		private static ExecutionMetricsColumnNames _executionMetricsColumnNames;

		// Token: 0x04000035 RID: 53
		private readonly Command _command;

		// Token: 0x04000036 RID: 54
		private readonly IPrivateInformationService _piiService;

		// Token: 0x04000037 RID: 55
		private RequestExecutionMetricsKind _executionMetricsKind;

		// Token: 0x04000038 RID: 56
		private int? _executionMetricsMaxEventCount;

		// Token: 0x04000039 RID: 57
		private ExecutionMetricsCache _executionMetricsCache;

		// Token: 0x0400003A RID: 58
		private ExecutionMetricsDataReader _executionMetricsDataReader;
	}
}
