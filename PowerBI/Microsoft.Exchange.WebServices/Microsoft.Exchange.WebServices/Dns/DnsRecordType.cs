using System;

namespace Microsoft.Exchange.WebServices.Dns
{
	// Token: 0x02000200 RID: 512
	internal enum DnsRecordType : ushort
	{
		// Token: 0x04000DC3 RID: 3523
		A = 1,
		// Token: 0x04000DC4 RID: 3524
		CNAME = 5,
		// Token: 0x04000DC5 RID: 3525
		SOA,
		// Token: 0x04000DC6 RID: 3526
		PTR = 12,
		// Token: 0x04000DC7 RID: 3527
		MX = 15,
		// Token: 0x04000DC8 RID: 3528
		TXT,
		// Token: 0x04000DC9 RID: 3529
		AAAA = 28,
		// Token: 0x04000DCA RID: 3530
		SRV = 33
	}
}
