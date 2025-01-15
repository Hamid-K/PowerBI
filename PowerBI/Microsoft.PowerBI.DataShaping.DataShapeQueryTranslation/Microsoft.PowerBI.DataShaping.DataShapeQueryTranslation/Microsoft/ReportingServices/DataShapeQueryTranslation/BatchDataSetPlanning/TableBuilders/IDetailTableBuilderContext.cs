using System;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D3 RID: 467
	internal interface IDetailTableBuilderContext : ICommonPlanningContext
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001051 RID: 4177
		IFederatedConceptualSchema Schema { get; }
	}
}
