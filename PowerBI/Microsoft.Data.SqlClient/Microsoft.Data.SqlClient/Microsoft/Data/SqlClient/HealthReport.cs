using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C0 RID: 192
	internal class HealthReport
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0002CC31 File Offset: 0x0002AE31
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0002CC39 File Offset: 0x0002AE39
		private int Size { get; set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0002CC42 File Offset: 0x0002AE42
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0002CC4A File Offset: 0x0002AE4A
		public X509Certificate2 Certificate { get; set; }

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0002CC53 File Offset: 0x0002AE53
		public HealthReport(byte[] payload)
		{
			this.Size = payload.Length;
			this.Certificate = new X509Certificate2(payload);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002CC70 File Offset: 0x0002AE70
		public int GetSizeInPayload()
		{
			return this.Size;
		}
	}
}
