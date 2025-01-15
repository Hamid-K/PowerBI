using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D4 RID: 468
	internal interface ILimitPlanningContext : ICommonPlanningContext
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001052 RID: 4178
		double EnhancedSamplingAdditionalKeyPointsRatio { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001053 RID: 4179
		DataShapeQueryTranslationTelemetry TelemetryInfo { get; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001054 RID: 4180
		IFederatedConceptualSchema Schema { get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001055 RID: 4181
		IFeatureSwitchProvider FeatureSwitches { get; }
	}
}
