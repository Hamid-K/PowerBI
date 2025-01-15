using System;
using System.Collections.Concurrent;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000080 RID: 128
	internal sealed class SQLFallbackDNSCache
	{
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000204DC File Offset: 0x0001E6DC
		public static SQLFallbackDNSCache Instance
		{
			get
			{
				return SQLFallbackDNSCache._SQLFallbackDNSCache;
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000204E4 File Offset: 0x0001E6E4
		private SQLFallbackDNSCache()
		{
			int num = 4 * Environment.ProcessorCount;
			this.DNSInfoCache = new ConcurrentDictionary<string, SQLDNSInfo>(num, SQLFallbackDNSCache.initialCapacity, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00020515 File Offset: 0x0001E715
		internal bool AddDNSInfo(SQLDNSInfo item)
		{
			if (item != null)
			{
				if (this.DNSInfoCache.ContainsKey(item.FQDN))
				{
					this.DeleteDNSInfo(item.FQDN);
				}
				return this.DNSInfoCache.TryAdd(item.FQDN, item);
			}
			return false;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00020550 File Offset: 0x0001E750
		internal bool DeleteDNSInfo(string FQDN)
		{
			SQLDNSInfo sqldnsinfo;
			return this.DNSInfoCache.TryRemove(FQDN, out sqldnsinfo);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002056B File Offset: 0x0001E76B
		internal bool GetDNSInfo(string FQDN, out SQLDNSInfo result)
		{
			return this.DNSInfoCache.TryGetValue(FQDN, out result);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002057C File Offset: 0x0001E77C
		internal bool IsDuplicate(SQLDNSInfo newItem)
		{
			SQLDNSInfo sqldnsinfo;
			return newItem != null && this.GetDNSInfo(newItem.FQDN, out sqldnsinfo) && (newItem.AddrIPv4 == sqldnsinfo.AddrIPv4 && newItem.AddrIPv6 == sqldnsinfo.AddrIPv6) && newItem.Port == sqldnsinfo.Port;
		}

		// Token: 0x040002A8 RID: 680
		private static readonly SQLFallbackDNSCache _SQLFallbackDNSCache = new SQLFallbackDNSCache();

		// Token: 0x040002A9 RID: 681
		private static readonly int initialCapacity = 101;

		// Token: 0x040002AA RID: 682
		private ConcurrentDictionary<string, SQLDNSInfo> DNSInfoCache;
	}
}
