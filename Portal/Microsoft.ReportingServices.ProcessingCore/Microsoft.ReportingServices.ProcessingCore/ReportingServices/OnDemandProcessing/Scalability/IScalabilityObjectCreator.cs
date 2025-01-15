using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000884 RID: 2180
	internal interface IScalabilityObjectCreator
	{
		// Token: 0x060077F1 RID: 30705
		bool TryCreateObject(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType, out Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable newObject);

		// Token: 0x060077F2 RID: 30706
		List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> GetDeclarations();
	}
}
