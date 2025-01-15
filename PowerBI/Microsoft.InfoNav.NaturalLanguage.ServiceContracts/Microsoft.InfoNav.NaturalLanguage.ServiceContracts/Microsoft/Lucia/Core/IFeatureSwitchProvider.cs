using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200008A RID: 138
	public interface IFeatureSwitchProvider
	{
		// Token: 0x06000273 RID: 627
		bool IsFeatureSwitchEnabled(FeatureSwitch featureSwitch);

		// Token: 0x06000274 RID: 628
		IList<FeatureSwitch> GetFeatureSwitches();
	}
}
