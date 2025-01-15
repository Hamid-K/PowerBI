using System;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004D1 RID: 1233
	internal interface IReportInstanceContainer
	{
		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06003E81 RID: 16001
		IReference<ReportInstance> ReportInstance { get; }

		// Token: 0x06003E82 RID: 16002
		IReference<ReportInstance> SetReportInstance(ReportInstance reportInstance, OnDemandMetadata odpMetadata);
	}
}
