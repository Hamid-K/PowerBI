using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001A3 RID: 419
	internal class TcpTransportProperty
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0002EC7C File Offset: 0x0002CE7C
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0002EC84 File Offset: 0x0002CE84
		public TimeSpan SendTimeout { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0002EC8D File Offset: 0x0002CE8D
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0002EC95 File Offset: 0x0002CE95
		public TimeSpan ReceiveTimeout { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0002EC9E File Offset: 0x0002CE9E
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0002ECA6 File Offset: 0x0002CEA6
		public TimeSpan ChnlOpenTimeout { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0002ECAF File Offset: 0x0002CEAF
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x0002ECB7 File Offset: 0x0002CEB7
		public int ConnectionBufferSize { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0002ECC0 File Offset: 0x0002CEC0
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x0002ECC8 File Offset: 0x0002CEC8
		public int ListenBackLog { get; set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0002ECD1 File Offset: 0x0002CED1
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x0002ECD9 File Offset: 0x0002CED9
		public bool NoDelay { get; set; }
	}
}
