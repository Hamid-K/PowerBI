using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000081 RID: 129
	internal sealed class SQLDNSInfo
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000205EA File Offset: 0x0001E7EA
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x000205F2 File Offset: 0x0001E7F2
		public string FQDN { get; set; }

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000205FB File Offset: 0x0001E7FB
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00020603 File Offset: 0x0001E803
		public string AddrIPv4 { get; set; }

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0002060C File Offset: 0x0001E80C
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00020614 File Offset: 0x0001E814
		public string AddrIPv6 { get; set; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0002061D File Offset: 0x0001E81D
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x00020625 File Offset: 0x0001E825
		public string Port { get; set; }

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002062E File Offset: 0x0001E82E
		internal SQLDNSInfo(string FQDN, string ipv4, string ipv6, string port)
		{
			this.FQDN = FQDN;
			this.AddrIPv4 = ipv4;
			this.AddrIPv6 = ipv6;
			this.Port = port;
		}
	}
}
