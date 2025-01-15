using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x0200040B RID: 1035
	public interface IConfigurationManagerHost
	{
		// Token: 0x06001F7F RID: 8063
		void RequestShutdown();

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001F80 RID: 8064
		IActivityFactory ActivityFactory { get; }
	}
}
