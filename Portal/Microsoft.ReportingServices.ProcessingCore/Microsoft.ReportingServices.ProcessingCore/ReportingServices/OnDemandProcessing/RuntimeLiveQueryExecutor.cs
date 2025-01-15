using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000810 RID: 2064
	internal abstract class RuntimeLiveQueryExecutor
	{
		// Token: 0x060072A4 RID: 29348 RVA: 0x001DCAA4 File Offset: 0x001DACA4
		internal RuntimeLiveQueryExecutor(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, OnDemandProcessingContext odpContext)
		{
			this.m_dataSource = dataSource;
			this.m_dataSet = dataSet;
			this.m_odpContext = odpContext;
			this.m_executionMetrics = new DataProcessingMetrics(dataSet, this.m_odpContext.JobContext, this.m_odpContext.ExecutionLogContext);
		}

		// Token: 0x170026D9 RID: 9945
		// (get) Token: 0x060072A5 RID: 29349 RVA: 0x001DCAE3 File Offset: 0x001DACE3
		internal DataProcessingMetrics DataSetExecutionMetrics
		{
			get
			{
				return this.m_executionMetrics;
			}
		}

		// Token: 0x170026DA RID: 9946
		// (get) Token: 0x060072A6 RID: 29350 RVA: 0x001DCAEB File Offset: 0x001DACEB
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSet DataSet
		{
			get
			{
				return this.m_dataSet;
			}
		}

		// Token: 0x170026DB RID: 9947
		// (get) Token: 0x060072A7 RID: 29351 RVA: 0x001DCAF3 File Offset: 0x001DACF3
		internal bool IsConnectionOwner
		{
			get
			{
				return this.m_isConnectionOwner;
			}
		}

		// Token: 0x060072A8 RID: 29352 RVA: 0x001DCAFC File Offset: 0x001DACFC
		internal void Abort()
		{
			IDbCommand command = this.m_command;
			IDbCommand commandWrappedForCancel = this.m_commandWrappedForCancel;
			if (command != null)
			{
				if (Global.Tracer.TraceVerbose)
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Data set '{0}': Cancelling command.", new object[] { this.m_dataSet.Name });
				}
				if (commandWrappedForCancel != null)
				{
					commandWrappedForCancel.Cancel();
					return;
				}
				command.Cancel();
			}
		}

		// Token: 0x060072A9 RID: 29353 RVA: 0x001DCB5A File Offset: 0x001DAD5A
		protected void CloseConnection()
		{
			if (this.m_isConnectionOwner && this.m_dataSourceConnection != null)
			{
				RuntimeDataSource.CloseConnection(this.m_dataSourceConnection, this.m_dataSource, this.m_odpContext, this.m_executionMetrics);
				this.m_dataSourceConnection = null;
			}
		}

		// Token: 0x060072AA RID: 29354 RVA: 0x001DCB90 File Offset: 0x001DAD90
		protected IDataReader RunLiveQuery(List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue> queryParams, object[] paramValues)
		{
			IDataReader dataReader = null;
			IDbCommand dbCommand = null;
			IJobContext jobContext = this.m_odpContext.JobContext;
			if (this.m_dataSourceConnection == null)
			{
				this.m_dataSourceConnection = RuntimeDataSource.OpenConnection(this.m_dataSource, this.m_dataSet, this.m_odpContext, this.m_executionMetrics);
			}
			try
			{
				this.m_executionMetrics.StartTimer(DataProcessingMetrics.MetricType.Query);
				dbCommand = this.CreateCommand();
				this.SetCommandParameters(dbCommand, queryParams, paramValues);
				string text = this.SetCommandText(dbCommand);
				this.StoreCommandText(text);
				this.SetCommandType(dbCommand);
				this.SetTransaction(dbCommand);
				this.m_odpContext.CheckAndThrowIfAborted();
				this.SetCommandTimeout(dbCommand);
				this.ExtractRewrittenCommandText(dbCommand);
				this.SetRestartPosition(dbCommand);
				DataSourceInfo dataSourceInfo = null;
				if (dbCommand is IDbImpersonationNeededForCommandCancel)
				{
					dataSourceInfo = this.m_dataSource.GetDataSourceInfo(this.m_odpContext);
				}
				this.m_command = dbCommand;
				this.m_commandWrappedForCancel = new CommandWrappedForCancel(this.m_command, this.m_odpContext.CreateAndSetupDataExtensionFunction, this.m_dataSource, dataSourceInfo, this.m_dataSet.Name, this.m_dataSourceConnection);
				if (jobContext != null)
				{
					jobContext.SetAdditionalCorrelation(this.m_command);
					jobContext.ApplyCommandMemoryLimit(this.m_command);
				}
				DataSourceErrorInspector dataSourceErrorInspector = this.CreateErrorInspector();
				dataReader = this.ExecuteReader(jobContext, dataSourceErrorInspector, text);
				this.StoreDataReader(dataReader, dataSourceErrorInspector);
			}
			catch (RSException)
			{
				this.EagerInlineCommandAndReaderCleanup(ref dataReader, ref dbCommand);
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				this.EagerInlineCommandAndReaderCleanup(ref dataReader, ref dbCommand);
				throw;
			}
			finally
			{
				this.m_executionMetrics.RecordTimerMeasurement(DataProcessingMetrics.MetricType.Query);
			}
			return dataReader;
		}

		// Token: 0x060072AB RID: 29355
		protected abstract void StoreDataReader(IDataReader dataReader, DataSourceErrorInspector errorInspector);

		// Token: 0x060072AC RID: 29356
		protected abstract void ExtractRewrittenCommandText(IDbCommand command);

		// Token: 0x060072AD RID: 29357 RVA: 0x001DCD44 File Offset: 0x001DAF44
		private IDbCommand CreateCommand()
		{
			IDbCommand dbCommand;
			try
			{
				dbCommand = this.m_dataSourceConnection.CreateCommand();
			}
			catch (Exception ex)
			{
				throw new ReportProcessingException(ErrorCode.rsErrorCreatingCommand, ex, new object[] { this.m_dataSource.Name });
			}
			return dbCommand;
		}

		// Token: 0x060072AE RID: 29358 RVA: 0x001DCD94 File Offset: 0x001DAF94
		private IDataReader ExecuteReader(IJobContext jobContext, DataSourceErrorInspector errorInspector, string commandText)
		{
			IDataReader dataReader = null;
			try
			{
				if (jobContext != null)
				{
					jobContext.AddCommand(this.m_commandWrappedForCancel);
				}
				this.m_executionMetrics.StartTimer(DataProcessingMetrics.MetricType.ExecuteReader);
				try
				{
					dataReader = this.m_command.ExecuteReader(CommandBehavior.SingleResult);
				}
				catch (Exception ex)
				{
					if (this.m_odpContext.ContextMode == OnDemandProcessingContext.Mode.Streaming)
					{
						ErrorCode errorCode = ErrorCode.rsSuccess;
						bool flag = errorInspector != null && errorInspector.TryInterpretProviderErrorCode(ex, out errorCode);
						this.TraceExecuteReaderFailed(ex, commandText, flag ? new ErrorCode?(errorCode) : null);
						if (flag)
						{
							string text = string.Format(CultureInfo.CurrentCulture, RPRes.rsErrorExecutingCommand, this.m_dataSet.Name);
							throw new ReportProcessingQueryException(errorCode, ex, new object[] { text });
						}
						if (errorInspector != null && errorInspector.IsOnPremiseServiceException(ex))
						{
							throw new ReportProcessingQueryOnPremiseServiceException(ErrorCode.rsErrorExecutingCommand, ex, new object[] { this.m_dataSet.Name });
						}
					}
					throw new ReportProcessingException(ErrorCode.rsErrorExecutingCommand, ex, new object[] { this.m_dataSet.Name });
				}
				finally
				{
					this.m_executionMetrics.RecordTimerMeasurement(DataProcessingMetrics.MetricType.ExecuteReader);
				}
			}
			finally
			{
				if (jobContext != null)
				{
					jobContext.RemoveCommand(this.m_commandWrappedForCancel);
				}
			}
			if (dataReader == null)
			{
				if (Global.Tracer.TraceError)
				{
					Global.Tracer.Trace(TraceLevel.Error, "The source data reader is null. Cannot read results.");
				}
				throw new ReportProcessingException(ErrorCode.rsErrorCreatingDataReader, new object[] { this.m_dataSet.Name });
			}
			return dataReader;
		}

		// Token: 0x060072AF RID: 29359 RVA: 0x001DCF14 File Offset: 0x001DB114
		private void TraceExecuteReaderFailed(Exception e, string commandText, ErrorCode? specificErrorCode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("An error occured running the query for DataSet: \"");
			stringBuilder.Append(this.m_dataSet.Name);
			stringBuilder.Append("\"");
			if (specificErrorCode != null)
			{
				stringBuilder.Append(" ErrorCode: \"").Append(specificErrorCode.Value).Append("\"");
			}
			if (this.m_dataSet.Query != null && this.m_dataSet.Query.TimeOut > 0)
			{
				stringBuilder.Append(" Timeout: \"").Append(this.m_dataSet.Query.TimeOut).Append("\"");
			}
			if (!string.IsNullOrEmpty(this.m_dataSource.ConnectionCategory))
			{
				stringBuilder.Append(" ConnectionCategory: \"").Append(this.m_dataSource.ConnectionCategory).Append("\"");
			}
			stringBuilder.Append("Error: \"").Append(e.Message).Append("\"");
			if (!string.IsNullOrEmpty(commandText))
			{
				stringBuilder.Append("Query: ");
				if (commandText.Length > 2048)
				{
					stringBuilder.Append(commandText.Substring(0, 2048));
					stringBuilder.Append(" ...");
				}
				else
				{
					stringBuilder.Append(commandText);
				}
			}
			Global.Tracer.Trace(TraceLevel.Error, stringBuilder.ToString());
		}

		// Token: 0x060072B0 RID: 29360
		protected abstract void SetRestartPosition(IDbCommand command);

		// Token: 0x060072B1 RID: 29361 RVA: 0x001DD080 File Offset: 0x001DB280
		private void SetCommandTimeout(IDbCommand command)
		{
			try
			{
				if (this.m_dataSet.Query.TimeOut == 0 && command is CommandWrapper && ((CommandWrapper)command).UnderlyingCommand is SqlCommand)
				{
					command.CommandTimeout = 2147483646;
				}
				else
				{
					command.CommandTimeout = this.m_dataSet.Query.TimeOut;
				}
			}
			catch (Exception ex)
			{
				throw new ReportProcessingException(ErrorCode.rsErrorSettingQueryTimeout, ex, new object[] { this.m_dataSet.Name });
			}
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x001DD110 File Offset: 0x001DB310
		private void SetTransaction(IDbCommand command)
		{
			if (this.m_transInfo != null)
			{
				try
				{
					command.Transaction = this.m_transInfo.Transaction;
				}
				catch (Exception ex)
				{
					throw new ReportProcessingException(ErrorCode.rsErrorSettingTransaction, ex, new object[] { this.m_dataSet.Name });
				}
			}
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x001DD16C File Offset: 0x001DB36C
		private void SetCommandType(IDbCommand command)
		{
			try
			{
				command.CommandType = (CommandType)this.m_dataSet.Query.CommandType;
			}
			catch (Exception ex)
			{
				throw new ReportProcessingException(ErrorCode.rsErrorSettingCommandType, ex, new object[] { this.m_dataSet.Name });
			}
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x001DD1C4 File Offset: 0x001DB3C4
		internal string SetCommandText(IDbCommand command)
		{
			string text;
			try
			{
				if (this.m_odpContext.UsePreviewCommands && this.m_dataSet.Query.PreviewCommandText != null)
				{
					string previewCommandText = this.m_dataSet.Query.PreviewCommandText;
					command.CommandText = previewCommandText;
					if (this.m_odpContext.UseVerboseExecutionLogging)
					{
						this.m_executionMetrics.CommandText = previewCommandText;
					}
					text = previewCommandText;
				}
				else if (this.m_dataSet.Query.CommandText != null)
				{
					Microsoft.ReportingServices.RdlExpressions.StringResult stringResult = this.m_odpContext.ReportRuntime.EvaluateCommandText(this.m_dataSet);
					if (stringResult.ErrorOccurred)
					{
						throw new ReportProcessingException(ErrorCode.rsQueryCommandTextProcessingError, new object[] { this.m_dataSet.Name });
					}
					command.CommandText = stringResult.Value;
					if (this.m_odpContext.UseVerboseExecutionLogging)
					{
						this.m_executionMetrics.CommandText = stringResult.Value;
					}
					text = stringResult.Value;
				}
				else
				{
					text = null;
				}
			}
			catch (Exception ex)
			{
				throw new ReportProcessingException(ErrorCode.rsErrorSettingCommandText, ex, new object[] { this.m_dataSet.Name });
			}
			return text;
		}

		// Token: 0x060072B5 RID: 29365
		protected abstract void StoreCommandText(string commandText);

		// Token: 0x060072B6 RID: 29366 RVA: 0x001DD2E0 File Offset: 0x001DB4E0
		private void SetCommandParameters(IDbCommand command, List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue> queryParams, object[] paramValues)
		{
			if (queryParams == null)
			{
				return;
			}
			for (int i = 0; i < paramValues.Length; i++)
			{
				if (!this.m_odpContext.IsSharedDataSetExecutionOnly || !((DataSetParameterValue)queryParams[i]).OmitFromQuery)
				{
					IDataParameter dataParameter;
					try
					{
						dataParameter = command.CreateParameter();
					}
					catch (Exception ex)
					{
						throw new ReportProcessingException(ErrorCode.rsErrorCreatingQueryParameter, ex, new object[] { this.m_dataSet.Name });
					}
					dataParameter.ParameterName = queryParams[i].Name;
					IDataUseAllValidValuesParameter dataUseAllValidValuesParameter = dataParameter as IDataUseAllValidValuesParameter;
					if (dataUseAllValidValuesParameter != null)
					{
						dataUseAllValidValuesParameter.UseAllValidValues = queryParams[i].UseAllValidValues;
					}
					object obj = paramValues[i];
					if (obj == null)
					{
						obj = DBNull.Value;
					}
					if (!(dataParameter is IDataMultiValueParameter) && paramValues[i] is ICollection)
					{
						throw new ReportProcessingException(ErrorCode.rsErrorAddingMultiValueQueryParameter, null, new object[]
						{
							this.m_dataSet.Name,
							dataParameter.ParameterName
						});
					}
					if (dataParameter is IDataMultiValueParameter && paramValues[i] is ICollection)
					{
						int count = ((ICollection)obj).Count;
						if (1 == count)
						{
							try
							{
								Global.Tracer.Assert(obj is object[], "(paramValue is object[])");
								dataParameter.Value = (obj as object[])[0];
								goto IL_0199;
							}
							catch (Exception ex2)
							{
								throw new ReportProcessingException(ErrorCode.rsErrorAddingQueryParameter, ex2, new object[] { this.m_dataSource.Name });
							}
						}
						object[] array = new object[count];
						((ICollection)obj).CopyTo(array, 0);
						((IDataMultiValueParameter)dataParameter).Values = array;
					}
					else
					{
						try
						{
							dataParameter.Value = obj;
						}
						catch (Exception ex3)
						{
							throw new ReportProcessingException(ErrorCode.rsErrorAddingQueryParameter, ex3, new object[] { this.m_dataSource.Name });
						}
					}
					IL_0199:
					try
					{
						command.Parameters.Add(dataParameter);
					}
					catch (Exception ex4)
					{
						throw new ReportProcessingException(ErrorCode.rsErrorAddingQueryParameter, ex4, new object[] { this.m_dataSource.Name });
					}
					if (this.m_odpContext.UseVerboseExecutionLogging)
					{
						this.m_executionMetrics.SetQueryParameters(command.Parameters);
					}
				}
			}
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x001DD518 File Offset: 0x001DB718
		protected void EagerInlineCommandAndReaderCleanup(ref IDataReader reader, ref IDbCommand command)
		{
			this.EagerInlineReaderCleanup(ref reader);
			this.EagerInlineCommandCleanup(ref command);
		}

		// Token: 0x060072B8 RID: 29368
		protected abstract void EagerInlineReaderCleanup(ref IDataReader reader);

		// Token: 0x060072B9 RID: 29369 RVA: 0x001DD528 File Offset: 0x001DB728
		private void EagerInlineCommandCleanup(ref IDbCommand command)
		{
			if (this.m_command != null)
			{
				command = null;
				this.DisposeCommand();
				return;
			}
			this.DisposeDataExtensionObject<IDbCommand>(ref command, "command");
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x001DD548 File Offset: 0x001DB748
		protected void DisposeCommand()
		{
			this.m_commandWrappedForCancel = null;
			this.DisposeDataExtensionObject<IDbCommand>(ref this.m_command, "command");
		}

		// Token: 0x060072BB RID: 29371 RVA: 0x001DD564 File Offset: 0x001DB764
		protected void CancelCommand()
		{
			if (this.m_commandWrappedForCancel != null)
			{
				try
				{
					this.m_executionMetrics.StartTimer(DataProcessingMetrics.MetricType.CancelCommand);
					this.m_commandWrappedForCancel.Cancel();
					this.m_executionMetrics.RecordTimerMeasurementWithUpdatedTotal(DataProcessingMetrics.MetricType.CancelCommand);
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					Global.Tracer.Trace(TraceLevel.Warning, "Error occurred while canceling the command for DataSet '" + this.m_dataSet.Name + "'. Details: " + ex.ToString());
				}
			}
		}

		// Token: 0x060072BC RID: 29372 RVA: 0x001DD5F8 File Offset: 0x001DB7F8
		protected void DisposeDataExtensionObject<T>(ref T obj, string objectType) where T : class, IDisposable
		{
			QueryExecutionUtils.DisposeDataExtensionObject<T>(ref obj, objectType, this.m_dataSet.Name);
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x001DD60C File Offset: 0x001DB80C
		protected void DisposeDataExtensionObject<T>(ref T obj, string objectType, DataProcessingMetrics.MetricType? metricType) where T : class, IDisposable
		{
			QueryExecutionUtils.DisposeDataExtensionObject<T>(ref obj, objectType, this.m_dataSet.Name, this.m_executionMetrics, metricType);
		}

		// Token: 0x060072BE RID: 29374 RVA: 0x001DD627 File Offset: 0x001DB827
		private DataSourceErrorInspector CreateErrorInspector()
		{
			if (this.m_odpContext.ReportDefinition != null && this.m_odpContext.ReportDefinition.DataShapes != null)
			{
				return new DataSourceErrorInspector(this.m_dataSourceConnection);
			}
			return null;
		}

		// Token: 0x04003AC4 RID: 15044
		protected readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSource m_dataSource;

		// Token: 0x04003AC5 RID: 15045
		protected readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_dataSet;

		// Token: 0x04003AC6 RID: 15046
		protected readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003AC7 RID: 15047
		protected DataProcessingMetrics m_executionMetrics;

		// Token: 0x04003AC8 RID: 15048
		protected IDbConnection m_dataSourceConnection;

		// Token: 0x04003AC9 RID: 15049
		protected ReportProcessing.TransactionInfo m_transInfo;

		// Token: 0x04003ACA RID: 15050
		protected bool m_isConnectionOwner;

		// Token: 0x04003ACB RID: 15051
		protected IDbCommand m_command;

		// Token: 0x04003ACC RID: 15052
		protected IDbCommand m_commandWrappedForCancel;
	}
}
