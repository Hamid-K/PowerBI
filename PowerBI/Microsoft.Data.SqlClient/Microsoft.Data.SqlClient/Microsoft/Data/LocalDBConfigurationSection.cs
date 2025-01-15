using System;
using System.Configuration;

namespace Microsoft.Data
{
	// Token: 0x02000013 RID: 19
	internal sealed class LocalDBConfigurationSection : ConfigurationSection
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000B1B4 File Offset: 0x000093B4
		[ConfigurationProperty("localdbinstances", IsRequired = true)]
		public LocalDBInstancesCollection LocalDbInstances
		{
			get
			{
				return ((LocalDBInstancesCollection)base["localdbinstances"]) ?? new LocalDBInstancesCollection();
			}
		}
	}
}
