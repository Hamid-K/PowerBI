using System;
using System.Linq;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C6 RID: 198
	internal class EnclaveReportModule
	{
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0002D266 File Offset: 0x0002B466
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x0002D26E File Offset: 0x0002B46E
		public EnclaveReportModuleHeader Header { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0002D277 File Offset: 0x0002B477
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x0002D27F File Offset: 0x0002B47F
		public uint Svn { get; set; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0002D288 File Offset: 0x0002B488
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0002D290 File Offset: 0x0002B490
		public string ModuleName { get; set; }

		// Token: 0x06000E10 RID: 3600 RVA: 0x0002D29C File Offset: 0x0002B49C
		public EnclaveReportModule(byte[] payload)
		{
			int num = 0;
			this.Header = new EnclaveReportModuleHeader(payload);
			num += Convert.ToInt32(this.Header.GetSizeInPayload());
			int imageEnclaveLongIdLength = EnclaveReportModule.ImageEnclaveLongIdLength;
			this.UniqueId = payload.Skip(num).Take(imageEnclaveLongIdLength).ToArray<byte>();
			num += imageEnclaveLongIdLength;
			int imageEnclaveLongIdLength2 = EnclaveReportModule.ImageEnclaveLongIdLength;
			this.AuthorId = payload.Skip(num).Take(imageEnclaveLongIdLength2).ToArray<byte>();
			num += imageEnclaveLongIdLength2;
			int imageEnclaveShortIdLength = EnclaveReportModule.ImageEnclaveShortIdLength;
			this.FamilyId = payload.Skip(num).Take(imageEnclaveShortIdLength).ToArray<byte>();
			num += imageEnclaveShortIdLength;
			int imageEnclaveShortIdLength2 = EnclaveReportModule.ImageEnclaveShortIdLength;
			this.ImageId = payload.Skip(num).Take(imageEnclaveShortIdLength).ToArray<byte>();
			num += imageEnclaveShortIdLength2;
			this.Svn = BitConverter.ToUInt32(payload, num);
			num += 4;
			int num2 = Convert.ToInt32(this.Header.ModuleSize) - num;
			this.ModuleName = BitConverter.ToString(payload, num, 1);
			num += 2;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0002D3D1 File Offset: 0x0002B5D1
		public int GetSizeInPayload()
		{
			return this.Header.GetSizeInPayload() + Convert.ToInt32(this.Header.ModuleSize);
		}

		// Token: 0x0400061E RID: 1566
		private static readonly int ImageEnclaveLongIdLength = 32;

		// Token: 0x0400061F RID: 1567
		private static readonly int ImageEnclaveShortIdLength = 16;

		// Token: 0x04000621 RID: 1569
		public byte[] UniqueId = new byte[EnclaveReportModule.ImageEnclaveLongIdLength];

		// Token: 0x04000622 RID: 1570
		public byte[] AuthorId = new byte[EnclaveReportModule.ImageEnclaveLongIdLength];

		// Token: 0x04000623 RID: 1571
		public byte[] FamilyId = new byte[EnclaveReportModule.ImageEnclaveShortIdLength];

		// Token: 0x04000624 RID: 1572
		public byte[] ImageId = new byte[EnclaveReportModule.ImageEnclaveShortIdLength];
	}
}
