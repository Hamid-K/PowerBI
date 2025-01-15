using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.QueryExecution;
using Microsoft.DataShaping.Processing.QueryExecutionCommon;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DsqGeneration;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.Common;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;
using Microsoft.PowerBI.Query.Contracts;
using MsolapWrapper;

namespace Microsoft.DataShaping.RawDataProcessing
{
	// Token: 0x02000009 RID: 9
	internal class RawDataCommandExecutor : ICommandExecutor
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000207D File Offset: 0x0000027D
		internal RawDataCommandExecutor(ITelemetryService telemetryService, ITracer tracer, IDataShapingDataSourceInfo dataSourceInfo, RawDataDefinition rawDataDefinition, QueryCommandOptions queryCommandOptions, IRawConnectionExtractor connectionExtractor, IPageReaderFactory pageReaderFactory)
		{
			this._telemetryService = telemetryService;
			this._tracer = tracer;
			this._dataSourceInfo = dataSourceInfo;
			this._queryCommandOptions = queryCommandOptions;
			this._rawDataDefinition = rawDataDefinition;
			this._connectionExtractor = connectionExtractor;
			this._pageReaderFactory = pageReaderFactory;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		internal IPageReader GetDataReader()
		{
			IPageReader pageReader;
			try
			{
				pageReader = this._pageReaderFactory.CreatePageReader(this._targetRowset, this._rawDataDefinition.ColumnMapping);
			}
			catch (Exception ex) when (!ErrorUtils.IsStoppingException(ex))
			{
				throw new RawDataException("Failed to read the resulting rowset.", ex, ErrorSource.Unknown, ex.Message.MarkAsCustomerContent());
			}
			return pageReader;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002130 File Offset: 0x00000330
		public bool IsClosed()
		{
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002133 File Offset: 0x00000333
		public Task CloseAsync(bool shouldNotThrow)
		{
			return Task.CompletedTask;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000213C File Offset: 0x0000033C
		public async Task ExecuteAsync(IDbConnection connection, CancellationToken token, CancellationToken internalCancelToken)
		{
			Action <>9__1;
			await Task.Run(delegate
			{
				ITelemetryService telemetryService = this._telemetryService;
				ActivityKind activityKind = ActivityKind.ExecuteReader;
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						try
						{
							this._targetRowset = this.GetRowSet(connection);
						}
						catch (DataShapeEngineException ex)
						{
							this._tracer.TraceSanitizedQueryError(ex);
							throw;
						}
					});
				}
				telemetryService.RunInActivity(activityKind, action);
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002188 File Offset: 0x00000388
		private Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.IRowset GetRowSet(IDbConnection connection)
		{
			IDBCreateSession idbcreateSession;
			if (!this._connectionExtractor.TryExtractRawConnection(connection, out idbcreateSession))
			{
				throw new InvalidOperationException("Got a non-MSOLAP connection from the pool.");
			}
			Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.IRowset rowset;
			try
			{
				object obj = ((IDBCreateCommand)idbcreateSession.CreateSession()).CreateCommand();
				ICommandProperties commandProperties = (ICommandProperties)obj;
				commandProperties.TrySetValue(DBPROPGROUP.Rowset, DBPROPID.BOOKMARKS, false);
				commandProperties.TrySetValue(DBPROPGROUP.Rowset, DBPROPID.COMMANDTIMEOUT, this._queryCommandOptions.Timeout);
				commandProperties.TrySetValue(DBPROPGROUP.Rowset, DBPROPID.CANFETCHBACKWARDS, false);
				commandProperties.TrySetValue(DBPROPGROUP.Rowset, DBPROPID.CANSCROLLBACKWARDS, false);
				int? num;
				this.SetMSOLAPProperties(commandProperties, out num);
				ICommandText commandText = (ICommandText)obj;
				commandText.SetCommand(DBGUID.Default, this._rawDataDefinition.DaxCommand);
				this._tracer.TraceSanitizedQuery(this._queryCommandOptions.MemoryLimit, this._queryCommandOptions.Timeout, this._queryCommandOptions.RequestPriority, RequestExecutionMetricsKind.None, this._dataSourceInfo.Category, this._rawDataDefinition.DaxCommand, this._queryCommandOptions.ApplicationContextObject, num);
				rowset = commandText.Execute(null);
			}
			catch (Exception ex) when (!ErrorUtils.IsStoppingException(ex))
			{
				throw new RawDataException("Failed to execute OleDb command.", ex, ErrorSource.Unknown, ex.Message.MarkAsCustomerContent());
			}
			return rowset;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022E8 File Offset: 0x000004E8
		private void SetMSOLAPProperties(ICommandProperties targetCommandProperties, out int? appContextLength)
		{
			targetCommandProperties.TrySetValue(ProviderSpecificPropertyGroups.MSOLAP.MSOLAPCommand, (DBPROPID)4209U, this._queryCommandOptions.MemoryLimit);
			this.SetCommandRequestPriority(targetCommandProperties);
			this.SetCommandTelemetryIds(targetCommandProperties);
			this.SetApplicationContext(targetCommandProperties, out appContextLength);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002324 File Offset: 0x00000524
		private void SetCommandRequestPriority(ICommandProperties targetCommandProperties)
		{
			RequestPriorityKind requestPriority = this._queryCommandOptions.RequestPriority;
			if (requestPriority == RequestPriorityKind.Normal)
			{
				return;
			}
			if (requestPriority == RequestPriorityKind.Low)
			{
				targetCommandProperties.TrySetValue(ProviderSpecificPropertyGroups.MSOLAP.MSOLAPCommand, (DBPROPID)4221U, CommandRequestPriorityKind.PF_REQUEST_PRIORITY_LOW);
				return;
			}
			throw new NotSupportedException(StringUtil.FormatInvariant("Invalid QueryRequestPriority specified: {0}", new object[] { this._queryCommandOptions.RequestPriority }));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002388 File Offset: 0x00000588
		private void SetCommandTelemetryIds(ICommandProperties targetCommandProperties)
		{
			string text;
			string text2;
			string text3;
			if (this._telemetryService.TryGetTelemetryIDs(out text, out text2, out text3))
			{
				targetCommandProperties.TrySetValue(ProviderSpecificPropertyGroups.MSOLAP.MSOLAPCommand, (DBPROPID)4181U, text);
				targetCommandProperties.TrySetValue(ProviderSpecificPropertyGroups.MSOLAP.MSOLAPCommand, (DBPROPID)4210U, text2);
				targetCommandProperties.TrySetValue(ProviderSpecificPropertyGroups.MSOLAP.MSOLAPCommand, (DBPROPID)4182U, text3);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023E0 File Offset: 0x000005E0
		private void SetApplicationContext(ICommandProperties targetCommandProperties, out int? appContextLength)
		{
			appContextLength = null;
			if (this._queryCommandOptions.ApplicationContextObject == null)
			{
				return;
			}
			string text = ApplicationContextSerializer.Serialize(this._queryCommandOptions.ApplicationContextObject);
			appContextLength = new int?(text.Length);
			targetCommandProperties.TrySetValue(ProviderSpecificPropertyGroups.MSOLAP.MSOLAPCommand, (DBPROPID)4225U, text);
		}

		// Token: 0x04000029 RID: 41
		internal const int DBPROP_MSMD_APPLICATIONCONTEXT = 4225;

		// Token: 0x0400002A RID: 42
		private readonly ITelemetryService _telemetryService;

		// Token: 0x0400002B RID: 43
		private readonly IDataShapingDataSourceInfo _dataSourceInfo;

		// Token: 0x0400002C RID: 44
		private readonly ITracer _tracer;

		// Token: 0x0400002D RID: 45
		private readonly QueryCommandOptions _queryCommandOptions;

		// Token: 0x0400002E RID: 46
		private readonly RawDataDefinition _rawDataDefinition;

		// Token: 0x0400002F RID: 47
		private readonly IRawConnectionExtractor _connectionExtractor;

		// Token: 0x04000030 RID: 48
		private readonly IPageReaderFactory _pageReaderFactory;

		// Token: 0x04000031 RID: 49
		private Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.IRowset _targetRowset;
	}
}
