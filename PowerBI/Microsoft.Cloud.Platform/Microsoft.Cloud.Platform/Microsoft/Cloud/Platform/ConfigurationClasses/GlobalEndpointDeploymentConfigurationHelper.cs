using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationClasses
{
	// Token: 0x02000438 RID: 1080
	public static class GlobalEndpointDeploymentConfigurationHelper
	{
		// Token: 0x06002191 RID: 8593 RVA: 0x0007CFDC File Offset: 0x0007B1DC
		public static string GetAzurePrivateConnectionStringSuffix(this GlobalEndpointDeploymentConfiguration configuration)
		{
			return ";EndpointSuffix=core.{0}".FormatWithInvariantCulture(new object[] { configuration.AzurePrivateServiceEndpointSuffix });
		}
	}
}
