using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C1 RID: 193
	internal class EnclaveReportPackage
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0002CC78 File Offset: 0x0002AE78
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0002CC80 File Offset: 0x0002AE80
		private int Size { get; set; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0002CC89 File Offset: 0x0002AE89
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0002CC91 File Offset: 0x0002AE91
		public EnclaveReportPackageHeader PackageHeader { get; set; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0002CC9A File Offset: 0x0002AE9A
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0002CCA2 File Offset: 0x0002AEA2
		public EnclaveReport Report { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0002CCAB File Offset: 0x0002AEAB
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0002CCB3 File Offset: 0x0002AEB3
		public List<EnclaveReportModule> Modules { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0002CCBC File Offset: 0x0002AEBC
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0002CCC4 File Offset: 0x0002AEC4
		public byte[] ReportAsBytes { get; set; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0002CCCD File Offset: 0x0002AECD
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0002CCD5 File Offset: 0x0002AED5
		public byte[] SignatureBlob { get; set; }

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		public EnclaveReportPackage(byte[] payload)
		{
			this.Size = payload.Length;
			int num = 0;
			this.PackageHeader = new EnclaveReportPackageHeader(payload.Skip(num).ToArray<byte>());
			num += this.PackageHeader.GetSizeInPayload();
			this.Report = new EnclaveReport(payload.Skip(num).ToArray<byte>());
			num += this.Report.GetSizeInPayload();
			num = this.PackageHeader.GetSizeInPayload();
			int num2 = Convert.ToInt32(this.PackageHeader.SignedStatementSize);
			this.ReportAsBytes = payload.Skip(num).Take(num2).ToArray<byte>();
			num += num2;
			int num3 = Convert.ToInt32(this.PackageHeader.SignatureSize);
			this.SignatureBlob = payload.Skip(num).Take(num3).ToArray<byte>();
			num += num3;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0002CDAE File Offset: 0x0002AFAE
		public int GetSizeInPayload()
		{
			return this.Size;
		}
	}
}
