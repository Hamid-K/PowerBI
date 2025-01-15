using System;
using System.Configuration;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x02000153 RID: 339
	internal class ProviderElement : ConfigurationElement
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000387F7 File Offset: 0x000369F7
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x00038809 File Offset: 0x00036A09
		[ConfigurationProperty("invariantName", IsRequired = true)]
		public string InvariantName
		{
			get
			{
				return (string)base["invariantName"];
			}
			set
			{
				base["invariantName"] = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00038817 File Offset: 0x00036A17
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x00038829 File Offset: 0x00036A29
		[ConfigurationProperty("type", IsRequired = true)]
		public string ProviderTypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x040009EF RID: 2543
		private const string InvariantNameKey = "invariantName";

		// Token: 0x040009F0 RID: 2544
		private const string TypeKey = "type";
	}
}
