using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x02000400 RID: 1024
	public interface IConfigurationProviderOwner
	{
		// Token: 0x06001F58 RID: 8024
		void UpdateConfiguration(IConfigurationProvider provider, Dictionary<Type, IConfigurationClass> newConfigurationPairs, NotificationOptions options);

		// Token: 0x06001F59 RID: 8025
		void UpdateConfiguration(IEnumerable<ConfigurationSection> newConfigurationPairs, NotificationOptions options);

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001F5A RID: 8026
		IConfigurationManagerHost ConfigurationManagerHost { get; }
	}
}
