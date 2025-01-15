using System;
using System.Linq;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C3 RID: 195
	internal class EnclaveReport
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0002CE9B File Offset: 0x0002B09B
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0002CEA3 File Offset: 0x0002B0A3
		private int Size { get; set; }

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0002CEAC File Offset: 0x0002B0AC
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0002CEB4 File Offset: 0x0002B0B4
		public uint ReportSize { get; set; }

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0002CEBD File Offset: 0x0002B0BD
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x0002CEC5 File Offset: 0x0002B0C5
		public uint ReportVersion { get; set; }

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0002CECE File Offset: 0x0002B0CE
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x0002CED6 File Offset: 0x0002B0D6
		public byte[] EnclaveData { get; set; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0002CEDF File Offset: 0x0002B0DF
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0002CEE7 File Offset: 0x0002B0E7
		public EnclaveIdentity Identity { get; set; }

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0002CEF0 File Offset: 0x0002B0F0
		public EnclaveReport(byte[] payload)
		{
			this.Size = payload.Length;
			int num = 0;
			this.ReportSize = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.ReportVersion = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.EnclaveData = payload.Skip(num).Take(64).ToArray<byte>();
			num += 64;
			this.Identity = new EnclaveIdentity(payload.Skip(num).ToArray<byte>());
			num += this.Identity.GetSizeInPayload();
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0002CF73 File Offset: 0x0002B173
		public int GetSizeInPayload()
		{
			return 72 + this.Identity.GetSizeInPayload();
		}

		// Token: 0x0400060C RID: 1548
		private const int EnclaveDataLength = 64;
	}
}
