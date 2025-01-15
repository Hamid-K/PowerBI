using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000608 RID: 1544
	internal sealed class DefinitionOnlyOnDemandProcessingResult : OnDemandProcessingResult
	{
		// Token: 0x0600552F RID: 21807 RVA: 0x0016748C File Offset: 0x0016568C
		internal DefinitionOnlyOnDemandProcessingResult(ReportSnapshot newOdpSnapshot, ChunkManager.OnDemandProcessingManager chunkManager, bool newOdpSnapshotChanged, IChunkFactory createChunkFactory, ParameterInfoCollection parameters, int autoRefresh, int numberOfPages, ProcessingMessageList warnings, bool eventInfoChanged, EventInformation newEventInfo, PaginationMode updatedPaginationMode, ReportProcessingFlags updatedProcessingFlags, UserProfileState usedUserProfileState, ExecutionLogContext executionLogContext)
			: base(createChunkFactory, newOdpSnapshot.DefinitionTreeHasDocumentMap, newOdpSnapshot.HasShowHide || newOdpSnapshot.HasUserSortFilter, parameters, autoRefresh, numberOfPages, warnings, eventInfoChanged, newEventInfo, updatedPaginationMode, updatedProcessingFlags, usedUserProfileState, executionLogContext)
		{
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x06005530 RID: 21808 RVA: 0x001674CC File Offset: 0x001656CC
		public override bool SnapshotChanged
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x001674CF File Offset: 0x001656CF
		public override void Save()
		{
		}
	}
}
