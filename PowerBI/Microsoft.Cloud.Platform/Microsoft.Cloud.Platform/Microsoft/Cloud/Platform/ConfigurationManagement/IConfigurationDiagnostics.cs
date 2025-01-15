using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x02000405 RID: 1029
	public interface IConfigurationDiagnostics
	{
		// Token: 0x06001F70 RID: 8048
		IDictionary<Type, IConfigurationClass> GetConfigurationSnapshot();
	}
}
