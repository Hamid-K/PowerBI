using System;
using System.Collections.Specialized;
using System.Configuration;

namespace NLog.Internal
{
	// Token: 0x02000110 RID: 272
	public class ConfigurationManager : IConfigurationManager
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00024324 File Offset: 0x00022524
		public NameValueCollection AppSettings
		{
			get
			{
				return ConfigurationManager.AppSettings;
			}
		}
	}
}
