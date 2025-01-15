using System;
using System.IO;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200007C RID: 124
	internal interface IDataSegmentRenderer
	{
		// Token: 0x06000778 RID: 1912
		void RenderSegment(Report report, Stream dataSegmentQuery, CreateAndRegisterStream createAndRegisterStream);

		// Token: 0x06000779 RID: 1913
		void ExecuteQueries(Stream executeQueriesRequest, ExecuteQueriesContext executeQueriesContext);
	}
}
