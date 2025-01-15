using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000609 RID: 1545
	internal sealed class YukonProcessingResult : OnDemandProcessingResult
	{
		// Token: 0x06005532 RID: 21810 RVA: 0x001674D4 File Offset: 0x001656D4
		internal YukonProcessingResult(ReportSnapshot newSnapshot, ChunkManager.ProcessingChunkManager chunkManager, IChunkFactory createChunkFactory, ParameterInfoCollection parameters, int autoRefresh, int numberOfPages, ProcessingMessageList warnings, bool renderingInfoChanged, RenderingInfoManager renderingInfoManager, bool eventInfoChanged, EventInformation newEventInfo, PaginationMode updatedPaginationMode, ReportProcessingFlags updatedProcessingFlags, UserProfileState usedUserProfileState, ExecutionLogContext executionLogContext)
			: base(createChunkFactory, newSnapshot.HasDocumentMap, newSnapshot.HasShowHide, parameters, autoRefresh, numberOfPages, warnings, eventInfoChanged, newEventInfo, updatedPaginationMode, updatedProcessingFlags, usedUserProfileState, executionLogContext)
		{
			this.m_snapshotChanged = this.Initialize(newSnapshot, chunkManager, renderingInfoChanged, renderingInfoManager);
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x0016751C File Offset: 0x0016571C
		internal YukonProcessingResult(bool renderingInfoChanged, IChunkFactory createChunkFactory, bool hasInteractivity, RenderingInfoManager renderingInfoManager, bool eventInfoChanged, EventInformation newEventInfo, ParameterInfoCollection parameters, ProcessingMessageList warnings, int autoRefresh, int numberOfPages, PaginationMode updatedPaginationMode, ReportProcessingFlags updatedProcessingFlags, UserProfileState usedUserProfileState, ExecutionLogContext executionLogContext)
			: base(createChunkFactory, false, hasInteractivity, parameters, autoRefresh, numberOfPages, warnings, eventInfoChanged, newEventInfo, updatedPaginationMode, updatedProcessingFlags, usedUserProfileState, executionLogContext)
		{
			this.m_snapshotChanged = this.Initialize(null, null, renderingInfoChanged, renderingInfoManager);
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x00167558 File Offset: 0x00165758
		internal YukonProcessingResult(ReportSnapshot newSnapshot, ChunkManager.ProcessingChunkManager chunkManager, ParameterInfoCollection parameters, int autoRefresh, int numberOfPages, ProcessingMessageList warnings, ReportProcessingFlags updatedProcessingFlags, UserProfileState usedUserProfileState, ExecutionLogContext executionLogContext)
			: base(null, newSnapshot.HasDocumentMap, newSnapshot.HasShowHide, parameters, autoRefresh, numberOfPages, warnings, false, null, PaginationMode.Progressive, updatedProcessingFlags, usedUserProfileState, executionLogContext)
		{
			this.m_snapshotChanged = this.Initialize(newSnapshot, chunkManager, false, null);
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x00167598 File Offset: 0x00165798
		private bool Initialize(ReportSnapshot newSnapshot, ChunkManager.ProcessingChunkManager chunkManager, bool renderingInfoChanged, RenderingInfoManager renderingInfoManager)
		{
			this.m_newSnapshot = newSnapshot;
			this.m_legacyChunkManager = chunkManager;
			this.m_renderingInfoChanged = renderingInfoChanged;
			this.m_renderingInfoManager = renderingInfoManager;
			return this.m_renderingInfoChanged || this.m_newSnapshot != null;
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x001675CA File Offset: 0x001657CA
		public override bool SnapshotChanged
		{
			get
			{
				return this.m_snapshotChanged;
			}
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x001675D4 File Offset: 0x001657D4
		public override void Save()
		{
			lock (this)
			{
				if (this.m_newSnapshot != null && this.m_legacyChunkManager != null)
				{
					this.m_legacyChunkManager.SaveFirstPage();
					this.m_legacyChunkManager.SaveReportSnapshot(this.m_newSnapshot);
					this.m_newSnapshot = null;
				}
				if (this.m_renderingInfoChanged && this.m_renderingInfoManager != null)
				{
					ChunkFactoryAdapter chunkFactoryAdapter = new ChunkFactoryAdapter(this.m_createChunkFactory);
					this.m_renderingInfoManager.Save(new ReportProcessing.CreateReportChunk(chunkFactoryAdapter.CreateReportChunk));
					this.m_renderingInfoManager = null;
				}
			}
		}

		// Token: 0x04002D15 RID: 11541
		private ReportSnapshot m_newSnapshot;

		// Token: 0x04002D16 RID: 11542
		private ChunkManager.ProcessingChunkManager m_legacyChunkManager;

		// Token: 0x04002D17 RID: 11543
		private bool m_renderingInfoChanged;

		// Token: 0x04002D18 RID: 11544
		private RenderingInfoManager m_renderingInfoManager;

		// Token: 0x04002D19 RID: 11545
		private readonly bool m_snapshotChanged;
	}
}
