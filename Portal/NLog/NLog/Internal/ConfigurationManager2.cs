using System;
using System.Collections.Specialized;
using System.Configuration;

namespace NLog.Internal
{
	// Token: 0x02000111 RID: 273
	internal class ConfigurationManager2 : IConfigurationManager2, IConfigurationManager
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x00024333 File Offset: 0x00022533
		public ConnectionStringSettings LookupConnectionString(string name)
		{
			return ConfigurationManager.ConnectionStrings[name];
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00024340 File Offset: 0x00022540
		public NameValueCollection AppSettings
		{
			get
			{
				return ConfigurationManager.AppSettings;
			}
		}
	}
}
