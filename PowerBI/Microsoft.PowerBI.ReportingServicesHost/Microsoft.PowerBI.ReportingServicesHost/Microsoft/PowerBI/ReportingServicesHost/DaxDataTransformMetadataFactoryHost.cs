using System;
using Microsoft.InfoNav.Analytics;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200002E RID: 46
	public static class DaxDataTransformMetadataFactoryHost
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00003E2D File Offset: 0x0000202D
		public static DaxDataTransformMetadataFactory Create(IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			return new DaxDataTransformMetadataFactory(new DaxExtensionMetadataFactory(), analyticsFeatureSwitchProvider);
		}
	}
}
