using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000888 RID: 2184
	internal interface IStaticReferenceable
	{
		// Token: 0x170027ED RID: 10221
		// (get) Token: 0x06007805 RID: 30725
		int ID { get; }

		// Token: 0x06007806 RID: 30726
		void SetID(int id);

		// Token: 0x06007807 RID: 30727
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType();
	}
}
