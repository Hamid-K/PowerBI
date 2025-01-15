using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000082 RID: 130
	internal interface IInteractivityPaginationModule
	{
		// Token: 0x06000784 RID: 1924
		int ProcessFindStringEvent(Report report, int totalPages, int startPage, int endPage, string findValue);

		// Token: 0x06000785 RID: 1925
		int ProcessBookmarkNavigationEvent(Report report, int totalPages, string bookmarkId, out string uniqueName);

		// Token: 0x06000786 RID: 1926
		int ProcessUserSortEvent(Report report, string textbox, ref int numberOfPages, ref PaginationMode paginationMode);

		// Token: 0x06000787 RID: 1927
		string ProcessDrillthroughEvent(Report report, int totalPages, string drillthroughId, out NameValueCollection parameters);

		// Token: 0x06000788 RID: 1928
		int ProcessDocumentMapNavigationEvent(Report report, string documentMapId);
	}
}
