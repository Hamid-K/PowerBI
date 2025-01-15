using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000130 RID: 304
	internal class MemcacheShimConfigurationSection : ConfigurationSection
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001DBBF File Offset: 0x0001BDBF
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x0001DBD1 File Offset: 0x0001BDD1
		[ConfigurationCollection(typeof(MemcachePortsCollection), AddItemName = "cache")]
		[ConfigurationProperty("memcachePorts", IsDefaultCollection = false, IsRequired = true)]
		internal MemcachePortsCollection MemcachePortsCollection
		{
			get
			{
				return (MemcachePortsCollection)base["memcachePorts"];
			}
			set
			{
				base["memcachePorts"] = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0001F313 File Offset: 0x0001D513
		internal static string Name
		{
			get
			{
				return "memcache";
			}
		}

		// Token: 0x0400069E RID: 1694
		private const string MEMCACHE_SHIM = "memcache";
	}
}
