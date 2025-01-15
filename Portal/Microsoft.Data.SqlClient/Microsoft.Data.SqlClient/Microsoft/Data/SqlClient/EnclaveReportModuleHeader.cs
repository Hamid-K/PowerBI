using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C5 RID: 197
	internal class EnclaveReportModuleHeader
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0002D208 File Offset: 0x0002B408
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x0002D210 File Offset: 0x0002B410
		public uint DataType { get; set; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0002D219 File Offset: 0x0002B419
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x0002D221 File Offset: 0x0002B421
		public uint ModuleSize { get; set; }

		// Token: 0x06000E08 RID: 3592 RVA: 0x0002D22C File Offset: 0x0002B42C
		public EnclaveReportModuleHeader(byte[] payload)
		{
			int num = 0;
			this.DataType = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.ModuleSize = BitConverter.ToUInt32(payload, num);
			num += 4;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0002D263 File Offset: 0x0002B463
		public int GetSizeInPayload()
		{
			return 8;
		}
	}
}
