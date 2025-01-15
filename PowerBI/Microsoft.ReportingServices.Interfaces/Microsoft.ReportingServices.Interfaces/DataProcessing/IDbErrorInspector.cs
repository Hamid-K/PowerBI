using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000027 RID: 39
	public interface IDbErrorInspector
	{
		// Token: 0x06000051 RID: 81
		bool IsQueryTimeout(Exception e);

		// Token: 0x06000052 RID: 82
		bool IsQueryMemoryLimitExceeded(Exception e);

		// Token: 0x06000053 RID: 83
		bool IsOnPremisesServiceException(Exception e);
	}
}
