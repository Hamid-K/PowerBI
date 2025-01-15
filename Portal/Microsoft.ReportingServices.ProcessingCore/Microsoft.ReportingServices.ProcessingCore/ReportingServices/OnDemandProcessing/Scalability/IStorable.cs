using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200088A RID: 2186
	public interface IStorable : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x170027F2 RID: 10226
		// (get) Token: 0x06007817 RID: 30743
		int Size { get; }
	}
}
