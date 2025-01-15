using System;
using System.Configuration;
using System.Web.Configuration;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001C7 RID: 455
	internal class ConfigurationSwitches : IApplicationSwitchesProvider
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x00028870 File Offset: 0x00026A70
		internal ConfigurationSwitches(ApplicationSwitchesTypes appSwitchesType)
		{
			this.m_configType = appSwitchesType;
		}

		// Token: 0x170001B6 RID: 438
		public string this[string name]
		{
			get
			{
				if ((this.m_configType & ApplicationSwitchesTypes.AppConfig) != (ApplicationSwitchesTypes)0)
				{
					return ConfigurationManager.AppSettings[name];
				}
				return WebConfigurationManager.AppSettings[name];
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000202D2 File Offset: 0x0001E4D2
		public bool GetBoolSwitch(string name, out bool specified)
		{
			return ApplicationSwitchesProviderUtil.GetBoolSwitch(this, name, out specified);
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x000288A2 File Offset: 0x00026AA2
		public string Name
		{
			get
			{
				if ((this.m_configType & ApplicationSwitchesTypes.AppConfig) == (ApplicationSwitchesTypes)0)
				{
					return "web.config file";
				}
				return "app.config file";
			}
		}

		// Token: 0x0400048B RID: 1163
		private ApplicationSwitchesTypes m_configType;
	}
}
