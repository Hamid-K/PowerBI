using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D6 RID: 470
	internal interface IShowAllTableBuilderContext : ICommonPlanningContext
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06001062 RID: 4194
		IFederatedConceptualSchema Schema { get; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06001063 RID: 4195
		IFeatureSwitchProvider FeatureSwitches { get; }
	}
}
