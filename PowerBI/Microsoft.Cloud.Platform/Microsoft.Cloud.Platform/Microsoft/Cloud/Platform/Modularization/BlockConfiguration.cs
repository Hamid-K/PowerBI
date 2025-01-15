using System;
using System.Configuration;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000CA RID: 202
	internal class BlockConfiguration : ConfigurationElement
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00014AC5 File Offset: 0x00012CC5
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00014AD7 File Offset: 0x00012CD7
		[ConfigurationProperty("assembly", IsRequired = true)]
		public string Assembly
		{
			get
			{
				return (string)base["assembly"];
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00014AE9 File Offset: 0x00012CE9
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
		}
	}
}
