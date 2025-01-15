using System;
using System.Linq;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000BF RID: 191
	internal class AttestationInfo
	{
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0002CAA7 File Offset: 0x0002ACA7
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x0002CAAF File Offset: 0x0002ACAF
		public uint TotalSize { get; set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0002CAB8 File Offset: 0x0002ACB8
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0002CAC0 File Offset: 0x0002ACC0
		public EnclavePublicKey Identity { get; set; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0002CAC9 File Offset: 0x0002ACC9
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0002CAD1 File Offset: 0x0002ACD1
		public HealthReport HealthReport { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0002CADA File Offset: 0x0002ACDA
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0002CAE2 File Offset: 0x0002ACE2
		public EnclaveReportPackage EnclaveReportPackage { get; set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0002CAEB File Offset: 0x0002ACEB
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x0002CAF3 File Offset: 0x0002ACF3
		public long SessionId { get; set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0002CAFC File Offset: 0x0002ACFC
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x0002CB04 File Offset: 0x0002AD04
		public EnclaveDiffieHellmanInfo EnclaveDHInfo { get; set; }

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002CB10 File Offset: 0x0002AD10
		public AttestationInfo(byte[] attestationInfo)
		{
			int num = 0;
			this.TotalSize = BitConverter.ToUInt32(attestationInfo, num);
			num += 4;
			int num2 = BitConverter.ToInt32(attestationInfo, num);
			num += 4;
			int num3 = BitConverter.ToInt32(attestationInfo, num);
			num += 4;
			int num4 = BitConverter.ToInt32(attestationInfo, num);
			num += 4;
			byte[] array = attestationInfo.Skip(num).Take(num2).ToArray<byte>();
			this.Identity = new EnclavePublicKey(array);
			num += num2;
			byte[] array2 = attestationInfo.Skip(num).Take(num3).ToArray<byte>();
			this.HealthReport = new HealthReport(array2);
			num += num3;
			byte[] array3 = attestationInfo.Skip(num).Take(num4).ToArray<byte>();
			this.EnclaveReportPackage = new EnclaveReportPackage(array3);
			num += this.EnclaveReportPackage.GetSizeInPayload();
			uint num5 = BitConverter.ToUInt32(attestationInfo, num);
			num += 4;
			this.SessionId = BitConverter.ToInt64(attestationInfo, num);
			num += 8;
			int num6 = Convert.ToInt32(num5) - 4;
			byte[] array4 = attestationInfo.Skip(num).Take(num6).ToArray<byte>();
			this.EnclaveDHInfo = new EnclaveDiffieHellmanInfo(array4);
			num += Convert.ToInt32(this.EnclaveDHInfo.Size);
		}
	}
}
