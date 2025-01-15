using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D4 RID: 212
	internal class DataCacheClientsSection : ConfigurationSection
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00017EC6 File Offset: 0x000160C6
		[ConfigurationProperty("", IsDefaultCollection = true)]
		[ConfigurationCollection(typeof(DataCacheNamedClientCollection), AddItemName = "dataCacheClient")]
		public DataCacheNamedClientCollection Clients
		{
			get
			{
				return (DataCacheNamedClientCollection)base[""];
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00017AED File Offset: 0x00015CED
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00017AFF File Offset: 0x00015CFF
		[ConfigurationProperty("tracing", IsRequired = false)]
		public ClientTraceSettings TraceSettings
		{
			get
			{
				return (ClientTraceSettings)base["tracing"];
			}
			set
			{
				base["tracing"] = value;
			}
		}

		// Token: 0x040003CE RID: 974
		internal const string TRACE_SETTINGS = "tracing";

		// Token: 0x040003CF RID: 975
		internal const string Name = "dataCacheClients";
	}
}
