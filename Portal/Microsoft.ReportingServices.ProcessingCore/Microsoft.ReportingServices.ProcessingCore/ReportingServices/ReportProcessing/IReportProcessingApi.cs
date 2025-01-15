using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200060D RID: 1549
	public interface IReportProcessingApi
	{
		// Token: 0x0600554F RID: 21839
		OnDemandProcessingResult RenderReport(IRenderingExtension newRenderer, DateTime executionTimeStamp, ProcessingContext pc, RenderingContext rc, IChunkFactory yukonCompiledDefinition);

		// Token: 0x06005550 RID: 21840
		OnDemandProcessingResult RenderSnapshot(IRenderingExtension newRenderer, RenderingContext rc, ProcessingContext pc);

		// Token: 0x06005551 RID: 21841
		ProcessingMessageList ProcessReportParameters(DateTime executionTimeStamp, ProcessingContext pc, bool isSnapshot, out bool needsUpgrade);

		// Token: 0x06005552 RID: 21842
		OnDemandProcessingResult ProcessUserSortEvent(string reportItem, SortOptions sortOption, bool clearOldSorts, ProcessingContext pc, RenderingContext rc, IChunkFactory originalSnapshot, out string newReportItem, out int page);

		// Token: 0x06005553 RID: 21843
		bool ProcessToggleEvent(string showHideToggle, IChunkFactory getReportChunkFactory, EventInformation oldShowHideInfo, out EventInformation newShowHideInfo, out bool showHideInfoChanged);

		// Token: 0x06005554 RID: 21844
		int ProcessBookmarkNavigationEvent(string bookmarkId, EventInformation eventInfo, ProcessingContext processingContext, out string uniqueName, out OnDemandProcessingResult result);

		// Token: 0x06005555 RID: 21845
		int ProcessFindStringEvent(int startPage, int endPage, string findValue, EventInformation eventInfo, ProcessingContext processingContext, out OnDemandProcessingResult result);

		// Token: 0x06005556 RID: 21846
		string ProcessDrillthroughEvent(string drillthroughId, EventInformation eventInfo, ProcessingContext processingContext, out NameValueCollection parameters, out OnDemandProcessingResult result);

		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x06005557 RID: 21847
		IConfiguration Configuration { get; }
	}
}
