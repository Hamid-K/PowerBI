using System;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000014 RID: 20
	public interface IFeatureSwitchProvider
	{
		// Token: 0x06000068 RID: 104
		bool IsEnabled(FeatureSwitchKind kind);
	}
}
