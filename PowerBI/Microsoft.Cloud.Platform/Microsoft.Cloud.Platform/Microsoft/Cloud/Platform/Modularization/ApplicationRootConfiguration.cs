using System;
using System.Configuration;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000C8 RID: 200
	internal class ApplicationRootConfiguration : ConfigurationSection
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00014A7A File Offset: 0x00012C7A
		[ConfigurationProperty("Blocks", IsRequired = true)]
		internal BlocksConfiguration BlocksConfiguration
		{
			get
			{
				return (BlocksConfiguration)base["Blocks"];
			}
		}
	}
}
