using System;
using System.Configuration;

namespace Microsoft.Data
{
	// Token: 0x02000011 RID: 17
	internal sealed class LocalDBInstanceElement : ConfigurationElement
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0000B15B File Offset: 0x0000935B
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return base["name"] as string;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000B16D File Offset: 0x0000936D
		[ConfigurationProperty("version", IsRequired = true)]
		public string Version
		{
			get
			{
				return base["version"] as string;
			}
		}
	}
}
