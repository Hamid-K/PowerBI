using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000877 RID: 2167
	internal class RuntimeMemberObjReference : Reference<RuntimeMemberObj>
	{
		// Token: 0x0600777F RID: 30591 RVA: 0x001EDA51 File Offset: 0x001EBC51
		internal RuntimeMemberObjReference()
		{
		}

		// Token: 0x06007780 RID: 30592 RVA: 0x001EDA59 File Offset: 0x001EBC59
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObjReference;
		}
	}
}
