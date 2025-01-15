using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000123 RID: 291
	internal class ConfigProviderTypes
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001E561 File Offset: 0x0001C761
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0001E569 File Offset: 0x0001C769
		public string CacheConfigReaderType { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001E572 File Offset: 0x0001C772
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0001E57A File Offset: 0x0001C77A
		public string HostConfigReaderType { get; set; }

		// Token: 0x06000864 RID: 2148 RVA: 0x0001E583 File Offset: 0x0001C783
		public ConfigProviderTypes(string cacheConfigReaderType, string hostConfigReaderType)
		{
			this.CacheConfigReaderType = cacheConfigReaderType;
			this.HostConfigReaderType = hostConfigReaderType;
		}
	}
}
