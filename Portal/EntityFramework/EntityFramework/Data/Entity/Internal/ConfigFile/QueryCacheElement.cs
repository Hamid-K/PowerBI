using System;
using System.Configuration;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x02000154 RID: 340
	internal class QueryCacheElement : ConfigurationElement
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0003883F File Offset: 0x00036A3F
		// (set) Token: 0x060015C9 RID: 5577 RVA: 0x00038851 File Offset: 0x00036A51
		[ConfigurationProperty("size")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int Size
		{
			get
			{
				return (int)base["size"];
			}
			set
			{
				base["size"] = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x00038864 File Offset: 0x00036A64
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x00038876 File Offset: 0x00036A76
		[ConfigurationProperty("cleaningIntervalInSeconds")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int CleaningIntervalInSeconds
		{
			get
			{
				return (int)base["cleaningIntervalInSeconds"];
			}
			set
			{
				base["cleaningIntervalInSeconds"] = value;
			}
		}

		// Token: 0x040009F1 RID: 2545
		private const string SizeKey = "size";

		// Token: 0x040009F2 RID: 2546
		private const string CleaningIntervalInSecondsKey = "cleaningIntervalInSeconds";
	}
}
