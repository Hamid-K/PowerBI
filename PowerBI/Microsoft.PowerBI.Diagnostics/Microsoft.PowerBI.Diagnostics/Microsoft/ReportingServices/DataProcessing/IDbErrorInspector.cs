using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200001E RID: 30
	internal interface IDbErrorInspector
	{
		// Token: 0x06000090 RID: 144
		bool IsQueryTimeout(Exception e);

		// Token: 0x06000091 RID: 145
		bool IsQueryMemoryLimitExceeded(Exception e);

		// Token: 0x06000092 RID: 146
		bool IsOnPremisesServiceException(Exception e);
	}
}
