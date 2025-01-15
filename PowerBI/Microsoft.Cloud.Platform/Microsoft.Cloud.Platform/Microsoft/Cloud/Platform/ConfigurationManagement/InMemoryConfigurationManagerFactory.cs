using System;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x0200040D RID: 1037
	public class InMemoryConfigurationManagerFactory : ConfigurationManagerFactoryBase
	{
		// Token: 0x06001F8C RID: 8076 RVA: 0x0007665C File Offset: 0x0007485C
		public InMemoryConfigurationManagerFactory()
			: this(typeof(InMemoryConfigurationManagerFactory).Name)
		{
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00076673 File Offset: 0x00074873
		public InMemoryConfigurationManagerFactory(string name)
			: base(name)
		{
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0007667C File Offset: 0x0007487C
		public override IConfigurationManager GetConfigurationManager()
		{
			return base.GetConfigurationManager("DefaultSpecification");
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x00076689 File Offset: 0x00074889
		protected override IConfigurationManager CreateConfigurationManager(string specification)
		{
			return new InMemoryConfigurationManager();
		}

		// Token: 0x04000B0E RID: 2830
		private const string c_defaultSpecification = "DefaultSpecification";
	}
}
