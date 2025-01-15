using System;

namespace Microsoft.BIServer.Owin.Common
{
	// Token: 0x0200000B RID: 11
	public class UserCredentialsContract
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002684 File Offset: 0x00000884
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000268C File Offset: 0x0000088C
		public string UserName { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002695 File Offset: 0x00000895
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000269D File Offset: 0x0000089D
		public string Password { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000026A6 File Offset: 0x000008A6
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000026AE File Offset: 0x000008AE
		public string Domain { get; set; }
	}
}
