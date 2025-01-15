using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000606 RID: 1542
	internal sealed class FullOnDemandProcessingResult : OnDemandProcessingResult
	{
		// Token: 0x06005529 RID: 21801 RVA: 0x0016739C File Offset: 0x0016559C
		internal FullOnDemandProcessingResult(ReportSnapshot newOdpSnapshot, ChunkManager.OnDemandProcessingManager chunkManager, bool newOdpSnapshotChanged, IChunkFactory createChunkFactory, ParameterInfoCollection parameters, int autoRefresh, int numberOfPages, ProcessingMessageList warnings, bool eventInfoChanged, EventInformation newEventInfo, PaginationMode updatedPaginationMode, ReportProcessingFlags updatedProcessingFlags, UserProfileState usedUserProfileState, ExecutionLogContext executionLogContext)
			: base(createChunkFactory, newOdpSnapshot.DefinitionTreeHasDocumentMap, newOdpSnapshot.HasShowHide || newOdpSnapshot.HasUserSortFilter, parameters, autoRefresh, numberOfPages, warnings, eventInfoChanged, newEventInfo, updatedPaginationMode, updatedProcessingFlags, usedUserProfileState, executionLogContext)
		{
			this.m_chunkManager = chunkManager;
			this.m_snapshotChanged = newOdpSnapshotChanged;
		}

		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x0600552A RID: 21802 RVA: 0x001673EA File Offset: 0x001655EA
		public override bool SnapshotChanged
		{
			get
			{
				return this.m_snapshotChanged;
			}
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x001673F4 File Offset: 0x001655F4
		public override void Save()
		{
			lock (this)
			{
				if (this.m_chunkManager != null)
				{
					this.m_chunkManager.SerializeSnapshot();
					this.m_chunkManager = null;
				}
			}
		}

		// Token: 0x04002D13 RID: 11539
		private ChunkManager.OnDemandProcessingManager m_chunkManager;

		// Token: 0x04002D14 RID: 11540
		private readonly bool m_snapshotChanged;
	}
}
