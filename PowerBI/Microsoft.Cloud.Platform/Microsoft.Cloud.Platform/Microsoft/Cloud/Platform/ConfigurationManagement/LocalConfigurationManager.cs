using System;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x0200040E RID: 1038
	public sealed class LocalConfigurationManager : ConfigurationManagerBase
	{
		// Token: 0x06001F90 RID: 8080 RVA: 0x00076690 File Offset: 0x00074890
		public LocalConfigurationManager(string specification, IConfigurationManagerHost host)
			: base(host, new LocalConfigurationProvider[]
			{
				new LocalConfigurationProvider(specification)
			})
		{
		}
	}
}
