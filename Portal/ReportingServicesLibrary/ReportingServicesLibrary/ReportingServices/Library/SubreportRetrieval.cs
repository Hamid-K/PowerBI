using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D6 RID: 726
	internal class SubreportRetrieval : ISubreportRetrieval, IRdlPersistence, IDisposable
	{
		// Token: 0x060019FB RID: 6651 RVA: 0x00068C1D File Offset: 0x00066E1D
		public static ISubreportRetrieval Create(ReportSnapshot compiledDefinition, RSService service)
		{
			return new SubreportRetrieval(compiledDefinition, service);
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00068C26 File Offset: 0x00066E26
		public static ISubreportRetrieval Create(SnapshotManager snapshotManager, RSService service)
		{
			return new SubreportRetrieval.SubreportSnapshotManagerRetrieval(snapshotManager, service);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00068C2F File Offset: 0x00066E2F
		protected SubreportRetrieval(ReportSnapshot compiledDefinition, RSService service)
		{
			RSTrace.CatalogTrace.Assert(compiledDefinition != null, "compiledDefinition");
			RSTrace.CatalogTrace.Assert(service != null, "service");
			this.m_service = service;
			this.m_originalCompiledDefinition = compiledDefinition;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00068C6C File Offset: 0x00066E6C
		public void GetSubreportDataSources(ICatalogItemContext reportContext, string subreportPath, ReportProcessing.NeedsUpgrade needsUpgradeCallback, out ICatalogItemContext subreportContext, out IChunkFactory compiledDefinitionChunkFactory, out DataSourceInfoCollection dataSources, out DataSetInfoCollection dataSets)
		{
			subreportContext = reportContext.GetSubreportContext(subreportPath);
			CatalogItemContext catalogItemContext = subreportContext as CatalogItemContext;
			RSTrace.CatalogTrace.Assert(catalogItemContext != null);
			CatalogItem catalogItem = this.m_service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
			baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadProperties);
			ReportCompiledDefinition reportCompiledDefinition = ReportCompiledDefinition.Load(catalogItemContext, this.m_service, SecurityRequirements.GenerateForLoadCompiledDefinition(this.m_service.SecMgr, this.m_service.UserName), needsUpgradeCallback, true);
			SubreportRetrieval.HookTransactionContext(this.m_originalCompiledDefinition, reportCompiledDefinition.DefinitionSnapshot);
			compiledDefinitionChunkFactory = reportCompiledDefinition.DefinitionSnapshot;
			dataSets = baseReportCatalogItem.SharedDataSets;
			dataSources = this.m_service.CombineDataSources(dataSets, baseReportCatalogItem.DataSources);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00068D34 File Offset: 0x00066F34
		public void GetSubreport(ICatalogItemContext reportContext, string subreportPath, string newChunkName, ReportProcessing.NeedsUpgrade needsUpgradeCallback, ParameterInfoCollection parentQueryParameters, out ICatalogItemContext subreportContext, out string description, out IChunkFactory compiledDefinitionChunkFactory, out ParameterInfoCollection parameters)
		{
			subreportContext = reportContext.GetSubreportContext(subreportPath);
			CatalogItemContext catalogItemContext = subreportContext as CatalogItemContext;
			RSTrace.CatalogTrace.Assert(catalogItemContext != null);
			ItemType itemType;
			string text;
			Guid guid;
			byte[] array;
			if (!this.m_service.Storage.GetParameters(catalogItemContext.CatalogItemPath, out itemType, out text, out guid, out array))
			{
				throw new ItemNotFoundException(catalogItemContext.OriginalItemPath.Value);
			}
			RSService.EnsureItemTypeIsReport(itemType, catalogItemContext.OriginalItemPath.Value);
			parameters = ParameterInfoCollection.DecodeFromXml(text);
			CatalogItemContext catalogItemContext2 = reportContext as CatalogItemContext;
			RSTrace.CatalogTrace.Assert(catalogItemContext2 != null);
			ReportCompiledDefinition reportCompiledDefinition = ReportCompiledDefinition.Load(catalogItemContext, this.m_service, SecurityRequirements.GenerateForExecuteReport(this.m_service.SecMgr, this.m_service.UserName), needsUpgradeCallback, true, new SubreportRetrieval.ParentReportContext(parentQueryParameters, catalogItemContext2));
			description = reportCompiledDefinition.Description;
			if (ExecutionOptions.IsSnapshotExecution(reportCompiledDefinition.ExecutionOptions))
			{
				throw new SubreportFromSnapshotException();
			}
			if (this.StoreRdlChunks)
			{
				RSTrace.CatalogTrace.Assert(this.RdlChunkMapper != null, "RdlChunkMapper");
				bool flag;
				string rdlChunkName = this.RdlChunkMapper.GetRdlChunkName(catalogItemContext, false, out flag);
				if (flag)
				{
					this.CreateRdlChunk(guid, rdlChunkName);
				}
			}
			this.Service.Storage.Commit();
			compiledDefinitionChunkFactory = reportCompiledDefinition.DefinitionSnapshot;
			this.FinishGetSubreport(reportCompiledDefinition.DefinitionSnapshot, newChunkName);
			if (reportCompiledDefinition.IsRdceReport)
			{
				if (this.m_rdceSnapshots == null)
				{
					this.m_rdceSnapshots = new List<ReportSnapshot>();
				}
				this.m_rdceSnapshots.Add(reportCompiledDefinition.DefinitionSnapshot);
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00068EAB File Offset: 0x000670AB
		public virtual void SetTargetSnapshot(ReportSnapshot target)
		{
			if (this.m_targetSnapshot != null)
			{
				throw new InternalCatalogException("Attempt to set target multiple times");
			}
			this.m_targetSnapshot = target;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00068EC8 File Offset: 0x000670C8
		public void Dispose()
		{
			if (this.m_rdceSnapshots != null)
			{
				foreach (ReportSnapshot reportSnapshot in this.m_rdceSnapshots)
				{
					reportSnapshot.DeleteSnapshotAndChunks();
				}
				this.m_rdceSnapshots = null;
			}
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00068F28 File Offset: 0x00067128
		public void InitializeRdlMapping()
		{
			RSTrace.CatalogTrace.Assert(this.m_chunkMapper == null, "already initialized");
			this.m_chunkMapper = new RdlChunkMapper();
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00068F4D File Offset: 0x0006714D
		public void ResetRdlMapping()
		{
			this.m_chunkMapper = null;
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00068F56 File Offset: 0x00067156
		public bool StoreRdlChunks
		{
			[DebuggerStepThrough]
			get
			{
				return this.RdlChunkMapper != null;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x00068F61 File Offset: 0x00067161
		public RdlChunkMapper RdlChunkMapper
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_chunkMapper;
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00068F69 File Offset: 0x00067169
		protected virtual void CreateRdlChunk(Guid itemId, string chunkName)
		{
			RSTrace.CatalogTrace.Assert(this.m_targetSnapshot != null, "m_targetSnapshot");
			CreateSnapshotExecutor.CreateRdlChunkHelper(itemId, this.m_targetSnapshot, chunkName);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00068F90 File Offset: 0x00067190
		protected virtual void FinishGetSubreport(ReportSnapshot subreportCompiledDefinition, string newChunkName)
		{
			RSTrace.CatalogTrace.Assert(this.m_targetSnapshot != null, "m_targetSnapshot");
			SubreportRetrieval.HookTransactionContext(this.m_targetSnapshot, subreportCompiledDefinition);
			subreportCompiledDefinition.PrepareExecutionSnapshot(this.m_targetSnapshot, newChunkName);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00068FC4 File Offset: 0x000671C4
		protected static void HookTransactionContext(ReportSnapshot originalCompiledDefinition, ReportSnapshot subreportCompiledDefinition)
		{
			originalCompiledDefinition.ShareTransactionContext(subreportCompiledDefinition);
			originalCompiledDefinition.PreTransactionEvent += delegate(object sender, ServerSnapshot.SnapshotTransactionEventArgs eventArgs)
			{
				subreportCompiledDefinition.EnsureAllStreamsAreClosed(false);
			};
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00068FFD File Offset: 0x000671FD
		protected RSService Service
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x04000971 RID: 2417
		private readonly ReportSnapshot m_originalCompiledDefinition;

		// Token: 0x04000972 RID: 2418
		private readonly RSService m_service;

		// Token: 0x04000973 RID: 2419
		private RdlChunkMapper m_chunkMapper;

		// Token: 0x04000974 RID: 2420
		private ReportSnapshot m_targetSnapshot;

		// Token: 0x04000975 RID: 2421
		private List<ReportSnapshot> m_rdceSnapshots;

		// Token: 0x020004E6 RID: 1254
		private class SubreportSnapshotManagerRetrieval : SubreportRetrieval
		{
			// Token: 0x060024A5 RID: 9381 RVA: 0x00086C02 File Offset: 0x00084E02
			public SubreportSnapshotManagerRetrieval(SnapshotManager snapshotManager, RSService service)
				: base(snapshotManager.ChunkTargetSnapshot, service)
			{
				this.m_snapshotManager = snapshotManager;
			}

			// Token: 0x060024A6 RID: 9382 RVA: 0x00086C18 File Offset: 0x00084E18
			public override void SetTargetSnapshot(ReportSnapshot target)
			{
				throw new InternalCatalogException("SetTargetSnapshot not supported on SubreportSnapshotManagerRetrieval");
			}

			// Token: 0x060024A7 RID: 9383 RVA: 0x00086C24 File Offset: 0x00084E24
			protected override void CreateRdlChunk(Guid itemId, string chunkName)
			{
				object snapshotUpdateSync = this.m_snapshotManager.SnapshotUpdateSync;
				lock (snapshotUpdateSync)
				{
					if (RSTrace.ChunkTracer.TraceVerbose)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Creating RDL chunk (ItemId = {0}, ChunkName= {1}) in Snapshot {2}", new object[]
						{
							itemId,
							chunkName,
							this.m_snapshotManager.ChunkTargetSnapshot.SnapshotDataID
						});
					}
					base.Service.Storage.CreateRdlChunk(itemId, this.m_snapshotManager.ChunkTargetSnapshot, chunkName);
				}
			}

			// Token: 0x060024A8 RID: 9384 RVA: 0x00086CC8 File Offset: 0x00084EC8
			protected override void FinishGetSubreport(ReportSnapshot subreportCompiledDefinition, string newChunkName)
			{
				if (this.m_snapshotManager == null || this.m_snapshotManager.ChunkTargetSnapshot == null)
				{
					throw new InternalCatalogException("Snapshot data id is an empty GUID in report callback.");
				}
				object snapshotUpdateSync = this.m_snapshotManager.SnapshotUpdateSync;
				lock (snapshotUpdateSync)
				{
					subreportCompiledDefinition.PrepareExecutionSnapshot(this.m_snapshotManager.ChunkTargetSnapshot, newChunkName);
					if (RSTrace.ChunkTracer.TraceVerbose)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Using shared transaction to retrieve compiled definition for snapshot '{0}.", new object[] { subreportCompiledDefinition.SnapshotDataID });
					}
					SubreportRetrieval.HookTransactionContext(this.m_snapshotManager.ChunkTargetSnapshot, subreportCompiledDefinition);
				}
			}

			// Token: 0x04001146 RID: 4422
			private readonly SnapshotManager m_snapshotManager;
		}

		// Token: 0x020004E7 RID: 1255
		internal class ParentReportContext
		{
			// Token: 0x060024A9 RID: 9385 RVA: 0x00086D7C File Offset: 0x00084F7C
			internal ParentReportContext(ParameterInfoCollection parameters, CatalogItemContext itemContext)
			{
				this.m_parameters = parameters;
				this.m_itemContext = itemContext;
			}

			// Token: 0x17000AA9 RID: 2729
			// (get) Token: 0x060024AA RID: 9386 RVA: 0x00086D92 File Offset: 0x00084F92
			internal ParameterInfoCollection Parameters
			{
				get
				{
					return this.m_parameters;
				}
			}

			// Token: 0x17000AAA RID: 2730
			// (get) Token: 0x060024AB RID: 9387 RVA: 0x00086D9A File Offset: 0x00084F9A
			internal CatalogItemContext ItemContext
			{
				get
				{
					return this.m_itemContext;
				}
			}

			// Token: 0x04001147 RID: 4423
			private ParameterInfoCollection m_parameters;

			// Token: 0x04001148 RID: 4424
			private CatalogItemContext m_itemContext;
		}
	}
}
