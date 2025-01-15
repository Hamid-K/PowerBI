using System;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x0200040A RID: 1034
	public interface IConfigurationManagerFactory
	{
		// Token: 0x06001F7D RID: 8061
		IConfigurationManager GetConfigurationManager(string specification);

		// Token: 0x06001F7E RID: 8062
		IConfigurationManager GetConfigurationManager();
	}
}
