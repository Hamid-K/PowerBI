using System;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x0200040F RID: 1039
	public class LocalConfigurationManagerFactory : ConfigurationManagerFactoryBase
	{
		// Token: 0x06001F91 RID: 8081 RVA: 0x000766A8 File Offset: 0x000748A8
		public LocalConfigurationManagerFactory()
			: base(typeof(LocalConfigurationManagerFactory).Name)
		{
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000766BF File Offset: 0x000748BF
		protected override IConfigurationManager CreateConfigurationManager(string specification)
		{
			return new LocalConfigurationManager(specification, this);
		}
	}
}
