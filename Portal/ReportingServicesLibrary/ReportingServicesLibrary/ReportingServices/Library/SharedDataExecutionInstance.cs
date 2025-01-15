using System;
using System.Data;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000130 RID: 304
	internal class SharedDataExecutionInstance
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x0002D99B File Offset: 0x0002BB9B
		internal SharedDataExecutionInstance(IExecutionDataProvider service, ReportSnapshot targetSnapshot)
		{
			this.m_service = service;
			this.m_targetSnapshot = targetSnapshot;
			this.InitExecutionInfo();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002D9B7 File Offset: 0x0002BBB7
		internal SharedDataExecutionInstance(IExecutionDataProvider service, SnapshotManager targetSnapshotManager)
		{
			this.m_service = service;
			this.m_targetSnapshotManager = targetSnapshotManager;
			this.InitExecutionInfo();
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		internal void Execute(DataSetInfo dataSet, string targetChunkNameInReportSnapshot, ParameterInfoCollection dataSetParameterValues, ReportProcessingContext originalProcessingContext, bool originalRequestNeedsDataChunk, IRowConsumer originalRequest)
		{
			try
			{
				this.m_dataSet = dataSet;
				this.m_executionTime = DateTime.Now;
				this.m_executionInfo.ItemPath = this.m_service.PathTranslator.CatalogToExternal(this.m_dataSet.AbsolutePath);
				this.m_service.CheckAccess(this.m_dataSet.SecurityDescriptor, ItemType.Report, ReportOperation.ExecuteAndView, this.m_dataSet.AbsolutePath);
				this.m_definition = this.GetDefinition(this.m_dataSet.CompiledDefinitionId);
				this.m_chunkFactory = originalProcessingContext.ChunkFactory;
				ParameterInfoCollection parameterInfoCollection = this.ProcessParameters(dataSetParameterValues, originalProcessingContext, originalRequestNeedsDataChunk, originalRequest);
				bool flag = true;
				bool flag2 = false;
				this.CheckIfCached(parameterInfoCollection, out flag, out flag2);
				RSTrace.CatalogTrace.Assert(this.m_dc != null, "m_dc");
				RuntimeDataSourceInfoCollection dataSources = this.m_dc.DataSources;
				bool flag3 = DataSourceCatalogItem.GoodForUnattendedExecution(dataSources);
				if (dataSources != null && dataSources.HasConnectionStringUseridReference())
				{
					flag3 = false;
				}
				flag2 = flag2 && flag3;
				if (!flag)
				{
					if (flag2)
					{
						bool flag4 = true;
						this.m_cacheData = ReportSnapshot.Create(false, ReportProcessingFlags.OnDemandEngine);
						this.m_cacheData.WriteNewSnapshotToDB(parameterInfoCollection, this.m_executionTime, "");
						using (ISnapshotTransaction snapshotTransaction = this.m_cacheData.EnterTransactionContext())
						{
							this.m_dc.MustCreateDataChunk = true;
							this.CreateDataChunk(this.m_cacheData, "SharedDataSet");
							if (this.m_hasUserDependencies)
							{
								flag4 = false;
							}
							snapshotTransaction.Commit();
						}
						this.m_executionInfo.ByteCount = this.m_cacheData.TotalCreatedChunkLength;
						if (originalRequestNeedsDataChunk)
						{
							this.CopyCachedChunkToTargetSnapshot(targetChunkNameInReportSnapshot);
						}
						this.UpdateCacheSnapshot(flag4);
						this.UpdateContentCacheSnapshot(dataSet, parameterInfoCollection, originalProcessingContext);
					}
					else
					{
						this.CreateDataChunk(originalProcessingContext.ChunkFactory, targetChunkNameInReportSnapshot);
					}
				}
				else if (originalRequestNeedsDataChunk)
				{
					this.CopyCachedChunkToTargetSnapshot(targetChunkNameInReportSnapshot);
					this.ProcessCachedDataChunk(this.m_chunkFactory, targetChunkNameInReportSnapshot);
				}
				else
				{
					using (ISnapshotTransaction snapshotTransaction2 = this.m_cacheData.EnterTransactionContext())
					{
						this.ProcessCachedDataChunk(this.m_cacheData, "SharedDataSet");
						snapshotTransaction2.Commit();
					}
				}
			}
			catch (Exception ex)
			{
				this.m_executionInfo.SetStatusFromException(ex);
				throw;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0002DC2C File Offset: 0x0002BE2C
		public bool HasUserDependencies
		{
			get
			{
				return this.m_hasUserDependencies;
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002DC34 File Offset: 0x0002BE34
		private DataSetDefinition GetDefinition(Guid compiledDefinitionId)
		{
			ReportSnapshot reportSnapshot = ReportSnapshot.Create(compiledDefinitionId, true, false, ReportProcessingFlags.NotSet);
			this.TargetSnapshot.ShareTransactionContext(reportSnapshot);
			return new DataSetDefinition(reportSnapshot);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002DC60 File Offset: 0x0002BE60
		private void CreateDataChunk(IChunkFactory targetChunkFactory, string dataChunkName)
		{
			this.m_dc.TargetChunkNameInSnapshot = dataChunkName;
			this.m_dc.CreateChunkFactory = targetChunkFactory;
			DataSetResult dataSetResult = this.m_repProc.ProcessSharedDataSet(this.m_dc, this.m_definition);
			this.m_hasUserDependencies = (dataSetResult.UsedUserProfileState & UserProfileState.InQuery) > UserProfileState.None;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002DCB0 File Offset: 0x0002BEB0
		private RuntimeDataSourceInfoCollection GetSharedDataSetDataSource(RuntimeDataSourceInfoCollection allDataSources, Guid id)
		{
			RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection = new RuntimeDataSourceInfoCollection();
			DataSourceInfo byID = allDataSources.GetByID(id);
			if (byID != null)
			{
				runtimeDataSourceInfoCollection.Add(byID, null);
			}
			return runtimeDataSourceInfoCollection;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002DCD8 File Offset: 0x0002BED8
		private void CheckIfCached(ParameterInfoCollection dataSetParameterValues, out bool foundInCache, out bool cachingRequested)
		{
			DBInterface dbinterface = new DBInterface(this.m_service.UserContext);
			dbinterface.ConnectionManager = this.TargetSnapshot.ConnectionManager;
			string text = dataSetParameterValues.ToXml(true);
			this.m_executionInfo.Parameters = dataSetParameterValues.ToUrl(false);
			ItemProperties itemProperties;
			if (!dbinterface.GetDataSetForExecution(this.m_dataSet.LinkedSharedDataSetID, text, out foundInCache, out this.m_cacheData, out cachingRequested, out itemProperties))
			{
				throw new ItemNotFoundException(this.m_dataSet.AbsolutePath);
			}
			if (cachingRequested && itemProperties != null && itemProperties.QueryDependsOnUser)
			{
				cachingRequested = false;
				foundInCache = false;
			}
			this.m_executionInfo.Source = (foundInCache ? ExecutionLogExecType.Cache : ExecutionLogExecType.Live);
			if (itemProperties != null && !string.IsNullOrEmpty(itemProperties.QueryExecutionTimeOut))
			{
				int num = 0;
				if (int.TryParse(itemProperties.QueryExecutionTimeOut, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					this.m_definition.DataSetCore.Query.TimeOut = num;
				}
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002DDB3 File Offset: 0x0002BFB3
		private ReportSnapshot TargetSnapshot
		{
			get
			{
				if (this.m_targetSnapshotManager == null)
				{
					return this.m_targetSnapshot;
				}
				return this.m_targetSnapshotManager.ChunkTargetSnapshot;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002DDCF File Offset: 0x0002BFCF
		private void InitExecutionInfo()
		{
			this.m_executionInfo = Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.GetSecondaryExecutionInfo();
			this.m_executionInfo.EventType = ReportEventType.Execute;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002DDF0 File Offset: 0x0002BFF0
		private void UpdateCacheSnapshot(bool storeCache)
		{
			ConnectionManager connectionManager = ConnectionManager.Create(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			connectionManager.WillDisconnectStorage();
			try
			{
				if (storeCache)
				{
					DateTime dateTime;
					new DBInterface(this.m_service.UserContext)
					{
						ConnectionManager = connectionManager
					}.AddReportToExecutionCache(this.m_dataSet.LinkedSharedDataSetID, this.m_cacheData, this.m_executionTime, false, out dateTime);
					new ChunkStorage
					{
						ConnectionManager = connectionManager
					}.DecreaseTransientSnapshotRefcount(this.m_cacheData.SnapshotDataID, this.m_cacheData.IsPermanentSnapshot);
				}
				else
				{
					new ChunkStorage
					{
						ConnectionManager = connectionManager
					}.DeleteSnapshotAndChunks(this.m_cacheData.SnapshotDataID, this.m_cacheData.IsPermanentSnapshot);
				}
			}
			finally
			{
				if (connectionManager != null)
				{
					connectionManager.DisconnectStorage();
				}
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002DEB4 File Offset: 0x0002C0B4
		private void UpdateContentCacheSnapshot(DataSetInfo dataSet, ParameterInfoCollection parameters, ReportProcessingContext originalProcessingContext)
		{
			RSService rsservice = new RSService(false);
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			rsservice.WillDisconnectStorage(connectionManager);
			ContentCacheManagerFactory.CreateJsonContentCache(dataSet.LinkedSharedDataSetID, dataSet.AbsolutePath, parameters, originalProcessingContext.ReportContext.RSRequestParameters, rsservice).CreateOrUpdateCacheIfNeededAsync();
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002DF00 File Offset: 0x0002C100
		private ParameterInfoCollection ProcessParameters(ParameterInfoCollection dataSetParameterValues, ReportProcessingContext pc, bool originalRequestNeedsDataChunk, IRowConsumer originalRequest)
		{
			ParameterInfoCollection parameterInfoCollection = ((!string.IsNullOrEmpty(this.m_dataSet.ParametersXml)) ? ParameterInfoCollection.DecodeFromXml(this.m_dataSet.ParametersXml) : new ParameterInfoCollection());
			parameterInfoCollection = ParameterInfoCollection.Combine(parameterInfoCollection, dataSetParameterValues, true, false, false, true, Localization.ReportParameterCulture);
			this.InitDataSetContext(parameterInfoCollection, pc, originalRequestNeedsDataChunk, originalRequest);
			this.m_repProc.ProcessDataSetParameters(this.m_dc, this.m_definition);
			return parameterInfoCollection;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002DF6C File Offset: 0x0002C16C
		private void InitDataSetContext(ParameterInfoCollection dataSetParameterValues, ReportProcessingContext pc, bool originalRequestNeedsDataChunk, IRowConsumer originalRequest)
		{
			this.m_repProc = new ReportProcessing();
			this.m_repProc.Configuration = Global.ProcessingConfiguration;
			this.m_dc = new DataSetContext("", null, originalRequestNeedsDataChunk, originalRequest, pc.ReportContext, this.GetSharedDataSetDataSource(pc.DataSources, this.m_dataSet.DataSourceId), pc.RequestUserName, this.m_executionTime, dataSetParameterValues, this.m_chunkFactory, pc.InteractiveExecution, pc.UserLanguage, pc.AllowUserProfileState, pc.InitialUserProfileState, pc.CreateDataExtensionInstanceFunction, pc.CreateStreamCallback, pc.ReportRuntimeSetup, ServerJobContext.ConstructJobContext(this.m_executionInfo), pc.DataProtection);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002E014 File Offset: 0x0002C214
		private void CopyCachedChunkToTargetSnapshot(string targetChunkNameInReportSnapshot)
		{
			if (this.m_targetSnapshotManager != null)
			{
				this.m_targetSnapshotManager.VersionSnapshot();
			}
			this.TargetSnapshot.ShareTransactionContext(this.m_cacheData);
			this.m_cacheData.CopyDataChunksTo(this.TargetSnapshot, this.TargetSnapshot.ConnectionManager, "SharedDataSet", targetChunkNameInReportSnapshot);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002E068 File Offset: 0x0002C268
		private void ProcessCachedDataChunk(IChunkFactory cacheChunkFactory, string cachedDataChunkName)
		{
			this.m_dc.CachedDataChunkName = cachedDataChunkName;
			this.m_dc.MustCreateDataChunk = false;
			this.m_dc.CreateChunkFactory = cacheChunkFactory;
			this.m_repProc.ProcessSharedDataSet(this.m_dc, this.m_definition);
		}

		// Token: 0x040004F6 RID: 1270
		private IExecutionDataProvider m_service;

		// Token: 0x040004F7 RID: 1271
		private ReportSnapshot m_targetSnapshot;

		// Token: 0x040004F8 RID: 1272
		private SnapshotManager m_targetSnapshotManager;

		// Token: 0x040004F9 RID: 1273
		private ReportExecutionInfo m_executionInfo;

		// Token: 0x040004FA RID: 1274
		private ReportSnapshot m_cacheData;

		// Token: 0x040004FB RID: 1275
		private DataSetInfo m_dataSet;

		// Token: 0x040004FC RID: 1276
		private DateTime m_executionTime;

		// Token: 0x040004FD RID: 1277
		private DataSetDefinition m_definition;

		// Token: 0x040004FE RID: 1278
		private IChunkFactory m_chunkFactory;

		// Token: 0x040004FF RID: 1279
		private ReportProcessing m_repProc;

		// Token: 0x04000500 RID: 1280
		private DataSetContext m_dc;

		// Token: 0x04000501 RID: 1281
		private bool m_hasUserDependencies;
	}
}
