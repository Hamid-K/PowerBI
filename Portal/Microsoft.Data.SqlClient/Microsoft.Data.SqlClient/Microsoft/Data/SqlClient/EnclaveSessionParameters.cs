using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000079 RID: 121
	internal class EnclaveSessionParameters
	{
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0001DEC2 File Offset: 0x0001C0C2
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0001DECA File Offset: 0x0001C0CA
		internal string ServerName { get; set; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0001DED3 File Offset: 0x0001C0D3
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x0001DEDB File Offset: 0x0001C0DB
		internal string AttestationUrl { get; set; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x0001DEEC File Offset: 0x0001C0EC
		internal string Database { get; set; }

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001DEF5 File Offset: 0x0001C0F5
		internal EnclaveSessionParameters(string serverName, string attestationUrl, string database)
		{
			this.ServerName = serverName;
			this.AttestationUrl = attestationUrl;
			this.Database = database;
		}
	}
}
