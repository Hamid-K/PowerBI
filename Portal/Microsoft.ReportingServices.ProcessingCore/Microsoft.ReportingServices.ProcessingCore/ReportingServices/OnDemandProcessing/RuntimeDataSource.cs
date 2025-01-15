using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000801 RID: 2049
	internal abstract class RuntimeDataSource
	{
		// Token: 0x06007244 RID: 29252 RVA: 0x001DB118 File Offset: 0x001D9318
		protected RuntimeDataSource(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, OnDemandProcessingContext processingContext, bool mergeTransactions)
		{
			this.m_report = report;
			this.m_dataSource = dataSource;
			this.m_odpContext = processingContext;
			this.m_runtimeDataSets = null;
			this.m_mergeTran = mergeTransactions;
			this.m_executionMetrics = new DataProcessingMetrics(this.m_odpContext.JobContext, this.m_odpContext.ExecutionLogContext);
			Global.Tracer.Assert(this.m_dataSource.Name != null, "The name of a data source cannot be null.");
		}

		// Token: 0x170026BE RID: 9918
		// (get) Token: 0x06007245 RID: 29253 RVA: 0x001DB18D File Offset: 0x001D938D
		internal DataProcessingMetrics ExecutionMetrics
		{
			get
			{
				return this.m_executionMetrics;
			}
		}

		// Token: 0x170026BF RID: 9919
		// (get) Token: 0x06007246 RID: 29254
		internal abstract bool NoRows { get; }

		// Token: 0x170026C0 RID: 9920
		// (get) Token: 0x06007247 RID: 29255 RVA: 0x001DB195 File Offset: 0x001D9395
		protected Microsoft.ReportingServices.ReportIntermediateFormat.Report ReportDefinition
		{
			get
			{
				return this.m_report;
			}
		}

		// Token: 0x170026C1 RID: 9921
		// (get) Token: 0x06007248 RID: 29256 RVA: 0x001DB19D File Offset: 0x001D939D
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataSource DataSourceDefinition
		{
			get
			{
				return this.m_dataSource;
			}
		}

		// Token: 0x170026C2 RID: 9922
		// (get) Token: 0x06007249 RID: 29257 RVA: 0x001DB1A5 File Offset: 0x001D93A5
		protected OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x170026C3 RID: 9923
		// (get) Token: 0x0600724A RID: 29258 RVA: 0x001DB1AD File Offset: 0x001D93AD
		protected virtual bool AllowConcurrentProcessing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170026C4 RID: 9924
		// (get) Token: 0x0600724B RID: 29259 RVA: 0x001DB1B0 File Offset: 0x001D93B0
		protected virtual bool NeedsExecutionLogging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170026C5 RID: 9925
		// (get) Token: 0x0600724C RID: 29260 RVA: 0x001DB1B3 File Offset: 0x001D93B3
		protected virtual bool CreatesDataChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600724D RID: 29261 RVA: 0x001DB1B8 File Offset: 0x001D93B8
		internal virtual void Abort()
		{
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Abort handler called. CanAbort = {1}.", new object[]
				{
					this.m_dataSource.Name,
					this.m_canAbort
				});
			}
			if (!this.m_canAbort || this.m_runtimeDataSets == null)
			{
				return;
			}
			int count = this.m_runtimeDataSets.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_runtimeDataSets[i].Abort();
			}
		}

		// Token: 0x0600724E RID: 29262 RVA: 0x001DB240 File Offset: 0x001D9440
		internal void EraseDataChunk()
		{
			Global.Tracer.Assert(this.CreatesDataChunks, "EraseDataChunk is invalid for the current RuntimeDataSource implementation.");
			if (this.m_runtimeDataSets != null)
			{
				foreach (RuntimeDataSet runtimeDataSet in this.m_runtimeDataSets)
				{
					runtimeDataSet.EraseDataChunk();
				}
			}
		}

		// Token: 0x0600724F RID: 29263 RVA: 0x001DB2B0 File Offset: 0x001D94B0
		protected bool InitializeDataSource(ExecutedQuery existingQuery)
		{
			if (this.m_dataSource.DataSets == null || 0 >= this.m_dataSource.DataSets.Count)
			{
				return false;
			}
			this.m_connection = null;
			this.m_transaction = null;
			this.m_needToCloseConnection = false;
			this.m_isGlobalConnection = false;
			this.m_isTransactionOwner = false;
			this.m_isGlobalTransaction = false;
			this.m_runtimeDataSets = this.CreateRuntimeDataSets();
			if (0 >= this.m_runtimeDataSets.Count)
			{
				return false;
			}
			this.m_canAbort = true;
			this.m_odpContext.CheckAndThrowIfAborted();
			this.m_useConcurrentDataSetProcessing = this.m_runtimeDataSets.Count > 1 && this.AllowConcurrentProcessing;
			if (!this.m_dataSource.IsArtificialForSharedDataSets)
			{
				if (existingQuery != null)
				{
					this.InitializeFromExistingQuery(existingQuery);
				}
				else
				{
					this.OpenInitialConnectionAndTransaction();
				}
			}
			return true;
		}

		// Token: 0x06007250 RID: 29264 RVA: 0x001DB374 File Offset: 0x001D9574
		protected void TeardownDataSource()
		{
			Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Processing of all data sets completed.", new object[] { this.m_dataSource.Name });
			this.m_odpContext.CheckAndThrowIfAborted();
			this.ComputeAndUpdateRowCounts();
			this.CommitTransaction();
		}

		// Token: 0x06007251 RID: 29265 RVA: 0x001DB3B1 File Offset: 0x001D95B1
		protected void HandleException(Exception e)
		{
			if (!(e is ProcessingAbortedException))
			{
				Global.Tracer.Trace(TraceLevel.Error, "Data source '{0}': An error has occurred. Details: {1}", new object[]
				{
					this.m_dataSource.Name,
					e.ToString()
				});
			}
			this.RollbackTransaction();
		}

		// Token: 0x06007252 RID: 29266 RVA: 0x001DB3EE File Offset: 0x001D95EE
		protected virtual void FinalCleanup()
		{
			this.CloseConnection();
		}

		// Token: 0x06007253 RID: 29267 RVA: 0x001DB3F8 File Offset: 0x001D95F8
		private void CloseConnection()
		{
			if (this.m_needToCloseConnection)
			{
				RuntimeDataSource.CloseConnection(this.m_connection, this.m_dataSource, this.m_odpContext, this.m_executionMetrics);
				if (this.NeedsExecutionLogging && this.m_odpContext.ExecutionLogContext != null)
				{
					int num = ((this.m_runtimeDataSets == null) ? 0 : this.m_runtimeDataSets.Count);
					List<DataProcessingMetrics> list = new List<DataProcessingMetrics>();
					for (int i = 0; i < num; i++)
					{
						if (this.m_runtimeDataSets[i].IsConnectionOwner)
						{
							this.m_odpContext.ExecutionLogContext.AddDataSourceParallelExecutionMetrics(this.m_dataSource.Name, this.m_dataSource.DataSourceReference, this.m_dataSource.Type, this.m_runtimeDataSets[i].DataSetExecutionMetrics);
						}
						else
						{
							list.Add(this.m_runtimeDataSets[i].DataSetExecutionMetrics);
						}
					}
					this.m_odpContext.ExecutionLogContext.AddDataSourceMetrics(this.m_dataSource.Name, this.m_dataSource.DataSourceReference, this.m_dataSource.Type, this.m_executionMetrics, list.ToArray());
				}
			}
			this.m_connection = null;
		}

		// Token: 0x06007254 RID: 29268 RVA: 0x001DB523 File Offset: 0x001D9723
		internal void RecordTimeDataRetrieval()
		{
			this.m_odpContext.ExecutionLogContext.AddDataProcessingTime(this.m_executionMetrics.TotalDuration);
		}

		// Token: 0x06007255 RID: 29269 RVA: 0x001DB540 File Offset: 0x001D9740
		internal static DataSourceInfo GetDataSourceInfo(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, OnDemandProcessingContext processingContext)
		{
			if (processingContext.CreateAndSetupDataExtensionFunction.MustResolveSharedDataSources)
			{
				return dataSource.GetDataSourceInfo(processingContext);
			}
			return null;
		}

		// Token: 0x06007256 RID: 29270 RVA: 0x001DB558 File Offset: 0x001D9758
		private void RollbackTransaction()
		{
			if (this.m_transaction == null)
			{
				return;
			}
			this.m_transaction.RollbackRequired = true;
			if (this.m_isGlobalTransaction)
			{
				this.m_odpContext.GlobalDataSourceInfo.Remove(this.m_dataSource.Name);
			}
			if (this.m_isTransactionOwner)
			{
				Global.Tracer.Trace(TraceLevel.Error, "Data source '{0}': Rolling the transaction back.", new object[] { this.m_dataSource.Name });
				try
				{
					this.m_transaction.Transaction.Rollback();
				}
				catch (Exception ex)
				{
					throw new ReportProcessingException(ErrorCode.rsErrorRollbackTransaction, ex, new object[] { this.m_dataSource.Name });
				}
			}
			this.m_transaction = null;
		}

		// Token: 0x06007257 RID: 29271 RVA: 0x001DB614 File Offset: 0x001D9814
		private void CommitTransaction()
		{
			if (this.m_isTransactionOwner)
			{
				if (this.m_isGlobalTransaction)
				{
					if (this.m_isGlobalConnection)
					{
						this.m_needToCloseConnection = false;
					}
				}
				else
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Committing transaction.", new object[] { this.m_dataSource.Name });
					try
					{
						this.m_transaction.Transaction.Commit();
					}
					catch (Exception ex)
					{
						throw new ReportProcessingException(ErrorCode.rsErrorCommitTransaction, ex, new object[] { this.m_dataSource.Name });
					}
				}
				this.m_isTransactionOwner = false;
			}
			this.m_transaction = null;
		}

		// Token: 0x06007258 RID: 29272 RVA: 0x001DB6B8 File Offset: 0x001D98B8
		private void ComputeAndUpdateRowCounts()
		{
			for (int i = 0; i < this.m_runtimeDataSets.Count; i++)
			{
				this.m_executionMetrics.AddRowCount((long)this.m_runtimeDataSets[i].NumRowsRead);
			}
			IJobContext jobContext = this.m_odpContext.JobContext;
			if (this.NeedsExecutionLogging && jobContext != null)
			{
				object syncRoot = jobContext.SyncRoot;
				lock (syncRoot)
				{
					jobContext.RowCount += this.m_executionMetrics.TotalRowsRead;
				}
			}
		}

		// Token: 0x06007259 RID: 29273 RVA: 0x001DB754 File Offset: 0x001D9954
		private void InitializeFromExistingQuery(ExecutedQuery query)
		{
			query.ReleaseOwnership(ref this.m_connection);
			this.m_needToCloseConnection = true;
			this.MergeAutoCollationSettings(this.m_connection);
			this.m_executionMetrics.Add(DataProcessingMetrics.MetricType.OpenConnection, query.ExecutionMetrics.OpenConnectionDurationMs);
			this.m_executionMetrics.ConnectionFromPool = query.ExecutionMetrics.ConnectionFromPool;
			this.m_totalDurationFromExistingQuery = new TimeMetric(query.ExecutionMetrics.TotalDuration);
		}

		// Token: 0x0600725A RID: 29274 RVA: 0x001DB7C4 File Offset: 0x001D99C4
		protected virtual void OpenInitialConnectionAndTransaction()
		{
			if (this.m_dataSource.Transaction && this.m_mergeTran)
			{
				ReportProcessing.DataSourceInfo dataSourceInfo = this.m_odpContext.GlobalDataSourceInfo[this.m_dataSource.Name];
				if (dataSourceInfo != null)
				{
					this.m_connection = dataSourceInfo.Connection;
					this.m_transaction = dataSourceInfo.TransactionInfo;
				}
			}
			Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Transaction = {1}, MergeTran = {2}, NumDataSets = {3}", new object[]
			{
				this.m_dataSource.Name,
				this.m_dataSource.Transaction,
				this.m_mergeTran,
				this.m_runtimeDataSets.Count
			});
			if (this.m_connection == null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_runtimeDataSets[0].DataSet;
				this.m_connection = RuntimeDataSource.OpenConnection(this.m_dataSource, dataSet, this.m_odpContext, this.m_executionMetrics);
				this.m_needToCloseConnection = true;
				Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Created a connection.", new object[] { this.m_dataSource.Name });
			}
			bool flag = false;
			if (this.m_dataSource.Transaction)
			{
				if (this.m_transaction == null)
				{
					IDbTransaction dbTransaction = this.m_connection.BeginTransaction();
					Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Begun a transaction.", new object[] { this.m_dataSource.Name });
					this.m_transaction = new ReportProcessing.TransactionInfo(dbTransaction);
					this.m_isTransactionOwner = true;
				}
				IDbTransactionExtension dbTransactionExtension = this.m_transaction.Transaction as IDbTransactionExtension;
				flag = dbTransactionExtension != null && dbTransactionExtension.AllowMultiConnection;
				this.m_useConcurrentDataSetProcessing = this.m_useConcurrentDataSetProcessing && flag;
				Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': TransactionCanSpanConnections = {1}, ConcurrentDataSets = {2}", new object[]
				{
					this.m_dataSource.Name,
					flag,
					this.m_useConcurrentDataSetProcessing
				});
			}
			this.MergeAutoCollationSettings(this.m_connection);
			if (this.m_isTransactionOwner && this.m_report.SubReportMergeTransactions && !this.m_odpContext.ProcessReportParameters)
			{
				IDbConnection dbConnection;
				if (flag)
				{
					dbConnection = null;
					this.m_isGlobalConnection = false;
				}
				else
				{
					dbConnection = this.m_connection;
					this.m_isGlobalConnection = true;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, "Data source '{0}': Storing trans+conn into GlobalDataSourceInfo. CloseConnection = {1}.", new object[]
				{
					this.m_dataSource.Name,
					this.m_needToCloseConnection
				});
				DataSourceInfo dataSourceInfo2 = RuntimeDataSource.GetDataSourceInfo(this.m_dataSource, this.m_odpContext);
				this.m_odpContext.GlobalDataSourceInfo.Add(this.m_dataSource, dbConnection, this.m_transaction, dataSourceInfo2);
				this.m_isGlobalTransaction = true;
			}
		}

		// Token: 0x0600725B RID: 29275 RVA: 0x001DBA60 File Offset: 0x001D9C60
		private void MergeAutoCollationSettings(IDbConnection connection)
		{
			if (connection is IDbCollationProperties && this.m_dataSource.AnyActiveDataSetNeedsAutoDetectCollation())
			{
				try
				{
					string text;
					bool flag;
					bool flag2;
					bool flag3;
					bool flag4;
					if (((IDbCollationProperties)connection).GetCollationProperties(out text, out flag, out flag2, out flag3, out flag4))
					{
						this.m_dataSource.MergeCollationSettingsForAllDataSets(this.m_odpContext.ErrorContext, text, flag, flag2, flag3, flag4);
					}
				}
				catch (Exception ex)
				{
					this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsCollationDetectionFailed, Severity.Warning, ObjectType.DataSource, this.m_dataSource.Name, "Collation", new string[] { ex.ToString() });
				}
			}
		}

		// Token: 0x0600725C RID: 29276
		protected abstract List<RuntimeDataSet> CreateRuntimeDataSets();

		// Token: 0x0600725D RID: 29277 RVA: 0x001DBB04 File Offset: 0x001D9D04
		internal static IDbConnection OpenConnection(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSourceObj, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSetObj, OnDemandProcessingContext pc, DataProcessingMetrics metrics)
		{
			IDbConnection dbConnection = null;
			try
			{
				metrics.StartTimer(DataProcessingMetrics.MetricType.OpenConnection);
				DataSourceInfo dataSourceInfo = null;
				string text = null;
				if (pc.CreateAndSetupDataExtensionFunction.MustResolveSharedDataSources)
				{
					text = dataSourceObj.ResolveConnectionString(pc, out dataSourceInfo);
					if (pc.UseVerboseExecutionLogging)
					{
						metrics.ResolvedConnectionString = text;
					}
				}
				dbConnection = pc.CreateAndSetupDataExtensionFunction.OpenDataSourceExtensionConnection(dataSourceObj, text, dataSourceInfo, dataSetObj.Name);
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
				throw new ReportProcessingException(ErrorCode.rsErrorOpeningConnection, ex, new object[] { dataSourceObj.Name });
			}
			finally
			{
				long num = metrics.RecordTimerMeasurementWithUpdatedTotal(DataProcessingMetrics.MetricType.OpenConnection);
				Global.Tracer.Trace(TraceLevel.Verbose, "Opening a connection for DataSource: {0} took {1} ms.", new object[] { dataSourceObj.Name, num });
			}
			return dbConnection;
		}

		// Token: 0x0600725E RID: 29278 RVA: 0x001DBBE0 File Offset: 0x001D9DE0
		internal static void CloseConnection(IDbConnection connection, Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, OnDemandProcessingContext odpContext, DataProcessingMetrics executionMetrics)
		{
			try
			{
				DataSourceInfo dataSourceInfo = RuntimeDataSource.GetDataSourceInfo(dataSource, odpContext);
				odpContext.CreateAndSetupDataExtensionFunction.CloseConnection(connection, dataSource, dataSourceInfo);
			}
			catch (Exception ex)
			{
				throw new ReportProcessingException(ErrorCode.rsErrorClosingConnection, ex, new object[] { dataSource.Name });
			}
		}

		// Token: 0x0600725F RID: 29279 RVA: 0x001DBC34 File Offset: 0x001D9E34
		protected bool CheckNoRows(RuntimeDataSet runtimeDataSet)
		{
			return runtimeDataSet != null && runtimeDataSet.NoRows;
		}

		// Token: 0x04003AA6 RID: 15014
		protected readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003AA7 RID: 15015
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;

		// Token: 0x04003AA8 RID: 15016
		protected readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSource m_dataSource;

		// Token: 0x04003AA9 RID: 15017
		protected List<RuntimeDataSet> m_runtimeDataSets;

		// Token: 0x04003AAA RID: 15018
		private bool m_canAbort;

		// Token: 0x04003AAB RID: 15019
		protected TimeMetric m_totalDurationFromExistingQuery;

		// Token: 0x04003AAC RID: 15020
		protected DataProcessingMetrics m_executionMetrics;

		// Token: 0x04003AAD RID: 15021
		private readonly bool m_mergeTran;

		// Token: 0x04003AAE RID: 15022
		protected IDbConnection m_connection;

		// Token: 0x04003AAF RID: 15023
		protected ReportProcessing.TransactionInfo m_transaction;

		// Token: 0x04003AB0 RID: 15024
		private bool m_needToCloseConnection;

		// Token: 0x04003AB1 RID: 15025
		private bool m_isGlobalConnection;

		// Token: 0x04003AB2 RID: 15026
		private bool m_isTransactionOwner;

		// Token: 0x04003AB3 RID: 15027
		private bool m_isGlobalTransaction;

		// Token: 0x04003AB4 RID: 15028
		protected bool m_useConcurrentDataSetProcessing;
	}
}
