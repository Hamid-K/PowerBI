using System;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000485 RID: 1157
	internal interface ISapBwDbProviderFactoryService : IDbProviderFactoryService
	{
		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x0600268B RID: 9867
		string SapConnectorVersion { get; }

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x0600268C RID: 9868
		string SapBwProviderVersion { get; }

		// Token: 0x0600268D RID: 9869
		SapBwDestinationTracker GetEvaluationCleanup();
	}
}
