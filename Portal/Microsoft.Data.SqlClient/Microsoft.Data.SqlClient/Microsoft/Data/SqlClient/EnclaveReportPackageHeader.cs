using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C2 RID: 194
	internal class EnclaveReportPackageHeader
	{
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0002CDB6 File Offset: 0x0002AFB6
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0002CDBE File Offset: 0x0002AFBE
		public uint PackageSize { get; set; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0002CDC7 File Offset: 0x0002AFC7
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0002CDCF File Offset: 0x0002AFCF
		public uint Version { get; set; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0002CDD8 File Offset: 0x0002AFD8
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x0002CDE0 File Offset: 0x0002AFE0
		public uint SignatureScheme { get; set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0002CDE9 File Offset: 0x0002AFE9
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0002CDF1 File Offset: 0x0002AFF1
		public uint SignedStatementSize { get; set; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0002CDFA File Offset: 0x0002AFFA
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0002CE02 File Offset: 0x0002B002
		public uint SignatureSize { get; set; }

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0002CE0B File Offset: 0x0002B00B
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0002CE13 File Offset: 0x0002B013
		public uint Reserved { get; set; }

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002CE1C File Offset: 0x0002B01C
		public EnclaveReportPackageHeader(byte[] payload)
		{
			int num = 0;
			this.PackageSize = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.Version = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.SignatureScheme = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.SignedStatementSize = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.SignatureSize = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.Reserved = BitConverter.ToUInt32(payload, num);
			num += 4;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002CE97 File Offset: 0x0002B097
		public int GetSizeInPayload()
		{
			return 24;
		}
	}
}
