using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200000D RID: 13
	public class MachineKeySettings
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002970 File Offset: 0x00000B70
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002978 File Offset: 0x00000B78
		public string Validation { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002981 File Offset: 0x00000B81
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002989 File Offset: 0x00000B89
		public string ValidationKey { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002992 File Offset: 0x00000B92
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000299A File Offset: 0x00000B9A
		public string Decryption { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000029A3 File Offset: 0x00000BA3
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000029AB File Offset: 0x00000BAB
		public string DecryptionKey { get; set; }
	}
}
