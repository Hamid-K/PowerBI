using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002AE RID: 686
	internal abstract class CancelableLibraryStep : CancelablePhaseBase
	{
		// Token: 0x0600190F RID: 6415 RVA: 0x00064C98 File Offset: 0x00062E98
		protected CancelableLibraryStep(string jobId, ExternalItemPath requestPath, JobActionEnum action, JobType jobType, UserContext userContext)
			: base(jobId, requestPath, action, jobType, userContext, Globals.Configuration.ExecutionLogLevel)
		{
		}
	}
}
