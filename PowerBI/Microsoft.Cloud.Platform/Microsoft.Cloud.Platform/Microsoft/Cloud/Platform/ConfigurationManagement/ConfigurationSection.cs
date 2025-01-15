using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x020003F9 RID: 1017
	public class ConfigurationSection
	{
		// Token: 0x06001F41 RID: 8001 RVA: 0x00075940 File Offset: 0x00073B40
		public ConfigurationSection(string sectionName, Dictionary<string, string> settings)
		{
			this.m_settings = settings;
			this.m_sectionName = sectionName;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x00075956 File Offset: 0x00073B56
		public string SectionName
		{
			get
			{
				return this.m_sectionName;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x0007595E File Offset: 0x00073B5E
		public Dictionary<string, string> Settings
		{
			get
			{
				return this.m_settings;
			}
		}

		// Token: 0x04000AF9 RID: 2809
		private readonly string m_sectionName;

		// Token: 0x04000AFA RID: 2810
		private readonly Dictionary<string, string> m_settings;
	}
}
