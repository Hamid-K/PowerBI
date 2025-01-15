using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000607 RID: 1543
	internal sealed class StreamingOnDemandProcessingResult : OnDemandProcessingResult
	{
		// Token: 0x0600552C RID: 21804 RVA: 0x00167444 File Offset: 0x00165644
		internal StreamingOnDemandProcessingResult(ReportSnapshot newOdpSnapshot, ChunkManager.OnDemandProcessingManager chunkManager, bool newOdpSnapshotChanged, IChunkFactory createChunkFactory, ParameterInfoCollection parameters, int autoRefresh, int numberOfPages, ProcessingMessageList warnings, bool eventInfoChanged, EventInformation newEventInfo, PaginationMode updatedPaginationMode, ReportProcessingFlags updatedProcessingFlags, UserProfileState usedUserProfileState, ExecutionLogContext executionLogContext)
			: base(createChunkFactory, newOdpSnapshot.DefinitionTreeHasDocumentMap, newOdpSnapshot.HasShowHide || newOdpSnapshot.HasUserSortFilter, parameters, autoRefresh, numberOfPages, warnings, eventInfoChanged, newEventInfo, updatedPaginationMode, updatedProcessingFlags, usedUserProfileState, executionLogContext)
		{
		}

		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x00167484 File Offset: 0x00165684
		public override bool SnapshotChanged
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x00167487 File Offset: 0x00165687
		public override void Save()
		{
		}
	}
}
