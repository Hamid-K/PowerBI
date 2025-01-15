using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x020003FF RID: 1023
	public interface IConfigurationProvider : IShuttable, IIdentifiable
	{
		// Token: 0x06001F56 RID: 8022
		Dictionary<Type, IConfigurationClass> Start(IConfigurationProviderOwner owner);

		// Token: 0x06001F57 RID: 8023
		IEnumerable<ConfigurationSection> GetInitialConfiguration();
	}
}
