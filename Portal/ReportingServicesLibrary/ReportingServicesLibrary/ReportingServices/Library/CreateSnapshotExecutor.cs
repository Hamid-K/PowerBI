using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200008E RID: 142
	internal class CreateSnapshotExecutor : StandaloneExecutionBase<BaseReportCatalogItem>
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00017F08 File Offset: 0x00016108
		internal CreateSnapshotExecutor(BaseReportCatalogItem item, bool isHistory)
			: base(item)
		{
			this.m_isForHistory = isHistory;
			base.UsePermanentSnapshot = isHistory;
			item.ThrowIfNotUsableByProperties();
			if (item.ThisItemType == ItemType.LinkedReport)
			{
				CatalogItemPath pathById = base.Service.Storage.GetPathById(((LinkedReportCatalogItem)base.Item).LinkID);
				base.ItemContext.SetReportDefinitionPath(base.Service.CatalogToExternal(pathById));
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00017F71 File Offset: 0x00016171
		protected override bool IsHistorySnapshotGeneration
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_isForHistory;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00017F7C File Offset: 0x0001617C
		internal DateTime ExecutionDateUtcCatalogPrecision
		{
			get
			{
				return new DateTime(base.ExecutionTime.Year, base.ExecutionTime.Month, base.ExecutionTime.Day, base.ExecutionTime.Hour, base.ExecutionTime.Minute, base.ExecutionTime.Second);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00017FE2 File Offset: 0x000161E2
		internal OnDemandProcessingResult ProcessingResult
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_procResult;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00017FEC File Offset: 0x000161EC
		protected override void CallProcessing()
		{
			ReportProcessing.ExecutionType executionType;
			using (SurrogateContextFactory.CreateContext(out executionType))
			{
				UserProfileState userProfileState = UserProfileState.InReport;
				UserProfileState userProfileState2 = base.Item.Properties.DependsOnUser;
				if (base.Item.ThisItemType == ItemType.LinkedReport)
				{
					LinkedReportProperyResolver linkedReportProperyResolver = new LinkedReportProperyResolver(base.ItemContext.ReportDefinitionAsExternalItemPath, base.Service);
					linkedReportProperyResolver.Resolve();
					userProfileState2 = linkedReportProperyResolver.DependsOnUser;
				}
				ReportProcessing processingEngine = Global.GetProcessingEngine();
				using (base.Item.ReportCompiledDefinition.EnterTransactionContext())
				{
					using (ISubreportRetrieval subreportRetrieval = SubreportRetrieval.Create(base.Item.ReportCompiledDefinition, base.Service))
					{
						if (this.IsHistorySnapshotGeneration)
						{
							subreportRetrieval.InitializeRdlMapping();
						}
						IChunkFactory chunkFactory = ReadOnlyChunkFactory.FromSnapshot(base.Item.ReportCompiledDefinition);
						ReportProcessingContext reportProcessingContext = new ReportProcessingContext(base.ItemContext, base.UserName, base.Item.GetCombinedRequestParameters(), base.Item.RuntimeDataSources, base.Item.RuntimeSharedDataSets, new ReportProcessing.OnDemandSubReportCallback(subreportRetrieval.GetSubreport), new ServerGetResourceForProcessing(base.Service), chunkFactory, executionType, Localization.ClientPrimaryCulture, userProfileState, userProfileState2, new ServerDataExtensionConnection(base.Service.HowToCreateDataExtensionInstance, base.Service.UserContext, executionType, new ServerAdditionalToken(base.Service, base.ItemContext)), ReportRuntimeSetup.GetDefault(), new CreateAndRegisterStream(base.Service.StreamManager.GetNewStream), this.IsHistorySnapshotGeneration, ServerJobContext.ConstructJobContext(Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext), new ServerExtensionFactory(), DataProtection.Instance, new SharedDataSetExecution(new RSServiceDataProvider(base.Service), base.Item.ReportCompiledDefinition));
						DateTime executionDateNotTruncatedLocalTime = base.ExecutionDateNotTruncatedLocalTime;
						bool flag;
						processingEngine.ProcessReportParameters(executionDateNotTruncatedLocalTime, reportProcessingContext, false, out flag);
						base.TargetSnapshot = base.Service.AllocateNewReportSnapshot(base.UsePermanentSnapshot, base.Item.GetCombinedRequestParameters(), executionDateNotTruncatedLocalTime, base.Item.Description, chunkFactory.ReportProcessingFlags);
						subreportRetrieval.SetTargetSnapshot(base.TargetSnapshot);
						if (this.IsHistorySnapshotGeneration)
						{
							RSTrace.CatalogTrace.Assert(subreportRetrieval.StoreRdlChunks, "StoreRdlChunks");
							RSTrace.CatalogTrace.Assert(subreportRetrieval.RdlChunkMapper != null, "RdlChunkMapper");
							bool flag2;
							string rdlChunkName = subreportRetrieval.RdlChunkMapper.GetRdlChunkName(base.ItemContext, true, out flag2);
							if (flag2)
							{
								CreateSnapshotExecutor.CreateRdlChunkHelper(base.Item.ItemID, base.TargetSnapshot, rdlChunkName);
							}
						}
						base.Item.CompiledDefinition.PrepareExecutionSnapshot(base.TargetSnapshot, null);
						reportProcessingContext.ChunkFactory = base.TargetSnapshot;
						reportProcessingContext.DataSetExecute.TargetSnapshot = base.TargetSnapshot;
						Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.ExecutionInfo.Parameters = reportProcessingContext.Parameters.ToUrl(false);
						base.Service.Storage.Commit();
						using (ISnapshotTransaction snapshotTransaction2 = base.TargetSnapshot.EnterTransactionContext())
						{
							ReadOnlyChunkFactory readOnlyChunkFactory = ReadOnlyChunkFactory.FromSnapshot(base.Item.ReportCompiledDefinition);
							this.m_procResult = processingEngine.CreateSnapshot(executionDateNotTruncatedLocalTime, reportProcessingContext, readOnlyChunkFactory);
							if (this.IsHistorySnapshotGeneration)
							{
								subreportRetrieval.RdlChunkMapper.CreateChunkAndSerializeRdlMapping(base.TargetSnapshot);
							}
							snapshotTransaction2.Commit();
						}
						if (this.m_procResult != null)
						{
							if ((this.m_procResult.UsedUserProfileState & UserProfileState.InReport) != UserProfileState.None)
							{
								base.TargetSnapshot.MarkAsDependentOnUser();
							}
							PaginationMode paginationMode = PaginationMode.Progressive;
							base.Service.Storage.PromoteSnapshotInfo(base.TargetSnapshot, this.m_procResult.NumberOfPages, this.m_procResult.HasDocumentMap, paginationMode, this.m_procResult.UpdatedReportProcessingFlags);
							base.Service.Storage.Commit();
						}
					}
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000183F4 File Offset: 0x000165F4
		internal static void CreateRdlChunkHelper(Guid itemId, ReportSnapshot targetSnapshot, string chunkName)
		{
			RSTrace.CatalogTrace.Assert(itemId != Guid.Empty, "itemId");
			RSTrace.CatalogTrace.Assert(targetSnapshot != null, "targetSnapshot");
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(chunkName), "chunkName");
			DBInterface dbinterface = new DBInterface();
			dbinterface.ConnectionManager = ConnectionManager.Create(ConnectionTransactionType.Explicit, IsolationLevel.ReadUncommitted);
			dbinterface.ConnectionManager.WillDisconnectStorage();
			try
			{
				dbinterface.CreateRdlChunk(itemId, targetSnapshot, chunkName);
				dbinterface.Commit();
			}
			finally
			{
				dbinterface.ConnectionManager.DisconnectStorage();
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00018498 File Offset: 0x00016698
		internal void UpdateExecutionSnapshot()
		{
			base.Service.ExecCacheDb.UpdateSnapshot(base.Item.ItemContext, base.Item.ItemID, base.ExecutionDateNotTruncatedLocalTime, base.TargetSnapshot);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000184CC File Offset: 0x000166CC
		internal void AddSnapshotToHistory(bool usedByExecutionSnapshot)
		{
			int num = (usedByExecutionSnapshot ? 0 : (-1));
			if (!base.Service.Storage.AddHistoryRecord(Guid.NewGuid(), base.Item.ItemID, this.ExecutionDateUtcCatalogPrecision, base.TargetSnapshot, num))
			{
				if (!usedByExecutionSnapshot)
				{
					base.TargetSnapshot.DeleteSnapshotAndChunks();
				}
				this.m_procResult = null;
				return;
			}
			int num2 = base.Item.HistorySnapshotLimit;
			if (num2 == -2)
			{
				num2 = base.Service.SystemSnapshotLimit;
			}
			if (num2 == 0)
			{
				throw new InternalCatalogException("historyLimit is set to zero on CreateSnapshot.");
			}
			if (num2 > 0)
			{
				base.Service.Storage.CleanHistoryForReport(base.Item.ItemID, num2);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00018572 File Offset: 0x00016772
		internal ProcessingMessageList Messages
		{
			get
			{
				if (this.m_procResult != null)
				{
					return this.m_procResult.Warnings;
				}
				return null;
			}
		}

		// Token: 0x04000323 RID: 803
		private readonly bool m_isForHistory;

		// Token: 0x04000324 RID: 804
		private OnDemandProcessingResult m_procResult;
	}
}
